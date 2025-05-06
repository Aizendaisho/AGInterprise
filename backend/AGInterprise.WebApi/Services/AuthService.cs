using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AGInterprise.Domain.Entities.Seguridad;
using AGInterprise.WebApi.Models.Requests;
using AGInterprise.WebApi.Models.Auth;  // LoginRequest, RegisterRequest

namespace AGInterprise.WebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Usuario>               _userManager;
        private readonly RoleManager<IdentityRole<int>>     _roleManager;
        private readonly IConfiguration                      _configuration;

        public AuthService(
            UserManager<Usuario> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            IConfiguration configuration)
        {
            _userManager   = userManager;
            _roleManager   = roleManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Valida credenciales y genera un JWT con roles y, si existe, el AlmacenId.
        /// </summary>
        public async Task<string> LoginAsync(LoginRequest request)
        {
            // 1) Busca usuario por nombre o email
            var user = await _userManager.FindByNameAsync(request.Username)
                       ?? await _userManager.FindByEmailAsync(request.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                throw new Exception("Usuario o contraseña incorrectos.");

            // 2) Obtiene roles
            var roles = await _userManager.GetRolesAsync(user);

            // 3) Construye claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,            user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier,              user.Id.ToString()),
                new Claim(ClaimTypes.Name,                        user.UserName!),
                new Claim(JwtRegisteredClaimNames.Email,          user.Email ?? "")
            };

            if (user.AlmacenId.HasValue)
                claims.Add(new Claim("almacenId", user.AlmacenId.Value.ToString()));

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            // 4) Firma y genera el token
            var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(2);

            var token = new JwtSecurityToken(
                issuer:             _configuration["Jwt:Issuer"],
                audience:           _configuration["Jwt:Audience"],
                claims:             claims,
                expires:            expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Crea un nuevo usuario con rol y, si es Vendedor, asigna un Almacén.
        /// </summary>
        public async Task RegisterAsync(RegisterRequest request)
        {
            // 1) Validar rol
            var validRoles = new[] { "Administrador", "Supervisor", "Vendedor" };
            if (!validRoles.Contains(request.Role))
                throw new Exception("Rol inválido.");

            // 2) Si es Vendedor, requiere AlmacenId
            if (request.Role == "Vendedor" && request.AlmacenId == null)
                throw new Exception("Debes asignar un Almacén al Vendedor.");

            // 3) Construir la entidad
            var user = new Usuario
            {
                UserName  = request.Username,
                Email     = request.Email,
                Nombre    = request.Nombre,
                Activo    = true,
                AlmacenId = request.Role == "Vendedor" ? request.AlmacenId : null
            };

            // 4) Crear usuario y validar errores
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));

            // 5) Asegurar existencia del rol
            if (!await _roleManager.RoleExistsAsync(request.Role))
                await _roleManager.CreateAsync(new IdentityRole<int>(request.Role));

            // 6) Asignar el rol al usuario
            await _userManager.AddToRoleAsync(user, request.Role);
        }
    }
}

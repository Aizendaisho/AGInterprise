// AGInterprise.Tests/Services/AuthServiceTests.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using AGInterprise.Domain.Entities.Seguridad;
using AGInterprise.WebApi.Services;
using AGInterprise.WebApi.Models.Auth;  // Ajusta al namespace real de tus Request DTOs

namespace AGInterprise.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly IConfiguration _config;

        public AuthServiceTests()
        {
            // Ahora usamos string? para ajustarnos a la firma de AddInMemoryCollection
var inMemory = new Dictionary<string, string?>
{
    ["Jwt:Key"]      = "ClaveSuperSecretaDe32BytesMinimo1234",
    ["Jwt:Issuer"]   = "test-issuer",
    ["Jwt:Audience"] = "test-audience"
};
            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemory)
                .Build();
        }

        [Fact]
        public async Task RegisterAsync_DoesNotThrow_ForAdmin()
        {
            // Arrange
            var users   = new List<Usuario>();
            var userMgr = MockIdentity.MockUserManager<Usuario>(users);
            var roleMgr = MockIdentity.MockRoleManager<IdentityRole<int>>();
            var svc     = new AuthService(userMgr, roleMgr, _config);

            var req = new RegisterRequest
            {
                Username       = "admin1",
                Email          = "a@a.com",
                Password       = "Password1!",
                NombreCompleto = "Admin Uno",
                Role           = "Administrador",
                AlmacenId      = null
            };

            // Act & Assert: no debe lanzar
            await svc.RegisterAsync(req);
        }

        [Fact]
        public async Task RegisterAsync_Throws_OnInvalidRole()
        {
            // Arrange
            var userMgr = MockIdentity.MockUserManager<Usuario>(new List<Usuario>());
            var roleMgr = MockIdentity.MockRoleManager<IdentityRole<int>>();
            var svc     = new AuthService(userMgr, roleMgr, _config);

            var req = new RegisterRequest
            {
                Username       = "u",
                Email          = "e@e.com",
                Password       = "Pass1!",
                NombreCompleto = "X",
                Role           = "Invitado",  // no soportado
                AlmacenId      = null
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => svc.RegisterAsync(req));
        }

        [Fact]
        public async Task RegisterAsync_Throws_IfVendorMissingAlmacen()
        {
            // Arrange
            var userMgr = MockIdentity.MockUserManager<Usuario>(new List<Usuario>());
            var roleMgr = MockIdentity.MockRoleManager<IdentityRole<int>>();
            var svc     = new AuthService(userMgr, roleMgr, _config);

            var req = new RegisterRequest
            {
                Username       = "v",
                Email          = "v@v.com",
                Password       = "Pass1!",
                NombreCompleto = "V",
                Role           = "Vendedor",
                AlmacenId      = null  // faltante
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => svc.RegisterAsync(req));
        }

        [Fact]
        public async Task LoginAsync_Returns_Token_OnValidCredentials()
        {
            // Arrange
            var user = new Usuario
            {
                UserName  = "user1",
                Email     = "u1@u.com",
                Id        = 42,
                AlmacenId = 7
            };
            var users   = new List<Usuario> { user };
            var userMgr = MockIdentity.MockUserManager<Usuario>(users);
            var roleMgr = MockIdentity.MockRoleManager<IdentityRole<int>>();
            var svc     = new AuthService(userMgr, roleMgr, _config);

            var req = new LoginRequest
            {
                Username = "user1",
                Password = "whatever"
            };

            // Act
            var token = await svc.LoginAsync(req);

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(token));
            Assert.Contains(".", token);  // forma básica de JWT
        }

        [Fact]
        public async Task LoginAsync_Throws_OnBadCredentials()
        {
            // Arrange: UserManager.FindByNameAsync devuelve null
            var userMgrMock = new Mock<UserManager<Usuario>>(
                Mock.Of<IUserStore<Usuario>>(), null, null, null, null, null, null, null, null
            );
            userMgrMock
                .Setup(x => x.FindByNameAsync("noexiste"))
                .ReturnsAsync((Usuario)null);

            var roleMgr = MockIdentity.MockRoleManager<IdentityRole<int>>();
            var svc     = new AuthService(userMgrMock.Object, roleMgr, _config);

            var req = new LoginRequest
            {
                Username = "noexiste",
                Password = "x"
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => svc.LoginAsync(req));
        }
    }
}

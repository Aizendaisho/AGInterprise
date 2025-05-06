using Moq;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class MockIdentity
{
    public static UserManager<TUser> MockUserManager<TUser>(IList<TUser> users)
        where TUser : class
    {
        var store = new Mock<IUserStore<TUser>>();
        var mgr = new Mock<UserManager<TUser>>(
            store.Object, null, null, null, null, null, null, null, null);

        mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>()))
           .ReturnsAsync(IdentityResult.Success);
        mgr.Setup(x => x.AddToRoleAsync(It.IsAny<TUser>(), It.IsAny<string>()))
           .ReturnsAsync(IdentityResult.Success);
        mgr.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
           .ReturnsAsync((string name) =>
                users.FirstOrDefault(u => 
                    u.GetType().GetProperty("UserName")?.GetValue(u)?.ToString() == name));
        mgr.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
           .ReturnsAsync((string email) =>
                users.FirstOrDefault(u => 
                    u.GetType().GetProperty("Email")?.GetValue(u)?.ToString() == email));
        mgr.Setup(x => x.CheckPasswordAsync(It.IsAny<TUser>(), It.IsAny<string>()))
           .ReturnsAsync(true);
        mgr.Setup(x => x.GetRolesAsync(It.IsAny<TUser>()))
           .ReturnsAsync(new List<string>());

        return mgr.Object;
    }

    public static RoleManager<TRole> MockRoleManager<TRole>()
        where TRole : class
    {
        var store = new Mock<IRoleStore<TRole>>();
        var mgr = new Mock<RoleManager<TRole>>(
            store.Object, null, null, null, null);

        mgr.Setup(x => x.RoleExistsAsync(It.IsAny<string>()))
           .ReturnsAsync(true);
        mgr.Setup(x => x.CreateAsync(It.IsAny<TRole>()))
           .ReturnsAsync(IdentityResult.Success);

        return mgr.Object;
    }
}

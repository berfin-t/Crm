using System;
using System.Threading.Tasks;
using Crm.GlobalExceptions;
using Crm.Users;
using Volo.Abp.Identity;

namespace Crm.Users;

public class UserRules(IdentityUserManager userManager) : IUserRules
{
    public async Task EnsureUsernameNotExistAsync(string userName) =>
        GlobalException.ThrowIf(
            await userManager.FindByNameAsync(userName) != null, "User already exists with the given username."
        );

    public async Task EnsureUsernameNotExistForOthersAsync(string userName, Guid userId)
    {
        var user = await userManager.FindByNameAsync(userName);
        GlobalException.ThrowIf(
            user != null && user.Id != userId, "User already exists with the given username."
        );
    }

    public async Task EnsureEmailNotExistAsync(string email) =>
        GlobalException.ThrowIf(
            await userManager.FindByEmailAsync(email) != null, "User already exists with the given email."
        );

    public async Task EnsureEmailNotExistForOthersAsync(string email, Guid userId)
    {
        var user = await userManager.FindByEmailAsync(email);
        GlobalException.ThrowIf(
            user != null && user.Id != userId, "User already exists with the given email."
        );
    }
}
using FullPotential.Models.User;

namespace FullPotential.Management.Features.Users;

public interface IUserService
{
    Task<bool> IsUsernameAvailableAsync(string username);

    Task<RegistrationResult> RegisterAsync(string username, string password);

    Task<UserData?> SignInWithPasswordAsync(string username, string password);

    Task<UserData?> SignInWithTokenAsync(string username, string token);

    Task ResetTokenAsync(string username);
}

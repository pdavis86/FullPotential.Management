namespace FullPotential.Management.Features.Users;

public interface IUserService
{
    Task<bool> IsUsernameAvailableAsync(string username);

    Task<RegistrationResult> RegisterAsync(string username, string password);

    Task<string?> SignInAsync(string username, string password);

    Task ResetTokenAsync(string username);

    Task<bool> IsTokenValidAsync(string username, string token);
}

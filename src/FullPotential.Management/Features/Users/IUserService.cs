namespace FullPotential.Management.Features.Users;

public interface IUserService
{
    Task<RegistrationResult> RegisterAsync(string userName, string password);

    Task<string?> SignInAsync(string userName, string password);

    Task SignOutAsync(string userName, string token);

    Task<bool> IsTokenValidAsync(string userName, string token);
}

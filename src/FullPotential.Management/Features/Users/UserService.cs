using FullPotential.Management.Utilities;
using FullPotential.Models.User;
using FullPotential.Persistence;
using FullPotential.Persistence.Entities;

using Microsoft.EntityFrameworkCore;

namespace FullPotential.Management.Features.Users;

public class UserService : IUserService
{
    private readonly ICryptoService _cryptoService;
    private readonly GeneralDbContext _dbContext;
    private readonly ITimeProvider _dateTimeProvider;

    public UserService(
        ICryptoService cryptoService,
        GeneralDbContext dbContext,
        ITimeProvider dateTimeProvider)
    {
        _cryptoService = cryptoService;
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<bool> IsUsernameAvailableAsync(string username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username) == null;
    }

    public async Task<RegistrationResult> RegisterAsync(string username, string password)
    {
        if (password.Length < 8)
        {
            return RegistrationResult.PasswordTooShort;
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);

        if (user != null)
        {
            return RegistrationResult.UsernameInUse;
        }

        var passwordSalt = _cryptoService.GetNewSalt();
        var passwordHash = _cryptoService.Pbkdf2(password, passwordSalt);

        var newUser = new User
        {
            Username = username,
            PasswordSalt = passwordSalt,
            PasswordHash = passwordHash
        };

        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync();

        return RegistrationResult.Success;
    }

    public async Task<UserData?> SignInWithPasswordAsync(string username, string password)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);

        if (user == null)
        {
            return null;
        }

        var passwordHash = _cryptoService.Pbkdf2(password, user.PasswordSalt);

        if (!user.PasswordHash.SequenceEqual(passwordHash))
        {
            return null;
        }

        if (user.Token == null)
        {
            user.Token = _cryptoService.GetNewToken();
        }

        user.TokenExpiry = _dateTimeProvider.GetUtcNow().AddMonths(1).DateTime;

        await _dbContext.SaveChangesAsync();

        var lastUsedCharacter = await _dbContext.Characters.FirstOrDefaultAsync(x => x.User == user);

        return new UserData
        {
            UserId = user.Id.ToString(),
            Username = user.Username,
            Token = user.Token,
            CharacterId = lastUsedCharacter?.Id.ToString()
        };
    }

    public async Task<UserData?> SignInWithTokenAsync(string username, string token)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username && x.Token == token);

        if (user == null || !user.TokenExpiry.HasValue)
        {
            return null;
        }

        if (user.TokenExpiry.Value < _dateTimeProvider.GetUtcNow())
        {
            return null;
        }

        var lastUsedCharacter = await _dbContext.Characters.FirstOrDefaultAsync(x => x.User == user);

        return new UserData
        {
            UserId = user.Id.ToString(),
            Username = user.Username,
            Token = user.Token,
            CharacterId = lastUsedCharacter?.Id.ToString()
        };
    }

    public async Task ResetTokenAsync(string username)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);

        if (user == null)
        {
            return;
        }

        user.Token = _cryptoService.GetNewToken();
        user.TokenExpiry = _dateTimeProvider.GetUtcNow().AddMonths(1).DateTime;

        await _dbContext.SaveChangesAsync();
    }
}

namespace FullPotential.Management.Features.Users;

using FullPotential.Management.Utilities;
using FullPotential.Persistence;
using FullPotential.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

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
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username) == null;
    }

    public async Task<RegistrationResult> RegisterAsync(string username, string password)
    {
        if (password.Length < 8)
        {
            return RegistrationResult.PasswordTooShort;
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);

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

    public async Task<string?> SignInAsync(string username, string password)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);

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
            user.TokenExpiry = _dateTimeProvider.GetUtcNow().AddMonths(1).DateTime;

            await _dbContext.SaveChangesAsync();
        }

        //todo: remove
        //var c = new Character { Owner = user };
        //_dbContext.Characters.Add(c);
        //_dbContext.SaveChanges();
        //_dbContext.CharacterResources.Add(new CharacterResource { Character = c, ResourceId = Guid.NewGuid(), Value = 56 });
        //_dbContext.SaveChanges();
        //var temp = _dbContext.Characters.Include(c => c.Resources);

        return user.Token;
    }

    public async Task ResetTokenAsync(string username)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            return;
        }

        user.Token = _cryptoService.GetNewToken();
        user.TokenExpiry = _dateTimeProvider.GetUtcNow().AddMonths(1).DateTime;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsTokenValidAsync(string username, string token)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username && u.Token == token);

        if (user == null || !user.TokenExpiry.HasValue)
        {
            return false;
        }

        if (user.TokenExpiry.Value < _dateTimeProvider.GetUtcNow())
        {
            return false;
        }

        return true;
    }
}

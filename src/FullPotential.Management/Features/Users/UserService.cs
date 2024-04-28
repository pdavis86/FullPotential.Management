﻿namespace FullPotential.Management.Features.Users;

using FullPotential.Management.Utilities;
using FullPotential.Persistence;
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

    public async Task<RegistrationResult> RegisterAsync(string userName, string password)
    {
        if (password.Length < 8)
        {
            return RegistrationResult.PasswordTooShort;
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);

        if (user != null)
        {
            return RegistrationResult.UserNameInUse;
        }

        var passwordSalt = _cryptoService.GetNewSalt();
        var passwordHash = _cryptoService.Pbkdf2(password, passwordSalt);

        var newUser = new Persistence.Entities.User(userName, passwordSalt, passwordHash);

        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync();

        return RegistrationResult.Success;
    }

    public async Task<string?> SignInAsync(string userName, string password)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);

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

        return user.Token;
    }

    public async Task SignOutAsync(string userName, string token)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Token == token);

        if (user == null)
        {
            return;
        }

        user.Token = _cryptoService.GetNewToken();
        user.TokenExpiry = _dateTimeProvider.GetUtcNow().AddMonths(1).DateTime;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsTokenValidAsync(string userName, string token)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Token == token);

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
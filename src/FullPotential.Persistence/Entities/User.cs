namespace FullPotential.Persistence.Entities;

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class User : EntityBase
{
    [Unicode(false)]
    [MaxLength(50)]
    public string UserName { get; set; }

    [MaxLength(32)]
    public byte[] PasswordSalt { get; set; }

    [MaxLength(256)]
    public byte[] PasswordHash { get; set; }

    [MaxLength(32)]
    public string? Token { get; set; }

    public DateTime? TokenExpiry { get; set; }

    public User(
        string userName,
        byte[] passwordSalt,
        byte[] passwordHash)
    {
        UserName = userName;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
    }
}

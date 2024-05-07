namespace FullPotential.Persistence.Entities;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FullPotential.Persistence.Utilities;
using Microsoft.EntityFrameworkCore;

[Index(nameof(Username), IsUnique = true)]
[ConditionalIndex(nameof(EmailAddress), $"[{nameof(IsEmailAddressValidated)}] = 1", true)]
public class User : EntityBase
{
    [Unicode(false)]
    [MaxLength(50)]
    public required string Username { get; set; }

    [MaxLength(32)]
    public required byte[] PasswordSalt { get; set; }

    [MaxLength(256)]
    public required byte[] PasswordHash { get; set; }

    [MaxLength(32)]
    public string? Token { get; set; }

    public DateTime? TokenExpiry { get; set; }

    [MaxLength(320)]
    public string? EmailAddress { get; set; }

    [DefaultValue(false)]
    public bool IsEmailAddressValidated { get; set; }
}

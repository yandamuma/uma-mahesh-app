using System.ComponentModel.DataAnnotations;

namespace UmaMahesh_BackApp.Entities.Users;

public class UserRegistration
{
    [Key]
    public int UserID { get; set; }
    public string UserName { get; set; } = string.Empty;
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string Email { get; set; } = string.Empty;
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UmaMahesh_BackApp.Models.User;

public class UserDto
{
    [Key]
    [JsonIgnore]
    public int UserID { get; set; }
    public string  UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

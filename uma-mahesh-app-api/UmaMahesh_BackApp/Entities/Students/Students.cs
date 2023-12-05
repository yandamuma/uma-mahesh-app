using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UmaMahesh_BackApp.Entities.Students;

public class Students
{
    [Key]
    [JsonIgnore]
    public ObjectId Id { get; set; }
    public string name { get; set; } = string.Empty;
    public int age { get; set; }
    public bool isEmployed { get; set; }

}

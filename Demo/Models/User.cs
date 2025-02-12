using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int RoleId { get; set; }
    
    [ForeignKey("RoleId")]
    public Role Role { get; set; }
}


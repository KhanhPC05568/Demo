using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models;

public class AllowAccess
{
    [Key]
    public int Id { get; set; }
    public int RoleId { get; set; }
    public string TableName { get; set; }
    public string AccessProperties { get; set; } 
    
    [ForeignKey("RoleId")]
    public Role Role { get; set; }
} 

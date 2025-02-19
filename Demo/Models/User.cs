using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models;

public class User
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    
    public string UserName { get; set; }
    
    public string Password { get; set; }
    
    public DateTime DateOfBirth { get; set; }

  
    public int RoleId { get; set; }
    public Role Role { get; set; }  
}



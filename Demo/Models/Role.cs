using System.ComponentModel.DataAnnotations;

namespace Demo.Models;

public class Role
{
    public int RoleId { get; set; }
    public string RoleName { get; set; }


    public User User { get; set; }

    public ICollection<AllowAccess> AllowAccesses { get; set; }
}

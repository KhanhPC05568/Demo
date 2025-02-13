using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models;

public class AllowAccess
{
    public int AllowAccessId { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; }

    public string TableName { get; set; }
    public string AccessProperties { get; set; }  
}

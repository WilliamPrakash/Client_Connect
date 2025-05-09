using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client_Connect.Models;

public class Employee
{
    // TODO: use "required" attribute, or modifier?
    [Required]
    [Column("Id")]
    public int Id { get; set; } // [Id] [int] IDENTITY(1,1) NOT NULL
    [Required]
    [Column("Name")]
    public string Name { get; set; } = new string(""); // [Name] [nvarchar](50) NULL
    [Required]
    [Column("Email")]
    public string Email { get; set; } = new string(""); // [Email] [nvarchar](50) NULL
    [Column("Occupation")]
    public string? Occupation { get; set; } // [Occupation] [nvarchar](50) NULL
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SOAProject.Models;

public class Asset_Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int AssetId { get; set; }
    [ForeignKey("AssetId")] public Asset Asset { get; set; }
    public int EmployeeId { get; set; }
    [ForeignKey("EmployeeId")] public Employee Employee { get; set; }
}
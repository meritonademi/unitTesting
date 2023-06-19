using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItemManagementSystem1.Models;

public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }
    public string SurName { get; set; }
    public string Tel { get; set; }

    public int DepartmentId { get; set; }
    [ForeignKey("DepartmentId")] public Department Department { get; set; }
}
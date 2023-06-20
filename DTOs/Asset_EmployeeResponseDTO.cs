using ItemManagementSystem1.Models;

namespace ItemManagementSystem1.DTOs;

public class Asset_EmployeeResponseDTO
{
    public int Id { get; set; }
    public int AssetId { get; set; }
    public List<Asset> Asset { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
}
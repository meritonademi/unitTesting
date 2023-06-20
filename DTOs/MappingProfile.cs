using AutoMapper;
using ItemManagementSystem1.Models;

namespace ItemManagementSystem1.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Asset, AssetDTO>();
            CreateMap<AssetDTO, Asset>();
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>();
            CreateMap<Asset_Employee, Asset_EmployeeDTO>();
            CreateMap<Asset_Employee, Asset_EmployeeDTO>();
            CreateMap<Asset_Employee, Asset_EmployeeResponseDTO>();
            CreateMap<Asset_EmployeeResponseDTO, Asset_Employee>();
        }
    }
}
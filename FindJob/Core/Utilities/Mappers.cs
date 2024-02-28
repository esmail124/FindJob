using FindJob.Data.Entities;
using FindJob.Models;

namespace FindJob.Core.Utilities
{
    public class Mappers
    {

        public static JobDTO MapToJobDTO(Job job)
        {
            return new JobDTO
            {
                Id = (job.Id),
                JobTitle = job.JobTitle,
                CompanyName = job.CompanyName,
                Description = job.Description,
                CompanyPhoneNumber = job.CompanyPhoneNumber
            };
        }

        public static Job MapToJob(JobDTO jobDTO)
        {
            return new Job
            {
                Id =jobDTO.Id,
                JobTitle = jobDTO.JobTitle,
                CompanyName = jobDTO.CompanyName,
                Description = jobDTO.Description,
                CompanyPhoneNumber = jobDTO.CompanyPhoneNumber
            };
        }
    }


    public class MyMapProfile : AutoMapper.Profile
    {
        public MyMapProfile()
        {
            CreateMap<AppUser, UserDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ReverseMap(); // If two-way mapping is required

            CreateMap<AppRole, RoleDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();



            //CreateMap<City, CityViewModel>()
            //      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Job, JobDTO>()
                  //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                //.ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId))
                //.ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobTitle))
                //.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                //.ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                //.ForMember(dest => dest.CompanyPhoneNumber, opt => opt.MapFrom(src => src.CompanyPhoneNumber))
                //.ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime))
                //.ForMember(dest => dest.UpDateTime, opt => opt.MapFrom(src => src.UpDateTime))
                //.ForMember(dest => dest.DeleteTime, opt => opt.MapFrom(src => src.DeleteTime))
                .ReverseMap();

            CreateMap<PermissionViewModel, Permission>()
            .ReverseMap();
        }
    }
}

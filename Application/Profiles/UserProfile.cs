namespace Application.Profiles
{
    using Application.DTOs.QuestionarityDTO;
    using Application.DTOs.UserDTOs;
    using Application.Services;
    using AutoMapper;
    using Domain;
    using System.Collections.Generic;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, LoginDTO>()
                .ForMember("DisplayName", src => src.MapFrom(x => x.PacientId.HasValue ? x.Pacient.FullName : x.Doctor.FullName))
                .ForMember("Role", src => src.MapFrom(x => x.Role.Description))
                .ForMember("Id", src => src.MapFrom(x => x.UserId));


            CreateMap<UserRegistrationModelDTO, User>()
                .ForMember(res => res.Email, src => src.MapFrom(x => x.Email))
                .ForMember(res => res.Password, src => src.MapFrom(x => CryptoService.ComputeHash(x.Password)))
                .ForMember(res => res.RoleId, src => src.MapFrom(x => 1))
                .ForMember(res => res.DoctorId, src => src.Ignore());

            CreateMap<UserRegistrationModelDTO, Pacient>()
                .ForMember(res => res.FullName, src => src.MapFrom(x => x.FirstName + " " + x.LastName))
                .ForMember(res => res.Age, src => src.MapFrom(x => x.Age))
                .ForMember(res => res.DoctorId, src => src.MapFrom(x => x.DoctorId))
                .ForMember(res => res.Diagnosis, src => src.MapFrom(x => x.Diagnosis ?? ""));

            CreateMap<User, PacientDTO>()
             .ForMember(res => res.FullName, src => src.MapFrom(x => x.Pacient.FullName))
             .ForMember(res => res.Email, src => src.MapFrom(x => x.Email))
             .ForMember(res => res.Age, src => src.MapFrom(x => x.Pacient.Age))
             .ForMember(res => res.UserId, src => src.MapFrom(x => x.UserId));

            CreateMap<AppointmentsDTO, Appointment>()
                .ForMember(res => res.AppointmentDescription, src => src.MapFrom(x => x.App))
                .ForMember(res => res.Pill, src => src.MapFrom(x => x.Pill))
                .ForMember(res => res.StartDate, src => src.MapFrom(x => x.StartDate))
                .ForMember(res => res.EndDate, src => src.MapFrom(x => x.EndDate))
                .ForMember(res => res.UserId, src => src.MapFrom(x => x.UserId));

            CreateMap<QuestionarityDTO, Questionaire>()
                .ForMember(res => res.Comments, src => src.MapFrom(x => x.Comments))
                .ForMember(res => res.Date, src => src.MapFrom(x => x.QDate))
                .ForMember(res => res.Temperature, src => src.MapFrom(x => x.Temperature));
        }
    }
}

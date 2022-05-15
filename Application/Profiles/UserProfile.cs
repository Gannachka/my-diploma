namespace Application.Profiles
{
    using Application.DTOs.ChatDTO;
    using Application.DTOs.QuestionarityDTO;
    using Application.DTOs.UserDTOs;
    using Application.Services;
    using AutoMapper;
    using Domain;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, LoginDTO>()
                .ForMember(res => res.DisplayName, src => src.MapFrom(x => x.PacientId.HasValue ? x.Pacient.FullName : x.DoctorId.HasValue ? x.Doctor.FullName : x.Admin.Institution))
                .ForMember(res => res.Role, src => src.MapFrom(x => x.Role.Description))
                .ForMember(res => res.Id, src => src.MapFrom(x => x.UserId));

            CreateMap<UserRegistrationModelDTO, User>()
                .ForMember(res => res.Email, src => src.MapFrom(x => x.Email))
                .ForMember(res => res.Password, src => src.MapFrom(x => CryptoService.ComputeHash("")))
                .ForMember(res => res.RoleId, src => src.MapFrom(x => 1))
                .ForMember(res => res.DoctorId, src => src.AllowNull());

            CreateMap<DoctorRegistrationModelDTO, User>()
               .ForMember(res => res.Email, src => src.MapFrom(x => x.Email))
               .ForMember(res => res.Password, src => src.MapFrom(x => CryptoService.ComputeHash("")))
               .ForMember(res => res.RoleId, src => src.MapFrom(x => 3))
               .ForMember(res => res.AdminId, src => src.AllowNull());

            CreateMap<PasswordSetupModelDTO, User>()
               .ForMember(res => res.Email, src => src.MapFrom(x => x.Email))
               .ForMember(res => res.Password, src => src.MapFrom(x => CryptoService.ComputeHash(x.Password)));

            CreateMap<UserRegistrationModelDTO, Pacient>()
                .ForMember(res => res.FullName, src => src.MapFrom(x => x.FullName))
                .ForMember(res => res.Age, src => src.MapFrom(x => x.Age))
                .ForMember(res => res.DoctorId, src => src.MapFrom(x => x.DoctorId))
                .ForMember(res => res.Diagnosis, src => src.MapFrom(x => x.Diagnosis ?? ""));

            CreateMap<DoctorRegistrationModelDTO, Doctor>()
               .ForMember(res => res.FullName, src => src.MapFrom(x => x.FullName))
               .ForMember(res => res.PhoneNumber, src => src.MapFrom(x => x.PhoneNumber))
               .ForMember(res => res.AdminId, src => src.MapFrom(x => x.AdminId))
               .ForMember(res => res.WorkExperience, src => src.MapFrom(x => x.WorkExperience));

            CreateMap<User, PacientDTO>()
               .ForMember(res => res.FullName, src => src.MapFrom(x => x.Pacient.FullName))
               .ForMember(res => res.Email, src => src.MapFrom(x => x.Email))
               .ForMember(res => res.Age, src => src.MapFrom(x => x.Pacient.Age))
               .ForMember(res => res.UserId, src => src.MapFrom(x => x.UserId));

            CreateMap<User, DoctorDTO>()
               .ForMember(res => res.FullName, src => src.MapFrom(x => x.Doctor.FullName))
               .ForMember(res => res.Email, src => src.MapFrom(x => x.Email))
               .ForMember(res => res.PhoneNumber, src => src.MapFrom(x => x.Doctor.PhoneNumber))
               .ForMember(res => res.WorkExperience, src => src.MapFrom(x => x.Doctor.WorkExperience))
               .ForMember(res => res.DoctorId, src => src.MapFrom(x => x.UserId));

            CreateMap<AppointmentsDTO, Appointment>()
                .ForMember(res => res.AppointmentDescription, src => src.MapFrom(x => x.App))
                .ForMember(res => res.Pill, src => src.MapFrom(x => x.Pill))
                .ForMember(res => res.StartDate, src => src.MapFrom(x => x.StartDate))
                .ForMember(res => res.EndDate, src => src.MapFrom(x => x.EndDate))
                .ForMember(res => res.UserId, src => src.MapFrom(x => x.UserId));

            CreateMap<QuestionarityDTO, Questionaire>()
                .ForMember(res => res.Comments, src => src.MapFrom(x => x.Comments))
                .ForMember(res => res.Date, src => src.MapFrom(x => x.QDate))
                .ForMember(res => res.Temperature, src => src.MapFrom(x => x.Temperature))
                .ForMember(res => res.Headache, src => src.MapFrom(x=>x.Headache))
                .ForMember(res => res.ObstructedBreathing, src => src.MapFrom(x=>x.ObstructedBreathing))
                .ForMember(res => res.Temperature, src => src.MapFrom(x => x.Tiredness));

            CreateMap< Questionaire, PacientsQuestionarityDTO>()
              .ForMember(res => res.Fullname, src => src.MapFrom(x=>x.Pacient.FullName))
              .ForMember(res => res.Comments, src => src.MapFrom(x => x.Comments))
              .ForMember(res => res.QDate, src => src.MapFrom(x => x.Date))
              .ForMember(res => res.Temperature, src => src.MapFrom(x => x.Temperature))
              .ForMember(res => res.Headache, src => src.MapFrom(x => x.Headache))
              .ForMember(res => res.ObstructedBreathing, src => src.MapFrom(x => x.ObstructedBreathing))
              .ForMember(res => res.Temperature, src => src.MapFrom(x => x.Tiredness));

            CreateMap<MessageDTO, Message>()
                .ForMember(res => res.SenderId, src => src.MapFrom(x => x.Sender))
                .ForMember(res => res.ReceiverId, src => src.MapFrom(x => x.Receiver))
                .ForMember(res => res.MessageDate, src => src.MapFrom(x => x.MessageDate))
                .ForMember(res => res.Content, src => src.MapFrom(x => x.Content))
                .ForMember(res => res.IsSenderDeleted, src => src.MapFrom(x => x.IsSenderDeleted))
                .ForMember(res => res.IsReceiverDeleted, src => src.MapFrom(x => x.IsReceiverDeleted));

            CreateMap<MessageDeleteModelDTO, Message>()
                .ForMember(res => res.Id, src => src.MapFrom(x => x.Id))
                .ForMember(res => res.SenderId, src => src.MapFrom(x => x.DeletedUserId))
                .ForMember(res => res.ReceiverId, src => src.MapFrom(x => x.DeletedUserId));

        }
    }
}

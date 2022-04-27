namespace Application.DTOs.UserDTOs
{
    public class DoctorRegistrationModelDTO
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public int WorkExperience { get; set; }

        public string Email { get; set; }

        public int? AdminId { get; set; }
    }
}

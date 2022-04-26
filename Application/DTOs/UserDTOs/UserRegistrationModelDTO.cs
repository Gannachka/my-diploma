namespace Application.DTOs.UserDTOs
{
    public class UserRegistrationModelDTO
    {
        public string FullName { get; set; }

        public string Diagnosis { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public int? DoctorId { get; set; }
    }
}

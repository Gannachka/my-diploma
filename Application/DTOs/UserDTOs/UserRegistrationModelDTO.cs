namespace Application.DTOs.UserDTOs
{
    public class UserRegistrationModelDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Diagnosis { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public int DoctorId { get; set; }
    }
}

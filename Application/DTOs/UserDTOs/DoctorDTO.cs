namespace Application.DTOs.UserDTOs
{
    public class DoctorDTO
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public int WorkExperience { get; set; }

        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}

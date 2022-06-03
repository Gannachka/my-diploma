namespace Application.DTOs.UserDTOs
{
    public class PacientDTO
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Diagnosis { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }
        public bool IsActive { get; set; }
    }
}

using System;

namespace Application.DTOs.QuestionarityDTO
{
    public class AppointmentsDTO
    {
        public string App { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string Pill { get; set; }
        public int UserId { get; set; }
    }
}

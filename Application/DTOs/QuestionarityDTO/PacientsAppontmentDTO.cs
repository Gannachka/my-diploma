using System;

namespace Application.DTOs.QuestionarityDTO
{
    class PacientsAppontmentDTO
    {
        public string FullName { get; set; }
        public string App { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string Pill { get; set; }
    }
}

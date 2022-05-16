using System;

namespace Application.DTOs.QuestionarityDTO
{
    public class PacientsQuestionarityDTO
    {
        public string Fullname { get; set; }

        public string Comments { get; set; }

        public double Temperature { get; set; }

        public DateTime QDate { get; set; }

        public bool Headache { get; set; }

        public bool Tiredness { get; set; }

        public bool ObstructedBreathing { get; set; }
    }
}

using System;

namespace Application.DTOs.QuestionarityDTO
{
    public class QuestionarityDTO
    {
        public string Comments { get; set; }

        public bool Headache { get; set; }

        public bool ObstructedBreathing { get; set; }

        public DateTime QDate { get; set; }

        public double Temperature { get; set; }

        public bool Tiredness { get; set; }

    }
}

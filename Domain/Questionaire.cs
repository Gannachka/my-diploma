using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("Questionaire")]
    public class Questionaire
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int QuestionId { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }

        [Required]
        [Column("Temperature")]
        public double Temperature { get; set; }

        [Required]
        [Column("Comments")]
        public string Comments { get; set; }

        [Required]
        [Column("PacientId")]
        [ForeignKey("Pacient")]
        public int PacientId { get; set; }

        public Pacient Pacient { get; set; }
    }
}


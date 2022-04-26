using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    [Table("Questionaire")]
    public class Questionaire
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int QuestionId { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Required]
        [Column("temperature")]
        public double Temperature { get; set; }

        [Required]
        [Column("Comments")]
        public string Comments { get; set; }

        [Required]
        [Column("UserId")]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}


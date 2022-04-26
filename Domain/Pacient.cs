namespace Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Pacient
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Diagnosis { get; set; }

        [Column("DoctorId")]
        [ForeignKey("Doctor")]
        public int? DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public virtual List<Questionaire> Questionaires { get; set; } = new List<Questionaire>();

        public User User { get; set; }
    }
}

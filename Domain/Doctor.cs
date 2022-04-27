namespace Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Doctors")]
    public class Doctor
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public int WorkExperience { get; set; }

        [Column("AdminId")]
        [ForeignKey("Admin")]
        public int? AdminId { get; set; }

        public Admin Admin { get; set; }

        public virtual List<Pacient> Pacients { get; set; } = new List<Pacient>();

        public User User { get; set; }
    }
}

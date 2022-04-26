namespace Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Users")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserId")]
        public int UserId { get; set; }

        [Required]
        [Column("Password")]
        public string Password { get; set; }

        [Required]
        [Column("Email")]
        public string Email { get; set; }

        [Column("DoctorId")]
        [ForeignKey("Doctor")]
        public int? DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        [Column("PacientId")]
        [ForeignKey("Pacient")]
        public int? PacientId { get; set; }

        public Pacient Pacient { get; set; }

        [Required]
        [Column("RoleId")]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}

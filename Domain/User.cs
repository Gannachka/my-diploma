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

        [Required]
        [Column("Age")]
        public int Age { get; set; }

        [Required]
        [Column("DoctorId")]
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        [Required]
        [Column("PacientId")]
        [ForeignKey("Pacient")]
        public int PacientId { get; set; }

        [Required]
        [Column("RoleId")]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [Required]
        [Column("FullName")]
        public string FullName { get; set; }

        [Required]
        [Column("Username")]
        public string Username { get; set; }

        public virtual Role Role { get; set; }

        public virtual List<Questionaire> Questionaires { get; set; } = new List<Questionaire>();
    }
}

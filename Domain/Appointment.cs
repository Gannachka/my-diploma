using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Table("Appointments")]
    public class Appointment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int AppointmentId { get; set; }

        [Column("StartDate")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("EndDate")]
        public DateTime EndDate { get; set; }

        [Required]
        [Column("Appointment")]
        public string AppointmentDescription { get; set; }

        [Required]
        [Column("Pill")]
        public string Pill { get; set; }
        [Required]
        [Column("PacientId")]
        [ForeignKey("Pacient")]
        public int PacientId { get; set; }

        public Pacient Pacient { get; set; }
    }
}

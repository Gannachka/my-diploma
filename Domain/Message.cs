using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Message
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("UserID")]
        [ForeignKey("User")]
        public int Sender { get; set; }

        [Required]
        [Column("UserID")]
        [ForeignKey("User")]
        public int Receiver { get; set; }

        [Required]
        public DateTime MessageDate { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public bool IsNew { get; set; }

        [Required]
        public bool IsSenderDeleted { get; set; }

        [Required]
        public bool IsReceiverDeleted { get; set; }

        public User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Message
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("SenderId")]
        [ForeignKey("Sender")]
        public int SenderId { get; set; }

        public User Sender { get; set; }

        [Required]
        [Column("ReceiverId")]
        [ForeignKey("Receiver")]
        public int ReceiverId { get; set; }

        public User Receiver { get; set; }

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
    }
}

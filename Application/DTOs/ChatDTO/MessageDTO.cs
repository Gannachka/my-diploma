﻿using System;


namespace Application.DTOs.ChatDTO
{
    public class MessageDTO
    {
        public int Id { get; set; }

        public int Sender { get; set; }

        public int Receiver { get; set; }

        public DateTime MessageDate { get; set; }

        public string Content { get; set; }

        public bool IsSenderDeleted { get; set; }

        public bool IsReceiverDeleted { get; set; }

    }
}

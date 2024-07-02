﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Enum
{
    public class TransactionHistoryDto
    {
        public string EventName { get; set; } = string.Empty;
        public int? TicketQuantity { get; set; }
        public int? TotalPrice { get; set; }
        public DateOnly? Time { get; set; }
        public string Status { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
    }
}

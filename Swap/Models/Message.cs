using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swap.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Datetime { get; set; }
        public string SenderId { get; set;}
        public string ReceiverId { get; set; }
    }
}

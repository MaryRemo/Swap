using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swap.Models
{
    public class Message
    {
        public int id { get; set; }
        public string Text { get; set; }
        public DateTime Datetime { get; set; }
        public ApplicationUser Users { get; set; }
        public int SenderId { get; set;}
        public int RecieverId { get; set; }
    }
}

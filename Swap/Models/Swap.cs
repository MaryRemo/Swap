using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swap.Models
{
    public class Swap
    {
        public int Id { get; set; }
        public Item items { get; set; }
        public int SenderItemId { get; set; }
        public int RecieverItemId { get; set; }
    }
}

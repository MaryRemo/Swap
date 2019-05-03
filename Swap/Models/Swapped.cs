using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swap.Models
{
    public class Swapped
    {
        public int Id { get; set; }
        public int SenderItemId { get; set; }
        public int ReceiverItemId { get; set; }
    }
}

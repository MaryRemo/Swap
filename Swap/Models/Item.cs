using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swap.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}

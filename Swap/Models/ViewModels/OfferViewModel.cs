using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swap.Models.ViewModels
{
    public class OfferViewModel
    {
        public List<Swapped> Swappeds { get; set; }
        public List<Item> Items { get; set; }
    }
}

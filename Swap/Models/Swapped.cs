using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Swap.Models
{
    public class Swapped
    {
        [Key]
        public int Id { get; set; }
        public Item SenderItem { get; set; }
        [Required]
        public int SenderItemId { get; set; }        
        public Item ReceiverItem { get; set; }
        [Required]
        public int ReceiverItemId { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Swap.Models.ViewModels
{
    public class SwapViewModel
    {
        public int ReceiverItemId { get; set; }
        public int SenderItemId { get; set; }
        public List<SelectListItem> ItemsSelectList { get; set; }
    }
}

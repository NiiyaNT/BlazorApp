using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppTest.Data.Models
{
    public class AccountDetailViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Status { get; set; }
        public string DisplayNumber { get; set; }
    }
}

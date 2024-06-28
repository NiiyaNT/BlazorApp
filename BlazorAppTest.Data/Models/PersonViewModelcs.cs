using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppTest.Data.Models
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AccountDetailViewModel> AccountDetails { get; set; }
        public int Total { get; set; }
    }
}

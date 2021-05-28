using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyPrimeUI.Library.Models
{
    public class MemberModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }

        public List<MemberAccountModel> AcccountList { get; set; }
    }
}

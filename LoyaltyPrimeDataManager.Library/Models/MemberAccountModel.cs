using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyPrimeDataManager.Library.Models
{
    public class MemberAccountModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public bool Status { get; set; }
    }
}

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyPrimeUI.Library.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MemberModel
    {
        public int Id { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public string Address { get; set; }
        public bool Status { get; set; }

        [JsonProperty]
        public List<MemberAccountModel> Accounts { get; set; } = new List<MemberAccountModel>();


        public string DisplayText
        {
            get
            {
               return Name;
            }
        
        }

    }
}

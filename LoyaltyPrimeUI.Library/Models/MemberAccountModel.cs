using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyPrimeUI.Library.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MemberAccountModel
    {
        public int Id { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public double Balance { get; set; }
        [JsonProperty]
        public bool Status { get; set; }

        public string DisplayText
        {
            get
            {
               return $"{Name} \tBalance: {Balance} \t Status: {(Status==true ? "Active" : "Inactive")}";
            }

        }
    }
}

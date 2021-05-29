﻿using Newtonsoft.Json;

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
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public string Address { get; set; }
        
        public bool Status { get; set; }

        [JsonProperty]
        public List<MemberAccountModel> AccountList { get; set; } = new List<MemberAccountModel>();


        public string DisplayText
        {
            get
            {
                if (Status) {
                    //return $"{Name} - {Environment.NewLine} \t {String.Join($"{Environment.NewLine} \t", AcccountList.Select(a => a.Name).ToArray())} - ";
                    return $"{Name}";
                }
                else { return null; }
            }
        
        }

    }
}

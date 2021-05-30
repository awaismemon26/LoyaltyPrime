using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyPrimeData
{
    public class Members
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        public List<MemberAccounts> AccountList { get; set; }

        
        public List<Members> members;
        public List<Members> GetAll()
        {
            members = new List<Members>()
            {
                new Members() { Id = 1, Name = "Anakin Skywalker", Address = "Landsberger Straße 110", Status = true,
                                AccountList = new List<MemberAccounts>() {

                                new MemberAccounts(){ Id = 1, Name = "Burger King", Balance = 10.0, Status = false},
                                new MemberAccounts(){ Id = 1, Name = "Fitness First", Balance = 150, Status = true}

                                } },
                new Members() { Id = 2, Name = "Darth Vader", Address = "Landsberger Straße 110", Status = true,
                                AccountList = new List<MemberAccounts>() {
                                new MemberAccounts(){ Id = 1, Name = "Lufthansa", Balance = 10, Status = true},
                                } }
            };

            return members;
        }
    }
}

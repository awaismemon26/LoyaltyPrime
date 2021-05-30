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
        public List<MemberAccounts> Accounts { get; set; }

        
        public List<Members> members;
        public List<Members> GetAll()
        {
            members = new List<Members>()
            {
                new Members() { Id = 1, Name = "Anakin Skywalker", Address = "Landsberger Straße 110", Status = true,
                                Accounts = new List<MemberAccounts>() {

                                new MemberAccounts(){ Id = 1, Name = "Burger King", Balance = 10, Status = false},
                                new MemberAccounts(){ Id = 2, Name = "Fitness First", Balance = 150, Status = true}

                                } },
                new Members() { Id = 2, Name = "Darth Vader", Address = "Landsberger Straße 112", Status = true,
                                Accounts = new List<MemberAccounts>() {
                                new MemberAccounts(){ Id = 1, Name = "Lufthansa", Balance = 100, Status = true},
                                } },

                new Members() { Id = 3, Name = "Obi-Wan Kenobi", Address = "Landsberger Straße 114", Status = true,
                                Accounts = new List<MemberAccounts>() {
                                new MemberAccounts(){ Id = 1, Name = "Lufthansa", Balance = 15, Status = true},
                                new MemberAccounts(){ Id = 2, Name = "Fitness First", Balance = 17, Status = true},
                                new MemberAccounts(){ Id = 3, Name = "Burger King", Balance = 60, Status = false},
                                } }
            };

            return members;
        }
    }
}

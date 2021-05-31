using AutoMapper;

using LoyaltyPrimeData;

using LoyaltyPrimeDataManager.Library.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyPrimeDataManager.Library.DataAccess
{
    public class MemberAccountData
    {
        public List<MemberAccountModel> GetAll()
        {
            Members list = new Members();
            List<Members> _list = list.GetAll().Where(x => x.Status == true).ToList();

            List<MemberAccounts> memberAccountList = _list.SelectMany(x => x.Accounts).GroupBy(x => x.Name).Select(x => new MemberAccounts { Balance = x.Sum(y => y.Balance), Name = x.Key }).ToList();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<MemberAccounts, MemberAccountModel>();
            });
            config.AssertConfigurationIsValid();

            IMapper iMapper = config.CreateMapper();
            List<MemberAccountModel> memberList = iMapper.Map<List<MemberAccounts>, List<MemberAccountModel>>(memberAccountList);


            return memberList;
        }
    }
}

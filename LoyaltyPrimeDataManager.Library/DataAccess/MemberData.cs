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
    public class MemberData
    {
        public List<MemberModel> GetAll()
        {
            Members list = new Members();
            List<Members> _list = list.GetAll().Where(x => x.Status == true).ToList();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Members, MemberModel>();
                cfg.CreateMap<MemberAccounts, MemberAccountModel>();
            });
            config.AssertConfigurationIsValid();

            IMapper iMapper = config.CreateMapper();
            List<MemberModel> memberList = iMapper.Map<List<Members>, List<MemberModel>>(_list);


            return memberList;
        }
    }
}

using LoyaltyPrimeDataManager.Library.DataAccess;
using LoyaltyPrimeDataManager.Library.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LoyaltyPrimeDataManager.Controllers
{
    [Authorize]
    public class MemberAccountController : ApiController
    { 
        public List<MemberAccountModel> GetAll()
        {
            MemberAccountData memberAccount = new MemberAccountData();
            return memberAccount.GetAll();
        }
    }
}

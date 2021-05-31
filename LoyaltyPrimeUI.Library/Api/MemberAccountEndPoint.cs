using LoyaltyPrimeUI.Library.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyPrimeUI.Library.Api
{
    public class MemberAccountEndPoint : IMemberAccountEndPoint
    {
        private IAPIHelper _apiHelper;
        public MemberAccountEndPoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<MemberAccountModel>> GetAllAsync()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/MemberAccount"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<MemberAccountModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}

using LoyaltyPrimeUI.Library.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyPrimeUI.Library.Api
{
    public class MemberEndPoint : IMemberEndPoint
    {
        private IAPIHelper _apiHelper;
        public MemberEndPoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<MemberModel>> GetAllAsync()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Member"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<MemberModel>>();
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

using LoyaltyPrimeUI.Library.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoyaltyPrimeUI.Library.Api
{
    public interface IMemberEndPoint
    {
        Task<List<MemberModel>> GetAllAsync();
    }
}
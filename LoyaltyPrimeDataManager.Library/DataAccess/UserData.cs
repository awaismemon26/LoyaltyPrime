using LoyaltyPrimeDataManager.Library.Internal.DataAccess;
using LoyaltyPrimeDataManager.Library.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyPrimeDataManager.Library.DataAccess
{
    public class UserData
    {
        public List<UserModel> GetUserById(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { Id = Id };
            var output = sql.LoadData<UserModel, dynamic>($"SELECT Id, Username, Email from [dbo].[AspNetUsers] where Id = @Id", p, "DefaultConnection");

            return output;
        }
    }
}

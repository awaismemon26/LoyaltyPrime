using Caliburn.Micro;

using LoyaltyPrimeUI.Library.Api;
using LoyaltyPrimeUI.Library.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyPrimeWPF.ViewModels
{
    public class DashboardViewModel : Screen
    {
        private IMemberEndPoint _memberEndpoint;
        public DashboardViewModel(IMemberEndPoint memberEndPoint)
        {
            _memberEndpoint = memberEndPoint;
        }

        private BindingList<MemberModel> _members = new BindingList<MemberModel>();
        public BindingList<MemberModel> Members { get { return _members; } set { _members = value; } }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadMembers();
        }

        public async Task LoadMembers()
        {
            List<MemberModel> list = await _memberEndpoint.GetAllAsync();
            Members = new BindingList<MemberModel>(list);

        }
    }
}

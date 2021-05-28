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
        private string _NameTxtBox;

        public string NameTxtBox
        {
            get { return _NameTxtBox; }
            set { _NameTxtBox = value; 
                NotifyOfPropertyChange(() => NameTxtBox); 
                NotifyOfPropertyChange(() => CanSubmit); }
        }
        private string _AddressTxtBox;

        public string AddressTxtBox
        {
            get { return _AddressTxtBox; }
            set { 
                _AddressTxtBox = value; 
                NotifyOfPropertyChange(() => AddressTxtBox); 
                NotifyOfPropertyChange(() => CanSubmit); }
        }


        private BindingList<MemberModel> _members = new BindingList<MemberModel>();
        public BindingList<MemberModel> Members { 
            get { return _members; } 
            set { 
                _members = value; NotifyOfPropertyChange(() => Members);
            } }

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

        public bool CanSubmit 
        { 
            get
            {
                bool output = false;

                if (!string.IsNullOrEmpty(NameTxtBox) && !string.IsNullOrEmpty(AddressTxtBox))
                {
                    output = true;
                }

                return output;
            }
        }

        public void Submit()
        {
            MemberModel member = new MemberModel();
            member.Name = NameTxtBox;
            member.Address = AddressTxtBox;
            member.Status = true;
            Members.Add(member);
        }

    }
}

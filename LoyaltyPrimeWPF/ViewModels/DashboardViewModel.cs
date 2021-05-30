using Caliburn.Micro;

using LoyaltyPrimeUI.Library.Api;
using LoyaltyPrimeUI.Library.Models;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace LoyaltyPrimeWPF.ViewModels
{
    public class DashboardViewModel : Caliburn.Micro.Screen
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
        private string _AccountNameTxtBox;

        public string AccountNameTxtBox
        {
            get { return _AccountNameTxtBox; }
            set { _AccountNameTxtBox = value; 
                NotifyOfPropertyChange(() => AccountNameTxtBox);
                NotifyOfPropertyChange(() => CanAccountSubmit);
            }
        }

        private double _AccountBalanceTxtBox;

        public double AccountBalanceTxtBox
        {
            get { return _AccountBalanceTxtBox; }
            set { _AccountBalanceTxtBox = value; 
                NotifyOfPropertyChange(() => AccountBalanceTxtBox);
                NotifyOfPropertyChange(() => CanAccountSubmit);
            }
        }

        private string _MemberComboBoxSelected;
        public string MemberComboBoxSelected
        {
            get { return _MemberComboBoxSelected; }
            set { _MemberComboBoxSelected = value; 
                NotifyOfPropertyChange(() => MemberComboBoxSelected);
                NotifyOfPropertyChange(() => CanAccountSubmit);
            }
        } 


        private BindingList<MemberModel> _members = new BindingList<MemberModel>();
        public BindingList<MemberModel> Members { 
            get { return _members; } 
            set { 
                _members = value;
                NotifyOfPropertyChange(() => Members);
                NotifyOfPropertyChange(() => CanAccountSubmit);
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
        public bool CanAccountSubmit
        {
            get
            {
                bool output = false;

                if (!string.IsNullOrEmpty(AccountNameTxtBox) && AccountBalanceTxtBox != 0 && !string.IsNullOrEmpty(MemberComboBoxSelected))
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
        
        public void AccountSubmit()
        {
            MemberAccountModel memberAccount = new MemberAccountModel();
            memberAccount.Name = AccountNameTxtBox;
            memberAccount.Balance = AccountBalanceTxtBox;
            memberAccount.Status = true;

            MemberModel member = Members.Where(x => x.Name == MemberComboBoxSelected).FirstOrDefault();
            member.AccountList.Add(memberAccount);
            Members.Add(member);
            Members.Remove(Members.Where(x => x.Name == MemberComboBoxSelected).FirstOrDefault());
        }
        
        public void ExportMembers()
        {
            var json = JsonConvert.SerializeObject(Members, Formatting.Indented);
            File.WriteAllText("Members.json", json);
            System.Windows.MessageBox.Show("File exported!");
        }
        
        public void ImportMembers()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse JSON Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "json",
                Filter = "JSON files (*.json)|*.json",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var jsonData = File.ReadAllText(openFileDialog.FileName);
                var importedMembers = JsonConvert.DeserializeObject<List<MemberModel>>(jsonData);

                Members.Clear();
                foreach (var item in importedMembers)
                {
                    Members.Add(item);
                }
                System.Windows.MessageBox.Show("Members Added", "Import Members", MessageBoxButton.OK);
            }
        }
    }
}

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
        private IMemberAccountEndPoint _memberAccountEndpoint;
        public DashboardViewModel(IMemberEndPoint memberEndPoint, IMemberAccountEndPoint memberAccountEndPoint)
        {
            _memberEndpoint = memberEndPoint;
            _memberAccountEndpoint = memberAccountEndPoint;
        }

        #region PROPERTIES

        
        private string _NameTxtBox;
        public string NameTxtBox
        {
            get { return _NameTxtBox; }
            set { _NameTxtBox = value; 
                NotifyOfPropertyChange(() => NameTxtBox); 
                NotifyOfPropertyChange(() => CanMemberSubmit); }
        }
        private string _AddressTxtBox;

        public string AddressTxtBox
        {
            get { return _AddressTxtBox; }
            set { 
                _AddressTxtBox = value; 
                NotifyOfPropertyChange(() => AddressTxtBox); 
                NotifyOfPropertyChange(() => CanMemberSubmit); }
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

        private bool _ExtractMoreThan50;
        public bool ExtractMoreThan50
        {
            get { return _ExtractMoreThan50; }
            set { _ExtractMoreThan50 = value; NotifyOfPropertyChange(() => ExtractMoreThan50); }
        }

        private bool _ExtractLessThan50;
        public bool ExtractLessThan50
        {
            get { return _ExtractLessThan50; }
            set { _ExtractLessThan50 = value; NotifyOfPropertyChange(() => ExtractLessThan50); }
        }

        private bool _ExtractInactive;
        public bool ExtractInactive
        {
            get { return _ExtractInactive; }
            set { _ExtractInactive = value; NotifyOfPropertyChange(() => ExtractInactive); }
        }

        private bool _ExtractInactiveMoreThan10;
        public bool ExtractInactiveMoreThan10
        {
            get { return _ExtractInactiveMoreThan10; }
            set { _ExtractInactiveMoreThan10 = value; NotifyOfPropertyChange(() => ExtractInactiveMoreThan10); }
        }
        private bool _ExtractAllMembers;
        public bool ExtractAllMembers
        {
            get { return _ExtractAllMembers; }
            set { _ExtractAllMembers = value; NotifyOfPropertyChange(() => ExtractAllMembers); }
        }


        private BindingList<MemberModel> _members = new BindingList<MemberModel>();
        public BindingList<MemberModel> Members { 
            get { return _members; } 
            set { 
                _members = value;
                NotifyOfPropertyChange(() => Members);
                NotifyOfPropertyChange(() => CanAccountSubmit);
            } }

        #endregion


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

        public bool CanMemberSubmit
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

        public void MemberSubmit()
        {
            MemberModel member = new MemberModel();
            member.Name = NameTxtBox;
            member.Address = AddressTxtBox;
            member.Status = true;
            Members.Add(member);

            // POST updated member to API
        }
        
        public void AccountSubmit()
        {
            MemberAccountModel memberAccount = new MemberAccountModel();
            memberAccount.Name = AccountNameTxtBox;
            memberAccount.Balance = AccountBalanceTxtBox;
            memberAccount.Status = true;

            MemberModel member = Members.FirstOrDefault(x => x.Name == MemberComboBoxSelected);

            if (member.Accounts.Any(x => x.Name.Contains(AccountNameTxtBox)))
            {
                member.Accounts.Where(x => x.Status)
                               .Where(x => x.Name.Contains(AccountNameTxtBox))
                               .ToList()
                               .ForEach(y => y.Balance += Convert.ToDouble(AccountBalanceTxtBox));
                Members.Add(member);
                Members.Remove(Members.FirstOrDefault(x => x.Name == MemberComboBoxSelected));
            }
            else
            {
                member.Accounts.Add(memberAccount);
                Members.Add(member);
                Members.Remove(Members.FirstOrDefault(x => x.Name == MemberComboBoxSelected));
            }

            // We can now POST updated Members to API
        }
        
        public void ExportMembers()
        {
            var _extractMoreThan50 = Members.Select(x => x.Accounts.FindAll(y => y.Balance > 50));
            var _extractLessThan50 = Members.Select(x => x.Accounts.FindAll(y => y.Balance < 50));
            var _extractInactive = Members.Select(x => x.Accounts.FindAll(y => y.Status == false));
            var _extractInactiveMoreThan10 = Members.Select(x => x.Accounts.FindAll(y => y.Status == false && y.Balance > 10));

            string json = "";
            if (ExtractMoreThan50)
                json = JsonConvert.SerializeObject(_extractMoreThan50, Formatting.Indented);
            else if(ExtractLessThan50)
                json = JsonConvert.SerializeObject(_extractLessThan50, Formatting.Indented);
            else if(ExtractInactive)
                json = JsonConvert.SerializeObject(_extractInactive, Formatting.Indented);
            else if(ExtractInactiveMoreThan10)
                json = JsonConvert.SerializeObject(_extractInactiveMoreThan10, Formatting.Indented);
            else
                json = JsonConvert.SerializeObject(Members, Formatting.Indented);

            SaveFileDialog saveFile = new SaveFileDialog
            {
                CheckPathExists = true,
                OverwritePrompt = true,
                RestoreDirectory = true,
                Filter = "JSON files (*.json)|*.json",
                DefaultExt = "json"
            };
            if(saveFile.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFile.FileName, json);
                System.Windows.MessageBox.Show("File exported!");
            }
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
                try
                {
                    string jsonData = File.ReadAllText(openFileDialog.FileName);
                    List<MemberModel> importedMembers = JsonConvert.DeserializeObject<List<MemberModel>>(jsonData);

                    Members.Clear();
                    foreach (var item in importedMembers)
                    {
                        Members.Add(item);
                    }
                    System.Windows.MessageBox.Show("Members Added", "Import Members", MessageBoxButton.OK);
                }
                catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        }
    }
}

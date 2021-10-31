using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TheAdminSolution.Models;
using TheAdminSolution.Services;

namespace TheAdminSolution.ViewModels
{
    public class ManageLeavesDialogViewModel : BindableBase, IDialogAware, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IList<string> Status { get; set; }
        public int SelectedStatusIndex { get; set; } = -1;
        public string TestText { get; set; }
        public string CommentText { get; set; }
        public Leaves LeaveDetails { get; set; }
        public DelegateCommand CloseCommand { get; }
        public DelegateCommand SubmitCommand { get; }
        public ManageLeavesDialogViewModel()
        {
            CloseCommand = new DelegateCommand(() => RequestClose(null));
            SubmitCommand = new DelegateCommand(() => OnSubmit());
            Status = new List<string> 
            {
                "Grant leave", "Reject leave"
            };
        }

        private async Task OnSubmit()
        {
            if (IsValid())
            {
                if (!string.IsNullOrEmpty(CommentText))
                {
                    LeaveDetails.Comment = CommentText;
                }

                UserDialogs.Instance.Loading("Loading...");

                DatabaseServices databaseServices = new DatabaseServices();
                LeaveDetails.Status = SelectedStatusIndex == 0 ? "Granted" : "Rejected";
                await databaseServices.UpdateLeaves(LeaveDetails);
                UserDialogs.Instance.Loading().Dispose();
                RequestClose(null);
            }
        }
            

        private bool IsValid()
        {
            if (SelectedStatusIndex == -1)
            {
                UserDialogs.Instance.Toast("Please select for decision");
                return false;
            }else if(SelectedStatusIndex == 1 && string.IsNullOrEmpty(CommentText))
            {
                UserDialogs.Instance.Toast("Please add a comment for rejection");
                return false;
            }
            return true;
        }

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            LeaveDetails = parameters.GetValue<Leaves>("LeaveDetails");
            NotifyPropertyChanged();
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

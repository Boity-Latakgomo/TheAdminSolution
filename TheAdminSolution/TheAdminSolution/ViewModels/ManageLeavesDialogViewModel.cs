using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TheAdminSolution.Models;

namespace TheAdminSolution.ViewModels
{
    public class ManageLeavesDialogViewModel : BindableBase, IDialogAware, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IList<string> Status { get; set; }
        public int SelectedEmailIndex { get; set; } = -1;
        public string TestText { get; set; }
        public Leaves LeaveDetails { get; set; }
        public DelegateCommand CloseCommand { get; }
        public DelegateCommand SubmitCommand { get; }
        public ManageLeavesDialogViewModel()
        {
            CloseCommand = new DelegateCommand(() => RequestClose(null));
            SubmitCommand = new DelegateCommand(OnSubmit);
            Status = new List<string> 
            {
                "Grant leave", "Reject leave"
            };
        }

        private void OnSubmit()
        {

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

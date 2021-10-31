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
    public class EmployeeTasksDialogViewModel : BindableBase, IDialogAware, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public EmployeeTask EmployeeTask { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public EmployeeTasksDialogViewModel()
        {
            CloseCommand = new DelegateCommand(() => RequestClose(null));
            DeleteCommand = new DelegateCommand(() => OnDelete());
        }

        private async Task OnDelete()
        {
            UserDialogs.Instance.Loading("Loading...");
            DatabaseServices databaseServices = new DatabaseServices();
            await databaseServices.DeleteTask(EmployeeTask.TaskId);
            UserDialogs.Instance.Loading().Dispose();

            RequestClose(null);
            UserDialogs.Instance.Toast("Delete successful");

        }

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() {}

        public void OnDialogOpened(IDialogParameters parameters)
        {
            EmployeeTask = parameters.GetValue<EmployeeTask>("LeaveDetails");
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

using Acr.UserDialogs;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
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
    public class TrackTasksPageViewModel : BindableBase, IPageLifecycleAware, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IDialogService _dialogService;
        public IList<EmployeeTask> Tasks { get; set; }
        public TrackTasksPageViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            //Tasks = new List<EmployeeTask>();
            
            //Tasks = new List<EmployeeTask>
            //{
            //    new EmployeeTask{Email = "edvfdvdvdvdvdvdbvdvbddbdffbgfhbfhfhffbfhffnbfbfbfgbfbfbfbee", Details = "fdgffsvsa", Title = "Title 1", Status = "Resolved"},
            //    new EmployeeTask{Email = "afbdfbdfbfbvfbfbfa", Details = "fdgffsvsvfegefedvsvsvdgsssfsfdsfsdvsvscsvdbvdvdvdvdvddvddvdgvddgergdgdgdgdgffssfsvcsva", Title = "Title 2", Status = "Resolved"},
            //    new EmployeeTask{Email = "bbb", Details = "fdgfbddbdnbbvdvfsvssdvsa", Title = "Title 2", Status = "Active"},
            //    new EmployeeTask{Email = "ccc", Details = "fdgfsa", Title = "Title 3", Status = "Not-started"},
            //    new EmployeeTask{Email = "ddd", Details = "fdgffdbsvsa", Title = "Title4", Status = "Active"},
            //};
        }

        private async Task GetAllTasks()
        {
            UserDialogs.Instance.Loading("Loading...");
            DatabaseServices databaseServices = new DatabaseServices();
            Tasks = await databaseServices.GetTasks();
            UserDialogs.Instance.Loading().Dispose();

            NotifyPropertyChanged();
        }

        public void ViewTask(EmployeeTask employeeTask)
        {
            var parameters = new DialogParameters
            {
                { "LeaveDetails", employeeTask }
            };
            _dialogService.ShowDialog("EmployeeTasksDialog", parameters);
        }

        public void OnAppearing()
        {
            GetAllTasks();
        }

        public void OnDisappearing()
        {}
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

using Acr.UserDialogs;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TheAdminSolution.Models;
using TheAdminSolution.Services;

namespace TheAdminSolution.ViewModels
{
    public class AddTaskPageViewModel : BindableBase, INotifyPropertyChanged
    {
        INavigationService _navigationService;
        public DelegateCommand SubmitCommand { get; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int SelectedEmailIndex { get; set; } = -1;
        public IList<UserEmail> ReturnedEmails { get; set; }
        public IList<string> Emails { get; set; }

        DatabaseServices databaseServices;

        public AddTaskPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GetUserEmails();
            SubmitCommand = new DelegateCommand(() => OnSubmitTask());
        }
        private async Task GetUserEmails()
        {
            databaseServices = new DatabaseServices();
            UserDialogs.Instance.Loading("Loading...");
            Emails = await databaseServices.GetUserEmail();
            UserDialogs.Instance.Loading().Dispose();
        }
        private async Task OnSubmitTask()
        {
           if((SelectedEmailIndex == -1) || string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Details))
            {
                UserDialogs.Instance.Toast("PLease enter all details");
            }
            else
            {
                UserDialogs.Instance.Loading("Loading...");
                Random random = new Random();
                int randomNumber = random.Next(10, 100000);
                string randomTaskID = randomNumber.ToString();

                EmployeeTask employeeTask = new EmployeeTask() {Email = Emails[SelectedEmailIndex], Details = Details, Status = "Pending", Title = Title, TaskId = randomTaskID };
                bool isSuccess = await databaseServices.AddTask(employeeTask);
                if (isSuccess)
                {
                    UserDialogs.Instance.Loading().Dispose();
                    UserDialogs.Instance.Toast("Task has been added successfully");
                    await _navigationService.GoBackAsync();

                }
                else
                {
                    UserDialogs.Instance.Loading().Dispose();
                    UserDialogs.Instance.Alert("Something went wrong, pease try again", "Error", "Close");
                }

            }
        }
    }
}

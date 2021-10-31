using Acr.UserDialogs;
using Prism.AppModel;
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
    public class LeavesPageViewModel : BindableBase, IPageLifecycleAware, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IDialogService _dialogService;
        public IList<Leaves> Leaves { get; set; }
        public LeavesPageViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            //Leaves = new List<Leaves>
            //{
            //    new Leaves{Email = "eeeadwdwdwdwdwd", LeaveType = "Test", ReasonForLeave = "tttwdwdwdwdwdwdwdwdwdwdwdwdwdwdwdwdwdwd", StartDate = DateTime.Now.ToString("dd/MM/yyyy"), EndDate = DateTime.Now.ToString("dd/MM/yyyy")},
            //    new Leaves{Email = "aaa", LeaveType = "trr", ReasonForLeave = "Test", StartDate = new DateTime(2020, 10, 10).ToString("dd/MM/yyyy"), EndDate = new DateTime(2021, 11, 11).ToString("dd/MM/yyyy")},
            //    new Leaves{Email = "sdf", LeaveType = "taa", ReasonForLeave = "Test", StartDate = DateTime.Now.ToString("dd/MM/yyyy"), EndDate = DateTime.Now.ToString("dd/MM/yyyy")},
            //    new Leaves{Email = "sdg", LeaveType = "tee", ReasonForLeave = "Test", StartDate = DateTime.Now.ToString("dd/MM/yyyy"), EndDate = DateTime.Now.ToString("dd/MM/yyyy")}
            //};
        }

        private async Task ViewAllLeaves()
        {
            UserDialogs.Instance.Loading("Loading...");
            DatabaseServices databaseServices = new DatabaseServices();
            Leaves = await databaseServices.GetAllAppliedLeaves();
            UserDialogs.Instance.Loading().Dispose();
            NotifyPropertyChanged();
        }

        // for manage leaves dialog
        public void LeaveEdit(Leaves leaves)
        {
            var parameters = new DialogParameters
            {
                { "LeaveDetails", leaves }
            };
            _dialogService.ShowDialog("ManageLeavesDialog", parameters);
        }

        public void OnAppearing()
        {

            ViewAllLeaves();
        }

        public void OnDisappearing()
        {
            
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

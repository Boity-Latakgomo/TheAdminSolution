using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using TheAdminSolution.Models;

namespace TheAdminSolution.ViewModels
{
    public class LeavesPageViewModel : BindableBase
    {
        IDialogService _dialogService;
        public IList<Leaves> Leaves { get; set; }
        public LeavesPageViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            Leaves = new List<Leaves>
            {
                new Leaves{Email = "eeeadwdwdwdwdwd", LeaveType = "Test", ReasonForLeave = "tttwdwdwdwdwdwdwdwdwdwdwdwdwdwdwdwdwdwd", StartDate = DateTime.Now.ToString("dd/MM/yyyy"), EndDate = DateTime.Now.ToString("dd/MM/yyyy")},
                new Leaves{Email = "aaa", LeaveType = "trr", ReasonForLeave = "Test", StartDate = new DateTime(2020, 10, 10).ToString("dd/MM/yyyy"), EndDate = new DateTime(2021, 11, 11).ToString("dd/MM/yyyy")},
                new Leaves{Email = "sdf", LeaveType = "taa", ReasonForLeave = "Test", StartDate = DateTime.Now.ToString("dd/MM/yyyy"), EndDate = DateTime.Now.ToString("dd/MM/yyyy")},
                new Leaves{Email = "sdg", LeaveType = "tee", ReasonForLeave = "Test", StartDate = DateTime.Now.ToString("dd/MM/yyyy"), EndDate = DateTime.Now.ToString("dd/MM/yyyy")}
            };
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
    }
}

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheAdminSolution.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        INavigationService _navigationService1;
        public DelegateCommand AddTaskCommand { get; }
        public DelegateCommand TrackTasksCommand { get; }
        public DelegateCommand LeavesCommand { get; }
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService1 = navigationService;
            AddTaskCommand = new DelegateCommand(OnAddTask);
            TrackTasksCommand = new DelegateCommand(OnTrackTasks);
            LeavesCommand = new DelegateCommand(OnLeaves);
        }

        private async void OnAddTask()
        {
            await _navigationService1.NavigateAsync("AddTaskPage");
        }
        private async void OnTrackTasks()
        {
            await _navigationService1.NavigateAsync("TrackTasksPage");
        }
        private async void OnLeaves()
        {
            await _navigationService1.NavigateAsync("LeavesPage");
        }
    }
}

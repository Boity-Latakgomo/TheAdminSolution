using Prism;
using Prism.Ioc;
using TheAdminSolution.ViewModels;
using TheAdminSolution.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace TheAdminSolution
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //await NavigationService.NavigateAsync("LoginPage");
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<AddTaskPage, AddTaskPageViewModel>();
            containerRegistry.RegisterForNavigation<TrackTasksPage, TrackTasksPageViewModel>();
            containerRegistry.RegisterForNavigation<LeavesPage, LeavesPageViewModel>();
            containerRegistry.RegisterDialog<ManageLeavesDialog, ManageLeavesDialogViewModel>();
            containerRegistry.RegisterDialog<EmployeeTasksDialog, EmployeeTasksDialogViewModel>();
        }
    }
}

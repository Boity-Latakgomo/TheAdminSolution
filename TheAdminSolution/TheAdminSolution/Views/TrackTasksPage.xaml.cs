using TheAdminSolution.Models;
using TheAdminSolution.ViewModels;
using Xamarin.Forms;

namespace TheAdminSolution.Views
{
    public partial class TrackTasksPage : ContentPage
    {
        public TrackTasksPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (((ListView)sender).SelectedItem == null)
                return;

            (BindingContext as TrackTasksPageViewModel).ViewTask((EmployeeTask)((ListView)sender).SelectedItem);

            ((ListView)sender).SelectedItem = null;
        }
    }
}

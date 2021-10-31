using TheAdminSolution.Models;
using TheAdminSolution.ViewModels;
using Xamarin.Forms;

namespace TheAdminSolution.Views
{
    public partial class LeavesPage : ContentPage
    {
        public LeavesPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (((ListView)sender).SelectedItem == null)
                return;

            (BindingContext as LeavesPageViewModel).LeaveEdit((Leaves)((ListView)sender).SelectedItem);

            ((ListView)sender).SelectedItem = null;
        }

    }
}

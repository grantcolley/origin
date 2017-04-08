using DevelopmentInProgress.ControlsExample.ViewModel;
using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.View;

namespace DevelopmentInProgress.ControlsExample.View
{
    /// <summary>
    /// Interaction logic for UserListView.xaml
    /// </summary>
    public partial class UserListView : DocumentViewBase
    {
        public UserListView(IViewContext viewContext, UserListViewModel userListViewModel)
            : base(viewContext, userListViewModel, Module.ModuleName)
        {
            InitializeComponent();

            DataContext = userListViewModel;
        }
    }
}

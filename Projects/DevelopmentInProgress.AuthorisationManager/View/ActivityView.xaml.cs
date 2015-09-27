using DevelopmentInProgress.AuthorisationManager.ViewModel;
using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.View;

namespace DevelopmentInProgress.AuthorisationManager.View
{
    /// <summary>
    /// Interaction logic for ActivityView.xaml
    /// </summary>
    public partial class ActivityView : DocumentViewBase
    {
        public ActivityView(IViewContext viewContext, ActivityViewModel activityViewModel)
            : base (viewContext, activityViewModel, Module.ModuleName)
        {
            InitializeComponent();

            DataContext = activityViewModel;
        }
    }
}

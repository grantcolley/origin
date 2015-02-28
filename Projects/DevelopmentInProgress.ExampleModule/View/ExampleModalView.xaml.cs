using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.View;

namespace DevelopmentInProgress.ExampleModule.View
{
    /// <summary>
    /// Interaction logic for ExampleModalView.xaml
    /// </summary>
    public partial class ExampleModalView : ModalViewBase
    {
        public ExampleModalView(IViewContext viewContext)
            : base(viewContext)
        {
            InitializeComponent();
        }
    }
}

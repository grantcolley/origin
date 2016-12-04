using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.Navigation;
using DevelopmentInProgress.Origin.ViewModel;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevelopmentInProgress.ExampleModule.ViewModel
{
    public class ExampleDocumentNavigationViewModel : DocumentViewModel
    {
        public ExampleDocumentNavigationViewModel(ViewModelContext viewModelContext)
            : base(viewModelContext)
        {            
            OpenDocumentCommand = new ViewModelCommand(OpenDocument);
            GetDocumentsCommand = new ViewModelCommand(GetDocuments);
            OpenWindowCommand = new ViewModelCommand(OpenWindow);

            OpenDocuments = new ObservableCollection<ViewModelBase>();
        }

        public ICommand OpenDocumentCommand { get; set; }
        public ICommand GetDocumentsCommand { get; set; }
        public ICommand OpenWindowCommand { get; set; }
        
        public object Parameter { get; set; }
        public bool HasParameter { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentParameter { get; set; }
        public string WindowTitle { get; set; }
        public string WindowParameter { get; set; }

        public ObservableCollection<ViewModelBase> OpenDocuments { get; private set; }

        protected async override void OnPublished(object data)
        {
            IsBusy = true;

            await Task.Run(() =>
            {
                Thread.Sleep(750);
                Parameter = data;
                HasParameter = !data.ToString().Equals(typeof (object).FullName);
            });

            ResetStatus();
            OnPropertyChanged("");
        }

        protected async override void SaveDocument()
        {
            IsBusy = true;

            await Task.Run(() => Thread.Sleep(1000));

            ResetStatus();
            OnPropertyChanged("");
        }
        
        private void OpenDocument(object param)
        {
            var navigationSettings = new NavigationSettings()
            {
                View = "ExampleDocumentNavigationView",
                Title = DocumentTitle,
                Data = DocumentParameter
            };

            PublishDocument(navigationSettings);
        }

        private void GetDocuments(object param)
        {
            var documentViewModels = new FindDocumentViewModel();
            OnGetViewModels(documentViewModels);

            ViewModelContext.UiDispatcher.Invoke(
                () =>
                {
                    OpenDocuments.Clear();
                    documentViewModels.ViewModels.ForEach(vm =>
                    {
                        if (vm != this)
                        {
                            OpenDocuments.Add(vm);
                        }
                    });
                });
        }

        private void OpenWindow(object param)
        {
            var modalSettings = new ModalSettings()
            {
                Title = WindowTitle,
                View = "DevelopmentInProgress.ExampleModule.View.ExampleModalView,DevelopmentInProgress.ExampleModule",
                ViewModel = "DevelopmentInProgress.ExampleModule.ViewModel.ExampleModalViewModel,DevelopmentInProgress.ExampleModule",
                Height = 700,
                Width = 700
            };

            modalSettings.Parameters.Add("ModalParameter", WindowParameter);
            ShowModal(modalSettings);
        }
    }
}

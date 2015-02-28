﻿using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.ViewModel;

namespace DevelopmentInProgress.ModuleTemplate.ViewModel
{
    public class NewDocumentViewModel : DocumentViewModel
    {
        public NewDocumentViewModel(ViewModelContext viewModelContext)
            : base(viewModelContext)
        {
        }

        protected override ProcessAsyncResult OnPublishedAsync(object data)
        {
            return base.OnPublishedAsync(data);
        }

        protected override ProcessAsyncResult SaveDocumentAsync()
        {
            return base.SaveDocumentAsync();
        }
    }
}

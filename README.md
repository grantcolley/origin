# origin

![Alt text](/README-images/origin-screenshot.png?raw=true "Origin screenshot")



# Motivation
I first started developing Origin in 2012 as an exercise in WPF shell development using Prism and Unity. Origin is a simple WPF shell that implements the AvalonDock docking system along with a custom navigation bar for hosting modules in a document style layout that supports MVVM. It is an ideal platform for developers who need to start writing line-of-business functionality without having to worry about writing a shell from scratch. I finally decided to dust of the cover and publish it in 2015.



# Getting Started
### Download the source code and run the solution
1. Download the source code from GitHub.

2. Open the solution file **DevelopmentInProgress.Origin.sln** using Visual Studio.

3. You will see three projects in the solution and each will be discussed below. For the time being note the start-up project is **DevelopmentInProgress.Origin** and the build output will go to a folder called **Binaries**.

   ![Alt text](/README-images/solution.PNG?raw=true "Solution")

4. Build the solution.

5. Run it.



### What you get with the solution
The solution contains the following three projects:

1. **DevelopmentInProgress.Origin** - this is the Origin framework containing the shell. You will create line-of-business modules as dll's that will plug into the framework. You will find out how to create a module below.

2. **DevelopmentInProgress.ExampleModule** - this module contains examples of some basic functionality offered by the Origin framework. It's not pretty but is a great place to start learning about how it works by stepping through the code. Once you are familiar with the Origin framework you can remove it from the solution. When you do, don’t forget to remove it from the *ModuleCatalog.xaml* file in the *DevelopmentInProgress.Origin* project too.

3. **DevelopmentInProgress.ModuleTemplate** - the quickest way to get started writing a module is to use this template. It comes with a document view and view model and is preconfigured in the navigation panel. Just rename the project and objects as appropriate. You’ll probably want a new image too!



# Creating a Module
The following three steps describe how to create your own module using the **DevelopmentInProgress.ModuleTemplate** project as an example.

![Alt text](/README-images/moduletemplate-project.png?raw=true "DevelopmentInProgress.ModuleTemplate project")


### Step 1 - Create a Class Library for the module
1.  Open the solution **DevelopmentInProgress.Origin.sln** in Visual Studio and add a new Class Library project.

2.  In the new project add a reference to the **DevelopmentInProgress.Origin** project.

3.  Add a reference to **System.Xaml.dll**.
  
4.  Add references to the following Prism libraries in the ThirdParty folder:
      * **Microsoft.Practices.Prism.dll**
      * **Microsoft.Practices.Prism.UnityExtensions.dll**
      * **Microsoft.Practices.Unity.dll**
    
5.  In the projects properties page go to the Build tab and change the build output path to the same location as the build output for the **DevelopmentInProgress.Origin** project e.g. `..\..\..\Binaries\`.

6.  Create the following three folders in the project:
      * **View**
      * **ViewModel**
      * **Images**
    
7. Add two **.png** images in the **Images** folder. One will be for the module and one will be for the document. These images will appear on the navigation panel. Don't forget to set their **BuildAction** property to **Resource**.

### Step 2 - Create your **View**, **ViewModel** and **Module** classes
1. In the **ViewModel** folder:
    1. Create a new class called **NewDocumentViewModel.cs** and make it inherit the `DocumentViewModel` abstract class.
    
    2. Modify the constructor to accept `ViewModelContext`, passing it into the base constructor.
    
    3. Override the `OnPublishedAsync(object data)`, `OnPublishedCompleted(ProcessAsyncResult processAsyncResult)` and `SaveDocumentAsync()` methods.


        ```C#
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
                
                protected override void OnPublishedCompleted(ProcessAsyncResult processAsyncResult)
                {
                    base.OnPublishedCompleted(processAsyncResult);
                }
                
                protected override ProcessAsyncResult SaveDocumentAsync()
                {
                    return base.SaveDocumentAsync();
                }
            }
        ```


2. In the **View** folder:
    1. Create a new WPF UserControl class called **NewDocumentView.xaml**. 

    2. In the code behind file inherit from `DocumentViewBase` instead of `UserControl`.

    3. Modify the constructor to accept `IViewContext` and `NewDocumentViewModel`, passing them into the base constructor along with `Module.ModuleName`.

    4. Set the data context to the `newDocumentViewModel`.

    5. In the xaml file rename the `UserControl` root element to `view:DocumentViewBase`.

    6. Remove the `DesignHeight` and `DesignWidth` properties.

      ```C#
          public partial class NewDocumentView : DocumentViewBase
          {
              public NewDocumentView(IViewContext viewContext, NewDocumentViewModel newDocumentViewModel)
                  : base(viewContext, newDocumentViewModel, Module.ModuleName)
              {
                  InitializeComponent();
      
                  DataContext = newDocumentViewModel;
              }
          }
      ```
      
      ```xaml
      <view:DocumentViewBase x:Class="DevelopmentInProgress.ModuleTemplate.View.NewDocumentView"
                   xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                   xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                   xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
                   xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
                   xmlns:view="clr-namespace:DevelopmentInProgress.Origin.View;assembly=DevelopmentInProgress.Origin"
                   mc:Ignorable="d">
          <Grid>
          </Grid>
      </view:DocumentViewBase>
      ```

3. Create a class called **Module.cs**.
    1. **Module** must Inherit from `ModuleBase`.

    2. Add a constant string property called `ModuleName` and give your module a name.

    3. Modify the constructor to accept `IUnityContainer, ModuleNavigator` and `ILoggerFacade`, passing them into the base constructor.

    4. Override the `Initialize()` Method and do the following:
      * Register the **ViewModel** and **View** with the `Unity` container.
      * Create and configure your navigation objects including **ModuleSettings**, **ModuleGroup** and **ModuleGroupItem** classes as shown in the code listing below.
 

      ```C#
          public class Module : ModuleBase
          {
              public const string ModuleName = "Module Template";
      
              public Module(IUnityContainer container, ModuleNavigator moduleNavigator, ILoggerFacade logger)
                  : base(container, moduleNavigator, logger)
              {
              }
      
              public override void Initialize()
              {
                  Container.RegisterType<Object, NewDocumentView>(typeof(NewDocumentView).Name);
                  Container.RegisterType<NewDocumentViewModel>(typeof(NewDocumentViewModel).Name);
      
                  var moduleSettings = new ModuleSettings();
                  moduleSettings.ModuleName = ModuleName;
                  moduleSettings.ModuleImagePath = @"/DevelopmentInProgress.ModuleTemplate;component/Images/ModuleTemplate.png";
      
                  var moduleGroup = new ModuleGroup();
                  moduleGroup.ModuleGroupName = "Module Template";
      
                  var newDocument = new ModuleGroupItem();
                  newDocument.ModuleGroupItemName = "New Document";
                  newDocument.TargetView = typeof(NewDocumentView).Name;
                  newDocument.TargetViewTitle = "New Document";
                  newDocument.ModuleGroupItemImagePath = @"/DevelopmentInProgress.ModuleTemplate;component/Images/NewDocument.png";
      
                  moduleGroup.ModuleGroupItems.Add(newDocument);
                  moduleSettings.ModuleGroups.Add(moduleGroup);
                  ModuleNavigator.AddModuleNavigation(moduleSettings);
      
                  Logger.Log("Initialize DevelopmentInProgress.ModuleTemplate Complete", Category.Info, Priority.None);
              }
          }
      ```

### Step 3 - Finally, add an entry for your module in the module catalog.
The `ModuleCatalog.xaml` file can be found in the **Configuration** folder of the **DevelopmentInProgress.Origin** project.

  ```xaml
      <prism:ModuleInfo Ref="Module Template" 
                        ModuleName="DevelopmentInProgress.ModuleTemplate" 
                        ModuleType="DevelopmentInProgress.ModuleTemplate.Module,DevelopmentInProgress.ModuleTemplate"
                        InitializationMode="WhenAvailable"/>
  ```


Rebuild the solution and run it.

![Alt text](/README-images/moduletemplate-screenshot.png?raw=true "DevelopmentInProgress.ModuleTemplate screenshot")


#Time To Get Coding

As already mentioned the best way to see how the Origin framework works and how you can write code to leverage its functionality is to look at the **DevelopmentInProgress.ExampleModule** project and step through the code using the debugger.


In the meanwhile lets take a look at some of the functionality the Origin framework gives you.


### Open a new document and pass parameters to it
Document view models inherit the `DocumentViewModel` class. You can open a new document from a view model by calling the inherited method `PublishDocument(NavigationSettings navigationSettings)`, passing a `NavigationSettings` object containing information about the target view, document title and parameter.

```C#
            var navigationSettings = new NavigationSettings()
            {
                View = "ExampleDocumentNavigationView",
                Title = "Example Document",
                Data = parameter
            };

            PublishDocument(navigationSettings);
```



### Document navigation history
Documents show a breadcrumb style navigation history. Click a link in the navigation history and jump back to the document you came from.

![](https://github.com/grantcolley/origin/raw/master/README-images/navigation-history.png)



### Open a modal window and pass parameters to it
Document view models and modal view models ultimately inherit the `ViewModelBase` class. To open a modal window call `ShowModal(ModalSettings modalSettings)` on the `ViewModelBase` class, passing a `ModalSettings` object containing information about the target view and view model, the window title, height and width, and the parameter.

```C#
            var modalSettings = new ModalSettings()
            {
                Title = "Window Title",
                View = "DevelopmentInProgress.ExampleModule.View.ExampleModalView,DevelopmentInProgress.ExampleModule",
                ViewModel = "DevelopmentInProgress.ExampleModule.ViewModel.ExampleModalViewModel,DevelopmentInProgress.ExampleModule",
                Height = 700,
                Width = 700
            };

            modalSettings.Parameters.Add("parameter", parameter);
            ShowModal(modalSettings);
```



### Asynchronously process the arguments passed to a view model

The arguments passed to the view model are handled in the view model by overriding the methods `OnPublishedAsync` and `OnPublishedCompleted`.

`OnPublishedAsync` runs on a different thread from the UI, giving the view model an opportunity to peform functions such as query query a database etc. in an asynchronous manner.

`OnPublishedCompleted` returns the results of `OnPublishedAsync` back to the view model on the UI thread, allowing the view model to update properties such as collections etc, which need to be done on a UI thread.

To asynchronously process parameters passed to a view model inheriting from `DocumentViewModel` as an object:
```C#
        protected override ProcessAsyncResult OnPublishedAsync(object data)
        {
            // process the data passed into the view model here...

            return new ProcessAsyncResult();
        }
```

To asynchronously process parameters passed to a view model inheriting from `ModalViewModel` in the form of a dictionary of key value pairs:
```C#
        protected override ProcessAsyncResult OnPublishedAsync(Dictionary<string, object> parameters)
        {
            // process the data passed into the view model here...

            return new ProcessAsyncResult();
        }
```

To process the results from `OnPublishedAsync` in the view model on the UI thread (applies to view models inheriting from `DocumentViewModel` or `ModalViewModel`).
```C#
        protected override void OnPublishedCompleted(ProcessAsyncResult processAsyncResult)
        {
        // process the data passed into the view model here...
            base.OnPublishedCompleted(processAsyncResult);
        }
```

### Save a document asynchronously
When saving a document by clicking the Save button in the toolbar, the shell executes the `Save` command of the documents `ViewModelBase` class. You can handle the save asynchronously by overriding the `ViewModelBase` class's `SaveDocumentAsync()` method.

```C#
        protected override ProcessAsyncResult SaveDocumentAsync()
        {
            // do save stuff here...

            return new ProcessAsyncResult();
        }
```


### Indicate when a document has been modified
The `ViewModelBase` class supports property change notification via `OnPropertyChanged(string propertyName, bool isDirty = false)`. Passing true to the isDirty parameter will show a “dirty” indicator as a red asterisk on the document or modal window. Calling `ResetStatus()` will clear the indicator. 

![](https://github.com/grantcolley/origin/raw/master/README-images/document-dirty.png)



### Show messages in the document pane
To show messages in the document footer call `ShowMessage(Message message, bool appendMessage = false)` or `ShowMessages(List<Message> messagesToShow, bool appendMessage = false)` on the `ViewModelBase` class.

```C#
            var message = new Message()
            {
                Text = "Message text",
                MessageType = MessageTypeEnum.Error
            };

            ShowMessage(message, true);
```

![](https://github.com/grantcolley/origin/raw/master/README-images/document-messages.png)



### Show a message box
To show a message box call `ShowMessageBox(MessageBoxSettings messageBoxSettings)` on the `ViewModelBase` class.

```C#
            var message = new Message()
            {
                Title = "Show Info"
                Text = "Example message...",
                MessageType = MessageTypeEnum.Info
            };

            var messageBoxSettings = new MessageBoxSettings() 
            {
                Message = message,
                MessageBoxButtons = MessageBoxButtonsEnum.YesNoCancel
            };

            var result = ShowMessageBox(messageBoxSettings);
```

![](https://github.com/grantcolley/origin/raw/master/README-images/message-box.png)



### Unhandled exceptions
Unhandled exceptions are ugly. When you get one the error message is displayed in a error message box along with the stack trace. The user can copy the content to the clipboard and send it to somebody to look at. Or you can get it from the log file.

![](https://github.com/grantcolley/origin/raw/master/README-images/unhandled-exceptions.png)



### Obtain a handle on open view models
This is probably best explained with an example, the following code illustrates how to populate a list of open document view models. Note the `FindDocumentViewModel` class allows you to specify a _NavigationId_ for returning a specific document view model, or the _ModuleName_ for returning all open document view models for the module. If you do not specify _NavigationId_ or _ModuleName_ then all open document view models will be returned.

```C#
        public ObservableCollection<ViewModelBase> OpenDocuments { get; private set; }

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
```


### The application toolbar
The application toolbar gives you buttons to ``Save, Save All`` and ``Refresh`` documents. The ``Save`` button will save the currently active document. ``Save All`` will save all open documents. ``Refresh`` will refresh all open documents. 
If you don't want to use the toolbar you can hide it. Toggle the visibility of the toolbar by setting the `IsShellToolBarVisible` attribute in the _App.config_ file in the **DevelopmentInProgress.Origin** project.

![](https://github.com/grantcolley/origin/raw/master/README-images/toolbar.png)


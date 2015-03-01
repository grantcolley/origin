# origin
The Origin framework is a WPF shell application for hosting line-of-business modules in a document style layout. Origin uses Prism, Unity, the AvalonDock docking system, a custom navigation bar and supports MVVM.

![Alt text](/Images/Origin-screenshot.png?raw=true "Origin screenshot")



# Motivation
I first started developing Origin in 2012 as an exercise in WPF shell development using Prism and Unity. Origin is a simple WPF shell that implements the AvalonDock docking system along with a custom navigation bar for hosting modules in a document style layout that supports MVVM. It is an ideal platform for developers who need to start writing line-of-business functionality without having to worry about writing a shell from scratch. So, in 2015 I finally decided to dust of the cover and publish it.



# Getting Started
## Download the source code and run the solution
1. Download the source code from GitHub.

2. In the **Solution** folder, open the solution file **DevelopmentInProgress.Origin.sln** using Visual Studio.

3. You will see three projects in the solution and each will be discussed below. For the time being note the start-up project is **DevelopmentInProgress.Origin** and the build output will go to a folder called **Binaries**.

   ![Alt text](/Images/solution.PNG?raw=true "Solution")

4. Build the solution.

5. Run it.



## What you get with the solution
The solution contains the following three projects:

1. **DevelopmentInProgress.Origin** - this is the Origin framework containing the shell. You will create line-of-business modules as dll's that will plug into the framework.

2. **DevelopmentInProgress.ExampleModule** - this module contains examples of some basic functionality offered by the Origin framework and is a great place to start learning about how it works by stepping through the code. Once you are familiar with the Origin framework you can remove it from the solution. When you do, don’t forget to remove it from the *ModuleCatalog.xaml* file in the *DevelopmentInProgress.Origin* project too.

3.  **DevelopmentInProgress.ModuleTemplate** - the quickest way to get started writing a module is to use this template. It comes with a document view and view model and is preconfigured in the navigation panel. Just rename the project and objects as appropriate. You’ll probably want a new image too.



# Creating a Module
The following three steps describe how to create your own module using the **DevelopmentInProgress.ModuleTemplate** project as an example.

![Alt text](/Images/moduletemplate-project.png?raw=true "DevelopmentInProgress.ModuleTemplate")


1. Create a Class Library for the module
    1.  Open the solution **DevelopmentInProgress.Origin.sln** in Visual Studio and add a new Class Library project.

    2.  In the new project add a reference to the **DevelopmentInProgress.Origin** project.

    3.  Add a reference to **System.Xaml.dll**
  
    4.  Add references to the following Prism libraries in the ThirdParty folder:
          * **Microsoft.Practices.Prism.dll**
          * **Microsoft.Practices.Prism.UnityExtensions.dll**
          * **Microsoft.Practices.Unity.dll**
    
    5.  In the projects properties Build page change the build output path to `..\..\Binaries\`

    6.  Create the following three folders in the project
          * **View**
          * **ViewModel**
          * **Images**
    
    7. Add two **.png** images in the **Images** folder. One will be for the module and one will be for the document. Set their **BuildAction** property to **Resource**. These images will appear on the navigation panel.

2. Create your **View**, **ViewModel** and **Module** classes
    1. In the **ViewModel** folder:
        1. Create a new class called **NewDocumentViewModel.cs** and make it inherit the `DocumentViewModel` abstract class.
        2. Modify the constructor to accept `ViewModelContext`.
        3. Override the `OnPublishedAsync(object data)` and `SaveDocumentAsync()` methods.


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
        
                protected override ProcessAsyncResult SaveDocumentAsync()
                {
                    return base.SaveDocumentAsync();
                }
            }
        ```


   2. In the **View** folder:
      1. Create a new WPF UserControl class called **NewDocumentView.xaml**. 

      2. In the code behind file inherit from `DocumentViewBase` instead of `UserControl`.

      3. Modify the constructor to accept `IViewContext` and `NewDocumentViewModel`, passing them into the base constructor along with `Module.ModuleName`

      4. Set the data context to the 'NewDocumentViewModel'

      5. In the xaml file rename the 'UserControl' root element to 'view:DocumentViewBase'

      6. Remove the 'DesignHeight' and 'DesignWidth' properties

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
      1. **Module** must Inherit from `ModuleBase`

      2. Add a constant string property called `ModuleName` and give your module a name.

      3. Modify the constructor to accept `IUnityContainer, ModuleNavigator and ILoggerFacade` passing them into the base constructor.

      4. Override the `Initialize()` Method and:
          * Register the **ViewModel** and **View** with the `Unity` container
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

4. Finally, add an entry for your module in the module catalog which can be found in the **Configuration** folder of the **DevelopmentInProgress.Origin** project.

  ```xaml
      <prism:ModuleInfo Ref="Module Template" 
                        ModuleName="DevelopmentInProgress.ModuleTemplate" 
                        ModuleType="DevelopmentInProgress.ModuleTemplate.Module,DevelopmentInProgress.ModuleTemplate"
                        InitializationMode="WhenAvailable"/>
  ```

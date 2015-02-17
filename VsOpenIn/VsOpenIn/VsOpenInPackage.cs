using EnvDTE;
using MasterDevs.VsOpenIn.Options;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MasterDevs.VsOpenIn
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidVsOpenInPkgString)]
    [ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids80.NoSolution)]
    [ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids80.SolutionExists)]
    [ProvideOptionPage(typeof(OptionDialog),    "VS Open In", "General", 0, 0, true)]
    public sealed class VsOpenInPackage : Package
    {
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/envdte80.dte2.aspx
        /// </summary>
        private EnvDTE.DTE _dte2;
        private MenuCommand _menuItem;
        private EditorLauncher _launcher;

        protected override void Initialize()
        {
            Debug.WriteLine("Initializing VS Open In Vim");
            base.Initialize();

            _dte2 = (EnvDTE.DTE)GetService(typeof(EnvDTE.DTE));
            _dte2.Events.WindowEvents.WindowActivated += new _dispWindowEvents_WindowActivatedEventHandler(WindowEvents_WindowActivated);

            _launcher = new EditorLauncher();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                CommandID menuCommandID = new CommandID(GuidList.guidVsOpenInCmdSet, (int)PkgCmdIDList.cmdidOpenInVim);
                _menuItem = new MenuCommand(MenuItemCallback, menuCommandID);
                mcs.AddCommand(_menuItem);
            }
        }

        void WindowEvents_WindowActivated(Window gotFocus, Window lostFocus)
        {
            Debug.WriteLine("New Window Focused:  {0}", gotFocus);
            try
            {
            _launcher.CurrentDocument = _dte2.ActiveDocument;
            _menuItem.Enabled = _launcher.CanOpenDocument(gotFocus.Document);

            }
            catch (Exception)
            {
                _launcher.CurrentDocument = null;
                _menuItem.Enabled = false;
            }
        }

        private void MenuItemCallback(object sender, EventArgs e)
        {
            OptionDialog options = (OptionDialog)GetDialogPage(typeof(OptionDialog));

            _launcher.Launch(options.EditorPath, options.Arguments, options.InitialDirectory);
        }

    }
}

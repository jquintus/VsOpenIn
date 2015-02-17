using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MasterDevs.VsOpenIn.Options
{

    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false), ComVisible(true)]
    public class OptionDialog : DialogPage
    {

        public const string DEFAULT_PATH = @"C:\Program Files (x86)\vim\vim74\gvim.exe";
        public const string DEFAULT_ARGUMENTS = "--servername VimStudio --remote-silent \"+call cursor({0}, {1})\" \"{2}\"";
        public const string ARGUMENTS_DESCRIPTION = @"Default:
    --servername VimStudio --remote-silent ""+call cursor({0}, {1})"" ""{2}""

Variables
    {0} is Current Line Number
    {1} is Current Column Number
    {2} is Full File Path
    {3} is File Directory
    {4} is File Name";


        [Category("VS Open In")]
        [DisplayName("Path to external editor")]
        [Description("The full path to the external editor")]
        [DefaultValue(DEFAULT_PATH)]
            [Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]

        public string OpenPath { get; set; }

        [Category("VS Open In")]
        [DisplayName("Arguments to pass to the external editor")]
        [Description(ARGUMENTS_DESCRIPTION)]
        public string Arguments { get; set; }
    }

}

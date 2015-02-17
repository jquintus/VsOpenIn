using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace MasterDevs.VsOpenIn.Options
{

    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false), ComVisible(true)]
    public class OptionDialog : DialogPage
    {
        public const string DEFAULT_PATH = @"C:\Program Files (x86)\vim\vim74\gvim.exe";

        /// <summary>
        /// explanation of arguments http://vim.wikia.com/wiki/Integrate_gvim_with_Visual_Studio
        /// </summary>
        public const string DEFAULT_ARGUMENTS = "--servername VimStudio --remote-silent \"+call cursor({CurrentLine}, {CurrentCol})\" \"{FullName}\"";
        public const string ARGUMENTS_DESCRIPTION = @"Default:
    --servername VimStudio --remote-silent ""+call cursor({0}, {1})"" ""{2}""" + VARIABLES;
        public const string DIRECTORY_DESCRIPTION = @"The working directory for the external editor" + VARIABLES;
        public const string VARIABLES = @"

Variables
    {CurrentLine} is Current Line Number
    {CurrentCol} is Current Column Number
    {FullName} is Full File Path
    {DirectoryName} is File Directory
    {FileName} is File Name";

        [Category("VS Open In")]
        [DisplayName("Path to external editor")]
        [Description("The full path to the external editor")]
        [DefaultValue(DEFAULT_PATH)]
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]

        public string OpenPath { get; set; }

        [Category("VS Open In")]
        [DisplayName("Arguments to pass to the external editor")]
        [Description(ARGUMENTS_DESCRIPTION)]
        public string Arguments { get; set; }

        [Category("VS Open In")]
        [DisplayName("Initial Directory")]
        [Description(DIRECTORY_DESCRIPTION)]
        [DefaultValue("{DirectoryName}")]
        public string InitialDirectory { get; set; }
    }

}

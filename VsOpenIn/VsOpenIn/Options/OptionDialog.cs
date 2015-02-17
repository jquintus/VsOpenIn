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
        #region Constants
        public const string DEFAULT_PATH = @"C:\Program Files (x86)\vim\vim74\gvim.exe";
        /// <summary>
        /// explanation of arguments http://vim.wikia.com/wiki/Integrate_gvim_with_Visual_Studio
        /// </summary>
        public const string DEFAULT_ARGUMENTS = "--servername VimStudio --remote-silent \"+call cursor({CurrentLine}, {CurrentCol})\" \"{FullName}\"";
        public const string DEFAULT_DIRECTORY = "{DirectoryName}";

        public const string DESCRIPTION_ARGUMENTS = "Default:\n    "  + DEFAULT_ARGUMENTS + DESCRIPTION_VARIABLES;
        public const string DESCRIPTION_DIRECTORY = @"The working directory for the external editor" + DESCRIPTION_VARIABLES;
        public const string DESCRIPTION_VARIABLES = "\n\n" + @"Variables
    {CurrentLine} is Current Line Number
    {CurrentCol} is Current Column Number
    {FullName} is Full File Path
    {DirectoryName} is File Directory
    {FileName} is File Name";

        #endregion

        public OptionDialog()
        {
            EditorPath = DEFAULT_PATH;
            Arguments = DEFAULT_ARGUMENTS;
            InitialDirectory = DEFAULT_DIRECTORY;
        }

        [Category("VS Open In")]
        [DisplayName("Editor Path")]
        [Description("The full path to the external editor")]
        [DefaultValue(DEFAULT_PATH)]
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string EditorPath { get; set; }

        [Category("VS Open In")]
        [DisplayName("Editor Arguments")]
        [DefaultValue(DEFAULT_ARGUMENTS)]
        [Description(DESCRIPTION_ARGUMENTS)]
        public string Arguments { get; set; }

        [Category("VS Open In")]
        [DisplayName("Initial Directory")]
        [Description(DESCRIPTION_DIRECTORY)]
        [DefaultValue(DEFAULT_DIRECTORY)]
        public string InitialDirectory { get; set; }
    }

}

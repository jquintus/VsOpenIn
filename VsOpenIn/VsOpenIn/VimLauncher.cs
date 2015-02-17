using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDevs.VsOpenIn
{
    public class VimLauncher
    {
        private int _curLine;
        private int _curCol;
        private FileInfo _path;
        private string _vimPath = @"C:\Program Files (x86)\vim\vim74\gvim.exe";
        private EnvDTE.Document _currentDoc;

        private void UpdateFields(EnvDTE.Document doc)
        {
            SetPosition(doc);
            _path = new FileInfo(doc.FullName);

        }
        public void Launch()
        {
            if (CanOpenDocument(_currentDoc))
            {
                UpdateFields(_currentDoc);
                Process p = new Process();

                p.StartInfo = new ProcessStartInfo();
                p.StartInfo.FileName = _vimPath;
                p.StartInfo.Arguments = GetArguments();
                p.StartInfo.WorkingDirectory = _path.Directory.FullName;

                p.Start();
            }
        }

        /// <summary>
        /// explanation of arguments http://vim.wikia.com/wiki/Integrate_gvim_with_Visual_Studio
        /// </summary>
        private string GetArguments()
        {
            return string.Format("--servername VimStudio --remote-silent \"+call cursor({0}, {1})\" \"{2}\"",
                _curLine,
                _curCol,
                _path.FullName);
        }

        private void SetPosition(dynamic activeDoc)
        {
            _curLine = 0;
            _curCol = 0;

            try
            {
                _curLine = activeDoc.Selection.CurrentLine;
                _curCol = activeDoc.Selection.CurrentColumn;
            }
            catch
            {
                // Swallow the exception.  The worst thing that happens is that the cursor is not in the right place
            }
        }

        public bool CanOpenDocument(EnvDTE.Document doc)
        {
            if (doc == null) return false;
            string type = doc.Type;
            return type.Equals("Text", StringComparison.CurrentCultureIgnoreCase);
        }

        public EnvDTE.Document CurrentDocument
        {
            get { return _currentDoc; }
            set { _currentDoc = value; }
        }
    }
}

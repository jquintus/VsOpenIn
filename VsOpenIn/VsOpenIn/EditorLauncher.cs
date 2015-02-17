using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterDevs.VsOpenIn.Utils;
namespace MasterDevs.VsOpenIn
{
    public class EditorLauncher
    {
        private int _curLine;
        private int _curCol;
        private FileInfo _path;
        private EnvDTE.Document _currentDoc;

        private readonly string _defaultDirectory;

        public EditorLauncher()
        {
            _defaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private void UpdateFields(EnvDTE.Document doc)
        {
            SetPosition(doc);
            _path = new FileInfo(doc.FullName);
        }

        public void Launch(string path, string arguments, string initialDirectory)
        {
            if (CanOpenDocument(_currentDoc))
            {
                UpdateFields(_currentDoc);
                Process p = new Process();

                p.StartInfo = new ProcessStartInfo();
                p.StartInfo.FileName = path;
                p.StartInfo.Arguments = FormatArguments(arguments);
                p.StartInfo.WorkingDirectory = FormatArguments(initialDirectory, _defaultDirectory);
                p.Start();
            }
        }

        /// <summary>
        /// </summary>
        private string FormatArguments(string arguments, string defaultValue = "")
        {
            if (string.IsNullOrWhiteSpace(arguments))
            {
                return defaultValue;
            }
            Hashtable mappings = new Hashtable()
            {
                {"CurrentLine" , _curLine},
                {"CurrentCol" , _curCol},
                {"FullName", _path.FullName},
                {"FileName", _path.Name},
                {"DirectoryName", _path.DirectoryName}
            };

            return arguments.Inject(mappings);
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

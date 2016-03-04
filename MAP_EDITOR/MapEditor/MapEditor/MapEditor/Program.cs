using System;
using System.Windows.Forms;
using MapEditor.Editor;

namespace MapEditor
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();

            using (EditorForm editor = new EditorForm())
            {
                Application.Run(editor);
            }
        }
    }
#endif
}


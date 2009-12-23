using System;
using AviFile;
using System.Drawing;

namespace VideoReccorder
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main()
        {
            StartRecording();
            return 0;
        }
      
        public static void StartRecording()
        {
            Console.WriteLine("Start Recording ...");

            Console.ReadLine();

            var videoTransfert = new InterProcessCommunication.VideoTranfert();

            VideoStream aviStream = null;
            AviManager aviManager = null;

            bool endOfReccord = false;
            string lastFileReccord = "";

            do
            {
                // Récupère le frame suivant
                var frame = videoTransfert.ReadFrame();
                Console.WriteLine("Frame read ...");

                // Vérfie que nous somme pas à la fin de l'enregistrement
                endOfReccord = frame.EndOfRecord;
                if(!endOfReccord)
                {
                    var bitmap = frame.Bitmap;
                    // Si le nom du fichier d'enregistrement est changé un nouveau fichier vidéo doit être créé
                    if (lastFileReccord.Equals(frame.FileName) == false)
                    {
                        // Ferme les streams s'ils existent
                        if (aviStream != null)
                        {
                            aviStream.Close();
                            Console.WriteLine(@"Close video stream ""{0}"" ...", lastFileReccord);
                        }

                        if (aviManager != null)
                            aviManager.Close();

                        lastFileReccord = frame.FileName;
                        aviManager = new AviManager(lastFileReccord, false);
                        aviStream = aviManager.AddVideoStream(false,frame.FrameRate, bitmap); //bitmap étant la première image, elle sert a sizer le format du vidéo de sorti
                        Console.WriteLine(@"Create video stream ""{0}"" ...",lastFileReccord);
                    }
                    else
                    {
                        aviStream.AddFrame(bitmap);
                    }
                    bitmap.Dispose();
                }
                
            } while (!endOfReccord);

            // Ferme les streams s'ils existent
            if (aviStream != null)
            {
                aviStream.Close();
                Console.WriteLine(@"Close video stream ""{0}"" ...", lastFileReccord);
            }

            if (aviManager != null)
                aviManager.Close();


            Console.WriteLine("End Recording ...");
        }
    }
}
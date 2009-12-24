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

            var videoTransfert = new InterProcessCommunication.VideoTranfert();

            VideoStream aviStream = null;
            AviManager aviManager = null;

            bool endOfReccord = false;
            string lastFileReccord = "";

            do
            {
                // Récupère le frame suivant
                Console.Write("Reading frame");
                var frame = videoTransfert.ReadFrame();
                Console.WriteLine(" ...");

                // Vérfie que nous somme pas à la fin de l'enregistrement
                endOfReccord = frame.EndOfRecord;
                if(!endOfReccord)
                {
                    var bitmap = frame.Bitmap;
                    // Si le nom du fichier d'enregistrement est changé un nouveau fichier vidéo doit être créé
                    if (lastFileReccord.Equals(frame.FileName) == false)
                    {

                        // Ferme le fichier s'il y en a un d'ouvert
                        if (aviManager != null)
                        {
                            Console.WriteLine(@"Close video stream ""{0}"" ...", lastFileReccord);
                            aviManager.Close();
                        }

                        lastFileReccord = frame.FileName;

                        Console.Write(@"Creating video stream ""{0}"" ...", lastFileReccord);
                        
                        // Si le fichier est déjà existant on le supprime
                        if (System.IO.File.Exists(lastFileReccord))
                            System.IO.File.Delete(lastFileReccord);

                        aviManager = new AviManager(lastFileReccord, false);
                        aviStream = aviManager.AddVideoStream(false,frame.FrameRate, bitmap); //bitmap étant la première image, elle sert a sizer le format du vidéo de sorti
                        Console.WriteLine(" ...");
                        
                    }
                    else
                    {
                        Console.Write("Add frame to stream");
                        aviStream.AddFrame(bitmap);
                        Console.WriteLine(" ...");
                        
                    }
                    bitmap.Dispose();
                }
                
            } while (!endOfReccord);

            // Ferme le fichier s'il y en a un d'ouvert
            if (aviManager != null)
            {
                Console.WriteLine(@"Close video stream ""{0}"" ...", lastFileReccord);
                aviManager.Close();
            }


            Console.WriteLine("End Recording ...");
        }
    }
}
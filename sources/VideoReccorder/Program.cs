using System;
using AviFile;
using System.Drawing;

namespace VideoReccorder
{
    public class Program
    {
        private AviManager aviManager;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
        }
      
        public void StartRecording()
        {
            string filename = "";
            Bitmap bitmap = (Bitmap)Image.FromFile(filename);

            //TODO : lire par MMF le nom du fichier de sorti
            //TODO : lire par MMF les fichier bitmap modifié
           // bitmap.loadFromStream
            aviManager = new AviManager(filename, false);
            VideoStream aviStream =  aviManager.AddVideoStream(true, 2, bitmap); //bitmap étant la première image, elle sert a sizer le format du vidéo de sorti
          
            int count = 0;
            
           
            //On doit lire toutes les images passé par mmf, et les ajouté au stream
            //for (int n = 1; n < txtFileNames.Lines.Length; n++)
            //{
               // if (txtFileNames.Lines[n].Trim().Length > 0)
                //{
                  //  bitmap = (Bitmap)Bitmap.FromFile(txtFileNames.Lines[n]);
                    aviStream.AddFrame(bitmap);
                    bitmap.Dispose();
                    //count++;
               // }
            //}
            aviStream.Close();
            aviManager.Close();
        }

        public void StoptRecording()
        {
            
        }
    }
}
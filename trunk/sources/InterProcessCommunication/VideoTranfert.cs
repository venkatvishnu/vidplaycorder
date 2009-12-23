using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ThreadMessaging;

namespace InterProcessCommunication
{
    public class VideoTranfert : IDisposable
    {
        //[Serializable]
        //public class RecordInformation
        //{
        //    public int ChannelSize;
        //}

        [Serializable]
        public class Frame 
        {
            private string _fileName;
            private bool _endOfRecord;
            private double _frameRate;
            private Bitmap _bitmap;

            /// <summary>
            /// Frame contenant un image
            /// </summary>
            /// <param name="filename"></param>
            /// <param name="frameRate"></param>
            /// <param name="image"></param>
            public Frame(string filename,double frameRate,Bitmap image)
            {
                _fileName = filename;
                _frameRate = frameRate;
                _bitmap = image;
                _endOfRecord = false;
            }

            /// <summary>
            /// Frame de fin d'enregistrement
            /// </summary>
            public Frame()
            {
                _endOfRecord = true;
            }

            public string FileName
            {
                get { return _fileName; }
            }

            public bool EndOfRecord
            {
                get { return _endOfRecord; }
            }

            public double FrameRate
            {
                get { return _frameRate; }
            }

            public Bitmap Bitmap
            {
                get { return _bitmap; }
            }
        }

        //private MemoryMappedFile _mmfRecordFile;
        //private MemoryMappedFileView _mmfRecordFileView;
        //private ProcessSemaphore _semaphoreReccordFile;

        private readonly ProcessChannel _pictureTransfert;

        public VideoTranfert()
        {
            // Crée un MMF pour contenir 512 frame de 1MB
            _pictureTransfert = new ProcessChannel(256, "VidplaycorderPciture", 2*1024*1024);
        }
        

        
        /// <summary>
        /// Ajout un frame à la queue
        /// </summary>
        public void WriteFrame(Frame frame)
        {
            _pictureTransfert.Send(frame);
        }

        /// <summary>
        /// Lie le prochain frame dans la queue
        /// </summary>
        public Frame ReadFrame()
        {
            return _pictureTransfert.Receive() as Frame;
        }

        /// <summary>
        /// Exécute les tâches définies par l'application associées à la libération ou à la redéfinition des ressources non managées.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            _pictureTransfert.Dispose();
        }
    }
}

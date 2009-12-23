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
            public string FileName;
            public bool EndOfRecord;
            public double FrameRate;
            public byte[] ImageByte;
        }

        //private MemoryMappedFile _mmfRecordFile;
        //private MemoryMappedFileView _mmfRecordFileView;
        //private ProcessSemaphore _semaphoreReccordFile;

        private readonly ProcessChannel _pictureTransfert;

        public VideoTranfert()
        {
            //_mmfRecordFile = MemoryMappedFile.CreateFile("VidplaycorderRecordFile.MMF", ThreadMessaging.MemoryMappedFile.FileAccess.ReadWrite, 1024);
            //_mmfRecordFileView = _mmfRecordFile.CreateView(0, 1024, MemoryMappedFileView.ViewAccess.ReadWrite);
            //_semaphoreReccordFile = new ProcessSemaphore("VidplaycorderRecordFile.Sem",1,1);

            //_mmfReccordInfo = MemoryMappedFile.CreateFile("VidplaycorderRecordInfo.MMF", ThreadMessaging.MemoryMappedFile.FileAccess.ReadWrite,128);
            //_mmfReccordInfoView = _mmfReccordInfo.CreateView(0, 1024, MemoryMappedFileView.ViewAccess.ReadWrite);
            //_semaphoreReccordInfo = new ProcessSemaphore("VidplaycorderRecordInfo.Sem", 1, 1);

            // Un frame à une langueur maximal de 1MB
            _pictureTransfert = new ProcessChannel(512, "VidplaycorderPciture", 1024*1024);
        }

         //public VideoTranfert()
         //{
         //   //_mmfRecordFile = MemoryMappedFile.CreateFile("VidplaycorderRecordFile.MMF", ThreadMessaging.MemoryMappedFile.FileAccess.ReadWrite, 1024);
         //   //_mmfRecordFileView = _mmfRecordFile.CreateView(0, 1024, MemoryMappedFileView.ViewAccess.ReadWrite);
         //   //_semaphoreReccordFile = new ProcessSemaphore("VidplaycorderRecordFile.Sem", 1, 1);

         //   _mmfReccordInfo = MemoryMappedFile.CreateFile("VidplaycorderRecordInfo.MMF", ThreadMessaging.MemoryMappedFile.FileAccess.ReadWrite, 128);
         //   _mmfReccordInfoView = _mmfReccordInfo.CreateView(0, 1024, MemoryMappedFileView.ViewAccess.ReadWrite);
         //   _semaphoreReccordInfo = new ProcessSemaphore("VidplaycorderRecordInfo.Sem", 1, 1);

         //   _pictureTransfert = new ProcessChannel(512, "VidplaycorderPciture", 1024*1024);
            
         //}
        

        
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
            var buffer = _pictureTransfert.Receive();
            return buffer as Frame;
        }

        public static byte[] ImageToBytes(Bitmap image)
        {
            var s = new System.IO.MemoryStream();
            image.Save(s,System.Drawing.Imaging.ImageFormat.Bmp);

            var b = new byte[s.Length];
            s.Read(b, 0, b.Length);
            return b;
        }

        public static Bitmap BytesToImage(byte[] buffer)
        {
            var s = new System.IO.MemoryStream(buffer);
            return new Bitmap(s);
        }

        /// <summary>
        /// Exécute les tâches définies par l'application associées à la libération ou à la redéfinition des ressources non managées.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            _pictureTransfert.Dump();
            _pictureTransfert.Dispose();
        }
    }
}

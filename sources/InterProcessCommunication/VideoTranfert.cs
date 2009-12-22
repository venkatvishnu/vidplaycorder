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
        [Serializable]
        public class RecordInformation
        {
            public double FrameRate;
            public int BitmapSize;
        }

        private MemoryMappedFile _mmfRecordFile;
        private MemoryMappedFileView _mmfRecordFileView;
        private ProcessSemaphore _semaphoreReccordFile;

        private MemoryMappedFile _mmfReccordInfo;
        private MemoryMappedFileView _mmfReccordInfoView;
        private ProcessSemaphore _semaphoreReccordInfo;

        private ProcessChannel _pictureTransfert;

        public VideoTranfert(int bitmapSize)
        {
            _mmfRecordFile = MemoryMappedFile.CreateFile("VidplaycorderRecordFile.MMF", ThreadMessaging.MemoryMappedFile.FileAccess.ReadWrite, 1024);
            _mmfRecordFileView = _mmfRecordFile.CreateView(0, 1024, MemoryMappedFileView.ViewAccess.ReadWrite);
            _semaphoreReccordFile = new ProcessSemaphore("VidplaycorderRecordFile.Sem",1,1);

            _mmfReccordInfo = MemoryMappedFile.CreateFile("VidplaycorderRecordInfo.MMF", ThreadMessaging.MemoryMappedFile.FileAccess.ReadWrite,128);
            _mmfReccordInfoView = _mmfReccordInfo.CreateView(0, 1024, MemoryMappedFileView.ViewAccess.ReadWrite);
            _semaphoreReccordInfo = new ProcessSemaphore("VidplaycorderRecordInfo.Sem", 1, 1);

            _pictureTransfert = new ProcessChannel(bitmapSize * 400, "VidplaycorderPciture", bitmapSize);
            FileName = "\n";
            RecordInfo = new RecordInformation(){BitmapSize = bitmapSize,FrameRate = 25};
        }

         public VideoTranfert()
         {
            _mmfRecordFile = MemoryMappedFile.CreateFile("VidplaycorderRecordFile.MMF", ThreadMessaging.MemoryMappedFile.FileAccess.ReadWrite, 1024);
            _mmfRecordFileView = _mmfRecordFile.CreateView(0, 1024, MemoryMappedFileView.ViewAccess.ReadWrite);
            _semaphoreReccordFile = new ProcessSemaphore("VidplaycorderRecordFile.Sem", 1, 1);

            _mmfReccordInfo = MemoryMappedFile.CreateFile("VidplaycorderRecordInfo.MMF", ThreadMessaging.MemoryMappedFile.FileAccess.ReadWrite, 128);
            _mmfReccordInfoView = _mmfReccordInfo.CreateView(0, 1024, MemoryMappedFileView.ViewAccess.ReadWrite);
            _semaphoreReccordInfo = new ProcessSemaphore("VidplaycorderRecordInfo.Sem", 1, 1);

            _pictureTransfert = new ProcessChannel(RecordInfo.BitmapSize * 400, "VidplaycorderPciture", RecordInfo.BitmapSize);
            FileName = "\n";
         }
        /// <summary>
        /// Nom du fichier dans lequel le vidéo sera enregistré
        /// </summary>
        public string FileName
        {
            set
            {
                _semaphoreReccordFile.Acquire();

                try
                {
                    _mmfRecordFileView.WriteBytes(System.Text.Encoding.Unicode.GetBytes(value + "\n"));
                }
                catch (Exception)
                {
                }
                finally
                {
                    _semaphoreReccordFile.Release();
                }
            }

            get
            {

                _semaphoreReccordFile.Acquire();

                byte[] buffer = new byte[1024];

                try
                {
                    _mmfRecordFileView.ReadBytes(buffer);
                }
                catch (Exception)
                {
                }
                finally
                {
                    _semaphoreReccordFile.Release();                    
                }

                var tempString = Encoding.Unicode.GetString(buffer);
                var afterLastValidChar = tempString.IndexOf("\n");
                return tempString.Substring(0,afterLastValidChar);
            }
        }

        /// <summary>
        /// Information sur la vidéo
        /// </summary>
        public RecordInformation RecordInfo
        {
            set
            {
                _semaphoreReccordInfo.Acquire();

                try
                {
                    _mmfReccordInfoView.WriteSerialize(value);
                }
                catch (Exception)
                {
                }
                finally
                {
                    _semaphoreReccordInfo.Release();
                }
            }

            get
            {
                _semaphoreReccordInfo.Acquire();

                RecordInformation information = null;
                try
                {
                    information = _mmfReccordInfoView.ReadDeserialize() as RecordInformation;
                }
                catch (Exception)
                {
                }
                finally
                {
                    _semaphoreReccordInfo.Release();
                }

                return information;
            }
        }

        /// <summary>
        /// Ajout un bitmap à la queue
        /// </summary>
        /// <param name="image"></param>
        public void WriteBitmap(Bitmap image)
        {
            _pictureTransfert.SendBytes(ImageToBytes(image));
        }

        /// <summary>
        /// Lie le prochain bitmap dans la queue
        /// </summary>
        /// <returns>Indique false si c'est le signal de la fin de l'enregistrement</returns>
        public bool ReadBitmap(out Bitmap image)
        {
            var buffer = _pictureTransfert.ReceiveBytes();

            image = BytesToImage(buffer);

            return true;
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

            _semaphoreReccordFile.Dispose();
            _mmfRecordFile.Dispose();
            _mmfRecordFileView.Dispose();
            
            _semaphoreReccordInfo.Dispose();
            _mmfReccordInfo.Dispose();
            _mmfReccordInfoView.Dispose();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using AviFile;

namespace VideoPlayer
{
    public class VideoSource
    {
        private const int BuferMaxSize = 2*30;
        
        private readonly Queue<Bitmap> _frameBuffer = new Queue<Bitmap>();

        private Thread _threadFillBuffer = null;
        
        private int _currentFrame = 0;
        private int _step = 1;


        private AviManager _aviManager;
        private VideoStream _videoStream;

        /// <summary>
        /// Récupère le frame courant
        /// </summary>
        /// <returns></returns>
        public Bitmap GetCurrentFrame()
        {
            while (!Availible)
                Thread.Sleep(5);

            lock (this)
            {
                return _frameBuffer.Peek();
            }
        }

        /// <summary>
        /// Passe au frame suivant
        /// </summary>
        /// <returns>Le frame courant</returns>
        public Bitmap NextFrame()
        {
            while(!Availible)
                Thread.Sleep(5);


            lock (this)
            {
                return _frameBuffer.Dequeue();
            }
        }
        
        /// <summary>
        /// Indique s'il y a des frames disponibles pour la lecture
        /// </summary>
        public bool Availible
        {
            get
            {
                lock (this)
                {
                    return _frameBuffer.Count > 0;
                }
            }
        }

        public int Step
        {
            get
            {
                lock (this)
                {
                    return _step;
                }
            }
            set
            {
                lock (this)
                {
                    _step = value;
                }
            }
        }

        public int FrameCount
        {
            get
            {
                lock (this)
                {
                    return _videoStream.CountFrames;
                }
            }
        }

        public double FrameRate
        {
            get
            {
                return _videoStream.FrameRate;
            }
        }

        public void Open(string file)
        {
            lock (this)
            {
                _aviManager = new AviManager(file, true);
                _videoStream = _aviManager.GetVideoStream();

                _videoStream.GetFrameOpen();
                _currentFrame = 0;
                _frameBuffer.Clear();
            }

            if (_threadFillBuffer == null)
            {
                _threadFillBuffer = new Thread(this.FillBuffer);
                _threadFillBuffer.Start();
            }

        }

        public void Close()
        {
            lock(this)
            {
                if (_threadFillBuffer != null)
                {
                    _threadFillBuffer.Abort();
                }

                _videoStream.GetFrameClose();
                _aviManager.Close();

                _frameBuffer.Clear();
            }
        }

        public void Reset()
        {
            lock (this)
            {
                _frameBuffer.Clear();
                _currentFrame = 0;
            }
        }

        private void FillBuffer()
        {
            while (true)
            {
                Monitor.Enter(this);
                if (_frameBuffer.Count <= BuferMaxSize)
                {
                    
                    if (_currentFrame >= 0 && _currentFrame < FrameCount)
                    {
                        _frameBuffer.Enqueue(_videoStream.GetBitmap(_currentFrame));
                        _currentFrame += Step;
                        
                    }
                    
                    Monitor.Exit(this);
                }
                else
                {
                    Monitor.Exit(this);
                    Thread.Sleep(10);
                }
            }
        }
    }
}

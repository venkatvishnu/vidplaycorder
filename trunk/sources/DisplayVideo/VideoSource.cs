using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;


using User.DirectShow;

namespace VideoPlayer
{
    class VideoSource
    {
        private const int BuferMaxSize = 2*30;
        
        private readonly Queue<Bitmap> _frameBuffer = new Queue<Bitmap>();

        private Thread _threadFillBuffer = null;
        
        private int _currentFrame = 0;
        private int _step = 1;
        private readonly FrameGrabber _frameGrabber = new FrameGrabber();
        
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
                    return _frameGrabber.FrameCount;
                }
            }
        }

        public void Open(string file)
        {
            Close();

            lock (this)
            {
                _frameGrabber.FileName = file;

                _currentFrame = 0;
                _frameBuffer.Clear();
            }

            _threadFillBuffer = new Thread(this.FillBuffer);
                _threadFillBuffer.Start();
            
        }

        public void Close()
        {
            lock(this)
            {
                if (_threadFillBuffer != null)
                {
                    _threadFillBuffer.Abort();
                }

                _frameBuffer.Clear();
            }
        }

        private void FillBuffer()
        {
            while (true)
            {
                Monitor.Enter(this);
                if (_frameBuffer.Count <= BuferMaxSize)
                {
                    
                    if (_currentFrame > 0 && _currentFrame < FrameCount)
                    {
                        
                        _frameBuffer.Enqueue(_frameGrabber.GetImage(_currentFrame));
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

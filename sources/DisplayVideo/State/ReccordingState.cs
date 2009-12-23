using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using InterProcessCommunication;
using ThreadMessaging;

namespace VideoPlayer.State
{
    class ReccordingState : TimerState
    {
        private VideoTranfert _videoTransfert;
        private Process _process;
        private string _fileName;

        private Queue<VideoTranfert.Frame> _imageToRecord = new Queue<VideoTranfert.Frame>();
        private Thread _threadTransfertFrame;

        public ReccordingState(PlayerStateController playerStateController, VideoSource videoSource, IFrameDisplay frameDisplay) : base(playerStateController, videoSource, frameDisplay)
        {
            _videoTransfert = new VideoTranfert();
            _process = Process.GetProcessesByName("VideoReccorder")[0];
        }

        public override void DoAction()
        {
            var frame = _videoSource.NextFrame();

            _frameDisplay.UpdateFrame((Bitmap) frame.Clone());

            var tranfertFrame = new VideoTranfert.Frame(_fileName, _videoSource.FrameRate, frame);

            lock (_imageToRecord)
            {
                _imageToRecord.Enqueue(tranfertFrame);
            }
        }

        public override void Forward()
        {
            _timer.Stop();
            ChangeState(_playerStateController.ForwardingState);
        }

        public override void Rewind()
        {
            _timer.Stop();
            ChangeState(_playerStateController.RewindingState);
        }

        public override void Pause()
        {
            _timer.Stop();
            ChangeState(_playerStateController.PauseReccordingState);
        }

        public override void Record(string _outputFile)
        {
            _timer.Stop();
            ChangeState(_playerStateController.PlayingState);
        }

        public override void Begin(object argument)
        {
            _fileName = (string) argument;
            StartRecording();

            _timer.Resolution = 1;
            _timer.Period = (int)(1000 / _videoSource.FrameRate);
            base.Begin(argument);
            _videoSource.Step = 1;
        }

        private void StartRecording()
        {
            // Lance le thread qui va tranférer les frames s'il n'est pas créer
            if (_threadTransfertFrame == null || !_threadTransfertFrame.IsAlive)
            {
                _threadTransfertFrame = new Thread(TransfertToRecorder);
                _threadTransfertFrame.Start();
            }
        }

        public override bool IsPlaying
        {
            get
            {
                return true;
            }
        }

        public override bool IsReccording
        {
            get
            {
                return true;
            }
        }

        public void TransfertToRecorder()
        {
            // Traite les frames jusqu'au frame indiquant la fin de l'enregistrement
            bool fin = false;

            do
            {
                VideoTranfert.Frame frame = null;
                lock (_imageToRecord)
                {
                    if (_imageToRecord.Count > 0)
                    {
                        frame = _imageToRecord.Dequeue();
                    }
                }
                if (frame != null)
                {
                    _videoTransfert.WriteFrame(frame);
                    fin = frame.EndOfRecord;
                }
                else
                {
                    Thread.Sleep(500);
                }
            } while (!fin);
        }

        protected override void Disposing()
        {
            base.Disposing();
            
            // Ajoute le frame indiquant la fin du processus d'enregistrement
            lock (_imageToRecord)
            {
                _imageToRecord.Enqueue(new VideoTranfert.Frame());
            }

            // Attend que le processus d'enregistrement soit terminé
            //int count;

            //do
            //{
            //    lock (_imageToRecord)
            //    {
            //        count = _imageToRecord.Count;
            //    }

            //    Thread.Sleep(100);

            //} while (count>0);

            while (!_process.HasExited)
            {
                Thread.Sleep(100);
            }
        }
    }
}
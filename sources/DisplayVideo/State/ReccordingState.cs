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
            _process = Process.Start("VideoReccorder");

            // Lance le thread qui va tranférer les frames à l'autre process
            _threadTransfertFrame = new Thread(TransfertToRecorder);
            _threadTransfertFrame.Start();
            
        }

        public override void DoAction()
        {
            if (!_videoSource.EndOfFile)
            {
                var frame = _videoSource.NextFrame();
                _frameDisplay.UpdateFrame((Bitmap)frame.Clone());

                var tranfertFrame = new VideoTranfert.Frame(_fileName, _videoSource.FrameRate, frame);

                lock (_imageToRecord)
                {
                    _imageToRecord.Enqueue(tranfertFrame);
                }
            }
            else
            {
                _timer.Stop();
                ChangeState(_playerStateController.StoppedState);
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

            _timer.Resolution = 1;
            _timer.Period = (int)(1000 / _videoSource.FrameRate);
            base.Begin(argument);
            _videoSource.Step = 1;
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

                    if (frame.Bitmap != null) frame.Bitmap.Dispose();

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
            
            // Augmente la priorité du process et du thread d'enregistrement
            _threadTransfertFrame.Priority = ThreadPriority.Highest;
            _process.PriorityClass  = ProcessPriorityClass.AboveNormal;

            // Ajoute le frame indiquant la fin du processus d'enregistrement
            lock (_imageToRecord)
            {
                _imageToRecord.Enqueue(new VideoTranfert.Frame());
            }

            
            // Attend que le processus d'enregistrement soit terminé
            while (!_process.HasExited)
            {
                Thread.Sleep(1000);
            }

            _videoTransfert.Dispose();
        }
    }
}
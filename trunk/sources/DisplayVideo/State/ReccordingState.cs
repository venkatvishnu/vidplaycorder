using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using InterProcessCommunication;
using ThreadMessaging;

namespace VideoPlayer.State
{
    class ReccordingState : TimerState
    {
        private VideoTranfert _videoTransfert;
        private Process _process;

        private Queue<VideoTranfert.Frame> _imageToRecord = new Queue<VideoTranfert.Frame>();

        public ReccordingState(PlayerStateController playerStateController, VideoSource videoSource, IFrameDisplay frameDisplay) : base(playerStateController, videoSource, frameDisplay)
        {
        }

        public override void DoAction()
        {
            var frame = _videoSource.NextFrame();

            _frameDisplay.UpdateFrame(frame);

            lock (_imageToRecord)
            {
                _imageToRecord.Enqueue(new VideoTranfert.Frame()
                {FrameRate = _videoSource.FrameRate,
                    ImageByte = VideoTranfert.ImageToBytes(frame)});
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

        public override void Begin()
        {
            StartRecording();

            _timer.Resolution = 1;
            _timer.Period = (int)(1000 / _videoSource.FrameRate);
            base.Begin();
            _videoSource.Step = 1;
        }

        private void StartRecording()
        {
            // Définie la communication à partir de la taille d'une image
            var bitmapSize = VideoTranfert.ImageToBytes(_videoSource.GetCurrentFrame()).Length;
            
            _videoTransfert = new VideoTranfert();

            new System.Threading.Thread(this.TransfertToRecorder).Start();
        }

        private void StopRecording()
        {
            lock (_imageToRecord)
            {
                _imageToRecord.Enqueue(null);
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
            // Lance le processus
            //_process = System.Diagnostics.Process.Start("VideoReccorder.exe");

            bool fin = true;

            do
            {
                VideoTranfert.Frame frame = null;
                lock (_imageToRecord)
                {
                    if (_imageToRecord.Count > 0)
                    {
                        frame = _imageToRecord.Dequeue();
                        fin = true;
                    }
                    else
                    {
                        fin = false;
                    }
                }
                if (frame != null)
                {
                    _videoTransfert.WriteFrame(frame);
                    fin = false;
                }
            } while (!fin);
        }
    }
}
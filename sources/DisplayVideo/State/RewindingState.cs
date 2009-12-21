using System;

namespace VideoPlayer.State
{
    class RewindingState : TimerState
    {
        public RewindingState(PlayerStateController playerStateController, VideoSource videoSource, IFrameDisplay frameDisplay) : base(playerStateController, videoSource, frameDisplay)
        {
        }

        public override void DoAction()
        {
            if (!_videoSource.BeginOfFile)
            {
                _frameDisplay.UpdateFrame(_videoSource.NextFrame());
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

        public override void Play()
        {
            _timer.Stop();
            ChangeState(_playerStateController.PlayingState);
        }

        public override void Pause()
        {
            _timer.Stop();
            ChangeState(_playerStateController.PausedState);
        }

        public override void Record(string _outputFile)
        {
            _timer.Stop();
            ChangeState(_playerStateController.ReccordingState);
        }

        public override void Begin()
        {
            _timer.Resolution = 1;
            _timer.Period = (int)(1000 / (_videoSource.FrameRate * 1));
            base.Begin();
            _videoSource.Step = -4;
        }

        public override bool IsPlaying
        {
            get
            {
                return true;
            }
        }
    }
}
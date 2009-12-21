using System;

namespace VideoPlayer.State
{
    class ForwardingState : TimerState
    {
        public ForwardingState(PlayerStateController playerStateController, VideoSource videoSource, IFrameDisplay frameDisplay) : base(playerStateController, videoSource, frameDisplay)
        {
        }

        public override void DoAction()
        {
            _frameDisplay.UpdateFrame(_videoSource.NextFrame());
        }

       public override void Rewind()
        {
            _timer.Stop();
            ChangeState(_playerStateController.RewindingState);
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

        public override void Record()
        {
            _timer.Stop();
            ChangeState(_playerStateController.ReccordingState);
        }

        public override void Begin()
        {
            base.Begin();
            _videoSource.Step = 10;
        }
    }
}
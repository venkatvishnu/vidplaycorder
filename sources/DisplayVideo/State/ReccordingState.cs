using System;

namespace VideoPlayer.State
{
    class ReccordingState : TimerState
    {
        public ReccordingState(PlayerStateController playerStateController, VideoSource videoSource, IFrameDisplay frameDisplay) : base(playerStateController, videoSource, frameDisplay)
        {
        }

        public override void DoAction()
        {
            _frameDisplay.UpdateFrame(_videoSource.NextFrame());
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

        public override void Record()
        {
            _timer.Stop();
            ChangeState(_playerStateController.PlayingState);
        }

        public override void Begin()
        {
            base.Begin();
            _videoSource.Step = 1;
        }
    }
}
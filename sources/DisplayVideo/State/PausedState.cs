namespace VideoPlayer.State
{
    class PausedState : BaseState
    {
        public PausedState(PlayerStateController playerStateController, VideoSource videoSource, IFrameDisplay frameDisplay) : base(playerStateController, videoSource, frameDisplay)
        {
        }

        public override void Forward()
        {
            ChangeState(_playerStateController.ForwardingState);
        }

        public override void Rewind()
        {
           ChangeState(_playerStateController.RewindingState);
        }

        public override void Play()
        {
            ChangeState(_playerStateController.PlayingState);
        }

        public override void Record()
        {
            ChangeState(_playerStateController.PauseReccordingState);
        }

        public override bool IsPaused
        {
            get
            {
                return true;
            }
        }

    }
}
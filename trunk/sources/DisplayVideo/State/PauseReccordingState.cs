namespace VideoPlayer.State
{
    class PauseReccordingState : BaseState
    {
        public PauseReccordingState(PlayerStateController playerStateController, VideoSource videoSource, IFrameDisplay frameDisplay) : base(playerStateController, videoSource, frameDisplay)
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
            ChangeState(_playerStateController.ReccordingState);
        }

        public override void Record(string _outputFile)
        {
            ChangeState(_playerStateController.PausedState);
        }

        public override bool IsPaused
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

    }
}
namespace VideoPlayer.State
{
    class StoppedState : BaseState
    {
        public StoppedState(PlayerStateController playerStateController, VideoSource videoSource, IFrameDisplay frameDisplay) : base(playerStateController, videoSource, frameDisplay)
        {
        }

        public override void Play()
        {
            ChangeState(_playerStateController.PlayingState);
        }

        public override void Begin(object argument)
        {
            _videoSource.Step = 1;
            _videoSource.Reset();
        }
    }
}
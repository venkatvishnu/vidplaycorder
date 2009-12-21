namespace VideoPlayer.State
{
    class InitialState : BaseState
    {
        public InitialState(PlayerStateController playerStateController, VideoSource videoSource, IFrameDisplay frameDisplay) : base(playerStateController, videoSource, frameDisplay)
        {
        }

        public override bool FileOpen
        {
            get
            {
                return false;
            }
        }
    }
}
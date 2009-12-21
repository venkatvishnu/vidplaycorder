using System.Threading;

namespace VideoPlayer.State
{
    abstract class TimerState : BaseState
    {
        protected readonly MultimediaTimer _timer;
        private MultimediaTimer.TimeProc _timeProc;

        protected TimerState(PlayerStateController playerStateController, VideoSource videoSource, IFrameDisplay frameDisplay)
            : base(playerStateController, videoSource, frameDisplay)
        {
            _timer = new MultimediaTimer {Mode = TimerMode.Periodic};
            _timeProc = new MultimediaTimer.TimeProc(Tick);
        }

        public override void Begin()
        {
            _timer.Start(_timeProc);
        }

        private void Tick(int id, int msg, int user, int param1, int param2)
        {
            DoAction();
        }

        public abstract void DoAction();
    }
}
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

        public override void Stop()
        {
            _timer.Stop();
            Thread.Sleep(500);
            base.Stop();
        }

        public override void Close()
        {
            _timer.Stop();
            Thread.Sleep(500);
            base.Close();
        }

        public override void Open(string file)
        {
            _timer.Stop();
            Thread.Sleep(500);
            base.Open(file);
        }

        private void Tick(int id, int msg, int user, int param1, int param2)
        {
            DoAction();
        }

        public abstract void DoAction();
    }
}
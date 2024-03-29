using System;

namespace VideoPlayer.State
{
    abstract class BaseState : IPlayerState
    {
        protected readonly PlayerStateController _playerStateController;
        protected readonly VideoSource _videoSource;
        protected readonly IFrameDisplay _frameDisplay;

        protected BaseState(PlayerStateController playerStateController,VideoSource videoSource,IFrameDisplay frameDisplay)
        {
            _playerStateController = playerStateController;
            _videoSource = videoSource;
            _frameDisplay = frameDisplay;
        }

        public virtual void Open(string file)
        {
            _videoSource.Open(file);
            ChangeState(_playerStateController.StoppedState);
        }

        public virtual void Close()
        {
            _videoSource.Close();
            ChangeState(_playerStateController.InitialState);
        }

        public virtual void Play()
        {
            throw new NotSupportedException();
        }

        public virtual void Pause()
        {
            throw new NotSupportedException();
        }

        public virtual void Stop()
        {
            ChangeState(_playerStateController.StoppedState);
        }

        public virtual void Forward()
        {
            throw new NotSupportedException();
        }

        public virtual void Rewind()
        {
            throw new NotSupportedException();
        }

        public virtual void Record(string _outputFile)
        {
            throw new NotSupportedException();
        }

        public virtual void Begin(object argument)
        {
        }

        public virtual bool IsPlaying
        {
            get { return false; }
        }

        public virtual bool IsPaused
        {
            get { return false; }
        }

        public virtual bool IsFastPlaying
        {
            get { return false; }
        }

        public virtual bool IsReccording
        {
            get { return false; }
        }

        public virtual bool FileOpen
        {
            get { return true; }
        }


        protected void ChangeState(IPlayerState state)
        {
            ChangeState(state,null);
        }

        protected void ChangeState(IPlayerState state,object argument)
        {
            _playerStateController.CurrentState = state;
            _playerStateController.CurrentState.Begin(argument);
        }

        /// <summary>
        /// Ex�cute les t�ches d�finies par l'application associ�es � la lib�ration ou � la red�finition des ressources non manag�es.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Disposing();
        }

        protected virtual void Disposing()
        {
            
        }
    }
}
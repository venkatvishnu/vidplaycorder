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

        public virtual void Record()
        {
            throw new NotSupportedException();
        }

        public virtual void Begin()
        {
        }

        protected void ChangeState(IPlayerState state)
        {
            _playerStateController.CurrentState = state;
            _playerStateController.CurrentState.Begin();
        }
    }
}
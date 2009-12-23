namespace VideoPlayer.State
{
    public interface IPlayerState : System.IDisposable
    {
        void Open(string file);
        void Close();
        void Play();
        void Pause();
        void Stop();
        void Forward();
        void Rewind();
        void Record(string _outputFile);
        void Begin(object argument);

        bool IsPlaying { get; }
        bool IsPaused { get; }
        bool IsFastPlaying { get; }
        bool IsReccording { get; }
        bool FileOpen { get; }
    }
}
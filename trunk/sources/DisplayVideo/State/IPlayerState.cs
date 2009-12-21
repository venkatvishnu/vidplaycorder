namespace VideoPlayer.State
{
    public interface IPlayerState
    {
        void Open(string file);
        void Close();
        void Play();
        void Pause();
        void Stop();
        void Forward();
        void Rewind();
        void Record(string _outputFile);
        void Begin();

        bool IsPlaying { get; }
        bool IsPaused { get; }
        bool IsFastPlaying { get; }
        bool IsReccording { get; }
        bool FileOpen { get; }
    }
}
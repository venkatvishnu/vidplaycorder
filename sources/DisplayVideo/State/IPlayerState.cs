namespace VideoPlayer.State
{
    public interface IPlayerState
    {
        void Open(string file);
        void Close();
        void Play();
        void Stop();
        void Forward();
        void Rewind();
        void Record();
    }
}
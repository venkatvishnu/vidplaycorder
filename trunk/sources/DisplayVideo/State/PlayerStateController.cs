using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace VideoPlayer.State
{
    class PlayerStateController
    {
        private VideoSource _source;

        public PlayerStateController(IFrameDisplay frameDisplay)
        {
            _source = new VideoSource();

            CurrentState = InitialState = new InitialState(this, _source, frameDisplay);
            StoppedState = new StoppedState(this, _source, frameDisplay);
            PlayingState = new PlayingState(this, _source, frameDisplay);
            PausedState = new PausedState(this, _source, frameDisplay);
            ReccordingState = new ReccordingState(this, _source, frameDisplay);
            PauseReccordingState = new PauseReccordingState(this, _source, frameDisplay);
            RewindingState = new RewindingState(this, _source, frameDisplay);
            ForwardingState = new ForwardingState(this, _source, frameDisplay);
        }

        public IPlayerState InitialState{ get;private set; }
        public IPlayerState StoppedState{ get;private set; }
        public IPlayerState PlayingState{ get;private set; }
        public IPlayerState PausedState{ get;private set; }
        public IPlayerState ReccordingState{ get;private set; }
        public IPlayerState PauseReccordingState{ get;private set; }
        public IPlayerState RewindingState{ get;private set; }
        public IPlayerState ForwardingState{ get;private set; }


        /// <summary>
        /// État courante du lecteur
        /// </summary>
        public IPlayerState CurrentState{ get; set; }
       
        /// <summary>
        /// Contraste utilisé pour le traitement de l'image (-255 à 255)
        /// </summary>
        public double Contraste
        {
            get{ return Traitement.Instance.Contraste;}
            set { Traitement.Instance.Contraste = value; }
        }
        /// <summary>
        /// Brillance utilisé pour le traitement de l'image (0.5 à 2)
        /// </summary>
        public int Brillance
        {
            get{ return Traitement.Instance.Brillance;}
            set { Traitement.Instance.Brillance = value; }
        }



      
        /// <summary>
        /// Tableau d'entier représentant la matrice de convolution pour le traitement
        /// </summary>
        public int[] Convolution
        {
            get{ return Traitement.Instance.Convolution;}
            set { Traitement.Instance.Convolution = value; }
        }

        public void Open(string file)
        {
            CurrentState.Open(file);
        }

        public void Close()
        {
            CurrentState.Close();
        }

        public void Play()
        {
            CurrentState.Play();
        }

        public void Pause()
        {
            CurrentState.Pause();
        }

        public void Stop()
        {
            CurrentState.Stop();
        }

        public void Forward()
        {
            CurrentState.Forward();
        }

        public void Rewind()
        {
            CurrentState.Rewind();
        }

        public void Record(string _outputFile)
        {
            CurrentState.Record(_outputFile);
        }

        public bool IsPlaying
        {
            get { return CurrentState.IsPlaying; }
        }

        public bool IsFastPlaying
        {
            get { return CurrentState.IsFastPlaying; }
        }

        public bool IsPaused
        {
            get{ return CurrentState.IsPaused;}
        }
        public bool IsReccording
        {
            get { return CurrentState.IsReccording; }
        }
        public bool FileOpen
        {
            get{ return CurrentState.FileOpen;}
        }
    }
}
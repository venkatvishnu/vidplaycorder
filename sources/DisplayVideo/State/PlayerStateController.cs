using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace VideoPlayer.State
{
    class PlayerStateController
    {
        private readonly IFrameDisplay _frameDisplay;
        private VideoSource _source;

        public PlayerStateController(IFrameDisplay frameDisplay)
        {
            _frameDisplay = frameDisplay;
            _source = new VideoSource();

            var t = new MultimediaTimer {Period = 1000/30, Resolution = 1000/30};
            t.Start(TimerCallback);
        }

        public IPlayerState InitialState{ get;private set; }
        public IPlayerState StoppedState{ get;private set; }
        public IPlayerState PlayingState{ get;private set; }
        public IPlayerState PausedState{ get;private set; }
        public IPlayerState ReccordingState{ get;private set; }
        public IPlayerState PauseReccordingState{ get;private set; }
        public IPlayerState RewindingState{ get;private set; }
        public IPlayerState ForwardingingState{ get;private set; }


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

        public void Record()
        {
            CurrentState.Record();
        }

        private void TimerCallback(int id, int msg, int user, int param1, int param2)
        {
            _frameDisplay.UpdateFrame(_source.NextFrame());
        }
    }
}
using System;
using System.Collections;
using System.Linq;
using System.Text;


using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using DirectShowLib;

namespace VideoPlayer
{
    internal enum PlayState
    {
        Stopped,
        Paused,
        Running,
        Init
    };

    internal enum MediaType
    {
        Audio,
        Video
    }

    class ConcretPlayerState
    {
        private const int WMGraphNotify = 0x0400 + 13;
        private const int VolumeFull = 0;
        private const int VolumeSilence = -10000;

        private IGraphBuilder graphBuilder = null;
        private IMediaControl mediaControl = null;
        private IMediaEventEx mediaEventEx = null;
        private IVideoWindow videoWindow = null;
        private IBasicAudio basicAudio = null;
        private IBasicVideo basicVideo = null;
        private IMediaSeeking mediaSeeking = null;
        private IMediaPosition mediaPosition = null;
        private IVideoFrameStep frameStep = null;

        private string filename = string.Empty;
        private bool isAudioOnly = false;
        private bool isFullScreen = false;
        private int currentVolume = VolumeFull;
        private PlayState currentState = PlayState.Stopped;
        private double currentPlaybackRate = 1.0;

        private IntPtr hDrain = IntPtr.Zero;

    #if DEBUG
            private DsROTEntry rot = null;
    #endif

        private int InitVideoWindow(int nMultiplier, int nDivider)
        {
            int hr = 0;
            int lHeight, lWidth;

            if (this.basicVideo == null)
                return 0;

            // Read the default video size
            hr = this.basicVideo.GetVideoSize(out lWidth, out lHeight);
            if (hr == DsResults.E_NoInterface)
                return 0;

            EnablePlaybackMenu(true, MediaType.Video);

            // Account for requests of normal, half, or double size
            lWidth = lWidth * nMultiplier / nDivider;
            lHeight = lHeight * nMultiplier / nDivider;

            this.ClientSize = new Size(lWidth, lHeight);
            Application.DoEvents();

            hr = this.videoWindow.SetWindowPosition(0, 0, lWidth, lHeight);

            return hr;
        }


        public ConcretPlayerState()
        {

        }

        public void doAction()
        {
        }
        public void open(string filename)
        {
            try
            {
                // If no filename specified by command line, show file open dialog
                if (filename == string.Empty)
                {
                  //  UpdateMainTitle();

                    this.filename = filename;
                    if (filename == string.Empty)
                        return;
                }

                // Reset status variables
                this.currentState = PlayState.Stopped;
                this.currentVolume = VolumeFull;

                // Start playing the media file
             //   PlayMovieInWindow(filename);
            }
            catch
            {
              //  CloseClip();
            }
        }
        public void close()
        {
        }
        public void play()
        {
        }
        public void stop()
        {
        }
        public void forward()
        {
        }
        public void rewind()
        {
        }
        public void record()
        {
        }
        public void getNextState()
        {
        }
    }
}

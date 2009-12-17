using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoPlayer
{
    interface IFrameDisplay
    {
        void UpdateFrame(System.Drawing.Bitmap frame);
    }
}

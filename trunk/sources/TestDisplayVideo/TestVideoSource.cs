using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using VideoPlayer;

namespace TestDisplayVideo
{

    [TestFixture]
    public class TestVideoSource
    {
        private VideoPlayer.VideoSource _videoSource = new VideoSource();

        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void Test()
        {
            var f = new DisplayBitmap();
            _videoSource.Open(@"\\thomas_disk\video\Dr House\Dr House - Saison 3\House.S03E01.FRENCH.DVDRip.XviD-FiXi0N.avi");

            _videoSource.Step = 1;
            
            Console.WriteLine("Frame count: {0}",_videoSource.FrameCount);
            Console.WriteLine("Frame rate: {0}", _videoSource.FrameRate);

            _videoSource.Close();
            
        }

    }
}
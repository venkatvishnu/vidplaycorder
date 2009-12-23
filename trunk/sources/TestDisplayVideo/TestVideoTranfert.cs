using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace TestDisplayVideo
{

    [TestFixture]
    public class TestVideoTranfert
    {

        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void Test1()
        {
            new FormTestVideoTransfert().ShowDialog();
        }

    }
}
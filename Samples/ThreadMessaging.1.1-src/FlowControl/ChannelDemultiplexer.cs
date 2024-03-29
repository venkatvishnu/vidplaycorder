#region Copyright 2004 Christoph Daniel R�egg [Modified BSD License]
/*
ThreadMessaging.NET, InterThread/-Process Communication Framework.
Copyright (c) 2004, Christoph Daniel Rueegg, http://cdrnet.net/.
All rights reserved.

[Modified BSD License]

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice,
this list of conditions and the following disclaimer. 

2. Redistributions in binary form must reproduce the above copyright notice,
this list of conditions and the following disclaimer in the documentation
and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF
THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;

namespace ThreadMessaging
{
	public class ChannelDemultiplexer : SingleRunnable
	{
		private HybridDictionary dictionary;
		private IChannel input;

		public ChannelDemultiplexer(IChannel[] channels, int[] ids, IChannel input, bool autoStart, bool waitOnStop) : base(true,autoStart,waitOnStop)
		{
			this.input = input;

			int count = channels.Length;
			if(count != ids.Length)
				throw new ArgumentException("Channel and ID count mismatch.","ids");

			dictionary = new HybridDictionary(count,true);
			for(int i=0;i<count;i++)
				dictionary.Add(ids[i],channels[i]);
		}

		protected override void Run()
		{
			//NOTE: IChannel.Send is interrupt save and automatically dumps the argument.  
			while(running)
			{
				MessageEnvelope env = (MessageEnvelope)input.Receive();
				IChannel channel = (IChannel)dictionary[env.ID];
				channel.Send(env.Message);
			}
		}
	}
}

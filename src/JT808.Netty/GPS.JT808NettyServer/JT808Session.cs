﻿using DotNetty.Transport.Channels;
using System;

namespace GPS.JT808NettyServer
{
    public class JT808Session: IAppSession
    {
        public JT808Session(IChannel channel, string terminalPhoneNo)
        {
            Channel = channel;
            TerminalPhoneNo = terminalPhoneNo;
            StartTime = DateTime.Now;
            LastActiveTime = DateTime.Now;
            SessionID = Channel.Id.AsShortText();
        }

        /// <summary>
        /// 终端手机号
        /// </summary>
        public string TerminalPhoneNo { get; set; }

        public string SessionID { get; }

        public IChannel Channel { get;}

        public DateTime LastActiveTime { get; set; }

        public DateTime StartTime { get; }
    }
}

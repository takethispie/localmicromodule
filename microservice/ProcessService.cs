﻿using System;

namespace microservice
{
	public class ProcessService : MicroService
	{
		public override event MessageEvent sendToBus;
		MicroServiceEventArgs e;

		public ProcessService ()
		{
			StartServiceAtProgramStartup = true;
			baseBusName = "Main";
			e = new MicroServiceEventArgs();
		}

		public override void addSendToBus(MessageEvent me)
		{
			sendToBus += me;
		}

		public override void receive(object sender, MicroServiceEventArgs args)
		{
			
			string test = args.message.content;
			string[] spl = test.Split(' ');
			if(spl.Length > 1 && spl[1] == "+")
			{
				int a = int.Parse(spl[0]);
				int b = int.Parse(spl[2]);
				int c = a + b;
				e.message = new StandardMessage("DISP_COMMAND",c.ToString(),"DisplayService",3);
				sendToBus(this,e);
			}
			else
			{
				e.message = new StandardMessage("DISP_COMMAND","Cant parse expression","DisplayService",3);
				sendToBus(this,e);
			}
		}

		public override void RunService()
		{

		}
	}
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Loja.Mvc.Hubs
{
    public class LeilaoHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Participar()
        {

        }
    }
}
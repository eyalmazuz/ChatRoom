using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace ChatRoomWindow
{
    public class Logger
    {
        public static readonly log4net.ILog guiLog = log4net.LogManager.GetLogger("ChatRoomLogger1");

    }
}

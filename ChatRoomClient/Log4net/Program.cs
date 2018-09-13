using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;


[assembly: log4net.Config.XmlConfigurator(Watch=true)]

namespace Logg4net
{
   public class Program
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger("ChatRoomLogger");
       
    }
}

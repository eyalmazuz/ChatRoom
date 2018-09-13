using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomWindow.BusinessLayer
{
   public abstract class ILinqAction
    {
        public String name;
        public ILinqAction() { }

        public abstract List<DataMessage> operate(List<DataMessage> msgList);
    }
}

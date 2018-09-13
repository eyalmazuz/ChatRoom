using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Linq Action that sorts a List by Time stamp the order of the sort is decided in the constructor
namespace ChatRoomWindow.BusinessLayer.LinqActions
{
    
    
    class TimeSort:ILinqAction
    {
        private bool reverse;
        public TimeSort(bool reverse)
        {
            this.name = "Time Sort";
            this.reverse = reverse;
        }

        public override List<DataMessage> operate(List<DataMessage> msgList)
        {
            if (!reverse)
                msgList = msgList.OrderBy(m => m.dateTime).ToList();
            else
                msgList = msgList.OrderByDescending(m => m.dateTime).ToList();

            return msgList;
        }
    }
}

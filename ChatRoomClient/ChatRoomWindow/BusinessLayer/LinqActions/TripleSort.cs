using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Linq Action that sorts a List by Group id than nickname and than timeStamp the order of the sort is decided in the constructor
namespace ChatRoomWindow.BusinessLayer.LinqActions
{
    class TripleSort : ILinqAction
    {
        private bool reverse;
        
        public TripleSort(bool reverse)
        {
            this.name = "Triple Sort";
            this.reverse = reverse;
        }
        public override List<DataMessage> operate(List<DataMessage> msgList)
        {
            if (!reverse)
                msgList = msgList.OrderBy (m => m.user.G_id_int).ThenBy(m => m.user.nickName).ThenBy(m => m.dateTime).ToList();
            else
                msgList = msgList.OrderByDescending(m =>(m.user.G_id_int)).ThenByDescending(m => m.user.nickName).ThenByDescending(m => m.dateTime).ToList();

            return msgList;
        }
    }
}

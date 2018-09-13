using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Linq Action that sorts a List by Group id the order of the sort is decided in the constructor
namespace ChatRoomWindow.BusinessLayer.LinqActions
{
    class G_idSort : ILinqAction
    {
        private bool reverse;
        public G_idSort(bool reverse)
        {
            this.reverse = reverse;
        }
        public override List<DataMessage> operate(List<DataMessage> msgList)
        {
            if (!reverse)
                msgList = msgList.OrderBy(m => m.user.G_id_int).ToList();
            else
                msgList = msgList.OrderByDescending(m => m.user.G_id_int).ToList();

            return msgList;
        }
    }
}

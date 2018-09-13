using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Linq Action that sorts a List by Nickname the order of the sort is decided in the constructor
namespace ChatRoomWindow.BusinessLayer.LinqActions
{
    class NicknameSort : ILinqAction
    {
        private bool reverse;
        public NicknameSort(bool reverse)
        {
            this.name = "Nickname Sort ";
            this.reverse = reverse;
        }
        public override List<DataMessage> operate(List<DataMessage> msgList)
        {
            if (!reverse)
                msgList = msgList.OrderBy(m => m.user.nickName).ToList();
            else
                msgList = msgList.OrderByDescending(m => m.user.nickName).ToList();

            return msgList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomWindow.BusinessLayer
{
   public class DataMessage
    {   // readonly message
        //fields
        private String _messageBody;
        public String messageBody { get { return _messageBody; } }


        private User _user;
        public User user { get { return _user; } }

        private Guid GUID;
        public Guid guid { get { return GUID; } }

        private DateTime timeStamp;
        public DateTime dateTime { get { return timeStamp; } }

        private bool editable;
        public bool Editable
        {
            get { return editable; }
            set { editable = value; }
        }
        //constructors
        public DataMessage(String msgBody, Guid msgGuid, DateTime msgDateTime, User user)
        {
            this.GUID = msgGuid;
            this.timeStamp = msgDateTime;
            this._messageBody = msgBody;
            this._user = user;
            this.editable = false;
            

        }
        public DataMessage (FuncMessage msg)
        {
            this.GUID = msg.guid;
            this.timeStamp = msg.dateTime;
            this._messageBody = msg.messageBody;
            this._user = msg.user;
            this.editable = msg.Editable;
        }
    }
}

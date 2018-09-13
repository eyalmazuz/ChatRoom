using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomWindow.PersistentLayer
{
    class NicknameFilter : IQueryAction
    {
        private string nickname;

        public NicknameFilter(string nickname)
        {
            this.nickname = nickname;
        }
        public string execute(string query)
        {
            return query + " and [Users].Nickname = '" + nickname + "'";
        }
    }
}

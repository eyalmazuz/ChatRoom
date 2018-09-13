using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomWindow.PersistentLayer
{
    class GroupFilter : IQueryAction
    {
        private int Group_id;

        public GroupFilter(int Group_id)
        {
            this.Group_id = Group_id;
        }
        public string execute(string query)
        {
            return query + " and [Users].Group_Id = '" + Group_id + "'";
        }
    }
}

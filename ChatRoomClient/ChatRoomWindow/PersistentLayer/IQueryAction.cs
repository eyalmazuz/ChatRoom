using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomWindow.PersistentLayer
{
    interface IQueryAction
    {
        String execute(String query);
    }
}

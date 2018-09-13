using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomWindow.PersistentLayer
{
    class JokesHandler
    {
        private String Jokes = "PresentationLayer/Jokes/Jokes.txt";
        public JokesHandler()
        {

        }
        public List<String> readFile()
        {
            var file = File.ReadLines(Jokes).ToList();
            return file;
        }
    }
}

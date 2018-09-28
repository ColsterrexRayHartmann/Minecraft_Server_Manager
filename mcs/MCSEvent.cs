using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcs
{
    class MCSEvent : EventArgs
    {
        public string cmd;
        public int exitCode;
        public MCSEvent(string cmd, int exitCode)
        {
            this.cmd = cmd;
            this.exitCode = exitCode;
        }
    }
}

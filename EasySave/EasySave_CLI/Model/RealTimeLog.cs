using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_CLI.Model
{
    internal class RealTimeLog : IRealTimeLog
    {
        public bool State 
        {
            get { return State; }
            set { State = value; }
        }

        public void createLog()
        {
            throw new NotImplementedException();
        }

        public void updateLog()
        {
            throw new NotImplementedException();
        }
    }
}

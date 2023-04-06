using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPTRobot.Domains
{
    internal interface IChatGPTService
    {
        Task<string> Send(string message);
    }
}

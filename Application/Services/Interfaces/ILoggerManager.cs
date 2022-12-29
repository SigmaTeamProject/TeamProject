using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogDebug(string message);
        void LogWarn(string message);
        void LogError(string message);
    }
}

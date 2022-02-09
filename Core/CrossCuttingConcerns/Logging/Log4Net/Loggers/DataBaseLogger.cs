using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using log4net;

namespace Core.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    public class DataBaseLogger:LoggerService
    {
        public DataBaseLogger() : base(LogManager.GetLogger("DataBaseLogger"))
        {
        }
    }
}

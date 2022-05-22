using UnityEngine;

namespace Log
{
    public static class GLogger
    {
        static readonly Logger logger = new Logger(Debug.unityLogger.logHandler);

        private static void Log(LogType logType, string log)
        {
#if !GRBE_PRODUCTION
            logger.Log(logType, log);
#endif
        }

        public static void LogLog(string log)
        {
            Log(LogType.Log, log);
        }

        public static void LogWarn(string log)
        {
            Log(LogType.Warning, log);
        }

        public static void LogError(string log)
        {
            Log(LogType.Error, log);
        }
    }
}
using System;
using UnityEngine;

namespace Utils
{
    public class Debugger : Debug
    {
        public static void Log(LogType logType, object message)
        {
            switch (Config.DebugLevel)
            {
                case Config.DebugLevelEnum.Notice:
                    if (logType == LogType.Log)
                    {
                        Debug.unityLogger.Log(logType, message);
                    }
                    break;
                case Config.DebugLevelEnum.Warning:
                    if (logType == LogType.Log
                        || logType == LogType.Warning)
                    {
                        Debug.unityLogger.Log(logType, message);
                    }
                    break;
                case Config.DebugLevelEnum.Error:
                    if (logType == LogType.Log 
                        || logType == LogType.Warning 
                        || logType == LogType.Error)
                    {
                        Debug.unityLogger.Log(logType, message);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
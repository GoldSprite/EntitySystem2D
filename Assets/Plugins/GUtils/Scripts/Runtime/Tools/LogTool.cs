#if UNITY_2017_1_OR_NEWER
using UnityEngine;
#endif
using System;
using System.Collections.Generic;

namespace GoldSprite.GUtils {
    public class LogTool {
        public static Dictionary<int, bool> logLevels = new Dictionary<int, bool>()
        {
            {ILogLevel.ERROR, true},
            {ILogLevel.WARNING, true},
            {ILogLevel.DEBUG, true},
            {ILogLevel.INFO, true},
            {ILogLevel.MSG, true},
        };
        public static string SubMsg;
#if UNITY_2017_1_OR_NEWER
        public static Action<int, string> LogAction = (lv, log) => {
            switch (lv) {
                case ILogLevel.ERROR:
                    Debug.LogError(log);
                    break;
                case ILogLevel.WARNING:
                    Debug.LogWarning(log);
                    break;
                case ILogLevel.DEBUG:
                    Debug.LogFormat("<color=#FFFFAA>{0}</color>", log);
                    break;
                case ILogLevel.INFO:
                    Debug.LogFormat("<color=#34f79f>{0}</color>", log);
                    break;
                case ILogLevel.MSG:
                    Debug.LogFormat("<color=#BBAAAA>{0}</color>", log);
                    break;
            }
        };
#elif APPLICATION_CONSOLE || APPLICATION_CLOUD
        public static Action<int, string> LogAction = (lv, log) => Console.WriteLine(log);
#endif


        public static void NLogMsg(object msg)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            NLog(ILogLevel.MSG, msg);
        }


        public static void NLogInfo(object msg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            NLog(ILogLevel.INFO, msg);
        }


        public static void NLogDebug(object msg)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            NLog(ILogLevel.DEBUG, msg);
        }


        public static void NLogWarn(object msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            NLog(ILogLevel.WARNING, msg);
        }


        public static void NLogErr(object msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            NLog(ILogLevel.ERROR, msg);
        }


        public static void NLog(int logLevel, object msg)
        {
            if (logLevel != ILogLevel.FORCE)
                if (!logLevels.ContainsKey(logLevel) || !logLevels[logLevel]) return;
            var log = ILogLevel.msgMap[logLevel]
                    + DateTool.CurrentFormatTime()
                    + "    "  //空位
                    + SubMsg
                    + msg
                    ;
            LogAction?.Invoke(logLevel, log);
            Console.ResetColor();
        }
    }
}
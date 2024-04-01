#if UNITY_2017_1_OR_NEWER
using UnityEngine;
#endif
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using GoldSprite.GUtils;

namespace GoldSprite.UnityTools {
    public class LogTool {

        public static Dictionary<int, bool> logLevels = new Dictionary<int, bool>()
        {
            {ILogLevel.ERROR, true},
            {ILogLevel.WARNING, true},
            {ILogLevel.DEBUG, true},
            {ILogLevel.INFO, true},
            {ILogLevel.MSG, true},
        };

        public static LogToolData data = new LogToolData();
        public static bool Init;
        public static void UseLogToolData(string dataPath)
        {
            try {
                if (!File.Exists(dataPath)) throw new Exception("文件不存在.");
                var readContent = File.ReadAllText(dataPath);
                var data = JsonConvert.DeserializeObject<LogToolData>(readContent);
                data.realtimeFilterList.Clear();  //清除上次缓存.
            }
            catch (Exception e) {
                Debug.LogWarning("读取Yaml错误: \n" + e.StackTrace);
                var saveContent = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(dataPath, saveContent);
                Debug.Log($"已在[{dataPath}]创建初始文件数据.");
            }
        }
        public static void SaveLogToolData(string dataPath)
        {
            var saveContent = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(dataPath, saveContent);
            Debug.Log($"已在[{dataPath}]保存LogToolData文件数据.");
        }

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


        public static void NLogMsg(object msg) => NLogMsg("", msg);
        public static void NLogMsg(string tag, object msg)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            NLog(ILogLevel.MSG, tag, msg);
        }


        public static void NLogInfo(object msg) => NLogInfo("", msg);
        public static void NLogInfo(string tag, object msg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            NLog(ILogLevel.INFO, tag, msg);
        }


        public static void NLogDebug(object msg) => NLogDebug("", msg);
        public static void NLogDebug(string tag, object msg)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            NLog(ILogLevel.DEBUG, tag, msg);
        }


        public static void NLogWarn(object msg) => NLogWarn("", msg);
        public static void NLogWarn(string tag, object msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            NLog(ILogLevel.WARNING, tag, msg);
        }


        public static void NLogErr(object msg) => NLogErr("", msg);
        public static void NLogErr(string tag, object msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            NLog(ILogLevel.ERROR, tag, msg);
        }


        public static void NLog(int logLevel, string tag, object msg)
        {
            if (logLevel != ILogLevel.FORCE)
                if (!logLevels.ContainsKey(logLevel) || !logLevels[logLevel]) return;

            if (string.IsNullOrEmpty(tag))
                tag = StackTraceHelper.GetStackAboveClassName(typeof(LogTool));

            //过滤Log
            if (!DisplayLog(logLevel, tag)) return;

            var log = ILogLevel.msgMap[logLevel]
                    + DateTool.CurrentFormatTime()
                    + "[" + tag + "]"
                    + "    "  //空位
                    + msg
                    ;
            LogAction?.Invoke(logLevel, log);
            Console.ResetColor();
        }


        private static bool DisplayLog(int logLevel, string tag)
        {
            var exist = data.filterList.TryGetValue(tag, out LogToolFilterInfo tagInfo);
            var realExist = data.realtimeFilterList.TryGetValue(tag, out LogToolFilterInfo realTimeTagInfo);
            //低于拦截等级编号(0为Err5为Msg), 或在白名单列表, 或默认显示时 --- 显示该log
            var result = (logLevel < data.interceptLevel) || (exist && tagInfo.display) || (!exist && data.defaultDispaly);

            if (!realExist) {
                data.realtimeFilterList[tag] = exist ? tagInfo : new LogToolFilterInfo();
                data.realtimeFilterList[tag].useInfo = result ? LogToolFilterInfo.UseInfo.Used : LogToolFilterInfo.UseInfo.Intercepted;
            }
            return result;
        }
    }
}
using GoldSprite.UnityTools.MyDict;
using System;
using System.Collections.Generic;

namespace GoldSprite.UnityTools {
    [Serializable]
    public class LogToolData {
        public int interceptLevel = ILogLevel.WARNING;
        public bool defaultDispaly = false;
        public MyDict<string, LogToolFilterInfo> filterList = new();
        public MyDict<string, LogToolFilterInfo> realtimeFilterList = new();
    }
}

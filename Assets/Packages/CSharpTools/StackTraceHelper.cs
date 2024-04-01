using System;
using System.Diagnostics;

namespace GoldSprite.GUtils {
    public class StackTraceHelper {
        public static string GetStackAboveClassName(object target)
        {
            var find = false;
            var frames = new StackTrace().GetFrames();
            for (int i = 0; i < frames.Length; i++) {
                var frame = frames[i];
                if (!find) {
                    if (IsObjectByframe(frame, target)) find = true;
                } else if (!IsObjectByframe(frame, target))
                    return GetObjectNameByframe(frame);
                //if (IsObjectByframe(frame, target) && i + 1 < frames.Length)
                //    return GetObjectNameByframe(frames[i + 1]);
            }

            throw new System.Exception("找不到调用者类.");
        }
        public static bool IsObjectByframe(StackFrame frame, object target)
        {
            if (target is Type type) { } else type = target.GetType();
            var method = frame.GetMethod();
            return method.DeclaringType == type;
        }
        public static string GetObjectNameByframe(StackFrame frame)
        {
            var method = frame.GetMethod();
            return method.DeclaringType.Name;
        }
    }
}

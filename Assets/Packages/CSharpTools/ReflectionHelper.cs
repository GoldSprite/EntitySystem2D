using System.Reflection;
using System;

namespace GoldSprite.GUtils {
    public class ReflectionHelper {

        /// <summary>
        /// 路径递归查找成员实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="propertyPath"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T GetField<T>(object target, string propertyPath, BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
        {
            return (T)GetField(target, propertyPath, flags);
        }
        public static object GetField(object target, string propertyPath, BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
        {
            return GetFieldInfo(ref target, propertyPath, flags)?.GetValue(target);
        }
        public static FieldInfo GetFieldInfo(ref object target, string propertyPath, BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
        {
            FieldInfo fieldInfo = null;
            var fieldNames = propertyPath.Split('.');
            var type = target.GetType();
            for (int i = 0; i < fieldNames.Length; i++) {
                var fieldName = fieldNames[i];
                fieldInfo = type.GetField(fieldName, flags);
                if (fieldInfo == null) break;
                if(i < fieldNames.Length-1) {
                    var value = fieldInfo.GetValue(target);
                    if (value == null) break;
                    type = value.GetType();
                    target = value;
                }
                if (i == fieldNames.Length-1) return fieldInfo;
            }

            throw new Exception("找不到该路径成员信息, 权限或路径错误.");
        }
        public static FieldInfo GetFieldInfo(object target, string propertyPath, BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance) => GetFieldInfo(ref target, propertyPath, flags);


        /// <summary>
        /// 根据类型在成员中查找实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T GetField<T>(object target, BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
        {
            var fieldInfos = target.GetType().GetFields(flags);
            foreach (var fieldInfo in fieldInfos) {
                if (typeof(T).IsAssignableFrom(fieldInfo.FieldType)) {
                    return (T)fieldInfo.GetValue(target);
                }
            }
            return default(T);
        }

        public static Type GetFieldType(object target, string propertyPath, BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
        {
            return GetFieldInfo(target, propertyPath, flags)?.FieldType;
        }
    }
}
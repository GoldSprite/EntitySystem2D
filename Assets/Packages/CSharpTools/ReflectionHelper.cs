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
        public static T GetField<T>(object target, string propertyPath)
        {
            foreach (var fieldName in propertyPath.Split('.')) {
                var fieldInfo = target.GetType().GetField(fieldName);
                if (fieldInfo == null) throw new Exception("找不到该路径成员信息.");
                target = fieldInfo.GetValue(target);
            }
            return (T)target;
        }


        /// <summary>
        /// 根据类型在成员中查找实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T GetField<T>(object target)
        {
            var fieldInfos = target.GetType().GetFields();
            foreach (var fieldInfo in fieldInfos) {
                if (typeof(T).IsAssignableFrom(fieldInfo.FieldType)) {
                    return (T)fieldInfo.GetValue(target);
                }
            }
            return default(T);
        }
    }
}
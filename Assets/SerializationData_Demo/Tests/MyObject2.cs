using System.Runtime.Serialization;
using System;
using UnityEngine;

namespace GoldSprite.UnityPlugins.SerializationData {



    [Serializable]
    public class MyObject2 : ISerializable {
        //public string SampleVal = "A";
        public object Value;

        public MyObject2() { }
        public MyObject2(SerializationInfo info, StreamingContext context)
        {
            //Debug.Log("MyObject2即将反序列化.");
            ////if (Value == null) return;

            //Value = "HaHaHaH!";
            //info.AddValue("ValueType", Value.GetType());
            //info.AddValue("Value", Value);

            //var type = (Type)info.GetValue("ValueType", typeof(Type));
            //Value = info.GetValue("Value", type);
        }
        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, StreamingContext context)
        {
            //Debug.Log("----MyObject2即将序列化.");
            //info.
        }
    }
}
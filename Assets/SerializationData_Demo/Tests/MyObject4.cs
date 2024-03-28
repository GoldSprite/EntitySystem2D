using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Debug=UnityEngine.Debug;

namespace Assets.SerializationData_Demo.Tests {
    [Serializable]
    public class MyObject4 : ISerializable {
        public int n1;
        public int n2;
        public object str2 ="DDDD";

        public MyObject4()
        {
        }

        protected MyObject4(SerializationInfo info, StreamingContext context)
        {
            Debug.Log("MyObject4 触发反序列化");
            n1 = info.GetInt32("i");
            n2 = info.GetInt32("j");
            str2 = info.GetString("k");
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Debug.Log("MyObject4 ------- 触发序列化");
            info.AddValue("i", n1);
            info.AddValue("j", n2);
            info.AddValue("k", str2);
        }
    }

}

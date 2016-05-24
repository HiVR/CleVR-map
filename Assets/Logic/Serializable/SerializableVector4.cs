using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Assets.Logic.Serializable
{
    [Serializable]
    public class SerializableVector4 : ISerializable
    {
        public float w { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public SerializableVector4()
        {
        }

        public SerializableVector4(Vector4 vector4)
        {
            w = vector4.w;
            x = vector4.x;
            y = vector4.y;
            z = vector4.z;
        }

        public SerializableVector4(Quaternion quaternion)
        {
            w = quaternion.w;
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
        }

        // Serialization
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("w", this.w);
            info.AddValue("x", this.x);
            info.AddValue("y", this.y);
            info.AddValue("z", this.z);
        }

        // Deserialization
        public SerializableVector4(SerializationInfo info, StreamingContext context)
        {
            w = (float)info.GetValue("w", typeof(float));
            x = (float)info.GetValue("x", typeof(float));
            y = (float)info.GetValue("y", typeof(float));
            z = (float)info.GetValue("z", typeof(float));
        }
    }
}

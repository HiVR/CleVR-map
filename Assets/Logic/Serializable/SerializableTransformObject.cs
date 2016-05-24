using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Assets.Logic.Serializable
{
    [Serializable]
    public class SerializableTransformObject : ISerializable
    {
        int id { get; set; }
        int type { get; set; }
        bool isStatic { get; set; }

        SerializableVector3 position { get; set; }
        SerializableVector3 scale { get; set; }
        SerializableVector4 rotation { get; set; }

        public SerializableTransformObject(int id, int type, bool isStatic, Transform transform)
        {
            this.id = id;
            this.type = type;
            this.isStatic = isStatic;

            this.position = new SerializableVector3(transform.position);
            this.scale = new SerializableVector3(transform.lossyScale);
            this.rotation = new SerializableVector4(transform.rotation);
        }

        // Serialization
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new System.ArgumentNullException("info");

            info.AddValue("id", id);
            info.AddValue("type", type);
            info.AddValue("isStatic", isStatic);
            info.AddValue("position", position);
            info.AddValue("scale", scale);
            info.AddValue("rotation", rotation);
        }

        // Deserialization
        public SerializableTransformObject(SerializationInfo info, StreamingContext context)
        {
            id = (int)info.GetValue("id", typeof(int));
            type = (int)info.GetValue("type", typeof(int));
            isStatic = (bool)info.GetValue("isStatic", typeof(bool));
            position = (SerializableVector3)info.GetValue("position", typeof(SerializableVector3));
            scale = (SerializableVector3)info.GetValue("scale", typeof(SerializableVector3));
            rotation = (SerializableVector4)info.GetValue("rotation", typeof(SerializableVector4));
        }
    }
}

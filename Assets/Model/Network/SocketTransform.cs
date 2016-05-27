// <copyright file="SocketTransform.cs" company="HiVR">
// Copyright (c) 2016 HiVR All Rights Reserved
// </copyright>
namespace Assets.Model.Network
{
    using SerializedObjects;
    using System.Net.Sockets;

    class SocketTransform
    {
        #region Fields

        /// <summary>
        /// Value: serializableTransformObject
        /// </summary>
        public SerializableTransformObject serializableTransformObject { get; set; }
        
        /// <summary>
        /// Value: socket
        /// </summary>
        public Socket socket { get; set; }

        #endregion Fields

        #region Methods

        /// <summary>
        /// Empty constructor
        /// </summary>
        public SocketTransform()
        {
        }

        /// <summary>
        /// Constructor to initialize values.
        /// </summary>
        public SocketTransform(SerializableTransformObject serializableTransformObject, Socket socket)
        {
            this.serializableTransformObject = serializableTransformObject;
            this.socket = socket;
        }

        #endregion Methods
    }
}

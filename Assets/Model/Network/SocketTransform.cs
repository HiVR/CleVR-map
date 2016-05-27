// <copyright file="SocketTransform.cs" company="HiVR">
// Copyright (c) 2016 HiVR All Rights Reserved
// </copyright>
namespace Assets.Model.Network
{
    using SerializedObjects;
    using System.Net.Sockets;

    /// <summary>
    /// This class is responsible linking the serializableTransformObject with the Socket.
    /// </summary>
    internal class SocketTransform
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SocketTransform class.
        /// Empty constructor.
        /// </summary>
        public SocketTransform()
        {
        }

        /// <summary>
        /// Initializes a new instance of the SocketTransform class.
        /// Constructor with initial values.
        /// </summary>
        /// <param name="serializableTransformObject">contains the SerializableTransformObject</param>
        /// <param name="socket">contains the Socket</param>
        public SocketTransform(SerializableTransformObject serializableTransformObject, Socket socket)
        {
            this.SerializableTransformObject = serializableTransformObject;
            this.Socket = socket;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the SerializableTransformObject.
        /// </summary>
        public SerializableTransformObject SerializableTransformObject { get; set; }

        /// <summary>
        /// Gets or sets the Socket.
        /// </summary>
        public Socket Socket { get; set; }

        #endregion Properties
    }
}
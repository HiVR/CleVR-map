// <copyright file="Server.cs" company="HiVR">
// Copyright (c) 2016 HiVR All Rights Reserved
// </copyright>
namespace Assets.Model.Network
{
    using SerializedObjects;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    using UnityEngine;

    /// <summary>
    /// The Server class, handles connection and data transfer with the client.
    /// </summary>
    public class Server
    {
        #region Fields

        /// <summary>
        /// Port to listen on.
        /// </summary>
        private const int PORT = 25565;

        /// <summary>
        /// Event notifier class, used to determine thread activity.
        /// </summary>
        private ManualResetEvent allDone = new ManualResetEvent(false);

        /// <summary>
        /// The Socket object on which we listen.
        /// </summary>
        private Socket listener;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Start the server! Configure the socket to start listening on a specific address/port.
        /// </summary>
        public void StartServer()
        {
            // Instantiate new socket object.
            this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Bind socket to specified address/port.
            this.listener.Bind(new IPEndPoint(IPAddress.Loopback, PORT));

            // Set listen backlog to 100 pending connection.
            this.listener.Listen(100);
        }

        /// <summary>
        /// Start the server! Configure the socket to start listening on a specific address/port.
        /// </summary>
        /// <param name="serializableTransformObject">the Unity object that gets send to the client</param>
        public void SendData(SerializableTransformObject serializableTransformObject)
        {
            // Create a combined transformObject/socket object.
            SocketTransform socketTransform = new SocketTransform(serializableTransformObject, this.listener);

            Debug.Log("Server: Initiating data transfer.");

            // Reset event notifier.
            this.allDone.Reset();

            // Begin accepting connections, if a connection has been accepted call "Accept".
            this.listener.BeginAccept(this.Accept, socketTransform);

            // Wait until the packet is sent.
            this.allDone.WaitOne();
        }

        /// <summary>
        /// Starts when the connection was accepted by the remote hosts and prepares to send data.
        /// </summary>
        /// <param name="result">contains the Connection to the client</param>
        public void Accept(IAsyncResult result)
        {
            Debug.Log("Connection accepted!!");

            // Retrieve the SerializableTransformObject from the ASync result.
            SerializableTransformObject serializableTransform = ((SocketTransform)result.AsyncState).SerializableTransformObject;

            // Retrieve the Socket form the ASync result.
            serializableTransform.Socket = ((SocketTransform)result.AsyncState).Socket.EndAccept(result);

            // Create a buffer containing the serialized transform object.
            byte[] buffer = serializableTransform.Serialize(); // Fills the buffer with data.

            // Start sending data from the buffer. Call "Send" once finished.
            serializableTransform.Socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, this.Send, serializableTransform);
        }

        /// <summary>
        /// Ends sending the data.
        /// </summary>
        /// <param name="result">contains the Connection to the client</param>
        public void Send(IAsyncResult result)
        {
            // Retrieve the SerializableTransformObject from the ASync result.
            SerializableTransformObject serializableTransform = (SerializableTransformObject)result.AsyncState;

            // Fetch the amount of bytes sent.
            int size = serializableTransform.Socket.EndSend(result);

            Debug.Log("Server: Send data: " + size + " bytes.");

            // Signal thread that message has been sent!
            this.allDone.Set();
        }

        /// <summary>
        /// Getter for the listener socket.
        /// </summary>
        /// <returns>the listener socket</returns>
        public Socket GetListener()
        {
            return this.listener;
        }

        #endregion Methods
    }
}
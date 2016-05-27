// <copyright file="Server.cs" company="HiVR">
// Copyright (c) 2016 HiVR All Rights Reserved
// </copyright>
namespace Assets.Model.Network
{
    using SerializedObjects;
    using System.Net.Sockets;
    using System.Threading;
    using System;
    using System.Net;
    using UnityEngine;

    /// <summary>
    /// The Server class, handles connection and data transfer with the client.
    /// </summary>
    public class Server
    {
        #region Fields

        /// <summary>
        /// Event notifier class, used to determine thread activity.
        /// </summary>
        ManualResetEvent allDone = new ManualResetEvent(false);

        /// <summary>
        /// Port to listen on.
        /// </summary>
        const int port = 25565;

        /// <summary>
        /// The Socket object on which we listen.
        /// </summary>
        Socket listener;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Start the server! Configure the socket to start listening on a specific address/port.
        /// </summary>
        public void StartServer()
        {
            // Instantiate new socket object.
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Bind socket to specified address/port.
            listener.Bind(new IPEndPoint(IPAddress.Loopback, port));

            // Set listen backlog to 100 pending connection.
            listener.Listen(100);
        }

        /// <summary>
        /// Start the server! Configure the socket to start listening on a specific address/port.
        /// </summary>
        public void sendData(SerializableTransformObject serializableTransformObject)
        {
            // Create a combined transformObject/socket object.
            SocketTransform socketTransform = new SocketTransform(serializableTransformObject, listener);

            Debug.Log("Server: Initiating data transfer.");

            // Reset event notifier.
            allDone.Reset();

            // Begin accepting connections, if a connection has been accepted call "Accept".
            listener.BeginAccept(Accept, socketTransform);

            // Wait until the packet is sent.
            allDone.WaitOne();
        }

        /// <summary>
        /// Starts when the connection was accepted by the remote hosts and prepares to send data.
        /// </summary>        
        public void Accept(IAsyncResult result)
        {
            Debug.Log("Connection accepted!!");

            // Retrieve the SerializableTransformObject from the ASync result.
            SerializableTransformObject serializableTransform = ((SocketTransform)result.AsyncState).serializableTransformObject;

            // Retrieve the Socket form the ASync result.
            serializableTransform.Socket = ((SocketTransform)result.AsyncState).socket.EndAccept(result);

            // Create a buffer containing the serialized transform object.
            byte[] buffer = serializableTransform.Serialize(); //fills the buffer with data

            // Start sending data from the buffer. Call "Send" once finished.
            serializableTransform.Socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, Send, serializableTransform);

        }

        /// <summary>
        /// Ends sending the data
        /// </summary>        
        public void Send(IAsyncResult result)
        {
            // Retrieve the SerializableTransformObject from the ASync result.
            SerializableTransformObject serializableTransform = (SerializableTransformObject)result.AsyncState;

            // Fetch the amount of bytes sent
            int size = serializableTransform.Socket.EndSend(result);

            Debug.Log("Server: Send data: " + size + " bytes.");
       
            // Signal thread that message has been sent!       
            allDone.Set();
        }

        /// <summary>
        /// Getter for the listener socket.
        /// </summary>        
        public Socket getListener()
        {
            return listener;
        }

        #endregion Methods
    }
}
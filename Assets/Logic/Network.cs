// <copyright file="Network.cs" company="HiVR">
// Copyright (c) 2016 HiVR All Rights Reserved
// </copyright>
namespace Assets.Logic
{
    using System.Net;
    using System.Net.Sockets;
    using UnityEngine;

    /// <summary>
    /// This class contains the network logic.
    /// </summary>
    public class Network : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// Contains the byte size used in the buffer for messages.
        /// </summary>
        private const int BYTESIZE = 1024 * 1024;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Start TCPListener server on a certain port.
        /// </summary>
        /// <param name="port">the port that the server listens on</param>
        /// <returns>The active server</returns>
        public static TcpListener StartServer(int port)
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Loopback, port); // Address + port.
            TcpListener server = new TcpListener(endpoint); // Instantiate the object.
            server.Start(); // Start listening...
            return server;
        }

        /// <summary>
        /// This method checks if there is a client trying to make a connection and accepts it.
        /// </summary>
        /// <param name="server">server that has a new connection</param>
        /// <returns>The client that just got accepted</returns>
        public static TcpClient AcceptNewConnection(TcpListener server)
        {
            TcpClient newClient = server.AcceptTcpClient();
            Debug.Log("Client connected from " + newClient.Client.LocalEndPoint.ToString());
            return newClient;
        }

        /// <summary>
        /// This method checks if there is a message from the client and reads it.
        /// </summary>
        /// <param name="client">the client that will get checked</param>
        /// <returns>The message that has just been received</returns>
        public static string ReadMessage(TcpClient client)
        {
            byte[] buffer = new byte[BYTESIZE];
            client.GetStream().Read(buffer, 0, BYTESIZE);
            string message = System.Text.Encoding.ASCII.GetString(buffer);
            Debug.Log(message);
            return message;
        }

        /// <summary>
        /// This method sends a specified message to a specified client.
        /// </summary>
        /// <param name="client">client that will receive the message</param>
        /// <param name="buffer">the message the client will receive</param>
        public static void SendMessage(TcpClient client, byte[] buffer)
        {
            client.GetStream().Write(buffer, 0, buffer.Length);
        }

        #endregion Methods
    }
}
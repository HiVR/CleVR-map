// <copyright file="NetworkManager.cs" company="HiVR">
// Copyright (c) 2016 HiVR All Rights Reserved
// </copyright>
namespace Assets.Logic
{
    using Serializable;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;

    /// <summary>
    /// This class is responsible for communicating with our client-map.
    /// </summary>
    public class NetworkManager : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// Some things will only get checked once in this many frames.
        /// Not a constant because editable in Unity.
        /// </summary>
        [SerializeField]
        private int frameCheck = 30;

        /// <summary>
        /// Port that the server will listen to.
        /// Not a constant because editable in Unity.
        /// </summary>
        [SerializeField]
        private int port = 25565;

        /// <summary>
        /// Contains the active server object.
        /// </summary>
        private TcpListener server;

        /// <summary>
        /// Contains every client that has been connected.
        /// </summary>
        private List<TcpClient> clientList;

        /// <summary>
        /// Counts the physics-frames because some things don't need to happen every frame.
        /// </summary>
        private int frameCounter = 0;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Initialize the NetworkManger on Unity startup by starting the server.
        /// </summary>
        private void Start()
        {
            Application.runInBackground = true; // Unity will continue running in the background.

            this.clientList = new List<TcpClient>();
            this.server = Network.StartServer(this.port);
        }

        /// <summary>
        /// This method will be called every physics frame and contains calls to various methods.
        /// </summary>
        private void FixedUpdate()
        {
            this.frameCounter++;

            // Called every fixed frame.
            if (this.server.Pending())
            {
                TcpClient newClient = Network.AcceptNewConnection(this.server);

                this.clientList.Add(newClient);
                this.SendStatic(newClient);
            }

            // Called every this.frameCheck frames.
            if (this.frameCounter % this.frameCheck == 0)
            {
                this.frameCounter = 0;

                foreach (TcpClient client in this.clientList)
                {
                    this.SendDynamic(client);
                }
            }
        }

        /// <summary>
        /// This method will sent all the static objects to a client,
        /// </summary>
        /// <param name="client">the client that will receive all static objects</param>
        private void SendStatic(TcpClient client)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            List<MonoBehaviour> staticObjects = ObjectTracker.GetStaticObjects();

            foreach (MonoBehaviour staticObject in staticObjects)
            {
                formatter.Serialize(client.GetStream(), new SerializableTransformObject(staticObject.GetInstanceID(), 0, true, staticObject.transform));
            }
        }

        /// <summary>
        /// This method will sent the dynamic objects to a client.
        /// </summary>
        /// <param name="client">the client that will receive the dynamic objects</param>
        private void SendDynamic(TcpClient client)
        {
        }

        #endregion Methods
    }
}
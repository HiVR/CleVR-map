// <copyright file="NetworkManager.cs" company="HiVR">
// Copyright (c) 2016 HiVR All Rights Reserved
// </copyright>
namespace Assets.Model.Network
{
    using System.Threading;
    using System.Net.Sockets;
    using SerializedObjects;
    using Map;
    using UnityEngine;

    /// <summary>
    /// This class is responsible for starting and interacting with the Server.
    /// </summary>
    public class NetworkManager : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// This server object.
        /// </summary>
        Server server = new Server();

        /// <summary>
        /// Thread in which the Server lives.
        /// </summary>
        Thread oThread;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Initialize the NetworkManger on Unity startup by starting the server.
        /// </summary>
        private void Start()
        {
            Application.runInBackground = true; // Unity will continue running in the background.

            Debug.Log("Starting Server!");
            oThread = new Thread(new ThreadStart(server.StartServer));
            
            // Start the thread
            oThread.Start();

            // Wait for the thread to become alive.
            while (!oThread.IsAlive);

            // Wait for socket to become available.
            while (server.getListener() == null)
            {
                Thread.Sleep(100);
            }

            // Sent static objects at start.
            // -> This will hangs unity until the packets have all been sent! Look into this.
            SendStaticObjects();
        }

        /// <summary>
        /// Stop Server and provide hook for cleanup.
        /// </summary>
        void OnDisable()
        {
            // Close the socket of the server.
            server.getListener().Close();

            // Stop Server thread.
            oThread.Abort();

            // Wait for thread to finish.
            oThread.Join();
        }

        /// <summary>
        /// This function is called by Unity at fixed intervals.
        /// </summary>
        void FixedUpdate()
        {
            // Sent dynamic updated objects.
            // TODO
        }

        /// <summary>
        /// Send all Static Objects.
        /// </summary>
        void SendStaticObjects()
        {
            foreach(MonoBehaviour monoBehaviour in ObjectTracker.GetStaticObjects())
            {
                SerializableVector3 position = new SerializableVector3(monoBehaviour.transform.position.x, monoBehaviour.transform.position.y, monoBehaviour.transform.position.z);
                SerializableVector3 scale = new SerializableVector3(monoBehaviour.transform.lossyScale.x, monoBehaviour.transform.lossyScale.y, monoBehaviour.transform.lossyScale.z);
                SerializableVector4 rotation = new SerializableVector4(monoBehaviour.transform.rotation.w, monoBehaviour.transform.rotation.x, monoBehaviour.transform.rotation.y, monoBehaviour.transform.rotation.z);

                SerializableTransformObject serializableTransformObject = new SerializableTransformObject(monoBehaviour.GetInstanceID(), 1, true, position, scale, rotation);

                server.sendData(serializableTransformObject);
            }
        }

        #endregion Methods
    }
}
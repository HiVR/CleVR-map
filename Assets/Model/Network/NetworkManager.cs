// <copyright file="NetworkManager.cs" company="HiVR">
// Copyright (c) 2016 HiVR All Rights Reserved
// </copyright>
namespace Assets.Model.Network
{
    using Map;
    using SerializedObjects;
    using System;
    using System.Threading;
    using UnityEngine;
    using ViewModel;

    /// <summary>
    /// This class is responsible for starting and interacting with the Server.
    /// </summary>
    public class NetworkManager : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// This server object.
        /// </summary>
        private Server server = new Server();

        /// <summary>
        /// Thread in which the Server lives.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// The number of fixed updates after which dynamic items are sent.
        /// </summary>
        private const int numberOfFramesDynamicObjectsUpdate = 50;

        /// <summary>
        /// The counted number of fixed updates after the last dynamic objects have been sent.
        /// </summary>
        private int numberOfFramesDynamicObjectsUpdateCounter = 0;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Initialize the NetworkManger on Unity startup by starting the server.
        /// </summary>
        private void Start()
        {
            Application.runInBackground = true; // Unity will continue running in the background.

            Debug.Log("Starting Server!");
            this.thread = new Thread(new ThreadStart(this.server.StartServer));

            // Start the thread.
            this.thread.Start();

            // Wait for the thread to become alive.
            while (!this.thread.IsAlive)
            {
            }

            // Wait for socket to become available.
            while (this.server.GetListener() == null)
            {
                Thread.Sleep(100);
            }

            // Sent static objects at start.
            // -> This will hangs unity until the packets have all been sent!
            // TODO: Look into this.
            this.SendStaticObjects();
        }

        /// <summary>
        /// Stop Server and provide hook for cleanup.
        /// </summary>
        private void OnDisable()
        {
            // Stop the server
            this.server.StopServer();

            // Stop Server thread.
            this.thread.Abort();

            // Wait for thread to finish.
            this.thread.Join();
        }

        /// <summary>
        /// This function is called by Unity at fixed intervals.
        /// </summary>
        private void FixedUpdate()
        {
            if (this.server.IsConnectionEstablished() && numberOfFramesDynamicObjectsUpdate < this.numberOfFramesDynamicObjectsUpdateCounter)
            {
                this.numberOfFramesDynamicObjectsUpdateCounter = 0;

                SendDynamicObjects();
            }

            // Increase counter
            this.numberOfFramesDynamicObjectsUpdateCounter++;
            
        }

        /// <summary>
        /// Send all Static Objects.
        /// </summary>
        private void SendStaticObjects()
        {
            foreach (MonoBehaviour monoBehaviour in ObjectTracker.GetStaticObjects())
            {
                // Only send instances of ViewModels.
                if (monoBehaviour.GetType().Name.EndsWith("ViewModel"))
                {
                    String type = monoBehaviour.GetType().Name.Replace("ViewModel", "");

                    SerializableVector3 position = new SerializableVector3(monoBehaviour.transform.position.x, monoBehaviour.transform.position.y, monoBehaviour.transform.position.z);
                    SerializableVector3 scale = new SerializableVector3(monoBehaviour.transform.lossyScale.x, monoBehaviour.transform.lossyScale.y, monoBehaviour.transform.lossyScale.z);
                    SerializableVector4 rotation = new SerializableVector4(monoBehaviour.transform.rotation.w, monoBehaviour.transform.rotation.x, monoBehaviour.transform.rotation.y, monoBehaviour.transform.rotation.z);

                    SerializableTransformObject serializableTransformObject = new SerializableTransformObject(monoBehaviour.GetInstanceID(), type, true, position, scale, rotation);

                    this.server.SendData(serializableTransformObject);
                }
            }
        }

        /// <summary>
        /// Send all Dynamic Objects.
        /// </summary>
        private void SendDynamicObjects()
        {
            foreach (MonoBehaviour monoBehaviour in ObjectTracker.GetDynamicObjects())
            {
                // Only send instances of ViewModels.
                if (monoBehaviour.GetType().Name.EndsWith("ViewModel"))
                {
                    String type = monoBehaviour.GetType().Name.Replace("ViewModel", "");

                    SerializableVector3 position = new SerializableVector3(monoBehaviour.transform.position.x, monoBehaviour.transform.position.y, monoBehaviour.transform.position.z);
                    SerializableVector3 scale = new SerializableVector3(monoBehaviour.transform.lossyScale.x, monoBehaviour.transform.lossyScale.y, monoBehaviour.transform.lossyScale.z);
                    SerializableVector4 rotation = new SerializableVector4(monoBehaviour.transform.rotation.w, monoBehaviour.transform.rotation.x, monoBehaviour.transform.rotation.y, monoBehaviour.transform.rotation.z);

                    SerializableTransformObject serializableTransformObject = new SerializableTransformObject(monoBehaviour.GetInstanceID(), type, false, position, scale, rotation);

                    this.server.SendData(serializableTransformObject);
                }
            }
        }

        #endregion Methods
    }
}
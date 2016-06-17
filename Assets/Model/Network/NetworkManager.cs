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
        /// The number of fixed updates after which dynamic items are sent.
        /// </summary>
        private const int NumberOfFramesDynamicObjectsUpdate = 5;

        /// <summary>
        /// This server object.
        /// </summary>
        private Server server = new Server();

        /// <summary>
        /// Thread in which the Server lives.
        /// </summary>
        private Thread thread;

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

            Debug.Log("[Server] Starting Server...");

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
            if (this.server.IsConnectionEstablished() && (NumberOfFramesDynamicObjectsUpdate < this.numberOfFramesDynamicObjectsUpdateCounter) && this.server.IsQueueEmpty())
            {
                this.numberOfFramesDynamicObjectsUpdateCounter = 0;

                this.SendDynamicObjects();
            }

            // Increase counter
            this.numberOfFramesDynamicObjectsUpdateCounter++;
        }

        /// <summary>
        /// Send all Static Objects.
        /// </summary>
        private void SendStaticObjects()
        {
            // Send the Ground object.
            GameObject ground = GameObject.Find("Ground");
            this.SendSerializableTransformObject(this.CreateSerializableTransformObject(
                ground.GetInstanceID(),
                ground.transform,
                true,
                SerializableType.Ground,
                null));

            foreach (MonoBehaviour monoBehaviour in ObjectTracker.GetStaticObjects())
            {
                this.SendSerializableTransformObject(this.CreateSerializableTransformObject(monoBehaviour, true));
            }
        }

        /// <summary>
        /// Send all Dynamic Objects.
        /// </summary>
        private void SendDynamicObjects()
        {
            foreach (MonoBehaviour monoBehaviour in ObjectTracker.GetDynamicObjects())
            {
                this.SendSerializableTransformObject(this.CreateSerializableTransformObject(monoBehaviour, false));
            }

            // Send the Patient
            GameObject user = GameObject.Find("User");
            this.SendSerializableTransformObject(this.CreateSerializableTransformObject(
                user.GetInstanceID(),
                user.transform,
                false,
                SerializableType.Character,
                new SerializableCharacter(
                    true,
                    "User")));
        }

        /// <summary>
        /// Send a SerializableTransformObject.
        /// </summary>
        /// <param name="serializableTransformObject">The SerializableTransformObject to send.</param>
        private void SendSerializableTransformObject(SerializableTransformObject serializableTransformObject)
        {
            if (serializableTransformObject != null)
            {
                this.server.SendData(serializableTransformObject);
            }
        }

        /// <summary>
        /// Create a SerializableTransformObject from the given MonoBehaviour.
        /// </summary>
        /// <param name="monoBehaviour">The monoBehaviour from which to create a SerializableTransformObject.</param>
        /// <param name="isStatic">Is the object static?</param>
        /// <returns>The SerializableTransformObject which was created from the MonoBehaviour</returns>
        private SerializableTransformObject CreateSerializableTransformObject(MonoBehaviour monoBehaviour, bool isStatic)
        {
            SerializableType type = this.GetTypeOfViewModel(monoBehaviour);

            // Don't send if the Type is unknown.
            if (type == SerializableType.Unknown)
            {
                return null;
            }

            SerializableCharacter character = null;

            // If it is of type character, parse the Character data.
            if (type == SerializableType.Character)
            {
                // Fetch the character data.
                Character characterModel = this.FetchCharacterModelFromMonoBehaviour(monoBehaviour);

                // Create the SerializableCharacter object.
                character = new SerializableCharacter(
                    false,
                    characterModel.Name);
            }

            SerializableVector3 position = new SerializableVector3(
                monoBehaviour.transform.position.x,
                monoBehaviour.transform.position.y,
                monoBehaviour.transform.position.z);

            SerializableVector3 scale = new SerializableVector3(
                monoBehaviour.transform.lossyScale.x,
                monoBehaviour.transform.lossyScale.y,
                monoBehaviour.transform.lossyScale.z);

            SerializableVector3 rotation = new SerializableVector3(
                monoBehaviour.transform.eulerAngles.x,
                monoBehaviour.transform.eulerAngles.y,
                monoBehaviour.transform.eulerAngles.z);

            return new SerializableTransformObject(
                monoBehaviour.GetInstanceID(),
                type,
                isStatic,
                position,
                scale,
                rotation,
                character);
        }

        /// <summary>
        /// Create a SerializableTransformObject from the given Transform and values.
        /// </summary>
        /// <param name="id">The monoBehaviour from which to create a SerializableTransformObject.</param>
        /// <param name="transform">Transform object to use.</param>
        /// <param name="isStatic">Is the object static?</param>
        /// <param name="type">Type of the object.</param>
        /// <returns>The SerializableTransformObject which was created from the Transform and values.</returns>
        private SerializableTransformObject CreateSerializableTransformObject(int id, Transform transform, bool isStatic, SerializableType type, SerializableCharacter character)
        {
            SerializableVector3 position = new SerializableVector3(
                transform.position.x,
                transform.position.y,
                transform.position.z);

            SerializableVector3 scale = new SerializableVector3(
                transform.lossyScale.x,
                transform.lossyScale.y,
                transform.lossyScale.z);

            SerializableVector3 rotation = new SerializableVector3(
                transform.eulerAngles.x,
                transform.eulerAngles.y,
                transform.eulerAngles.z);

            return new SerializableTransformObject(
                id,
                type,
                isStatic,
                position,
                scale,
                rotation,
                character);
        }

        /// <summary>
        /// Get the SerializableType of a MonoBehaviour. If the MonoBehaviour SerializableType is Unknown, return SerializableType.Unknown.
        /// </summary>
        /// <param name="monoBehaviour">The monoBehaviour from which to get the SerializableType.</param>
        /// <returns>The SerializableType of the MonoBehaviour</returns>
        private SerializableType GetTypeOfViewModel(MonoBehaviour monoBehaviour)
        {
            // Return value, Unknown by default.
            SerializableType type = SerializableType.Unknown;
            try
            {
                // If the SerializableType is recognized, set the return value.
                type = this.StringToSerializableType(monoBehaviour.GetType().Name.Replace("ViewModel", string.Empty));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            return type;
        }

        /// <summary>
        /// Get the name of a Character. If the monoBehaviour is not of type CharacterViewModel, return null.
        /// </summary>
        /// <param name="monoBehaviour">The monoBehaviour from which to get the Character model.</param>
        /// <returns>The Character model of the monoBehaviour if it is of type CharacterViewModel, otherwise null.</returns>
        private Character FetchCharacterModelFromMonoBehaviour(MonoBehaviour monoBehaviour)
        {
            // Check if the monoBehaviour is of type CharacterViewModel.
            if (monoBehaviour.GetType() == typeof(CharacterViewModel))
            {
                // Cast the monoBehaviour to a CharacterViewModel.
                CharacterViewModel model = (CharacterViewModel)monoBehaviour;

                // Get the name from the Observed model.
                return model.Observed;
            }

            return null;
        }

        /// <summary>
        /// Transform String to object of enum SerializableType
        /// </summary>
        /// <param name="typeName">Name of the ModelView</param>
        /// <returns>The SerializableType which represents the ModelView</returns>
        private SerializableType StringToSerializableType(string typeName)
        {
            Console.WriteLine(typeName);
            switch (typeName)
            {
                case "Bench":
                    return SerializableType.Bench;

                case "Car":
                    return SerializableType.Car;

                case "Building":
                    return SerializableType.Building;

                case "TreeGarden":
                    return SerializableType.Garden;

                case "TV":
                    return SerializableType.Television;

                case "Character":
                    return SerializableType.Character;

                default:
                    throw new ArgumentException("This object is not yet supported: " + typeName);
            }
        }

        #endregion Methods
    }
}
// <copyright file="NetworkManager.cs" company="HiVR">
// Copyright (c) 2016 HiVR All Rights Reserved
// </copyright>
using System.Collections.Generic;
using System.Net.Sockets;
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
    /// This method will be called every frame and will accept new connections and sent the static map.
    /// </summary>
    private void Update()
    {
        if (this.server.Pending())
        {
            TcpClient newClient = Network.AcceptNewConnection(this.server);
            this.clientList.Add(newClient);
            this.SentStatic(newClient);
        }
    }

    /// <summary>
    /// This method will be called every physics frame and contains calls to various methods.
    /// </summary>
    private void FixedUpdate()
    {
        this.frameCounter++;
        foreach (TcpClient client in this.clientList)
        {
            if (client.GetStream().DataAvailable)
            {
                Network.ReadMessage(client);
            }

            this.SentDynamic(client);

            // TODO Implement pings to check if connection is alive.
        }
    }

    /// <summary>
    /// This method will sent all the static objects to a client.
    /// </summary>
    /// <param name="client">the client that will receive all static objects</param>
    private void SentStatic(TcpClient client)
    {
        byte[] messageBytes = System.Text.Encoding.ASCII.GetBytes("Welcome, you are connected!");
        Network.SendMessage(client, messageBytes);
    }

    /// <summary>
    /// This method will sent the dynamic objects to a client.
    /// </summary>
    /// <param name="client">the client that will receive the dynamic objects</param>
    private void SentDynamic(TcpClient client)
    {
        if (this.frameCounter % this.frameCheck == 0)
        {
            this.frameCounter = 0;
        }
    }

    #endregion Methods
}
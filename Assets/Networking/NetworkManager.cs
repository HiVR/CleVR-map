// <copyright file="NetworkManager.cs" company="HiVR">
// Copyright (c) 2016 HiVR All Rights Reserved
// </copyright>
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

/// <summary>
/// This class is responsible for communicating with our client-map
/// </summary>
public class NetworkManager : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// Contains the byte size used in the buffer for messages
    /// </summary>
    private const int BYTESIZE = 1024 * 1024;

    /// <summary>
    /// Some things will only get checked once in this many frames
    /// Editable in Unity
    /// </summary>
    [SerializeField]
    private int frameCheck = 30;

    /// <summary>
    /// Port that the server will listen to
    /// Editable in Unity
    /// </summary>
    [SerializeField]
    private int port = 25565;

    /// <summary>
    /// Contains the active server object
    /// </summary>
    private TcpListener server;

    /// <summary>
    /// Contains every client that has been connected
    /// </summary>
    private List<TcpClient> clientList;

    /// <summary>
    /// Counts the frames because some things don't need to happen every frame
    /// </summary>
    private int frameCounter = 0;

    #endregion Fields

    #region Methods

    /// <summary>
    /// This method is responsible for receiving a buffer and converting it to a ASCII message
    /// </summary>
    /// <param name="bytes">Buffer with the client message</param>
    /// <returns>String that contains ASCII message</returns>
    private static string CleanMessage(byte[] bytes)
    {
        string message = System.Text.Encoding.ASCII.GetString(bytes);

        string res = string.Empty;
        foreach (char nullChar in message)
        {
            if (nullChar != '\0')
            {
                res += nullChar;
            }
        }

        return res;
    }

    /// <summary>
    /// Initialize the NetworkManger on Unity startup by starting the server
    /// </summary>
    private void Start()
    {
        Application.runInBackground = true; // Unity will continue running in the background

        this.clientList = new List<TcpClient>();

        IPEndPoint endpoint = new IPEndPoint(IPAddress.Loopback, this.port); // Address + port
        this.server = new TcpListener(endpoint); // Instantiate the object
        this.server.Start(); // Start listening...
    }

    /// <summary>
    /// This method will be called every frame and contains calls to various methods
    /// </summary>
    private void Update()
    {
        this.frameCounter++;
        this.AcceptNewConnection();

        foreach (TcpClient client in this.clientList)
        {
            this.ReadMessage(client);
            this.SentDynamic(client);

            // TODO Implement pings to check if connection is alive
        }
    }

    /// <summary>
    /// This method checks if there is a client trying to make a connection and accepts it
    /// </summary>
    private void AcceptNewConnection()
    {
        if (this.server.Pending())
        {
            TcpClient newClient = this.server.AcceptTcpClient();
            this.clientList.Add(newClient);
            Debug.Log("Client connected from " + newClient.Client.LocalEndPoint.ToString());
            this.SentStatic(newClient);
        }
    }

    /// <summary>
    /// This method checks if there is a message from the client and reads it
    /// </summary>
    /// <param name="client">The client that will get checked</param>
    private void ReadMessage(TcpClient client)
    {
        if (client.GetStream().DataAvailable)
        {
            byte[] buffer = new byte[BYTESIZE];
            client.GetStream().Read(buffer, 0, BYTESIZE);
            string message = CleanMessage(buffer);
            Debug.Log(message);
        }
    }

    /// <summary>
    /// This method will sent all the static objects to a client
    /// </summary>
    /// <param name="client">The client that will receive all static objects</param>
    private void SentStatic(TcpClient client)
    {
        byte[] messageBytes = System.Text.Encoding.Unicode.GetBytes("Welcome, you are connected!");
        client.GetStream().Write(messageBytes, 0, messageBytes.Length); // Send the stream
    }

    /// <summary>
    /// This method will sent the dynamic objects to a client
    /// </summary>
    /// <param name="client">The client that will receive the dynamic objects</param>
    private void SentDynamic(TcpClient client)
    {
        if (this.frameCounter % this.frameCheck == 0)
        {
            this.frameCounter = 0;
        }
    }

    #endregion Methods
}
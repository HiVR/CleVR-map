using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    #region Fields

    private const int bytesize = 1024 * 1024;
    private TcpListener listener;
    private List<TcpClient> sender;
    private string message;

    #endregion Fields

    #region Methods

    private static string cleanMessage(byte[] bytes)
    {
        string message = System.Text.Encoding.ASCII.GetString(bytes);

        string messageToPrint = null;
        foreach (var nullChar in message)
        {
            if (nullChar != '\0')
            {
                messageToPrint += nullChar;
            }
        }
        return messageToPrint;
    }

    // Use this for initialization
    private void Start()
    {
        Application.runInBackground = true; // Unity will continue running in the background

        sender = new List<TcpClient>();

        IPEndPoint endpoint = new IPEndPoint(IPAddress.Loopback, 8080); // Address + port
        listener = new TcpListener(endpoint); // Instantiate the object
        listener.Start(); // Start listening...
    }

    // Update is called once per frame
    private void Update()
    {
        if (listener.Pending())
        {
            TcpClient newClient = listener.AcceptTcpClient();
            sender.Add(newClient);
            Debug.Log("Client connected from " + newClient.Client.LocalEndPoint.ToString());
        }

        foreach (TcpClient client in sender)
        {
            if (client.GetStream().DataAvailable)
            {
                byte[] buffer = new byte[bytesize];
                client.GetStream().Read(buffer, 0, bytesize);
                message = cleanMessage(buffer);
                Debug.Log(message);
            }

            // TODO Implement pings to check if connection is alive
        }
    }

    #endregion Methods
}
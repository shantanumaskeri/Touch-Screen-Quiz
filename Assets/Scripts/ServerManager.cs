using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class ServerManager : MonoBehaviour
{

	public GameObject transition;

	#region private members 	
	/// <summary> 	
	/// TCPListener to listen for incomming TCP connection 	
	/// requests. 	
	/// </summary> 	
	private TcpListener tcpListener;
	/// <summary> 
	/// Background thread for TcpServer workload. 	
	/// </summary> 	
	private Thread tcpListenerThread;
	/// <summary> 	
	/// Create handle to connected tcp client.
	/// </summary> 	
	private TcpClient connectedTcpClient;
	#endregion

    private string lastMessage = "";
	private readonly List<TcpClient> listConnectedClients = new List<TcpClient>(new TcpClient[0]);

    private void Start()
    {
		tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequests));
		tcpListenerThread.IsBackground = true;
		tcpListenerThread.Start();
	}

    public void WriteMessageToSend(string message)
    {
		lastMessage = message;
		
		SendMessage_to_all(lastMessage);
	}

	private void ListenForIncommingRequests()
	{
		tcpListener = new TcpListener(IPAddress.Any, 8052);
		tcpListener.Start();

		ThreadPool.QueueUserWorkItem(this.ListenerWorker, null);
	}

	private void ListenerWorker(object token)
	{
		while (tcpListener != null)
		{
			print("Its here");
			connectedTcpClient = tcpListener.AcceptTcpClient();
			listConnectedClients.Add(connectedTcpClient);
			// Thread thread = new Thread(HandleClientWorker);
			// thread.Start(connectedTcpClient);
			ThreadPool.QueueUserWorkItem(this.HandleClientWorker, connectedTcpClient);
		}
	}

	private void HandleClientWorker(object token)
	{
		Byte[] bytes = new Byte[1024];

		using (var client = token as TcpClient)
		using (var stream = client.GetStream())
		{
			Debug.Log("New Client connected");
			// openCamera();
			transition.SetActive(true);
			int length;
			// Read incomming stream into byte arrary.                        
			while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
			{
				var incommingData = new byte[length];
				Array.Copy(bytes, 0, incommingData, 0, length);
				// Convert byte array to string message.                            
				string clientMessage = Encoding.ASCII.GetString(incommingData);
				lastMessage = clientMessage;
				Debug.Log("Client msg " + clientMessage);
				// msg = clientMessage;
			}
			if (connectedTcpClient == null)
			{
				return;
			}
		}
		//  ThreadPool.QueueUserWorkItem(this.SendMessage, connectedTcpClient);
	}

	/// <summary> 	
	/// Send message to client using socket connection. 	
	/// </summary> 	
	private void SendMessage_to_one(object token, string msg)
	{
		if (connectedTcpClient == null)
		{
			Debug.Log("Problem connectedTCPClient null");
			return;
		}

		var client = token as TcpClient;
		{
			try
			{
				NetworkStream stream = client.GetStream();
				if (stream.CanWrite)
				{
					// Get a stream object for writing.      

					// Convert string message to byte array.                
					byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(msg);
					// Write byte array to socketConnection stream.              
					stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
					Debug.Log("Server sent his message - should be received by client");
				}
			}
			catch (SocketException socketException)
			{
				Debug.Log("Socket exception: " + socketException);
				return;
			}
		}
	}

	private void SendMessage_to_all(string msg)
	{
		foreach (TcpClient this_client in listConnectedClients)
		{
			try
			{
				if (this_client.Connected)
				{

					NetworkStream stream = this_client.GetStream();

					if (stream.CanWrite)
					{
						// Get a stream object for writing.      

						// Convert string message to byte array.                
						byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(msg);
						// Write byte array to socketConnection stream.              
						stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
						UnityEngine.Debug.Log("Server sent his message - should be received by client");
					}
				}
			}
			catch (SocketException socketException)
			{
				UnityEngine.Debug.Log("Socket exception: " + socketException);
				return;
			}
		}
	}

}

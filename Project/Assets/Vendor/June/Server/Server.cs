/* See the copyright information at https://github.com/srfoster/June/blob/master/COPYRIGHT */
using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

using UnityEngine;
using System.Collections;


public class Server
{
    private static Server currInstance;
	private TcpListener tcpListener;
    private Thread listenThread;
	public String last_message = "";
	private CallResponseQueue queue;
	public bool applicationRunning = true;
	
    public Server(CallResponseQueue queue)
    {
		//Debug.Log ("In constructor for Server, setting applicationRunning to true");
		
		//Debug.Log ("Killing any existing Servers...currInstance = " + currInstance);
		if (currInstance != null)
		{
			currInstance.tcpListener.Stop(); // Kill the old listener so it doesn't interfere with new spells
		}
		currInstance = this;
		applicationRunning = true;
	  this.queue = queue;
		
		if (this.tcpListener != null)
		{
			//Debug.Log ("Killing accept socket");
			this.tcpListener.Stop();
		}
      this.tcpListener = new TcpListener(IPAddress.Loopback, 3000);
      this.listenThread = new Thread(new ThreadStart(ListenForClients));
	  this.listenThread.IsBackground = true;
      this.listenThread.Start();
    }
	
	
	private void ListenForClients()
    {
		//Debug.Log ("Starting TCP Listener");
      this.tcpListener.Start();

	  while (true)
	  {
		if(!this.tcpListener.Pending())
		{
			Thread.Sleep(1000);	
		}else{
	
		    //blocks until a client has connected to the server
		    TcpClient client = this.tcpListener.AcceptTcpClient();
		
		    //create a thread to handle communication 
		    //with connected client
		    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
			clientThread.IsBackground = true;
		    clientThread.Start(client);
		}
	  }
	}
	
	
	private void HandleClientComm(object client)
	{
	  //Debug.Log ("Called HandleClientComm, applicationRunning is: " + applicationRunning.ToString());
	  FileLogger.Log("New TCP Client accepted.");	
	  TcpClient tcpClient = (TcpClient)client;
	  NetworkStream clientStream = tcpClient.GetStream();
	  byte[] message = new byte[4096];
	  int bytesRead;
	
	  while (applicationRunning)
	  {

	    	bytesRead = 0;
			String message_string = "";
			try
			{
				do {
					FileLogger.Log("About to block, waiting for Java."); 
					//blocks until a client sends a message
					bytesRead = clientStream.Read(message, 0, 4096);
					
				    if(bytesRead == 0){
					  throw new Exception("");
				    }
					
					
					
					FileLogger.Log("Unblocked.  Got bytes from Java.");
					ASCIIEncoding encoder = new ASCIIEncoding();
					message_string += encoder.GetString(message, 0, bytesRead);
				} while (!message_string.EndsWith ("\n"));
			}
	    catch(Exception e)
	    {
		  FileLogger.Log("A socket error occured or there was not data while waiting for Java: " + e.Message);

	      //a socket error has occured
	      break;
	    }
			

	
	    /*if (bytesRead == 0)
	    {
		  FileLogger.Log("The Java client disconnected");

	      //the client has disconnected from the server
	      break;
	    }*/
	
	    //message has successfully been received
	    //ASCIIEncoding encoder = new ASCIIEncoding();
		//String message_string = encoder.GetString(message, 0, bytesRead);
		//} //ends while loop

			
		FileLogger.Log("Unity got response from Java: "+message_string);
			
		CallResponse call_response = new CallResponse(message_string, clientStream);
		queue.add(call_response);
			
	  }
	
	  FileLogger.Log("Closing TCP connection.");
	  //Debug.Log ("Closing TCP connection, applicationRunning is: " + applicationRunning.ToString());
	  tcpClient.Close();
	}
	
}



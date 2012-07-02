/* See the copyright information at https://github.com/srfoster/June/blob/master/COPYRIGHT */
using System.Collections;
using System.Net.Sockets;
using System.Text;



public class CallResponseQueue {
	private ArrayList queue = new ArrayList();
	
	public void add(CallResponse call)
	{
		queue.Add(call);
	}
	
	public CallResponse remove()
	{
		
		CallResponse call = queue[0] as CallResponse;
		
		queue.RemoveAt(0);
		
		return call;
	}
	
	public bool notEmpty()
	{
		return queue.Count > 0;	
	}
}

public class CallResponse
{
	private string call = null;
	private string response = null;
	private NetworkStream client_stream = null;
	
	public CallResponse(string call, NetworkStream client_stream)
	{
		this.call = call;	
		this.client_stream = client_stream;
	}
	
	public string getCall()
	{
		return call;	
	}
	
	public void setResponse(string response)
	{
		this.response = response;	
	}
	
	public string getResponse()
	{
		return this.response;	
	}
	
	public void respond()
	{
		if(client_stream == null)
			return;
		
	    ASCIIEncoding encoder = new ASCIIEncoding();
		byte[] buffer = encoder.GetBytes(response + "\n");

		client_stream.Write(buffer, 0 , buffer.Length);
		client_stream.Flush();	
	}

}

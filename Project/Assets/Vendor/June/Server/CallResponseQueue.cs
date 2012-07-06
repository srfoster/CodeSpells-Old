/* See the copyright information at https://github.com/srfoster/June/blob/master/COPYRIGHT */
using System.Collections;
using System.Net.Sockets;
using System.Text;



public class CallResponseQueue {
	private ArrayList queue = new ArrayList();
	
	public int crLength() 
	{
		return queue.Count;
	}
	
	public void add(CallResponse call)
	{
		call.setParent(this);
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
	private CallResponseQueue parent = null;
	
	public CallResponse(string call, NetworkStream client_stream)
	{
		this.call = call;	
		this.client_stream = client_stream;
	}
	
	public void setParent(CallResponseQueue crq) 
	{
		parent = crq;
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
		
		byte[] buffer = Encoding.ASCII.GetBytes(response + "\n");
		
		FileLogger.Log("Unity about to block, sending to Java: "+response + "\n");
		client_stream.Write(buffer, 0 , buffer.Length);
		client_stream.Flush();	
		
		FileLogger.Log("Unity finished writing to Java.");
	}

}

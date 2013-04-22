using UnityEngine;
using System.Collections;
using System.IO;

public class FileLogger {
	public static StreamWriter file = File.AppendText("./CodeSpellsUnity.log");

	public static void Log(string message)		
	{
		
		file.WriteLine("Unity: " + message);
		file.Flush();
	}

}

public class TraceLogger {
	public static StreamWriter file = File.AppendText("./CodeSpellsTrace.log");

	public static void Log(string message)		
	{
		
		file.WriteLine("trace: "+message.Trim());
		file.Flush();
	}
	
	public static void LogKV(string key, string message)
	{
	    file.WriteLine(key+": "+message.Trim());
		file.Flush();
	}

}


public class ProgramLogger {
	public static StreamWriter file = File.AppendText("./CodeSpellsProgram.log");
	
	public static void LogKV(string key, string message)
	{
	    file.WriteLine(key+": "+message.Trim());
		file.Flush();
	}
	
	public static void LogSS(string key, string message)
	{
	    file.WriteLine("begin: "+key+", "+Time.time);
	    file.WriteLine(message.Trim());
	    file.WriteLine("end: "+key);
	    file.Flush();
	}

}

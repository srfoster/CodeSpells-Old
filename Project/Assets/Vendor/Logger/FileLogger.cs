using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Xml;

public class FileLogger {
	public static StreamWriter file = File.AppendText("./CodeSpellsUnity.log");

	public static void Log(string message)		
	{
		
		file.WriteLine("Unity: " + message);
		file.Flush();
	}

}


public class BadgeLogger {
	public static StreamWriter file = File.AppendText("./CodeSpellsBadges.log");

	public static void Log(string message)		
	{
		file.WriteLine(message);
		file.Flush();
	}
}


public class TraceLogger {
	public static StreamWriter file = File.AppendText("./CodeSpellsTrace.log");

	public static void LogTrace(string message)		
	{
		
		file.WriteLine("trace: "+ProgramLogger.EncodeTo64(message.Trim()));
		file.Flush();
	}
	
	public static void LogKV(string key, string message)
	{
	    file.WriteLine(key+": "+message.Trim());
		file.Flush();
	}
	
	public static void LogKVtime(string key, string message)
	{
	    file.WriteLine(key+": "+message.Trim()+", "+Time.time);
		file.Flush();
	}
	
	public static void LogTrace(string spell, string effects)
	{
	    file.WriteLine("trace: "+spell+", "+ProgramLogger.EncodeTo64(effects));
	    file.Flush();
	}

    public static void LogError(string errors) {
	    file.WriteLine("error: "+ProgramLogger.EncodeTo64(errors)+", "+Time.time);
	    file.Flush();
	}

}


public class ProgramLogger {
    public static StreamWriter file = File.AppendText("./CodeSpellsProgram.log");

    static public string EncodeTo64(string toEncode)
    {
        byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
        string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
        return returnValue;
    }
	
	public static void LogKV(string key, string message)
	{
	    file.WriteLine(key+": "+message.Trim());
		file.Flush();
	}
	
	public static void LogKVtime(string key, string message)
	{
	    file.WriteLine(key+": "+message.Trim()+", "+Time.time);
		file.Flush();
	}
	
	public static void LogEdit(string etype, int start, int stop, string text) {
	    file.WriteLine(etype+": "+start+", "+stop+", "+EncodeTo64(text)+", "+Time.time);
	    file.Flush();
	}
	
	public static void LogError(string errors) {
	    file.WriteLine("error: "+EncodeTo64(errors)); //+", "+Time.time);
	    file.Flush();
	}
	
	public static void LogCode(string name, string message)
	{
	    file.WriteLine("code: "+name+", "+Time.time+", "+EncodeTo64(message));
	    file.Flush();
	}

}

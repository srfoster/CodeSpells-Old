using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Xml;
//using System.Web.Script.Serialization;

public class FileLogger {
	public static StreamWriter file = File.AppendText("./CodeSpellsUnity.log");

	public static void Log(string message)		
	{
		
		file.WriteLine("Unity: " + message);
		file.Flush();
	}

}

public class LogData {
    public CodeEdit edit;
    public CodeShot snapshot;
    
    public LogData() { }

    public struct CodeEdit {
        public int start;
        public int stop;
        public string text;
        public float time;
    }
    public struct CodeShot {
        public string spell;
        public string code;
        public float time;
    }
}

//Stuff for JSON for logging
// public class CodeEdit {
//     int start;
//     int stop;
//     string text;
//     float time;
//     
//     public CodeEdit(int start, int stop, string text) {
//         this.start = start;
//         this.stop = stop;
//         this.text = text;
//         this.time = Time.time;
//     }
// }
// 
// public class CodeShot {
//     string spell;
//     string code;
//     float time;
//     
//     public CodeShot(string spell, string code) {
//         this.spell = spell;
//         this.code = code;
//         this.time = Time.time;
//     }
// }


public class TraceLogger {
	//public static StreamWriter file = (StreamWriter) TextWriter.Synchronized(File.AppendText("./CodeSpellsTrace.log"));
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

}


public class ProgramLogger {
    public static StreamWriter file = File.AppendText("./CodeSpellsProgram.log");
// 	public static StreamWriter xmlfile = File.AppendText("./CodeSpellsProgram.xml");
// 	public static XmlWriter xml = XmlWriter.Create(xmlfile);

    static public string EncodeTo64(string toEncode)
    {
      byte[] toEncodeAsBytes
            = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
      string returnValue
            = System.Convert.ToBase64String(toEncodeAsBytes);
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
	
// 	public static void LogSS(string key, string message)
// 	{
// 	    file.WriteLine("begin: "+key+", "+Time.time);
// 	    file.WriteLine(message.Trim());
// 	    file.WriteLine("end: "+key);
// 	    file.Flush();
// 	}
	
	public static void LogEdit(string etype, int start, int stop, string text) {
	    file.WriteLine(etype+": "+start+", "+stop+", \""+EncodeTo64(text)+"\", "+Time.time);
	    file.Flush();
	}
	
	public static void LogError(string errors) {
	    file.WriteLine("error: \""+EncodeTo64(errors)+"\", "+Time.time);
	    file.Flush();
	}
	
	public static void LogCode(string name, string message)
	{
	    //LogSS("code, "+name, message);
	    file.WriteLine("code: "+name+", "+Time.time+", \""+EncodeTo64(message)+"\"");
	    file.Flush();
	    //Log(json.Serialize(new CodeShot(name, message)));
// 	    xml.WriteStartElement("codeshot");
// 	    xml.WriteAttributeString("spell", name);
// 	    xml.WriteStartElement("code");
// 	    xml.WriteString(message);
// 	    xml.WriteEndElement(); //code
// 	    xml.WriteEndElement(); //codeshot
// 	    xml.Flush();
	}

}

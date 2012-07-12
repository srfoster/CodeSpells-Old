using UnityEngine;
using System.Collections;
using System.IO;

public class FileLogger {
	public static StreamWriter file = File.AppendText("./CodeSpells.log");

	
	public static void Log(string message)		
	{
		file.WriteLine("Unity: " + message);
		file.Flush();
	}

}

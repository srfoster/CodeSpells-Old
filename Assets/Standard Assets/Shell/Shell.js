/* See the copyright information at https://github.com/srfoster/UShell/blob/master/COPYRIGHT */
import System.Diagnostics;

public class Shell
{
	static function shellp(filename : String, arguments : String) : Process  {
	    var p = new Process();
	    p.StartInfo.Arguments = arguments;
	    p.StartInfo.CreateNoWindow = true;
	    p.StartInfo.UseShellExecute = false;
	    p.StartInfo.RedirectStandardOutput = true;
	    p.StartInfo.RedirectStandardInput = true;
	    p.StartInfo.RedirectStandardError = true;
	    p.StartInfo.FileName = filename;
	    p.Start();
	    return p;
	}
	
	static function shell( filename : String, arguments : String) : String {
		UnityEngine.Debug.Log("Running: " + filename + " " + arguments);
		
	    var p = shellp(filename, arguments);
	
	    var output = p.StandardOutput.ReadToEnd();
	    
	    var error = p.StandardError.ReadToEnd();
	    
	    p.WaitForExit();
	    
	
	    return output + error;
	}
}
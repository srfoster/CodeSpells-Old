/* See the copyright information at https://github.com/srfoster/June/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;

public class JuneConfig : MonoBehaviour {
	public static string java_files_path = ""; 
	public static string june_files_path = ""; 

	void Awake(){
		if(Application.isEditor)
		{
			java_files_path = Application.dataPath + "/Vendor/CodeSpells/CodeSpellsJava";	
			june_files_path = Application.dataPath + "/Vendor/June/Java/bin";
		} else {
			//After deployment, the the directories /Vendor/June/MyJune/ and /Vendor/June/ are 
			//  moved to the root directory of the unity executable.
			//  This is done by the Editor/PostprocessBuildPlayer script
			java_files_path = Application.dataPath + "/../../CodeSpellsJava";	
			june_files_path = Application.dataPath + "/../../June/Java/bin";
		}
	}
}
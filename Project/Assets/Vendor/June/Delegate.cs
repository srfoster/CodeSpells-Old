/* See the copyright information at https://github.com/srfoster/June/blob/master/COPYRIGHT */
using UnityEngine;

public class Delegate : MonoBehaviour
{
	public string java_file_name;
	
	void Start () {
		June june = new June(this.gameObject, java_file_name);
		june.Start();
	}
}
/* See the copyright information at https://github.com/srfoster/UShell/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour {

	void Start () {
		Shell.shell("touch", Application.dataPath+"/ushell_was_here");
	}

}

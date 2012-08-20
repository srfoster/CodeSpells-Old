using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Tester : MonoBehaviour {

	void Start () {
		string input = "3: class MySpel is public, should be declared in a file named MySpel.java sadfhjkdasfjhadfsfdsa";
		//string pattern = @"^Information regarding (.+) sent orangejava";
		string pattern = @"^(.+) class (.+) is public, should be declared in a file named (.+)";
		Debug.Log ("-------> "+Regex.IsMatch(input, pattern));
		
	}
	
	void Update () {
		
		
	}
}

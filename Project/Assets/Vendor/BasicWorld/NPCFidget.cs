

/*
 * This sort of feels like something that should be in its own package, not in BasicWorld....
 * If it gets too complicated, move it.
 */

using UnityEngine;
using System.Collections;

public class NPCFidget: MonoBehaviour {
	
	public string[] fidgets;	

	void Awake() {
		animation.wrapMode = WrapMode.Loop;
	}
	
	void Start(){
		StartCoroutine(fidget());
		animation.Play("idle");
	}
	
	void Update(){
	}
	
	IEnumerator fidget(){
		while(true)
		{
			yield return new WaitForSeconds(3);
			string random_fidget = fidgets[Random.Range(0,fidgets.Length)];
			animation.CrossFade(random_fidget,1);
			yield return new WaitForSeconds(animation[random_fidget].length);
			animation.CrossFade("idle",1);
		}
	
	}

}

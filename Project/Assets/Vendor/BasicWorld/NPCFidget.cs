

/*
 * This sort of feels like something that should be in its own package, not in BasicWorld....
 * If it gets too complicated, move it.
 */

using UnityEngine;
using System.Collections;

public class NPCFidget: MonoBehaviour {
	
	public string[] fidgets;	
	private bool walking = false;

	void Awake() {
		animation.wrapMode = WrapMode.Loop;
	}
	
	void Start(){
		animation.Play("idle");
	}
	
	void Update(){
		if(!walking)
		{
			StartCoroutine(fidget());
		}
	}
	
	public void StartWalking()
	{
		if(walking) return;
		
		animation["walk"].speed = 2;
		
		animation.CrossFade("walk");
		walking = true;
	}
	
	public void StopWalking()
	{
		if(!walking) return;
		animation.CrossFade("idle",1);

		walking = false;
	}
	
	IEnumerator fidget(){
		yield return new WaitForSeconds(3);
		string random_fidget = fidgets[Random.Range(0,fidgets.Length)];
		animation.CrossFade(random_fidget,1);
		yield return new WaitForSeconds(animation[random_fidget].length);
		animation.CrossFade("idle",1);
	}
}

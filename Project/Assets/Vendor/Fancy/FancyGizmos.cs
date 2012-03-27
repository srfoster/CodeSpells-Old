using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public class FancyGizmos : MonoBehaviour {

	void OnDrawGizmos()
	{
		Fancy.gizmoFor(gameObject);
	}

	
	void OnDrawGizmosSelected() {
		Fancy.setSelected(gameObject);
    }
	
	
}

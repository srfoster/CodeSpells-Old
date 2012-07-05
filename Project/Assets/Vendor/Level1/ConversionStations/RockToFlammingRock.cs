using UnityEngine;
using System.Collections;

public class RockToFlammingRock : MonoBehaviour{

	public void OnTriggerStay(Collider rock)
	{
		Debug.Log("Something came into the firepit!");
		if(rock.gameObject.GetComponent("Substance") != null)
		{
			Debug.Log("Got a substance");
			if((rock.gameObject.GetComponent("Substance") as Substance).isRock())
			{
				Debug.Log("Got a rock");
				if(rock.gameObject.GetComponent("Flamable") != null)
				{
					Debug.Log("Got a flamable rock");
					if(!(rock.gameObject.GetComponent("Flamable") as Flamable).isIgnited())
					{
						Debug.Log("Got an un-ignited flamable rock");
						(rock.gameObject.GetComponent("Flamable") as Flamable).Ignite();
						Debug.Log("Ignited the rock");
					}
				}
			}
		}
	}
}
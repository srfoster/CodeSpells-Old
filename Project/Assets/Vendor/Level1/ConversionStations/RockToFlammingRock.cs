using UnityEngine;
using System.Collections;

public class RockToFlammingRock : MonoBehaviour{

	public void OnTriggerStay(Collider rock)
	{
		if(rock.gameObject.GetComponent("Substance") != null)
		{
			if((rock.gameObject.GetComponent("Substance") as Substance).isRock())
			{
				if(rock.gameObject.GetComponent("Flamable") != null)
				{
					if(!(rock.gameObject.GetComponent("Flamable") as Flamable).isIgnited())
					{
						(rock.gameObject.GetComponent("Flamable") as Flamable).Ignite();
					}
				}
			}
		}
	}
}
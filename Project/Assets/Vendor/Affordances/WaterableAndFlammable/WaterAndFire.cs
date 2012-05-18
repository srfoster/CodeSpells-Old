using UnityEngine;
using System.Collections;

public abstract class WaterFireAndSticky : MonoBehaviour {
	
	// Method that handles water going onto an object that is already on fire: Puts the fire out
	public abstract void waterOnFire();
	
	// Method that handles fire going on an object that is waterlogged: Depends
	public abstract void fireOnWaterlogged();
	
	// Method that handles water going on an object that is sticky: Makes it unsticky
	public abstract void waterOnSticky();	

	// Method that handles fire going on an object that is sticky: Makes it unsticky
	public abstract void fireOnSticky();	
	
	
	// METHODS THAT WE DONT NEED BUT HELPED ME THINK ABOUT COMPLETENESS
	// Method that handles water going on an object that is flammable: NOTHING
	//public abstract void waterOnFlammable();
	// Method that handles fire going on an object that is waterable: NOTHING
	//public abstract void fireOnWaterable();
	// Method that handles water going on an object that is stickable: NOTHING
	//public abstract void waterOnStickable();
	// Method that handles stickiness going on an object that is waterlogged: NOTHING
	//public abstract void stickyOnWater();
	// Method that handles stickiness going on an object that is waterable: NOTHING
	//public abstract void stickyOnWaterable();
	// Method that handles fire going on an object that is stickable: NOTHING
	//public abstract void fireOnStickable();
	// Method that handles stickiness going on an object that is on fire: NOTHING
	//public abstract void stickyOnFire();
	// Method that handles stickiness going on an object that is flammable: NOTHING
	//public abstract void stickyOnFlammable();
}
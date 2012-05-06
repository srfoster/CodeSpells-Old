// Copyright Carsten B. Larsen 2007.
// You may copy, use, and modify this script as you see fit
//
// In order to use this script, use a World Particle Collider,
// and set the Collision Energy Loss to be a _negative_ number
// which is numerically greater the burstEnergy below, which should
// be a positive number. 
// burstEnergy should be greater than the maximum energy a particle 
// could get from start, i.e. greater than particleEmitter.maxEnergy
//
// When the particle collides with something, its energy will become
// a large positive number, greater that burstEnergy, and the script
// will kill the particle and replace it with "explosionObject"

var burstEnergy : float = 10.0;
var explosionObject : Transform;

function LateUpdate () {
	var theParticles = particleEmitter.particles;
	var liveParticles = new int[theParticles.length];
	var particlesToKeep = 0;
	for (var i = 0; i < particleEmitter.particleCount; i++ )
	{
		if (theParticles[i].energy > burstEnergy)
		{
	    	theParticles[i].color = Color.yellow;
    		// We have so much energy, we must go boom
	    	if (explosionObject)
		    	Transform.Instantiate(explosionObject, 
		    		theParticles[i].position,  
		    		Quaternion.identity );
		
		} else {
			liveParticles[particlesToKeep++] = i;
		}
	}
	// Copy the ones we keep to a new array
	var keepParticles = new Particle[particlesToKeep];
	for (var j = 0; j < particlesToKeep; j++)
		keepParticles[j] = theParticles[liveParticles[j]];
	// And write changes back
	particleEmitter.particles = keepParticles;
}	
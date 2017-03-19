using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour 
{
	public float cycleInterval = 0.007f;					//	increase to make force greater

	public GameObject node;

	private List<Particle> particles;
	private List<MovingParticle> movingParticles;
	// Use this for initialization
	void Start () 
	{
		node = GameObject.FindGameObjectWithTag("Player");
		particles = new List<Particle>(FindObjectsOfType<Particle>());
		movingParticles = new List<MovingParticle>(FindObjectsOfType<MovingParticle>());

		foreach(MovingParticle particle in movingParticles)
		{
			StartCoroutine(Cycle(particle));
		}
	}

	public IEnumerator Cycle(MovingParticle particle)
	{
		bool isFirst = true;
		while(true)
		{
			if (isFirst)
			{
				isFirst = false;
				yield return new WaitForSeconds(Random.value * cycleInterval);
			}
			ApplyMagneticForce(particle);
			yield return new WaitForSeconds(cycleInterval);
		}
	}

	private void ApplyMagneticForce(MovingParticle particle)
	{
		Vector3 newFocrce = Vector3.zero;

		foreach(Particle thisParticle in particles)
		{
			if (thisParticle == particle)
			{
				continue;
			}

			float distance = Vector3.Distance(particle.transform.position, thisParticle.gameObject.transform.position);
			float force = 1000 * particle.charge *  thisParticle.charge / Mathf.Pow(distance, 2);

			Vector3 direction = particle.transform.position - thisParticle.transform.position;
			direction.Normalize();

			newFocrce += force * direction * cycleInterval;

			//	If force is undefined (divide by zero) the newForce is zero
			if (float.IsNaN(newFocrce.x))
			{
				newFocrce = Vector3.zero;
			}

			particle.myRigidbody.AddForce(newFocrce);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

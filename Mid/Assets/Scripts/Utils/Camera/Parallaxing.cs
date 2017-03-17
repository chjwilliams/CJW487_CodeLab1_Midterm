using UnityEngine;
using System.Collections;

namespace ChrsUtils
{
	namespace ChrsCamera
	{
		public class Parallaxing : MonoBehaviour 
		{

			private float[] m_ParallaxScales; 	//	Porportion of th camera's movement to move background by
			private Transform m_cam;			//	Reference to main cmaera's trnasform
			private Vector3 m_previousCamPos;	//	Stores camera position in previus frame

			public Transform[] backgrounds; 	//	List of all the back and foregrounds to be parallaxed
			public float smoothing = 1f;		//	How smooth the parallax is going to be. Set it above 0.

			// Caled before Start()
			void Awake	()
			{
				m_cam = Camera.main.transform;
			}

			// Use this for initialization
			void Start () 
			{
				// stores previous position
				m_previousCamPos = m_cam.position;
				// assigning corresponding parallax scales
				m_ParallaxScales = new float[backgrounds.Length];

				for (int i = 0; i < backgrounds.Length; i++)
				{
					m_ParallaxScales [i] = backgrounds [i].position.z * -1;
				}
			}
			
			// Update is called once per frame
			void LateUpdate () 
			{
				for (int i = 0; i < backgrounds.Length; i++) 
				{
					Vector3 parallax = (m_previousCamPos - m_cam.position) * (m_ParallaxScales [i] / smoothing);

					// set a target x positon which is the current position + parallax
					//float backgroundTargetPosX = backgrounds[i].position.x + parallax;

					// create a target position which is background's current position with target's x position
					//Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

					// fade between current position and target position using lerp
					backgrounds[i].position = new Vector3(backgrounds[i].position.x + parallax.x, backgrounds[i].position.y + parallax.y, backgrounds[i].position.z);
				}	

				// set previous cam position to the cmaera's position at the end of the frame
				m_previousCamPos = m_cam.position;
			}
		}
	}
}
using UnityEngine;
using System.Collections;

namespace ChrsUtils
{
	namespace ChrsCamera
	{
		[RequireComponent(typeof(MeshRenderer))]
		public class Tiling : MonoBehaviour {

			private float m_SpriteWidth = 0f;		// Width of element
			private Camera m_Camera;
			private Transform m_Transform;

			public bool hasARightTile = false;		// Used to instantiate right
			public bool hasALeftTile = false;		// Used to instaniate left
			public bool reverseScale = false;		// Used if object is not tilable
			public int offsetX = 2;					// Offset so I don't any weird errors

			void Awake () 
			{
				m_Camera = Camera.main;
				m_Transform = transform;
			}

			// Use this for initialization
			void Start () 
			{
				MeshRenderer renderer = GetComponent<MeshRenderer> ();

				m_SpriteWidth = renderer.bounds.extents.x;
			}
			
			// Update is called once per frame
			void Update () 
			{
				// Does the sprite need a buddy. If not do nothing.
				if (!hasALeftTile || !hasARightTile) 
				{
					float cameraHorizontalLimit = m_Camera.orthographicSize * Screen.width / Screen.height;

					// Calculate X position where camera can see the edge of the sprite (element)
					float edgeVisiblePositionRight = (m_Transform.position.x + m_SpriteWidth / 2) - cameraHorizontalLimit;
					float edgeVisiblePositionLeft = (m_Transform.position.x - m_SpriteWidth / 2) + cameraHorizontalLimit;

					// Checking is edge of element is visible. Create new tile if edge is visible
					if (m_Camera.transform.position.x >= edgeVisiblePositionRight - offsetX && !hasARightTile) 
					{
						MakeNewTile (1);
						hasARightTile = true;
					} 
					else if (m_Camera.transform.position.x <= edgeVisiblePositionLeft + offsetX && !hasALeftTile) 
					{
						MakeNewTile (-1);
						hasALeftTile = true;
					}
				}
			}

			// FUNC. Creates new tile on side required
			void MakeNewTile (int rightOfLeft)
			{
				// Calculating new position for new tile
				Vector3 newPosition = new Vector3 (m_Transform.position.x + m_SpriteWidth * rightOfLeft, m_Transform.position.y, m_Transform.position.z);
				//Instaniate new tile and storing it in newTile
				Transform newTile = (Transform)Instantiate (m_Transform, newPosition, m_Transform.rotation);

				// if not tilable, reverses X size of element to get rid of ugly seams
				if (reverseScale)
				{
					newTile.localScale = new Vector3 (newTile.localScale.x * -1, newTile.localScale.y, newTile.localScale.z);
				}

				// Parents new tile to older tile
				newTile.parent = m_Transform.parent;


				if (rightOfLeft > 0)
					newTile.GetComponent<Tiling> ().hasALeftTile = true;
				else
					newTile.GetComponent<Tiling> ().hasARightTile = true;
			}
		}
	}
}
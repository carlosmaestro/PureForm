using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
		public float zMin, zMax, xMin, xMax;
}

public class PlayerController : MonoBehaviour
{		
		public float speed;
		public float tilt;
		public Boundary boundary;
		public Transform shotSpawn;
		public GameObject shot;
		public float fireRate = 0.5F;
		private float nextFire = 0.0F;
		
		
		void Update ()
		{
				if (Input.GetButton ("Fire1") && Time.time > nextFire) {
						nextFire = Time.time + fireRate;
						//GameObject clone =
						Instantiate (shot, shotSpawn.position, shotSpawn.rotation);// as GameObject;
						audio.Play ();
				}
				
		}


		void FixedUpdate ()
		{
				float moveHorizontal = Input.GetAxis ("Horizontal");
				float moveVetical = Input.GetAxis ("Vertical");

				Vector3 moviment = new Vector3 (moveHorizontal, 0.0f, moveVetical);

				rigidbody.velocity = moviment * speed;

				rigidbody.position = new Vector3
				(
					Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax),
					0.0f,
					Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
				);
				
				//rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
		}
}
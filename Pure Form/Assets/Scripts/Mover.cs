using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
		public float speed;

		void Start ()
		{
				rigidbody.velocity = transform.forward * speed;
				
		}
		
//		void Update ()
//		{
//				if (rigidbody.position.z >= 19) {
//						Destroy (gameObject);
//				}
//		}

}

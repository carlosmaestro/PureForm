using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{		
		Image image;

		void Start ()
		{
				image = GetComponent<Image> ();
		}	

		public void changeToScene (string level)
		{
				
				Application.LoadLevel (level);
		}

		public void ChangeColor ()
		{
				image.color = Color.blue;
		}

}

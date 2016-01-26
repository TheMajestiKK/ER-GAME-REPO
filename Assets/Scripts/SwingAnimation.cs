using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animation))]
public class SwingAnimation : MonoBehaviour {

	//THIS WAS A TEST SCRIPT TO SEE IF WE COULD GET AN ANIMATION TO PLAY CORRECTLY

	void Start()
	{
		gameObject.GetComponent<Animation>().Play();
		//Debug.Log("Got Component and Play");
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Start();
			//Debug.Log("Pressed Left Click");
		}
	}
}

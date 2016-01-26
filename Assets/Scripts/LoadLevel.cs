using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	public string level;

	public void LevelLoad(string level)
	{
		SceneManager.LoadScene(level);		
	}
}


//-----------------------------------------------------------------------------------
//	Right now, I am just using this for the main menu button to get into the TEST_SCENE
//-----------------------------------------------------------------------------------
//I would love in the future to be able to create this into an array or Dictionary.
//Then to be able to attach this to anything any time you would like to change the scene.
//You would then be able to pick the scene out of the array/Dictionary and load it
// BUT THIS WILL HAPPEN LATER AND IS NOT NEEDED ATM.
//-----------------------------------------------------------------------------------
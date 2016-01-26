using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VersionCheck : MonoBehaviour
{
	//MUST BE CHANGED BY SCRIPT
	private string curVersion = "ALPHA 0.01"; //For every new version. Change this
	private string errorMessage = "Version Cannot load";// In-case there is an error loading the version #. Do not crash
	public Text textVersion;

	void Start()
	{
		VersionChecker();
	}

	void VersionChecker()
	{
		if(textVersion != null)
		{
			textVersion.text = curVersion; //assign the text to the text in the canvas UI
		}
		else{
			Debug.LogWarning("welcomeText not assigned");
			textVersion.text = errorMessage;

		}

	}
}
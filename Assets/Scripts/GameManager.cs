using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private const string PLAYER_ID_PREFIX = "Survivor "; //Word Before _playerID

	private static Dictionary<string, PlayerManager> players =  new Dictionary<string, PlayerManager>();

	void Update()
	{
		Debug.Log(SceneManager.GetActiveScene().name);
	}

	public static void RegisterPlayer(string _netID, PlayerManager _player)
	{
		string _playerID = PLAYER_ID_PREFIX + _netID;	//---------------------
		players.Add(_playerID, _player);				//Getting the player ID and prefix together
		_player.transform.name = _playerID;				//---------------------
	}

	public static void UnRegisterPlayer (string _playerID)
	{
		players.Remove(_playerID);		//When player leaves, remove the player from the Dictionary
	}

	public static PlayerManager GetPlayer(string _playerID)
	{
		return players[_playerID];			
	}



	/*void OnGUI()
	{
		GUILayout.BeginArea(new Rect(200, 200, 200, 500));
		GUILayout.BeginVertical();

		foreach (string _playerID in players.Keys)
		{
			GUILayout.Label (_playerID + "  -  " + players[_playerID].transform.name);
		}

		GUILayout.EndVertical();
		GUILayout.EndArea();
	}*/

}

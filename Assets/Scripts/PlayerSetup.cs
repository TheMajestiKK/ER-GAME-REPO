using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;

[RequireComponent(typeof(PlayerManager))]
public class PlayerSetup : NetworkBehaviour {

	[SerializeField]Behaviour[] componentsToDisable;

	[SerializeField]string remoteLayerName = "RemotePlayer";

	Camera sceneCamera;

	void Start()
	{
		if (!isLocalPlayer)		//If it is not the system controlling the player. Disable the controller components.
		{
			DisableComponents();
			AssignRemoteLayer();	//Make everyone have the RemotePlayer layer except for the local
		}
		else
		{
			sceneCamera = Camera.main;	//toggle off the main camera when player spawns
			if(sceneCamera != null)
			{
				sceneCamera.gameObject.SetActive(false);
			}
		}
			

		GetComponent<PlayerManager>().Setup();
	}

	public override void OnStartClient()		
	{
		base.OnStartClient();
		string _netID = GetComponent<NetworkIdentity>().netId.ToString();
		PlayerManager _player = GetComponent<PlayerManager>();

		GameManager.RegisterPlayer(_netID, _player);
	}
		

	void AssignRemoteLayer()
	{
		gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
	}

	void DisableComponents()
	{
		for (int i = 0; i < componentsToDisable.Length; i++)
		{
			componentsToDisable[i].enabled = false;
		}
	}


	void OnDisable()	
	{
		if(sceneCamera != null)
		{
			sceneCamera.gameObject.SetActive(true);
		}

		GameManager.UnRegisterPlayer(transform.name);
	}





}

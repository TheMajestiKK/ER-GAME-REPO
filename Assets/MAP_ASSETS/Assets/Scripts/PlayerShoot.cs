using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

	private const string PLAYER_TAG = "Player";	//THE TAG THE PLAYER RECIEVES

	//REFERENCE ALL WEAPON SCRIPTS BELOW
	public PlayerWeaponPistol01 weapon;



	[SerializeField]private Camera cam;
	[SerializeField]private LayerMask mask;
	void Start()
	{
		if (cam == null)
		{
			Debug.LogError("PlayerShoot: No camera referenced!");
			this.enabled =  false;
		}
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Shoot();
		}
			
	}

	[Client]  //Only called on Client
	void Shoot()	
	{
		RaycastHit _hit;
		if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
		{
			//Debug.Log("We hit" + _hit.collider.name);
			if(_hit.collider.tag == PLAYER_TAG)	//IF WE HIT A OBJECT TAGGED "PLAYER":
			{
				CmdPlayerShot(_hit.collider.name, weapon.damage);//DEAL THE AMOUNT OF DAMAGE FROM THE PLAYERWEAPON SCRIPT
			}
		}
	}

	[Command]  //Only called on Server
	void CmdPlayerShot(string _playerID, int _damage)
	{
		Debug.Log(_playerID + " has been shot");

		PlayerManager _player = GameManager.GetPlayer(_playerID);
		_player.RpcTakeDamage(_damage);		
	}

}

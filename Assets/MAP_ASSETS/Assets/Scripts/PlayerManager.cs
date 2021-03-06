using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour {

	public Transform spawnPoint;

	void Start()
	{
		spawnPoint = GameObject.Find ("SpawnPoint1").transform;
	}

	[SyncVar]
	private bool _isDead = false;
	public bool isDead
	{
		get { return _isDead; }					//Using public variable for a private state
		protected set {_isDead = value; }

	}

	[SerializeField]private int maxHealth = 100;

	[SyncVar]private int currentHealth;

		[SerializeField]
	private Behaviour[] disableOnDeath;	//Creates an array for objects to disableOnDeath
		private bool[] wasEnabled;

	public void Setup()
	{
		wasEnabled = new bool[disableOnDeath.Length];
		for (int i = 0; i < wasEnabled.Length; i++) {
			wasEnabled[i] = disableOnDeath[i].enabled;
		}

		SetDefaults();
	}

	void Update()
	{
		if (!isLocalPlayer)
			return;

		if(Input.GetKeyDown(KeyCode.K))
		{
			RpcTakeDamage(9999);
		}
	}

	[ClientRpc]
	public void RpcTakeDamage(int _amount)
	{
		if(isDead)
			return;

		currentHealth -= _amount;

		Debug.Log(transform.name + " now has " + currentHealth + " health.");  //We could use this on the HUD after we die

		if(currentHealth <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		isDead =  true;

		for (int i = 0; i < disableOnDeath.Length; i++) 
		{
			disableOnDeath[i].enabled = false;
		}

		Collider _col = GetComponent<Collider>();
		if(_col != null)
				_col.enabled = false;

		Rigidbody rb = GetComponent<Rigidbody> ();		
		if (rb != null)									//sets useGravity to false. If it would be true the player would
			rb.useGravity = false;						//go trough the ground while he's waiting 4 seconds to respawn

		Debug.Log(transform.name + " is DEAD!");

		StartCoroutine(Respawn ());
	}

	public float respawnTime = 4f;

	IEnumerator Respawn()
	{
		yield return new WaitForSeconds (respawnTime);		//waiting 4 seconds before player respawns
		SetDefaults ();
		transform.position = spawnPoint.position;			//primitive, but it works
		transform.rotation = spawnPoint.rotation;
	}



	public void SetDefaults()
	{
		isDead = false;

		currentHealth = maxHealth;

		//sets all the components back to true
		for (int i = 0; i < disableOnDeath.Length; i++) 
		{
			disableOnDeath[i].enabled = wasEnabled[i];
		}

		Collider _col = GetComponent<Collider>();
		if(_col != null)
			_col.enabled = true;

		//sets useGravity to true again.
		Rigidbody rb = GetComponent<Rigidbody> ();
		if (rb != null)	
			rb.useGravity = true;
	}

}

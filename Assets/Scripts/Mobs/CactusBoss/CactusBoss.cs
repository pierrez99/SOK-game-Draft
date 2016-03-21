using UnityEngine;
using System.Collections;

public class CactusBoss : MonoBehaviour {

	public float health, maxHealth;
	float [] timeToNormal = new float[3];
	float [] periodicTime = new float[5];
	float [] time = new float[5];
	/*	0. Normal fire  1.Grow Cactus  2.Cone attack  3.Rain friends  4.Circle of death */
	Transform playerPos, playerPosCirc;
	GameObject player;
	Flower flower;
	bool surroundON;
	public GameObject HpBar;

	// Use this for initialization
	void Start () 
	{
		maxHealth = 500f;
		health = maxHealth;

		surroundON = true;

		periodicTime[0] = 1.2f; //For normal firing rate
		periodicTime [1] = 10f; //For cactus grow
		periodicTime [2] = 5f; //For cone attack
		periodicTime [3] = 6f; //For rain friends
		periodicTime [4] = 25f; //For surround attack
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Time.time > time[0]) //Fire rate
		{
			time[0] = Time.time + periodicTime[0];
			fireNormal();
		}
		if (health < 200f) 
		{
			periodicTime[0] = 0.9f;
		}

		if (Time.time > time [1] && surroundON == false) //Cactus grow
		{
			time[1] = Time.time + periodicTime[1];
			growCactus();
			timeToNormal[1] = Time.time + 6.5f;
		}

		if (Time.time > time [2])  //Fire cone
		{
			time[2] = Time.time + periodicTime[2];
			fireCone();
		}

		if (Time.time > time [3]) //Rain friends
		{
			time[3] = Time.time + periodicTime[3];
			rainFriends();
		}

		if (Time.time > time [4] && Time.time > timeToNormal[1]) //Surround spikes
		{
			time[4] = Time.time + periodicTime[4];
			timeToNormal[0] = Time.time + 3f;//Edit the value in Cactus for timeSTAT if editted
			surroundSpikes();
			surroundON = true;
		}

		if (Time.time > timeToNormal[0]) //So surround attack isn't spammed with cactus grow
		{
			surroundON = false;
		}

		if (health <= 0) 
		{
			GameObject.Destroy (this.gameObject, 0.2f);
		}

		if(health > 500f)
		{
			health = 500f;
		}
	}

	public void damage(float dmg)
	{
		health -= dmg;
		SetHpBar (health);
	}

	public void SetHpBar(float myHealth)
	{
		float myHealthPercent = (myHealth / maxHealth);
		HpBar.transform.localScale = new Vector3 (Mathf.Clamp(myHealthPercent, 0f, 1f), HpBar.transform.localScale.y, HpBar.transform.localScale.z);
	}

	public void heal(float healing)
	{
			health += healing;
			SetHpBar (health);
	}

	void fireNormal() //Same as cactus enemy
	{
		Instantiate (Resources.Load("Prefabs/Spike"), transform.position, Quaternion.identity);
	}

	public bool swipeRightHand() //Call this in right hand script NOT IMPLEMENTED
	{
		return true;
	}

	void growCactus() //Call 2 cactuses that grow to the player's height
	{
		Instantiate (Resources.Load("Prefabs/CactusGrow"), new Vector3(player.transform.position.x + 1f, -6f, 0f), Quaternion.identity);
		Instantiate (Resources.Load("Prefabs/CactusGrow"), new Vector3(player.transform.position.x - 3f, -6f, 0f), Quaternion.identity);
	}

	void rainFriends()//FEEDBACK IS THAT THIS IS ANNOYING - REMOVE?
	{
		//Spawn small cactuses and let gravity do the rest
		float randomX = Random.Range (-10f, 0f);
		Instantiate (Resources.Load("Prefabs/CactusGrow"), new Vector3(randomX, 11f, 0f), Quaternion.identity);
	}

	void fireCone()
	{
		//This spike won't look at the player and is bigger
		Instantiate (Resources.Load("Prefabs/SpikeBOSS"), transform.position, Quaternion.Euler(0f, 0f, 20f)); 
		Instantiate (Resources.Load("Prefabs/SpikeBOSS"), transform.position, Quaternion.Euler(0f, 0f, 10f));
		Instantiate (Resources.Load("Prefabs/SpikeBOSS"), transform.position, Quaternion.Euler(0f, 0f, -10f));
		Instantiate (Resources.Load("Prefabs/SpikeBOSS"), transform.position, Quaternion.Euler(0f, 0f, -20f));
	}

	void surroundSpikes()
	{
		//Create a circle of spikes around the player(that absorb shots) for 2 secs, deactivate normalFire a few secs before this is called
		playerPosCirc = player.transform;

		for (int i = 0; i < 8; i++) 
		{
			float angle = (i * 2 * Mathf.PI)/8;//Need an explanation on how this works http://forum.unity3d.com/threads/how-to-instantiate-objects-in-a-circle.10693/
			Vector3 pos = (new Vector3(Mathf.Cos (angle), Mathf.Sin(angle), 0f)*3f)  + player.transform.position;

			Instantiate (Resources.Load("Prefabs/SpikeSURROUND"), pos, Quaternion.identity);
		}
	}
}

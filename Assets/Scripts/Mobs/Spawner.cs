using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public AudioClip TRAP;
	public AudioSource audio;

	bool bossBeaten;
	public float random, randomCheck, bossDelay, spawned;
	float showLevelTime;
	public GameObject levelText, winText, loseText, player, boss;
	GameObject[] Instance = new GameObject[5];//Below are the num tags for mobs
	/*
	 * 0: Grabby   1: Cactus01   2: Vulture   3: CactusBoss  4: Pumpkin
	 **/
	float [] time = new float[5];
	float[] toSpawn = new float[5];

	// Use this for initialization
	void Start () 
	{
		audio = GetComponent<AudioSource>();

		showLevelTime = Time.time + 8f;
		levelText.GetComponent<Rigidbody2D> ().velocity = -transform.up*0.5f;

		bossBeaten = false;
		//1st delay
		time [0] = showLevelTime + 2f;
		time [1] = showLevelTime + 1f;
		time [2] = showLevelTime + 3f;
		//Continous delay
		toSpawn [0] = 7f; //Better way instead of a lot of arrays?
		toSpawn [1] = 4f;
		toSpawn [2] = 3f;

		for (int i = 0; i < toSpawn.Length; i++) 
		{
			bossDelay += toSpawn[i];
		}

		player = GameObject.FindGameObjectWithTag ("Player");
		boss = GameObject.FindGameObjectWithTag ("Boss");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.time > showLevelTime)
		{
			levelText.SetActive(false);
		}

		//1st par = spawn delay | 2nd par = num tag for array
		spawnGrabby (3f, 0);
		spawnCactus01 (5f, 1);
		spawnVulture (6.28f, 2);

		GameObject enemy = GameObject.FindGameObjectWithTag ("Enemy");
		if(enemy == null && bossDelay <= spawned && bossBeaten == false)
		{
			Debug.Log("Spawn Boss");
			spawnCactusBoss(3);
			bossBeaten = true;
		}

		boss = GameObject.FindGameObjectWithTag ("Boss");
		if (boss == null && bossBeaten == true) 
		{
			winText.SetActive(true);
		}

		float playerHp = player.GetComponent<Move>().health;
		if(playerHp <= 0f)
		{
			loseText.SetActive(true);
		}


	}

	void spawnGrabby(float spawnTime, int mobNum) //infinite spawning since parameter "toSpawn" gets refreshed TEMP FIXED
	{
		Debug.Log ("SPAWNING");
		if(Time.time > time[mobNum] && toSpawn[mobNum] > 0)
		{
			time[mobNum] = Time.time + spawnTime;
			randomCheck = random;
			random = (int)Random.Range(-3f,3f);
			
			if(randomCheck == random)
			{
				do
				{
					random = (int)Random.Range(-3f,3f);
				}
				while(randomCheck == random);
			}
			
			Debug.Log ("Random: " + random);
			Vector3 spawnHere = new Vector3(12f, random, 0f);
			Instance[mobNum] = Instantiate (Resources.Load ("Prefabs/Enemy"), spawnHere, Quaternion.identity) as GameObject;
			toSpawn[mobNum]--;
			spawned++;
		}
	}

	void spawnCactus01(float spawnTime, int mobNum)
	{
		if (Time.time > time[mobNum] && toSpawn[mobNum] > 0) 
		{
			time[mobNum] = Time.time + spawnTime;

			randomCheck = random;
			random = (int)Random.Range(-3f,3f);
			
			if(randomCheck == random)
			{
				do
				{
					random = (int)Random.Range(-3f,3f);
				}
				while(randomCheck == random);
			}

			Vector3 spawnHere = new Vector3(12f, random, 0f);
			Instance[mobNum] = Instantiate (Resources.Load ("Prefabs/Cactus"), spawnHere, Quaternion.identity) as GameObject;
			toSpawn[mobNum]--;
			spawned++;
		}
	}

	void spawnVulture(float spawnTime, int mobNum)
	{

		if ((Time.time > time [mobNum]) && (toSpawn [mobNum] > 0)) 
		{
			GameObject playerGuy = GameObject.FindGameObjectWithTag("Player");
			float playerX = playerGuy.transform.position.x;
			Vector3 spawnHere = new Vector3(12f, 3f, 0f);
			Instance[mobNum] = Instantiate (Resources.Load ("Prefabs/Vulture"), spawnHere, Quaternion.identity) as GameObject;

			GameObject VultureObj = Resources.Load ("Prefabs/Vulture") as GameObject;
			time[mobNum] = Time.time + (spawnTime);

			toSpawn[mobNum]--;
			spawned++;
		}
	}

	void spawnCactusBoss(int mobNum)
	{
		Instance[mobNum] = Instantiate (Resources.Load ("Prefabs/CactusBoss"), new Vector3(11.3f, 0f, 0f), Quaternion.identity) as GameObject;
		audio.PlayOneShot (TRAP, 1f);
	}
}

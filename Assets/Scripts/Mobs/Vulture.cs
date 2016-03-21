using UnityEngine;
using System.Collections;

public class Vulture : MonoBehaviour {

	public float health;
	float time01, time02, time, timeTime, playerX, timeColour;
	bool attacked, gotHit;	//bool to check if vulture descended

	public float frequency, magnitude, random;
	public float movespeed;
	private Vector3 axis,pos;
	Transform playerTrans;
	GameObject player;
	

	// Use this for initialization
	void Start () 
	{
		health = 20f;

		pos = transform.position;
		axis = transform.up;
		player = GameObject.FindGameObjectWithTag ("Player");
		playerTrans = player.GetComponent<Transform> ();

		attacked = false;

		magnitude = Mathf.Abs(playerTrans.position.y) + 3.5f;
		playerX = playerTrans.transform.position.x;
		if(playerX < -6f)
		{
			playerX = -5f;
		}
		movespeed = 1f;
		frequency = movespeed;
		timeTime = Time.time;
		//The GS of Sin((x-X)*frequency) where x = Time.time is PI*n where n is an integer
		time01 = (3.14f/frequency) + Mathf.Abs(playerX); //Secondary value of above function
		time02 = (6.28f/frequency) + Mathf.Abs(playerX); //Tertiary value of above function
		//(This will create half a cycle, the first minimum)
	}

	// Update is called once per frame
	void Update () 
	{
		if (health <= 0 || transform.position.x < -13f) 
		{
			GameObject.Destroy(this.gameObject, 0.2f);
		}

		pos += (-transform.right) * Time.deltaTime * movespeed;
		transform.position = pos + axis * Mathf.Cos (Time.time * frequency) * magnitude;

		if(attacked == false && ((Time.time-timeTime) > time01))
		{
			swoopDown ();
			if((Time.time-timeTime) > time02) 
			{
				attacked = true;
			}
		}
		else
		{
			pos += (-transform.right) * Time.deltaTime * movespeed;
			transform.position = pos;
		}

		if (gotHit == true && Time.time > timeColour) 
		{
			GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 1f);
		}
	}

	void swoopDown()
	{
		pos += (-transform.right) * Time.deltaTime * movespeed;
		transform.position = pos + axis * (Mathf.Sin ((gameObject.transform.position.x-Mathf.Abs(playerX)) * frequency) * magnitude);
	}

	public void damage(float dmg)
	{
		GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 0.5f);
		health -= dmg;
		timeColour = Time.time + 0.3f;
		gotHit = true;
	}

	void OnTriggerStay2D(Collider2D Player)
	{
		if (Player.tag == "Player") 
		{
			Player.SendMessage ("damage", 10f, SendMessageOptions.DontRequireReceiver);
		}
	}
}

using UnityEngine;
using System.Collections;

public class Cactus : MonoBehaviour {

	public float health;
	float time, timeSTAT, timeColour;
	bool gotHit;

	Transform playerPos;
	GameObject Player;

	// Use this for initialization
	void Start () 
	{
		time = 0f;
		health = 20f; //For spikes
		time = 0.8f;

		
		Player = GameObject.FindGameObjectWithTag ("Player");
		playerPos = Player.GetComponent<Transform> ();

		if (tag == "Enemy") 
		{
			health = 50f; //For cactus
			GetComponent<Rigidbody2D> ().velocity = -transform.right;
		}

		if (tag == "BulletE") //Try to understand with link on notes
		{
			Vector3 diff = gameObject.transform.position - playerPos.transform.position; 
			diff.Normalize();
			float rotZ = Mathf.Atan2(diff.y, diff.x)*Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
			spikeMove(3.5f);
		}

		if(tag == "Spike")
		{
			if(gameObject.transform.eulerAngles.z > 10f && gameObject.transform.eulerAngles.z <= 30f) //For angle of 20
			{
				Vector3 diff = gameObject.transform.position - new Vector3(2.5f, -2.5f, 0);
				diff.Normalize();
				GetComponent<Rigidbody2D>().velocity = -diff * 3.5f;
			}
			else if(gameObject.transform.eulerAngles.z == 10f)
			{
				Vector3 diff = gameObject.transform.position - new Vector3(2.5f, -1.5f, 0);
				diff.Normalize();
				GetComponent<Rigidbody2D>().velocity = -diff * 3.5f;
			}
			else if(gameObject.transform.eulerAngles.z >= 350f) //For some reason == 350f doesn't work
			{
				gameObject.transform.position += new Vector3(0f, 0.1f, 0f);//Objects not symmetrical
				Vector3 diff = gameObject.transform.position - new Vector3(2.5f, 1.5f, 0);
				diff.Normalize();
				GetComponent<Rigidbody2D>().velocity = -diff * 3.5f;
			}
			else if(gameObject.transform.eulerAngles.z >= 340f && gameObject.transform.eulerAngles.z < 350f)
			{
				gameObject.transform.position += new Vector3(0f, 0.1f, 0f);
				Vector3 diff = gameObject.transform.position - new Vector3(2.5f, 2.5f, 0);
				diff.Normalize();
				GetComponent<Rigidbody2D>().velocity = -diff * 3.5f;
			}
			else
			{
				Debug.Log("Angle of " + gameObject.transform.eulerAngles.z + " won't move");
			}
		}

		if (tag == "SpikeSTAT") 
		{
			timeSTAT = Time.time + 3f; //Edit the value in Cactusboss to make surroundON = false if editted
			Vector3 diff = gameObject.transform.position - playerPos.transform.position; 
			diff.Normalize();
			float rotZ = Mathf.Atan2(diff.y, diff.x)*Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
			Vector2 x = new Vector2 (transform.position.x - playerPos.position.x, 0);
			Vector2 y = new Vector2(0, transform.position.y - playerPos.position.y);
			GetComponent<Rigidbody2D> ().velocity = -(x+y).normalized * 0.15f;
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (tag == "Enemy" && Time.time > time) 
		{
			time = Time.time + 1.5f;
			CreateSpike();
		}

		
		if(Time.time > timeSTAT && tag == "SpikeSTAT")
			Destroy(this.gameObject, 0.2f);

		if(health <= 0 || transform.position.x < -13f)
		{
			GameObject.Destroy(this.gameObject, 0.2f);
		}

		if (gotHit == true && Time.time > timeColour) 
		{
			GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 1f);
		}

	}

	public void damage(float dmg)
	{
		GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 0.5f);
		health -= dmg;
		timeColour = Time.time + 0.3f;
		gotHit = true;
	}

	void CreateSpike()
	{
		Instantiate (Resources.Load("Prefabs/Spike"), transform.position, Quaternion.identity);
	}

	void spikeMove(float speed)//Slope of straight line
	{
		Debug.Log ("Moving spike");
		Vector2 x = new Vector2 (transform.position.x - playerPos.position.x, 0);
		Vector2 y = new Vector2(0, transform.position.y - playerPos.position.y);
		GetComponent<Rigidbody2D> ().velocity = -(x+y).normalized * speed;
		Debug.Log ("Ending spike");
	}

	void OnTriggerStay2D(Collider2D Player)
	{
		if (Player.tag == "Player") 
		{
			Player.SendMessage ("damage", 10f, SendMessageOptions.DontRequireReceiver);
		}

		if (Player.tag == "Player" && (gameObject.tag == "BulletE" || gameObject.tag == "Spike")) 
		{
			GameObject.Destroy(this.gameObject, 0.1f);
		}
	}

}

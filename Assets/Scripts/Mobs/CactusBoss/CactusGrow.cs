using UnityEngine;
using System.Collections;

public class CactusGrow : MonoBehaviour {

	GameObject Cactus;
	float time, timeStop, timeTakeDmg;
	bool fallNow;

	// Use this for initialization
	void Start () 
	{
		timeStop = 0;
		Cactus = this.gameObject;
		time = Time.time + 0.15f;
		Cactus.transform.localScale = new Vector3 (1f, -1f, 1f);
		GameObject.Destroy (this.gameObject, 6f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if((Time.time > time) && timeStop < 35 && fallNow == false)
		{
			time = Time.time + 0.1f;
			timeStop++;
			Cactus.transform.localScale += new Vector3 (0.2f, -0.3f, 0f);
		}

		if (gameObject.transform.position.y > 10f)
		{
			fallNow = true;
			GetComponent<Rigidbody2D>().gravityScale = 1f;
			GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-100f, 100f);
		}
	}

	void OnTriggerStay2D (Collider2D Player)
	{
		if (Player.tag == "Player") 
		{
			Player.SendMessage("damage", 10f, SendMessageOptions.DontRequireReceiver);
		}
	}
}

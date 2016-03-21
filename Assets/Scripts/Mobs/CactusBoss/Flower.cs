using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour {

	public float health, nextHeal, renewTime;
	GameObject Boss;
	bool closed;
	public Sprite closedFlower, openFlower;

	// Use this for initialization
	void Start () 
	{
		health = 30f;
		closed = false;
		GetComponent<SpriteRenderer> ().sprite = openFlower;
		nextHeal = Time.time + 5f;
		Boss = GameObject.FindGameObjectWithTag ("Boss");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (closed == false) 
		{
			GetComponent<SpriteRenderer> ().color = Color.Lerp (Color.white, Color.gray, Mathf.PingPong ((nextHeal - Time.time) / 5f, 1f));
		}
		else if(closed == false)
		{
			GetComponent<SpriteRenderer> ().color = Color.Lerp (Color.white, Color.gray, Mathf.PingPong ((renewTime - Time.time) / 20f, 1f));
		}

		if (Time.time > nextHeal && health > 0f) 
		{
			nextHeal = Time.time + 5f;
			Boss.SendMessage("heal", 50f, SendMessageOptions.DontRequireReceiver);

		}

		if (Time.time > renewTime && closed == true) 
		{
			health = 30f;
			GetComponent<SpriteRenderer> ().sprite = openFlower;
			closed = false;
		}

		if(health <= 0f && closed == false)
		{
			GetComponent<SpriteRenderer>().sprite = closedFlower;
			closed = true;
			renewTime = Time.time + 20f;
			nextHeal = renewTime + 5f;
		}


	}

	public void damage(float dmg)
	{
		health -= dmg;
	}
}

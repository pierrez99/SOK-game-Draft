using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {


	public float health;
	float timeColour;
	bool gotHit;

	public float frequency, magnitude, movespeed, random;
	private Vector3 axis,pos;

	// Use this for initialization
	void Start () 
	{
		pos = transform.position;
		axis = transform.up;
		health = 30f;
		frequency = 0.6f;
		magnitude = (int)Random.Range(1,3);
		movespeed = 1.2f;
		random = Random.Range (0.8f, 1.5f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(health <= 0 || transform.position.x < -13f)
		{
			GameObject.Destroy(this.gameObject, 0.2f);
		}

		pos += (-transform.right) * Time.deltaTime * movespeed;
		transform.position = pos + axis * Mathf.Sin (Time.time * frequency * random) * magnitude;

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
	

	void OnTriggerStay2D(Collider2D Player)
	{
		if (Player.tag == "Player") 
		{
			Player.SendMessage ("damage", 10f, SendMessageOptions.DontRequireReceiver);
		}
	}
}

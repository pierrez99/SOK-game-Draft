using UnityEngine;
using System.Collections;

public class Pumpkin : MonoBehaviour {

	float health,speed, timeColour;
	int random;
	bool gotHit;
	public GameObject [] pumpkinPieces = new GameObject[4];
	public Sprite [] pumpkins = new Sprite[4];

	// Use this for initialization
	void Start () 
	{
		health = 1f;
		speed = 2f;

		random = (int)Random.Range (0, 3);

		GetComponent<SpriteRenderer> ().sprite = pumpkins [random];

		this.gameObject.GetComponent<Rigidbody2D> ().velocity = -transform.right * speed;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gotHit == true && Time.time > timeColour) 
		{
			GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 1f);
		}

		if (health <= 0f) 
		{
			Instantiate(Resources.Load("Prefabs/PumpkinPiece01"), new Vector2 (transform.position.x,transform.position.y + 0.5f), Quaternion.identity);
			pumpkinPieces[0] = GameObject.FindGameObjectWithTag("pumpkinPiece01");
			pumpkinPieces[0].SendMessage("fromSpawnY", transform.position.y, SendMessageOptions.DontRequireReceiver);
			pumpkinPieces[0].SendMessage("fromSpawnX", transform.position.x, SendMessageOptions.DontRequireReceiver);
			Instantiate(Resources.Load("Prefabs/PumpkinPiece02"), new Vector2 (transform.position.x - 0.5f, transform.position.y), Quaternion.identity);
			pumpkinPieces[1] = GameObject.FindGameObjectWithTag("pumpkinPiece02");
			pumpkinPieces[1].SendMessage("fromSpawnY", transform.position.y, SendMessageOptions.DontRequireReceiver);
			pumpkinPieces[1].SendMessage("fromSpawnX", transform.position.x, SendMessageOptions.DontRequireReceiver);
			Instantiate(Resources.Load("Prefabs/PumpkinPiece03"), new Vector2 (transform.position.x + 0.5f, transform.position.y), Quaternion.identity);
			pumpkinPieces[2] = GameObject.FindGameObjectWithTag("pumpkinPiece03");
			pumpkinPieces[2].SendMessage("fromSpawnY", transform.position.y, SendMessageOptions.DontRequireReceiver);
			pumpkinPieces[2].SendMessage("fromSpawnX", transform.position.x, SendMessageOptions.DontRequireReceiver);

			for (int i = 0; i < 8; i++) 
			{
				float angle = (i * 2 * Mathf.PI)/8;//Need an explanation on how this works http://forum.unity3d.com/threads/how-to-instantiate-objects-in-a-circle.10693/
				Vector3 pos = (new Vector3(Mathf.Cos (angle), Mathf.Sin(angle), 0f)*0.5f)  + transform.position;
				
				Instantiate (Resources.Load("Prefabs/pumpkinSeed"), pos, Quaternion.identity);

			}
			for(int i = 0; i < 8; i++)
			{
				GameObject[] pumpkinSeeds = GameObject.FindGameObjectsWithTag("Seed");
				pumpkinSeeds[i].SendMessage("fromSpawnY", transform.position.y, SendMessageOptions.DontRequireReceiver);
				pumpkinSeeds[i].SendMessage("fromSpawnX", transform.position.x, SendMessageOptions.DontRequireReceiver);
			}

			GameObject.DestroyImmediate(this.gameObject);
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

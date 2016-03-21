using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Move : MonoBehaviour {

	private Vector3 mousePosition;
	public Rigidbody2D player;
	public float moveSpeed, health, maxHealth, armour, bombs, time, timeGetHit;
	GameObject PlayerGO;
	bool isPaused;
	public GameObject HpBar;

	public Text hpText, armourText, bombText;
	public Canvas pauseMenu;
	
	AddStats A1;
	

	// Use this for initialization
	void Start () 
	{
		pauseMenu.enabled = false;

		isPaused = false;
		Time.timeScale = 1; //To remove pause bug

		PlayerGO = this.gameObject;
		A1 =  PlayerGO.AddComponent<AddStats> ();//Add that script

		maxHealth = 100f + (5f*A1.getHp());
		health = maxHealth;
		hpText.text = health.ToString();
		armour = 20f + (1.5f*A1.getDef());
		armourText.text = armour.ToString ();
		bombs = 3f;
		bombText.text = bombs.ToString ();
		player = GetComponent<Rigidbody2D> ();

		moveSpeed = A1.getSpeed();
		Debug.Log ("Speed: " + moveSpeed);
	}
	
	// Update is called once per frame
	void Update () 
	{
		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);//Get x and y co-ordinates
		transform.position += move * moveSpeed * 30 * Time.deltaTime;

		if (Input.GetKeyDown (KeyCode.P)) 
		{
			Pause();
		}

		if((Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.F)) && (bombs > 0))
		{
			bombs--;
			bombText.text = bombs.ToString ();
			GameObject [] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			for(int i = 0; i < enemies.Length; i++)//Find all enemies and deal damage to them (doesn't work on boss)
			{
				enemies[i].SendMessage("damage", 100f, SendMessageOptions.DontRequireReceiver);
			}
		}

		if (health <= 0) 
		{
			Debug.Log ("You died");
		}
	}

	public void damage(float dmg)
	{
		if (armour == 0 && Time.time > timeGetHit) 
		{
			timeGetHit = Time.time + 1f;
			health -= dmg;
			hpText.text = health.ToString ();
			SetHpBar(health);
		}
		else if(armour > 10 && Time.time > timeGetHit)
		{
			timeGetHit = Time.time + 1f;
			armour -= dmg;
			armourText.text = armour.ToString ();
		}
		else if(armour <= 10 && Time.time > timeGetHit)
		{
			timeGetHit = Time.time + 1f;
			armour = 0f;
			armourText.text = armour.ToString ();
		}
	}

	public void SetHpBar(float myHealth)
	{
		float myHealthPercent = (myHealth / maxHealth);
		HpBar.transform.localScale = new Vector3 (Mathf.Clamp(myHealthPercent, 0f, 1f), HpBar.transform.localScale.y, HpBar.transform.localScale.z);
	}
	

	public void Pause() //Pause game IMPLEMENT MENU FEATURE LATER
	{
		if (Time.timeScale == 1) 
		{
			Time.timeScale = 0;
			isPaused = true;
			pauseMenu.enabled = true;
		} 
		else 
		{
			Time.timeScale = 1;
			isPaused = !isPaused;
			pauseMenu.enabled = false;
		}
		
		//Menu.enabled = true for menu
	}


}
using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public class GunsV 
	{
		public float RPM, dmg, bulletSpeed;
		public int chooseGun;
		public GameObject pistolB, shotgunB, assaultB, laserGunB;
		public Sprite pistol, shotgun, assault, laserGun;
		public bool chosen;
	}

	GunsV gunP = new GunsV();//Pistol
	GunsV gunS = new GunsV(); //Shotgun
	GunsV gunA = new GunsV(); //Rifle
	GunsV gunL = new GunsV(); //Laser

	public GameObject Bullet, BulletShotgun, LaserB;

	float upgradeDmg, chooseNumShift;
	public float time;
	Animator Player;

	AddStats A1;

	// Use this for initialization
	void Start () 
	{
		A1 = gameObject.AddComponent<AddStats> ();
		Player = GetComponent<Animator> ();

		chooseNumShift = 1f;
		time = 0;

		gunP.RPM = (65f+(9f*A1.getSpeed())); //Values for pistol
		gunP.chosen = true;
		gunP.pistolB = Bullet;
		//gunP.pistol

		gunS.RPM = (55f+(8f*A1.getSpeed()));
		gunS.chosen = false;
		gunS.shotgunB = BulletShotgun;

		gunA.RPM = (115f+(6f*A1.getSpeed()));
		gunA.chosen = false;

		gunL.RPM = (40f+(4f*A1.getSpeed()));
		gunL.chosen = false;
		gunL.laserGunB = LaserB;

	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			if(chooseNumShift > 1)
				chooseNumShift--;
		}
		else if(Input.GetKeyDown (KeyCode.E))
		{
			if(chooseNumShift < 4)
				chooseNumShift++;
		}

		if (Input.GetKeyDown("1") || chooseNumShift == 1) 
		{
			chooseNumShift = 1;
			gunP.chosen = true;
			gunS.chosen = false;
			gunA.chosen = false;
			gunL.chosen = false;
		}

		if(gunP.chosen == true)
		{

			while(Input.GetKey(KeyCode.Space) && (Time.time > time))
			{
				time = (60/gunP.RPM) + Time.time;
				
				Instantiate(gunP.pistolB, transform.position, Quaternion.identity);
			}

			if(Input.GetKeyDown(KeyCode.Space))
			{
				Player.SetBool("FiringCarlos", true);
				GetComponent<Move>().moveSpeed -= 0.05f;
			}

			if(Input.GetKeyUp(KeyCode.Space))
			{
				Player.SetBool("FiringCarlos", false);
				GetComponent<Move>().moveSpeed += 0.05f;
			}
		}

		if(Input.GetKeyDown("2") || chooseNumShift == 2)
		{
			chooseNumShift = 2;
			gunP.chosen = false;
			gunS.chosen = true;
			gunA.chosen = false;
			gunL.chosen = false;
		}

		if(gunS.chosen == true)
		{
			while(Input.GetKey(KeyCode.Space) && (Time.time > time))
			{
				time = (60/gunS.RPM) + Time.time;

				Instantiate(gunS.shotgunB, transform.position, Quaternion.identity);
				Instantiate(gunS.shotgunB, (transform.position + (transform.up * 0.5f)), Quaternion.Euler(0f, 0f, 5f));
				Instantiate(gunS.shotgunB, (transform.position - (transform.up * 0.5f)), Quaternion.Euler(0f, 0f, (-5f)));
				//http://answers.unity3d.com/questions/291268/2d-bullet-spray-problems.html
			}
		}

		if(Input.GetKeyDown("3") || chooseNumShift == 3)
		{
			chooseNumShift = 3;
			gunP.chosen = false;
			gunS.chosen = false;
			gunA.chosen = true;
			gunL.chosen = false;
		}

		if (gunA.chosen == true) 
		{
			while(Input.GetKey(KeyCode.Space) && (Time.time > time))
			{
				time = (60/gunA.RPM) + Time.time;
				Instantiate(gunS.shotgunB, transform.position, Quaternion.identity);
			}
		}

		if (Input.GetKeyDown ("4") || chooseNumShift == 4) 
		{
			chooseNumShift = 4;
			gunP.chosen = false;
			gunS.chosen = false;
			gunA.chosen = false;
			gunL.chosen = true;
		}

		if (gunL.chosen == true) 
		{
			while(Input.GetKey(KeyCode.Space) && (Time.time > time))
			{
				time = (60/gunL.RPM) + Time.time;
				Instantiate(gunL.laserGunB, transform.position, Quaternion.identity);
			}
		}
	}
	
}


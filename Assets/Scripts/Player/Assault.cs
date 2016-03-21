using UnityEngine;
using System.Collections;

public class Assault : MonoBehaviour {

	float speed = 5f;
	float dmg = 3f;

	AddStats A1;

	// Use this for initialization
	void Start () 
	{
		A1 = gameObject.AddComponent<AddStats> ();
		GetComponent<Rigidbody2D>().velocity = transform.right * 2.5f * (speed+A1.getSpeed());
		Object.Destroy (this.gameObject, 5f);
	}

	float increaseUpgradeDmg(float upgradeNumber)
	{
		float totalDmg = dmg * ((upgradeNumber+10)/10);
		
		return totalDmg;
	}


	void OnTriggerEnter2D(Collider2D Mob)
	{
		if (Mob.tag == "Enemy" || Mob.tag == "Boss") 
		{
			Mob.SendMessage("damage" , increaseUpgradeDmg(A1.getDmg()), SendMessageOptions.DontRequireReceiver);
			Debug.Log ("Dmg dealt: " + increaseUpgradeDmg(A1.getDmg()));
			Object.Destroy (this.gameObject, 0.1f);
		}
	}
}

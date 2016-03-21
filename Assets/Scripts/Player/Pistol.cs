using UnityEngine;
using System.Collections;

public class Pistol : MonoBehaviour {

	float speed = 3f;
	float dmg = 10f;

	AddStats A1;

	void Start()
	{
		A1 = gameObject.AddComponent<AddStats> ();
		GetComponent<Rigidbody2D>().velocity = transform.right * 2.5f *(speed+A1.getSpeed());
		Object.Destroy (this.gameObject, 5f);
	}

	float increaseUpgradeDmg(float upgradeNumber)
	{
		float totalDmg = dmg * ((upgradeNumber+10)/10);
		
		return totalDmg;
	}

	void OnTriggerEnter2D(Collider2D Mob)
	{
		if (Mob.tag == "Enemy" || Mob.tag == "Boss" || Mob.tag == "SpikeSTAT" || Mob.tag == "Pumpkin") 
		{
			Mob.SendMessage("damage" , increaseUpgradeDmg(A1.getDmg()), SendMessageOptions.DontRequireReceiver);
			Debug.Log ("Dmg dealt: " + increaseUpgradeDmg(A1.getDmg()));
			Object.Destroy (this.gameObject, 0.1f);
		}
	}
}

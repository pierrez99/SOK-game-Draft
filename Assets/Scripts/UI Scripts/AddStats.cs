using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddStats : MonoBehaviour{

	private static float upgradeDmgNum, upgradeSpeedNum, upgradeDefNum, upgradeHpNum; //static so they an be accessed from other scripts

	public Text damageNow, speedNow, defNow, hpNow;

	public void ToTest ()
	{
		Application.LoadLevel("Test");//Loads scene named "Test"
	}

	public void increaseDmgButton()
	{
		if(upgradeDmgNum<20)
		{
			upgradeDmgNum++;
			Debug.Log ("Damage: " + upgradeDmgNum);
			damageNow.text = upgradeDmgNum.ToString();
		}
	}

	public float getDmg()
	{
		return upgradeDmgNum;
	}

	public void increaseSpeedButton()
	{
		if (upgradeSpeedNum < 20) 
		{
			upgradeSpeedNum++;
			Debug.Log ("Speed: " + upgradeSpeedNum);
			speedNow.text = upgradeSpeedNum.ToString ();
		}
	}

	public float getSpeed() //with function ready
	{
		return 0.1f + (upgradeSpeedNum/100);
	}

	public void increaseDefNum()
	{
		if(upgradeDefNum < 20)
		{
			upgradeDefNum++;
			Debug.Log ("Def: " + upgradeDefNum);
			defNow.text = upgradeDefNum.ToString();
		}
	}

	public float getDef()
	{
		return upgradeDefNum;
	}

	public void increaseHpButton()
	{
		if(upgradeHpNum<20)
		{
			upgradeHpNum++;
			Debug.Log ("Hp: " + upgradeHpNum);
			hpNow.text = upgradeHpNum.ToString();
		}
	}

	public float getHp()
	{
		return upgradeHpNum;
	}

	void Awake ()
	{
		if (Application.loadedLevel.Equals (2)) //Shop scene
		{
			damageNow.text = upgradeDmgNum.ToString ();
			speedNow.text = upgradeSpeedNum.ToString ();
			defNow.text = upgradeDefNum.ToString ();
			hpNow.text = upgradeHpNum.ToString ();
		}
	}
	
}

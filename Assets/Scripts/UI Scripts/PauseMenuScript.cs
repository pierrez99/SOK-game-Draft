using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {



	public void GoToMenu()
	{
		Application.LoadLevel ("MainMenu");
	}

	public void GoToLevelOrShop()
	{
		Debug.Log ("NOT YET IMPLEMENTED");
	}
}

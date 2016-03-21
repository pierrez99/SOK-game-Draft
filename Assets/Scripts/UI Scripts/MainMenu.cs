using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Canvas instructions;

	void Start()
	{
		instructions.enabled = false;
	}

	public void GoToTest()
	{
		Application.LoadLevel ("Test");
	}

	public void GoToShop()
	{
		Application.LoadLevel ("Shop");
	}

	public void GoToInstructionsAndBack()
	{
		instructions.enabled = !instructions.enabled;
	}

	public void Exit()
	{
		Application.Quit ();
	}
}

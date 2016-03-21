using UnityEngine;
using System.Collections;

public class pumpkinPieces : MonoBehaviour {

	public Sprite [] pumpkinPiecesSprites = new Sprite[4];
	float speed, theY, theX;
	Vector2 posX, posY;
	bool velocityApplied, xApplied, yApplied;

	// Use this for initialization
	void Start () 
	{
		speed = 4f;
		yApplied = false;
		xApplied = false;
		velocityApplied = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(yApplied == false)
		{
			posY = new Vector2 (0f, theY - transform.position.y);
			yApplied = true;
		} 

		if(xApplied == false)
		{
			posX = new Vector2 (theX - transform.position.x, 0f);
			xApplied = true;
		}

		if(velocityApplied == false)
		{
			GetComponent<Rigidbody2D> ().velocity = -(posX + posY).normalized * speed; //only works once
			theX = 0f;
			theY = 0f;
			velocityApplied = true;
		}
		Debug.Log ("The x " + theX + "\nThe y " + theY);

	}

	public void fromSpawnY(float pumpkinY)
	{
		theY = pumpkinY;
	}

	public void fromSpawnX(float pumpkinX)
	{
		theX = pumpkinX;
	}
	
}

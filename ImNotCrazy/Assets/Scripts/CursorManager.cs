using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour {

	public Sprite cDefault, cPoint, cGrab;
	public Image cursor; 

	void Start () 
	{
		cursor.sprite = cDefault;
	}
	

	public void Activate()
	{
		cursor.sprite = cPoint;
	}

	public void Return()
	{
		cursor.sprite = cDefault;
	}

	public void Grab()
	{
		cursor.sprite = cGrab;
	}
}
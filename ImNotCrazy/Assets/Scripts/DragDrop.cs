using UnityEngine;
using System.Collections;

public class DragDrop : MonoBehaviour {

	public GameObject cameraObj;
	public GameObject carriedObject;

	public float minDistance;
	public float maxDistance;

	public float currentDistance;
	public float distanceDrop;

	private float dist;

	public float smooth;

	public float throwForce;

	bool carrying;


	void Update () 
	{
		if(carrying)
		{
			carry(carriedObject);
			checkDrop();
		}
		else
		{
			pickup();
		}

		float mouseScroll =+ Input.GetAxis("Mouse ScrollWheel");
		currentDistance = Mathf.Clamp(currentDistance + mouseScroll, minDistance, maxDistance);
	}

	void carry(GameObject o)
	{
		o.transform.position = Vector3.Lerp (o.transform.position , cameraObj.transform.position + cameraObj.transform.forward * currentDistance, Time.deltaTime * smooth);

		dist = Vector3.Distance(o.transform.position, cameraObj.transform.position); // We check distance between this and player.
	}

	void pickup()
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			Ray ray = cameraObj.GetComponent<Camera>().ScreenPointToRay(new Vector3(x,y));
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit))
			{
				if( hit.distance < maxDistance || hit.distance == maxDistance)
				{
				Collider col = hit.collider.GetComponent<Collider>();

				if(col.tag == "pick")
				{
					carrying = true;
					carriedObject = col.gameObject;
					carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = false;
				}

					if(col.tag == "active")
					{
						Button active = col.gameObject.GetComponent<Button>();
						active.ButtonPressed();
					}
				}
			}
		}

		int x2 = Screen.width / 2;
		int y2 = Screen.height / 2;

		Ray ray2 = cameraObj.GetComponent<Camera>().ScreenPointToRay(new Vector3(x2,y2));
		RaycastHit hit2;

			if(Physics.Raycast(ray2, out hit2))
			{
				Collider col = hit2.collider.GetComponent<Collider>();

			if(col.tag == "Untagged" && !carrying || col.tag == null && !carrying || hit2.distance > maxDistance && !carrying)
				{
					CursorManager cursor = this.gameObject.GetComponent<CursorManager>();
					cursor.cursor.sprite = cursor.cDefault;
				}

				if( hit2.distance < maxDistance || hit2.distance == maxDistance)
				{ 


				if(col.tag == "pick" || col.tag == "drag")
				{
						CursorManager cursor = this.gameObject.GetComponent<CursorManager>();
						cursor.cursor.sprite = cursor.cGrab;
				}

				else if(col.tag == "active" && !carrying)
				{
						CursorManager cursor = this.gameObject.GetComponent<CursorManager>();
						cursor.cursor.sprite = cursor.cPoint;
				}

				if(Input.GetKey(KeyCode.Mouse0))
				{
					if(col.tag == "drag")
					{
						col.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * 6.5F, ForceMode.Acceleration);
					}
				}

				if(Input.GetKey(KeyCode.Mouse1))
				{
					if(col.tag == "drag")
					{
						col.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * -6.5F, ForceMode.Acceleration);
					}
				}

				}
			}




		currentDistance = maxDistance / 2;
	}

	void checkDrop()
	{
		if(Input.GetKeyDown(KeyCode.E) || carrying && dist > distanceDrop)
		{
			dropObject("drop");
		}

		if(Input.GetKeyDown(KeyCode.Mouse0) && carrying)
		{
			dropObject("throw");
		}
	}

	void dropObject(string message)
	{
		carrying = false;
		carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;

		if(message == "throw")
		{
			carriedObject.gameObject.GetComponent<Rigidbody>().AddForce(cameraObj.transform.forward * throwForce, ForceMode.Impulse);
		}

		carriedObject = null;

	}
}

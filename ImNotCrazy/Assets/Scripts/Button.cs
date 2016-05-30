using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(AudioSource))]
public class Button : MonoBehaviour {

	public AudioSource audio;
	public GameObject[] targets; // When button pressed we active this/these gameobjects.

	void Start()
	{
		audio = GetComponent<AudioSource>();
	}

	public void ButtonPressed()
	{
		Animation buttonAnim = this.gameObject.GetComponent<Animation>();
		buttonAnim.Play("button");

		audio.Play();

		foreach(GameObject target in targets)
		{
			Active active = target.GetComponent<Active>();
			active.activeThis();
		}
	}
}

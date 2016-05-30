using UnityEngine;
using System.Collections;

public class Active : MonoBehaviour {

	public enum Type{Light, LightButton ,Door};
	public Type types;

	public Color buttonOnColor;
	public Color buttonOffColor;

	void Start()
	{
		if(types == Type.LightButton)
		{
			Light thisLight = this.gameObject.GetComponent<Light>();
			thisLight.color = buttonOffColor;
		}
	}

	public void activeThis()
	{
		if(types == Type.Light)
		{
			Light thisLight = this.gameObject.GetComponent<Light>();
			thisLight.enabled = !thisLight.enabled;
		}

		if(types == Type.LightButton)
		{
			Light thisLight = this.gameObject.GetComponent<Light>();

			if(thisLight.color == buttonOnColor)
			{
				thisLight.color = buttonOffColor;
			}
			else 
			{
				thisLight.color = buttonOnColor;
			}
		}
	}
}

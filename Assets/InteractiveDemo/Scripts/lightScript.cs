using UnityEngine;
using System.Collections;

public class lightScript : MonoBehaviour 
{
	bool Impact = false;
	public float Sqr;
	// Use this for initialization
	void Start () 
	{
		Impact = true;
		GetComponent<Light>().intensity = 7;
		Sqr = GetComponent<Light>().intensity * GetComponent<Light>().intensity * ((GetComponent<Light>().intensity < 0.0f ) ? -1.0f : 1.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Impact)
		{
            GetComponent<Light>().intensity -= (1.0f / Time.deltaTime) * Sqr * .0001f;
			if (GetComponent<Light>().intensity <= 0)
			{
				Destroy (gameObject);
			}
		}
	}
}
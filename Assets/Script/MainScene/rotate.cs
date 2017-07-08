using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {
    public float speed = -0.8F;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Rotate(new Vector3(0, 1, 0) * speed, Space.Self);
    }
}

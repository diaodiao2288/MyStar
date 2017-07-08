using UnityEngine;
using System.Collections;

public class GenGameObject : MonoBehaviour {
    private static GenGameObject _instance;
    public GameObject segment;

    public static GenGameObject GetInstance() {
        if (_instance == null) {
            _instance = new GenGameObject();
        }
        return _instance;
    }

    public GameObject genSegment() {
        return Instantiate(segment, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
    }
    
	// Use this for initialization
	void Start() {

	}
	
	// Update is called once per frame
	void Update() {
	
	}
}

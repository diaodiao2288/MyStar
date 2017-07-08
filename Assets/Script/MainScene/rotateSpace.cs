using UnityEngine;
using System.Collections;

public class rotateSpace : MonoBehaviour {
    public GameObject Axis;
    public float speed = -0.8F;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        this.transform.RotateAround(Axis.transform.position, new Vector3(0, 1, 0), speed);
    }
}

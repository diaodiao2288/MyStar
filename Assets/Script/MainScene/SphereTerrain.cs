using UnityEngine;
using System.Collections;

public class SphereTerrain : MonoBehaviour {
    public GameObject segment;
    public Material[] diffuseMap;
    public Texture2D[] heightMap;

    private SphereTerrainSegment _instance;
    private static Data GlobalInstance;
    private GameObject[] segments = new GameObject[6];

    private float time = 0;

    void Awake() {
        GlobalInstance = Data.GetInstance();
    }

	// Use this for initialization

	void Start () {
        //GlobalInstance.init();
        _instance = SphereTerrainSegment.GetInstance();
        for (int i = 0; i < 6; i++) {
            segments[i] = Instantiate(segment, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            segments[i].transform.SetParent(gameObject.transform);
            if (i == 0) {
                _instance.initMesh(segments[i], "up");
            }
            else if (i == 2) {
                _instance.initMesh(segments[i], "down");
            }
            else if (i == 3) {
                _instance.initMesh(segments[i], "front");
            }
            else if (i == 4) {
                _instance.initMesh(segments[i], "back");
            }
            else if (i == 5) {
                _instance.initMesh(segments[i], "left");
            } else {
                _instance.initMesh(segments[i], "right");
            }
            for (int j = 0; j < 5; j++) {
                _instance.divideSegment(segments[i]);
            }
            if (GlobalInstance.getInit() == 0) {
                _instance.drawMesh(segments[i], diffuseMap[3]);
            } else {
                _instance.drawMesh(segments[i], diffuseMap[i / 2]);
            }
            _instance.setVertives(segments[i], heightMap[i / 2]);
        }

	}
	
	// Update is called once per frame
	void Update () {
        /*time = time + Time.deltaTime;
        print(time);
        if (time >= 3 && time <= 4) {
            for (int i = 0; i < 6; i++) {
                print("aa");
                _instance.setVertives(segments[i], heightMap[1]);
                time = 5;
            }
            
        }*/
	}
}

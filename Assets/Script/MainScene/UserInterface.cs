using UnityEngine;
using System.Collections;
using Com.gameOne;

public class UserInterface : MonoBehaviour {
    private Data dataInstance;
    private DNALevel dnaLevelInstance;
    private controller cInstance;
    /*void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 80, 50), "跳转")) {
            Application.LoadLevel("DNA");
        }
    }*/

    void OnGUI() {
        GUILayout.BeginArea(new Rect(Screen.width / 40, Screen.height / 20, Screen.width / 12, Screen.height / 10));
        GUILayout.BeginHorizontal();
        GUILayout.Label("温度:");
        GUILayout.Label(dataInstance.getTempreture().ToString());
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Screen.width / 40, Screen.height / 10, Screen.width / 12, Screen.height / 10));
        GUILayout.BeginHorizontal();
        GUILayout.Label("水分:");
        GUILayout.Label(dataInstance.getWater().ToString() + "%");
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Screen.width / 40, Screen.height * 3 / 20, Screen.width / 12, Screen.height / 10));
        GUILayout.BeginHorizontal();
        GUILayout.Label("光照:");
        GUILayout.Label(dataInstance.getLightIntensity().ToString());
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Screen.width / 40, Screen.height / 5, Screen.width / 12, Screen.height / 10));
        GUILayout.BeginHorizontal();
        GUILayout.Label("生物:");
        GUILayout.Label(dataInstance.getLife().ToString());
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Screen.width / 40, Screen.height / 4, Screen.width / 12, Screen.height / 4));
        GUILayout.BeginVertical();
        if (GUILayout.Button("创造生物")) {
            dnaLevelInstance.setData(dataInstance.getLevel1());
            Application.LoadLevel("DNA");
        }
        if (GUILayout.Button("寻找光源")) {
            cInstance = controller.getInstance();
            Application.LoadLevel("aa");
        }
        if (GUILayout.Button("保存")) {
            dataInstance.saveData();
        }
        if (GUILayout.Button("重置")) {
            dataInstance.resetData();
        }
        if (GUILayout.Button("退出")) {
            Application.Quit();
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    void Awake() {
        dataInstance = Data.GetInstance();
        dnaLevelInstance = DNALevel.GetInstance();
    }

	// Use this for initialization
	void Start () {
        if (dataInstance.getInit() == 0) {
            dataInstance.init();
        }
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

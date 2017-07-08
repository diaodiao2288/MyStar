using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Try : MonoBehaviour {
    private Button btn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI() {
        //width 34
        GUILayout.BeginArea(new Rect(Screen.width / 20, Screen.height / 10, Screen.width * 9 / 10, Screen.height / 10));
        GUILayout.BeginHorizontal();
        GUILayout.Button("A");
        GUILayout.Button("T");
        GUILayout.Button("C");
        GUILayout.Button("G");
        //GUILayout.EndHorizontal();
        //GUILayout.BeginHorizontal();
        GUILayout.Button("A");
        GUILayout.Button("T");
        GUILayout.Button("C");
        GUILayout.Button("G");
        GUILayout.Button("A");
        GUILayout.Button("T");
        GUILayout.Button("C");
        GUILayout.Button("G");
        GUILayout.Button("A");
        GUILayout.Button("T");
        GUILayout.Button("C");
        GUILayout.Button("G");
        GUILayout.Button("A");
        GUILayout.Button("T");
        GUILayout.Button("C");
        GUILayout.Button("G");
        GUILayout.Button("A");
        GUILayout.Button("T");
        GUILayout.Button("C");
        GUILayout.Button("G");
        GUILayout.Button("A");
        GUILayout.Button("T");
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        //GUILayout.EndHorizontal();
        //GUILayout.BeginHorizontal();
        GUILayout.Button("A");
        GUILayout.Button("T");
        GUILayout.Button("C");
        GUILayout.Button("G");
        GUILayout.Button("A");
        GUILayout.Button("T");
        GUILayout.Button("C");
        GUILayout.Button("G");
        GUILayout.Button("A");
        GUILayout.Button("T");
        GUILayout.Button("C");
        GUILayout.Button("G");
        GUILayout.Button("A");
        GUILayout.Button("T");
        GUILayout.Button("C");
        GUILayout.Button("G");
        GUILayout.Button("A");
        GUILayout.Button("T");
        GUILayout.Button("C");
        GUILayout.Button("G");
        GUILayout.Button("A");
        GUILayout.Button("T");
        GUILayout.Button("C");
        GUILayout.Button("G");
        GUILayout.Button("A");
        GUILayout.Button("T");
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        float x = Screen.width / 20;
        float y = Screen.height / 2;
        GUILayout.BeginArea(new Rect(x, y, Screen.width * 1 / 40 * 4, Screen.height / 10));
        GUILayout.BeginHorizontal();
        GUILayout.Button("G");
        GUILayout.Button("A");
        GUILayout.Button("G");
        GUILayout.Button("A");
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(x + Screen.width * 1 / 40 * 4 + Screen.width / 20, y, Screen.width * 1 / 40 * 4, Screen.height / 10));
        GUILayout.BeginHorizontal();
        GUILayout.Button("G");
        GUILayout.Button("A");
        GUILayout.Button("G");
        GUILayout.Button("A");
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(x, y + Screen.height / 10, Screen.width * 1 / 40 * 4, Screen.height / 10));
        GUILayout.BeginHorizontal();
        GUILayout.Button("G");
        GUILayout.Button("A");
        GUILayout.Button("G");
        GUILayout.Button("A");
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DNAController : MonoBehaviour {
    public static DNAController Instance;

    private static DNAJudgement judgeInstance;
    private ArrayList up;
    private ArrayList down;
    private ArrayList fragment;
    private int mouseType = 0;
    private int score;

    private string mouseString;
    private int fragmentIndex;

    public static DNAController GetInstacne() {
        if (Instance == null) {
            Instance = new DNAController();
        }
        return Instance;
    }

    public void sendUp(ArrayList _up) {
        up = _up;
    }

    public void sendDown(ArrayList _down) {
        down = _down;
    }

    public void sendFragment(ArrayList _fragment) {
        fragment = _fragment;
    }

    public int getScore() {
        return score;
    }

    public void On_fragment_button(int index) {
        mouseString = (string)fragment[index];
        mouseType = 1;
        fragmentIndex = index;
    }

    public void On_DNA_button(string updown, int index) {
        if (mouseType == 1 && mouseString != "") {
            int strlen = mouseString.Length;
            bool isEmpty = true;
            if (updown == "up") {
                for (int i = 0; i < strlen; i++) {
                    if ((string)up[index + i] != "") {
                        isEmpty = false;
                        break;
                    }
                }
                if (isEmpty) {
                    for (int i = 0; i < strlen; i++) {
                        up[index + i] = mouseString.Substring(i, 1);
                    }
                    fragment[fragmentIndex] = "";
                    judgeInstance.sendUp(up);
                    judgeInstance.sendDown(down);
                    score = judgeInstance.judgeScore();
                }
            } else {
                for (int i = 0; i < strlen; i++) {
                    if ((string)down[index + i] != "") {
                        isEmpty = false;
                        break;
                    }
                }
                if (isEmpty) {
                    for (int i = 0; i < strlen; i++) {
                        down[index + i] = mouseString.Substring(i, 1);
                    }
                    fragment[fragmentIndex] = "";
                    judgeInstance.sendUp(up);
                    judgeInstance.sendDown(down);
                    score = judgeInstance.judgeScore();
                }
            } 
        }
    }

    void Awake() {
        judgeInstance = DNAJudgement.GetInstance();
    }

	// Use this for initialization
	void Start () {
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

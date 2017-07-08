using UnityEngine;
using System.Collections;

public class DNAJudgement : MonoBehaviour {
    public static DNAJudgement Instance;
    private static int initscore;
    private int score;
    private ArrayList up;
    private ArrayList down;

    public static DNAJudgement GetInstance() {
        if (Instance == null) {
            Instance = new DNAJudgement();
        }
        return Instance;
    }

    public void sendUp(ArrayList _up) {
        up = _up;
    }

    public void sendDown(ArrayList _down) {
        down = _down;
    }

    public void initScore() {
        initscore = 0;
        score = 0;
        for (int i = 0; i < up.Count; i++) {
            if ((string)up[i] == "A" && (string)down[i] == "T" || (string)up[i] == "T" && (string)down[i] == "A" || 
                (string)up[i] == "C" && (string)down[i] == "G" || (string)up[i] == "G" && (string)down[i] == "C") {
                initscore++;
            }
        }
    }

    public int judgeScore() {
        int tempScore = 0;
        
        for (int i = 0; i < up.Count; i++) {
            if ((string)up[i] == "A" && (string)down[i] == "T" || (string)up[i] == "T" && (string)down[i] == "A" ||
                (string)up[i] == "C" && (string)down[i] == "G" || (string)up[i] == "G" && (string)down[i] == "C") {
                tempScore++;
            }
        }
        score = tempScore - initscore;
        return score;
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

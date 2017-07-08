using UnityEngine;
using System.Collections;

public class DNALevel : MonoBehaviour {
    public static DNALevel Instance; 

    private static int DNANum;
    private static int numUp;
    private static int numDown;
    private static float probability;
    private static float time;

    public static DNALevel GetInstance() {
        if (Instance == null) {
            Instance = new DNALevel();
        }
        return Instance;
    }

    public void setData(int level) {
        if (level == 0) {
            DNANum = 8;
            numUp = Mathf.FloorToInt(Random.Range(0, 100 * DNANum)) % (DNANum - 3) + 1;
            numDown = Mathf.FloorToInt(Random.Range(0, 100 * DNANum)) % (DNANum - 3) + 1;
            probability = 0.5F;
            time = 100;
        }
        else if (level == 1) {
            DNANum = 15;
            numUp = Mathf.FloorToInt(Random.Range(0, 100 * DNANum)) % (DNANum - 5) + 1;
            numDown = Mathf.FloorToInt(Random.Range(0, 100 * DNANum)) % (DNANum - 5) + 1;
            probability = 0.6F;
            time = 80;
        }
        else if (level == 2) {
            DNANum = 20;
            numUp = Mathf.FloorToInt(Random.Range(0, 100 * DNANum)) % (DNANum - 7) + 1;
            numDown = Mathf.FloorToInt(Random.Range(0, 100 * DNANum)) % (DNANum - 7) + 1;
            probability = 0.7F;
            time = 60;
        } else {
            DNANum = 25;
            numUp = Mathf.FloorToInt(Random.Range(0, 100 * DNANum)) % (DNANum - 7) + 1;
            numDown = Mathf.FloorToInt(Random.Range(0, 100 * DNANum)) % (DNANum - 7) + 1;
            probability = 0.80F;
            time = 40;
        }
    }

    public int getDNANum() {
        return DNANum;
    }

    public int getNumUp() {
        return numUp;
    }

    public int getNumDown() {
        return numDown;
    }

    public float getProbability() {
        return probability;
    }

    public float getTime() {
        return time;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

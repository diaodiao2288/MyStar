using UnityEngine;
using System.Collections;

public class DNAData : MonoBehaviour {
    public static DNAData Instance;

    private ArrayList up = new ArrayList();
    private ArrayList down = new ArrayList();
    
    //private ArrayList fragment = new ArrayList();
    private ArrayList upFragment = new ArrayList();
    private ArrayList downFragment = new ArrayList();

    public static DNAData GetInstance() {
        if (Instance == null) {
            Instance = new DNAData();
        }
        return Instance;
    }

    public void clearData() {
        up.Clear();
        down.Clear();
        //fragment.Clear();
        upFragment.Clear();
        downFragment.Clear();
    }

    public ArrayList getUp() {
        return up;
    }

    public ArrayList getDown() {
        return down;
    }

    public ArrayList getUpFragment() {
        return upFragment;
    }

    public ArrayList getDownFragment() {
        return downFragment;
    }

    //初始化DNA链
    public void genRandom(int num) {
        for (int i = 0; i < num; i++) {
            int ran = Mathf.FloorToInt(Random.Range(1, 1000));
            string tempUp;
            string tempDown;
            if (ran % 4 == 0) {
                tempUp = "A";
                tempDown = "T";    
            }
            else if (ran % 4 == 1) {
                tempUp = "T";
                tempDown = "A";
            }
            else if (ran % 4 == 2) {
                tempUp = "C";
                tempDown = "G";
            } else {
                tempUp = "G";
                tempDown = "C";
            }
            up.Add(tempUp);
            down.Add(tempDown);
        }
    }

    //产生DNA碎片
    public void genFragmentRandom(int numUp, int numDown, float probability) {
        if (numUp >= up.Count || numDown >= down.Count) {
            Debug.Log("Fragment Wrong");
            return;
        }
        //产生单个碎片
        int choosenNum = 0;
        for (int i = 0; i < up.Count; i++) {
            if (choosenNum < numUp) {
                float pro = (float)(numUp - choosenNum) / (float)(up.Count - i);
                float ran = Random.Range(0, 100) / 100F;
                if (pro >= ran) {
                    upFragment.Add(i);
                    choosenNum++;
                }
            } else {
                i = up.Count;
            }
        }

        choosenNum = 0;
        for (int i = 0; i < down.Count; i++) {
            if (choosenNum < numDown) {
                float pro = (float)(numDown - choosenNum) / (float)(down.Count - i);
                float ran = Random.Range(0, 10000) / 10000F;
                if (pro >= ran) {
                    downFragment.Add(i);
                    choosenNum++;
                }
            } else {
                i = up.Count;
            }
        }

        //按一定概率连接相邻的碎片
        ArrayList temp = new ArrayList();
        temp.Add(upFragment[0]);
        for (int i = 0; i < upFragment.Count - 1; i++) {
            if ((int)upFragment[i + 1] - (int)upFragment[i] == 1) {
                float ran = Random.Range(0, 1000) / 1000F;
                if (probability >= ran) {
                    temp.Add(temp[i]);
                } else {
                    temp.Add(upFragment[i + 1]);
                }
            } else {
                temp.Add(upFragment[i + 1]);
            }
        }
        upFragment.Clear();
        for (int i = 0; i < temp.Count; i++) {
            upFragment.Add(temp[i]);
        }
        temp.Clear();

        temp.Add(downFragment[0]);
        for (int i = 0; i < downFragment.Count - 1; i++) {
            if ((int)downFragment[i + 1] - (int)downFragment[i] == 1) {
                float ran = Random.Range(0, 1000) / 1000F;
                if (probability >= ran) {
                    temp.Add(temp[i]);
                } else {
                    temp.Add(downFragment[i + 1]);
                }
            } else {
                temp.Add(downFragment[i + 1]);
            }
        }
        downFragment.Clear();
        for (int i = 0; i < temp.Count; i++) {
            downFragment.Add(temp[i]);
        }
        temp.Clear();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

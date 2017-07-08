using UnityEngine;
using System.Collections;

public class DNAGenGameObject : MonoBehaviour {
    private DNAData dataInstance;
    private DNAController controllerInstacne;
    private DNAJudgement judgeInstance;
    private DNALevel levelInstance;

    private static Data GlobalInstance;

    private ArrayList up = new ArrayList();
    private ArrayList down = new ArrayList();
    private ArrayList upFragment = new ArrayList();
    private ArrayList downFragment = new ArrayList();
    private ArrayList updown = new ArrayList();
    private ArrayList Fragment = new ArrayList();

    private float time;
    private int score;
    private int succScore;
    private void gameButton() {
        int baseIndex = 0;
        for (int i = 0; i < upFragment.Count;) {
            baseIndex = (int)upFragment[i];
            int pace = 0;
            //print(upFragment.Count);
            do {
                pace++;
                i++;

            } while (i < upFragment.Count && (int)upFragment[i] == baseIndex);
            for (int k = 0; k < pace; k++) {
                up[baseIndex + k] = "";
            }
        }

        baseIndex = 0;
        for (int i = 0; i < downFragment.Count;) {
            baseIndex = (int)downFragment[i];
            int pace = 0;
            do {
                pace++;
                i++;
            } while (i < downFragment.Count && (int)downFragment[i] == baseIndex);
            for (int k = 0; k < pace; k++) {
                down[baseIndex + k] = "";
            }
        }
    }
    //随机化碎片
    private void randomFragment() {
        ArrayList tempFragment = new ArrayList();
        while (tempFragment.Count != upFragment.Count) {
            int ran = Mathf.FloorToInt(Random.Range(0, upFragment.Count * 100)) % upFragment.Count;
            int key = (int)upFragment[ran];
            int num;
            if (tempFragment.IndexOf(upFragment[ran]) == -1) {
                num = 1;
                for (int i = ran + 1; i < upFragment.Count; i++) {
                    if ((int)upFragment[i] == key) {
                        num++;
                    } else {
                        i = upFragment.Count;
                    }
                }
                for (int i = ran - 1; i >= 0; i--) {
                    if ((int)upFragment[i] == key) {
                        num++;
                    } else {
                        i = -1;
                    }
                }
                for (int i = 0; i < num; i++) {
                    tempFragment.Add(upFragment[ran]);
                }
            }
        }
        upFragment.Clear();
        for (int i = 0; i < tempFragment.Count; i++) {
            upFragment.Add(tempFragment[i]);
        }

        tempFragment.Clear();

        while (tempFragment.Count != downFragment.Count) {
            int ran = Mathf.FloorToInt(Random.Range(0, downFragment.Count * 100)) % downFragment.Count;
            int key = (int)downFragment[ran];
            int num;
            if (tempFragment.IndexOf(downFragment[ran]) == -1) {
                num = 1;
                for (int i = ran + 1; i < downFragment.Count; i++) {
                    if ((int)downFragment[i] == key) {
                        num++;
                    } else {
                        i = downFragment.Count;
                    }
                }
                for (int i = ran - 1; i >= 0; i--) {
                    if ((int)downFragment[i] == key) {
                        num++;
                    } else {
                        i = -1;
                    }
                }
                for (int i = 0; i < num; i++) {
                    tempFragment.Add(downFragment[ran]);
                }
            }
        }
        downFragment.Clear();
        for (int i = 0; i < tempFragment.Count; i++) {
            downFragment.Add(tempFragment[i]);
        }
        tempFragment.Clear();
    }

    private void indexTostring() {
        ArrayList upTemp = new ArrayList();
        ArrayList downTemp = new ArrayList();
        ArrayList temp = dataInstance.getUp();
        for (int i = 0; i < temp.Count; i++) {
            upTemp.Add(temp[i]);
        }
        temp = dataInstance.getDown();
        for (int i = 0; i < temp.Count; i++) {
            downTemp.Add(temp[i]);
        }

        ArrayList tempArray = new ArrayList();
        for (int i = 0; i < upFragment.Count;) {
            string tempString = (string)upTemp[(int)upFragment[i]];
            int j = i;
            i++;
            while (i < upFragment.Count && (int)upFragment[i] == (int)upFragment[i - 1]) {
                tempString = tempString + (string)upTemp[(int)upFragment[i] + i - j];
                i++;
            }
            tempArray.Add(tempString);
        }
        upFragment.Clear();
        for (int i = 0; i < tempArray.Count; i++) {
            upFragment.Add(tempArray[i]);
            //print(tempArray[i]);
        }
        tempArray.Clear();

        for (int i = 0; i < downFragment.Count;) {
            string tempString = (string)downTemp[(int)downFragment[i]];
            int j = i;
            i++;
            while (i < downFragment.Count && (int)downFragment[i] == (int)downFragment[i - 1]) {
                tempString = tempString + (string)downTemp[(int)downFragment[i] + i - j];
                i++;
            }
            tempArray.Add(tempString);
        }
        downFragment.Clear();
        for (int i = 0; i < tempArray.Count; i++) {
            downFragment.Add(tempArray[i]);
            //print(tempArray[i]);
        }
        tempArray.Clear();
    }

    //摆放碎片随机化
    private void updownRandom() {
        /*int upSize = upFragment.Count;
        int downSize = downFragment.Count;
        for (int i = 0, j = 0; ;) {
            if (i >= upFragment.Count && j >= downFragment.Count) {
                break;
            }
            print(upFragment.Count);
            print(i);
            float ran = (float)Random.Range(0, 100) / 100F;
            if (ran >= (float)upSize / (float)(upSize + downSize) && i < upFragment.Count) {
                updown.Add(1);
                i++;
                upSize--;
                while (i < upFragment.Count && (int)upFragment[i] == (int)upFragment[i - 1]) {
                    i++;
                    upSize--;
                }
            } else {
                if (j < downFragment.Count) {
                    updown.Add(0);
                    j++;
                    downSize--;
                    while (j < downFragment.Count && (int)downFragment[j] == (int)downFragment[j - 1]) {
                        j++;
                        downSize--;
                    }
                }
            }
        }*/
        int upSize = upFragment.Count;
        int downSize = downFragment.Count;
        for (int i = 0, j = 0; ;) {
            if (i >= upFragment.Count && j >= downFragment.Count) {
                break;
            }
            float ran = (float)Random.Range(0, 100) / 100F;
            if (ran >= (float)upSize / (float)(upSize + downSize)) {
                if (i < upFragment.Count) {
                    updown.Add(1);
                    i++;
                }
            } else {
                if (j < downFragment.Count) {
                    updown.Add(0);
                    j++;
                }
            }
        }

        int upIndex = 0;
        int downIndex = 0;
        for (int i = 0; i < updown.Count; i++) {
            if ((int)updown[i] == 1) {
                Fragment.Add(upFragment[upIndex]);
                upIndex++;
            } else {
                Fragment.Add(downFragment[downIndex]);
                downIndex++;
            }
        }
    }

    void Awake() {
        dataInstance = DNAData.GetInstance();
        controllerInstacne = DNAController.GetInstacne();
        judgeInstance = DNAJudgement.GetInstance();
        levelInstance = DNALevel.GetInstance();
        GlobalInstance = Data.GetInstance();
    }

	// Use this for initialization
	void Start () {
        time = levelInstance.getTime();
        score = 0;

        dataInstance.genRandom(levelInstance.getDNANum()); //34以下
        dataInstance.genFragmentRandom(levelInstance.getNumUp(), levelInstance.getNumDown(), levelInstance.getProbability());

        int tempmax = 0;
        int tempmin = 0;
        if (levelInstance.getNumUp() > levelInstance.getNumDown()) {
            tempmax = levelInstance.getNumUp();
            tempmin = levelInstance.getNumDown();
        } else {
            tempmax = levelInstance.getNumDown();
            tempmin = levelInstance.getNumUp();
        }
        succScore = tempmax + tempmin / 2;

            ArrayList temp = dataInstance.getUp();
        for (int i = 0; i < temp.Count; i++) {
            up.Add(temp[i]);
        }

        temp = dataInstance.getDown();
        for (int i = 0; i < temp.Count; i++) {
            down.Add(temp[i]);
        }

        temp = dataInstance.getUpFragment();
        for (int i = 0; i < temp.Count; i++) {
            upFragment.Add(temp[i]);
        }

        temp = dataInstance.getDownFragment();
        for (int i = 0; i < temp.Count; i++) {
            downFragment.Add(temp[i]);
        }

        gameButton();
        randomFragment();
        indexTostring();
        updownRandom();

        controllerInstacne.sendUp(up);
        controllerInstacne.sendDown(down);
        controllerInstacne.sendFragment(Fragment);

        judgeInstance.sendUp(up);
        judgeInstance.sendDown(down);
        judgeInstance.initScore();

    }
	
	// Update is called once per frame
	void Update () {
        time = time - Time.deltaTime;
        if (time <= 0) {
            if (score >= succScore) {
                if (GlobalInstance.getLevel1() < 4) {
                    GlobalInstance.setLevel1(GlobalInstance.getLevel1() + 1);
                    GlobalInstance.setTempreture(GlobalInstance.getTempreture() + 2);
                    GlobalInstance.setWater(GlobalInstance.getWater() + 4);
                    GlobalInstance.setLife(GlobalInstance.getLife() + 5);
                    GlobalInstance.setLightIntensity(GlobalInstance.getLightIntensity() + 10);
                    GlobalInstance.setInit(GlobalInstance.getInit() + 1);
                }
            }
            Application.LoadLevel("test");
        }
    }

    void OnGUI() {
        
        GUILayout.BeginArea(new Rect(Screen.width / 40, Screen.height / 20, Screen.width / 12, Screen.height / 10));
        GUILayout.BeginHorizontal();
        GUILayout.Label("Time:");
        GUILayout.Label(Mathf.FloorToInt(time).ToString());
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Screen.width / 40, Screen.height / 10, Screen.width / 12, Screen.height / 10));
        GUILayout.BeginHorizontal();
        GUILayout.Label("Score:");
        GUILayout.Label(score.ToString());
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Screen.width / 20, Screen.height / 5, Screen.width * 9 / 10, Screen.height / 10));
        GUILayout.BeginHorizontal();     
        for (int i = 0; i < up.Count; i++) {  
            if (GUILayout.Button((string)up[i])) {
                controllerInstacne.On_DNA_button("up", i);
                score = controllerInstacne.getScore();
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        for (int i = 0; i < down.Count; i++) {
            if (GUILayout.Button((string)down[i])) {
                controllerInstacne.On_DNA_button("down", i);  
                score = controllerInstacne.getScore();
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Screen.width / 40, Screen.height * 3 / 20, Screen.width / 12, Screen.height / 10));
        if (GUILayout.Button("结束游戏")) {
            //if (score >= succScore / 2) {
                if (GlobalInstance.getLevel1() < 4) {
                    GlobalInstance.setLevel1(GlobalInstance.getLevel1() + 1);
                    GlobalInstance.setTempreture(GlobalInstance.getTempreture() + 2);
                    GlobalInstance.setWater(GlobalInstance.getWater() + 4);
                    GlobalInstance.setLife(GlobalInstance.getLife() + 5);
                    GlobalInstance.setLightIntensity(GlobalInstance.getLightIntensity() + 10);
                    GlobalInstance.setInit(GlobalInstance.getInit() + 1);
                }
            //}
            Application.LoadLevel("test");
        }
        GUILayout.EndArea();

        float x = Screen.width / 20;
        float y = Screen.height / 2;
        float width = Screen.width / 16;
        float height = Screen.height / 15;
        float deltaX = Screen.height / 10;
        float deltaY = Screen.height / 10;

        int row = 0;
        int column = 0;

        for (int i = 0; i < updown.Count; i++) {
            float tempX = x + column * (width + deltaX);
            float tempY;
            column++;
            if (tempX + width > Screen.width * 19 / 20) {
                row++;
                tempX = x;
                column = 1;
            }
            tempY = y + row * (height + deltaY);
            /*if ((int)updown[i] == 1) {
                GUILayout.BeginArea(new Rect(tempX, tempY, width, height));
                if (GUILayout.Button((string)upFragment[upIndex])) {

                }
                GUILayout.EndArea();
            } else {
                GUILayout.BeginArea(new Rect(tempX, tempY, width, height));
                if (GUILayout.Button((string)downFragment[downIndex])) {

                }
                GUILayout.EndArea();
            }*/
            GUILayout.BeginArea(new Rect(tempX, tempY, width, height));
            if (GUILayout.Button((string)Fragment[i])) {
                controllerInstacne.On_fragment_button(i);
            }
            GUILayout.EndArea();
        }
    }
}

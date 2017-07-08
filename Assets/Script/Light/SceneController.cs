using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.gameOne;

namespace Com.gameOne
{
    public class controller: System.Object
    {
        public GameObject wormHoleIn;
        public GameObject wormHoleOut;
        public GameObject blackHole1;
        public GameObject blackHole2;
        public GameObject sun;
        public GameObject target;
        public GameObject obstacle;
        public GameObject glaxy;

        private static int currentLevel;
        private static controller instance;
        private static List<GameObject> wormHoleInList;
        private static List<GameObject> wormHoleOutList;
        private static GameObject sunObject;
        private static GameObject targetObject;
        private static GameObject glaxyObeject;
        private static List<GameObject> blackHoleOneInList;
        private static List<GameObject> blackHoleTwoInList;
        private static List<GameObject> obstacleObject;
        private static List<float> inAngle;
        private static List<float> outAngle;
        private static bool end;
        
        public static controller getInstance()
        {
            if (instance == null)
            {
                instance = new controller();
                wormHoleInList = new List<GameObject>();
                wormHoleOutList = new List<GameObject>();
                blackHoleOneInList = new List<GameObject>();
                blackHoleTwoInList = new List<GameObject>();
                obstacleObject = new List<GameObject>();
                inAngle = new List<float>();
                outAngle = new List<float>();
                currentLevel = 1;
                end = false;
            }
            return instance;
        }

        public bool getStatus()
        {
            return end;
        }

        public void setStatus(bool s)
        {
            end = s;
        }

        // 返回匹配的虫洞 0 for in , 1 for out
        public int pairOfwormHole(GameObject wormhole, int type)
        {
            if (type == 0) {
                for (int i = 0; i<wormHoleInList.Count; i++)
                {
                    if (wormhole.GetHashCode() == wormHoleInList[i].GetHashCode())
                        return i;
                }
            }
            else
            {
                for (int i = 0; i < wormHoleOutList.Count; i++)
                {
                    if (wormhole.GetHashCode() == wormHoleOutList[i].GetHashCode())
                        return i;
                }
            }
            return -1;
        }

        public float getInAngle(int index)
        {
            return inAngle[index];
        }

        public float getOutAngle(int index)
        {
            return outAngle[index];
        }

        public GameObject getInHole(int index)
        {
            return wormHoleInList[index];
        }

        public GameObject getOutHole(int index)
        {
            return wormHoleOutList[index];
        }

        public void free()
        {
            GameObject.Destroy(sunObject);
            GameObject.Destroy(targetObject);
            GameObject.Destroy(glaxyObeject);
            for (int i =0; i < blackHoleOneInList.Count; i++)
            {
                GameObject.Destroy(blackHoleOneInList[i]);
            }
            for (int i = 0; i < blackHoleTwoInList.Count; i++)
            {
                GameObject.Destroy(blackHoleTwoInList[i]);
            }
            for (int i = 0; i < obstacleObject.Count; i++)
            {
                GameObject.Destroy(obstacleObject[i]);
            }
            for (int i = 0; i < wormHoleInList.Count; i++)
            {
                GameObject.Destroy(wormHoleInList[i]);
            }
            for (int i = 0; i < wormHoleOutList.Count; i++)
            {
                GameObject.Destroy(wormHoleOutList[i]);
            }
            blackHoleOneInList.Clear();
            blackHoleTwoInList.Clear();
            obstacleObject.Clear();
            wormHoleInList.Clear();
            wormHoleOutList.Clear();
            inAngle.Clear();
            outAngle.Clear();
        }

        public void loadScene(int level)
        {
            switch(level)
            {
                case 1:
                    //
                    sunObject = GameObject.Instantiate(sun, new Vector3(4, 0, 0), Quaternion.identity) as GameObject;
                    blackHoleOneInList.Add(GameObject.Instantiate(blackHole1, new Vector3(-0.27f, 0.55f, 0), Quaternion.identity) as GameObject);
                    blackHoleOneInList.Add(GameObject.Instantiate(blackHole2, new Vector3(1.94f, 0.33f, 0), Quaternion.identity) as GameObject);
                    targetObject = GameObject.Instantiate(target, new Vector3(-4.8f, 2.2f, 0), Quaternion.identity) as GameObject;
                    obstacleObject.Add(GameObject.Instantiate(obstacle, new Vector3(-1.5f, 1.5f, 0), Quaternion.identity) as GameObject);
                    obstacleObject.Add(GameObject.Instantiate(obstacle, new Vector3(0.91f, 1.13f, 0), Quaternion.identity) as GameObject);
                    glaxyObeject = GameObject.Instantiate(glaxy, new Vector3(1.1f, -0.88f, 0), Quaternion.identity) as GameObject;
                    //
                    wormHoleInList.Add(GameObject.Instantiate(wormHoleIn, new Vector3(4, -2, 0), Quaternion.identity) as GameObject);
                    wormHoleInList[0].transform.Rotate(new Vector3(0, 50, 0));
                    wormHoleInList[0].transform.Rotate(Vector3.forward, -90, Space.World);
                    inAngle.Add(-90);
                    wormHoleInList.Add(GameObject.Instantiate(wormHoleIn, new Vector3(-4, -2, 0), Quaternion.identity) as GameObject);
                    wormHoleInList[1].transform.Rotate(new Vector3(0, 50, 0));
                    wormHoleInList[1].transform.Rotate(Vector3.forward, -135, Space.World);
                    inAngle.Add(-135);
                    wormHoleOutList.Add(GameObject.Instantiate(wormHoleOut, new Vector3(2, 2, 0), Quaternion.identity) as GameObject);
                    wormHoleOutList[0].transform.Rotate(new Vector3(0, 50, 0));
                    wormHoleOutList[0].transform.Rotate(Vector3.forward, 30, Space.World);
                    outAngle.Add(30);
                    wormHoleOutList.Add(GameObject.Instantiate(wormHoleOut, new Vector3(-3, 1, 0), Quaternion.identity) as GameObject);
                    wormHoleOutList[1].transform.Rotate(new Vector3(0, 50, 0));
                    wormHoleOutList[1].transform.Rotate(Vector3.forward, -45, Space.World);
                    outAngle.Add(-45);
                    break;
                case 2:
                    this.free();
                    sunObject = GameObject.Instantiate(sun, new Vector3(-4.5f, 2, 0), Quaternion.identity) as GameObject;
                    blackHoleOneInList.Add(GameObject.Instantiate(blackHole1, new Vector3(-4f, -0.15f, 0), Quaternion.identity) as GameObject);
                    blackHoleOneInList.Add(GameObject.Instantiate(blackHole1, new Vector3(0.45f, 1.54f, 0), Quaternion.identity) as GameObject);
                    blackHoleTwoInList.Add(GameObject.Instantiate(blackHole2, new Vector3(-1.21f, 1.33f, 0), Quaternion.identity) as GameObject);
                    blackHoleTwoInList.Add(GameObject.Instantiate(blackHole2, new Vector3(1.04f, -0.71f, 0), Quaternion.identity) as GameObject);
                    targetObject = GameObject.Instantiate(target, new Vector3(4.59f, -2.07f, 0), Quaternion.identity) as GameObject;
                    //obstacleObject.Add(GameObject.Instantiate(obstacle, new Vector3(-1.5f, 1.5f, 0), Quaternion.identity) as GameObject);
                    //obstacleObject.Add(GameObject.Instantiate(obstacle, new Vector3(0.91f, 1.13f, 0), Quaternion.identity) as GameObject);
                    //glaxyObeject = GameObject.Instantiate(glaxy, new Vector3(1.1f, -0.88f, 0), Quaternion.identity) as GameObject;
                    //
                    wormHoleInList.Add(GameObject.Instantiate(wormHoleIn, new Vector3(-2, 0, 0), Quaternion.identity) as GameObject);
                    wormHoleInList[0].transform.Rotate(new Vector3(0, 50, 0));
                    wormHoleInList[0].transform.Rotate(Vector3.forward, -90, Space.World);
                    inAngle.Add(-90);
                    wormHoleInList.Add(GameObject.Instantiate(wormHoleIn, new Vector3(2.5f, 2, 0), Quaternion.identity) as GameObject);
                    wormHoleInList[1].transform.Rotate(new Vector3(0, 50, 0));
                    wormHoleInList[1].transform.Rotate(Vector3.forward, 0, Space.World);
                    inAngle.Add(0);
                    wormHoleInList.Add(GameObject.Instantiate(wormHoleIn, new Vector3(2, 0, 0), Quaternion.identity) as GameObject);
                    wormHoleInList[2].transform.Rotate(new Vector3(0, 50, 0));
                    wormHoleInList[2].transform.Rotate(Vector3.forward, 90, Space.World);
                    inAngle.Add(90);
                    wormHoleOutList.Add(GameObject.Instantiate(wormHoleOut, new Vector3(-2, -2.3f, 0), Quaternion.identity) as GameObject);
                    wormHoleOutList[0].transform.Rotate(new Vector3(0, 50, 0));
                    wormHoleOutList[0].transform.Rotate(Vector3.forward, -180, Space.World);
                    outAngle.Add(-180);
                    wormHoleOutList.Add(GameObject.Instantiate(wormHoleOut, new Vector3(-4, -2, 0), Quaternion.identity) as GameObject);
                    wormHoleOutList[1].transform.Rotate(new Vector3(0, 50, 0));
                    wormHoleOutList[1].transform.Rotate(Vector3.forward, -120, Space.World);
                    outAngle.Add(-120);
                    wormHoleOutList.Add(GameObject.Instantiate(wormHoleOut, new Vector3(3, -1, 0), Quaternion.identity) as GameObject);
                    wormHoleOutList[2].transform.Rotate(new Vector3(0, 50, 0));
                    wormHoleOutList[2].transform.Rotate(Vector3.forward, -225, Space.World);
                    outAngle.Add(-225);
                    break;
                case 3:
                    break;
            }
        }
    }

}

public class SceneController : MonoBehaviour  {
    public GameObject wormHoleIn;
    public GameObject wormHoleOut;
    public GameObject blackHole1;
    public GameObject blackHole2;
    public GameObject sun;
    public GameObject target;
    public GameObject obstacle;
    public GameObject glaxy;

    private controller cInstance;
    private static Data dataInstance;
    

// Use this for initialization
void Start () {
        cInstance = controller.getInstance();
        dataInstance = Data.GetInstance();
        cInstance.blackHole1 = blackHole1;
        cInstance.blackHole2 = blackHole2;
        cInstance.wormHoleIn = wormHoleIn;
        cInstance.wormHoleOut = wormHoleOut;
        cInstance.sun = sun;
        cInstance.target = target;
        cInstance.glaxy = glaxy;
        cInstance.obstacle = obstacle;
        cInstance.loadScene(dataInstance.getLevel2());
        cInstance.setStatus(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (cInstance.getStatus() == true)
        {
            if (dataInstance.getLevel2() < 3)
            {
                dataInstance.setLevel2(dataInstance.getLevel2() + 1);
                dataInstance.setTempreture(dataInstance.getTempreture() + 2);
                dataInstance.setWater(dataInstance.getWater() + 4);
                dataInstance.setLife(dataInstance.getLife() + 5);
                dataInstance.setLightIntensity(dataInstance.getLightIntensity() + 10);
                dataInstance.setInit(dataInstance.getInit() + 1);
            }
            cInstance.setStatus(false);
            Application.LoadLevel("test");
        }

	}
}

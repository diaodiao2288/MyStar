using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.gameOne;

namespace Com.gameOne
{
    public class lightFactory : System.Object
    {
        public GameObject lightPrefab;
        private static lightFactory instance;
        private static List<GameObject> lights;
        private static List<Vector3> positions;
        private static List<Vector3> directions;

        public static lightFactory getInstance()
        {
            if (instance == null)
            {
                instance = new lightFactory();
                lights = new List<GameObject>();
                directions = new List<Vector3>();
                positions = new List<Vector3>();
            }
            return instance;
        }

        public void newLight(int index, Vector3 position, Vector3 direction)
        {
            if (index >= lights.Count)
            {
                lights.Add(GameObject.Instantiate(lightPrefab, position, Quaternion.identity) as GameObject);
                directions.Add(direction);
                positions.Add(position);
            }
            else
            {
                lights[index].transform.position = position;
                lights[index].SetActive(true);
                positions[index] = position;
                directions[index] = direction;
            }
        }

        public void remove(int index)
        {
            int count = lights.Count - index;
            for (int i = index; i < lights.Count; i++)
            {
                lights[i].SetActive(false);
            }
        }

        public GameObject getObject(int index)
        {
            return lights[index];
        }

        public Vector3 getDirection(int index)
        {
            return directions[index];
        }

        public Vector3 getPosition(int index)
        {
            return positions[index];
        }

        public int getNum()
        {
            return lights.Count;
        }
    }
}
public class LightFactoryBC : MonoBehaviour
{
    public GameObject light;

    private void Awake()
    {
        lightFactory.getInstance().lightPrefab = light;
    }
}

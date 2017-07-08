using UnityEngine;
using System.Collections;

public class Data : MonoBehaviour {
    public static Data Instance;

    private static int Game1Level;
    private static int Game2Level;
    private static int Game3Level;

    private static float water;
    private static float tempreture;
    private static float lightIntensity;
    private static float life;

    private static int isInit;

    public static Data GetInstance() {
        if (Instance == null) {
            Instance = new Data();
        }
        return Instance;
    }

    public void init() {
        int temp = PlayerPrefs.GetInt("IsInit");
        if (temp == 1) {
            loadData();
        } else {
            water = 10;
            tempreture = -5;
            lightIntensity = 100;
            life = 10;
            isInit = 0;
            Game1Level = 1;
            Game2Level = 1;
            Game3Level = 1;
            PlayerPrefs.SetFloat("Water", water);
            PlayerPrefs.SetFloat("Tempreture", tempreture);
            PlayerPrefs.SetFloat("LightInstensity", lightIntensity);
            PlayerPrefs.SetFloat("Life", life);
            PlayerPrefs.SetInt("Game1", 1);
            PlayerPrefs.SetInt("Game2", 1);
            PlayerPrefs.SetInt("Game3", 1);
            PlayerPrefs.SetInt("IsInit", isInit);
        }
    }

    public void setWater(float _water) {
        water = _water;
    }

    public void setTempreture(float _tempreture) {
        tempreture = _tempreture;
    }

    public void setLightIntensity(float _lightIntensity) {
        lightIntensity = _lightIntensity;
    }

    public void setLife(float _life) {
        life = _life;
    }

    public void setLevel1(int _level) {
        Game1Level = _level;
    }

    public void setLevel2(int _level) {
        Game2Level = _level;
    }

    public void setLevel3(int _level) {
        Game3Level = _level;
    }

    public void setInit(int _isInit) {
        isInit = _isInit;
    }

    public float getWater() {
        return water;
    }

    public float getTempreture() {
        return tempreture;
    }

    public float getLightIntensity() {
        return lightIntensity;
    }

    public float getLife() {
        return life;
    }

    public int getLevel1() {
        return Game1Level;
    }

    public int getLevel2() {
        return Game2Level;
    }

    public int getLevel3() {
        return Game3Level;
    }

    public int getInit() {
        return isInit;
    }

    public void saveData() {
        PlayerPrefs.SetFloat("Water", water);
        PlayerPrefs.SetFloat("Tempreture", tempreture);
        PlayerPrefs.SetFloat("LightInstensity", lightIntensity);
        PlayerPrefs.SetFloat("Life", life);
        PlayerPrefs.SetInt("Game1", Game1Level);
        PlayerPrefs.SetInt("Game2", Game2Level);
        PlayerPrefs.SetInt("Game3", Game3Level);
        PlayerPrefs.SetInt("IsInit", isInit);
    }

    public void resetData() {
        PlayerPrefs.SetFloat("Water", 10);
        PlayerPrefs.SetFloat("Tempreture", -5);
        PlayerPrefs.SetFloat("LightInstensity", 100);
        PlayerPrefs.SetFloat("Life", 10);
        PlayerPrefs.SetInt("Game1", 1);
        PlayerPrefs.SetInt("Game2", 1);
        PlayerPrefs.SetInt("Game3", 1);
        PlayerPrefs.SetInt("IsInit", 0);
    }

    public void loadData() {
        water = PlayerPrefs.GetFloat("Water");
        tempreture = PlayerPrefs.GetFloat("Tempreture");
        lightIntensity = PlayerPrefs.GetFloat("LightInstensity");
        life = PlayerPrefs.GetFloat("Life");
        Game1Level = PlayerPrefs.GetInt("Game1");
        Game2Level = PlayerPrefs.GetInt("Game2");
        Game3Level = PlayerPrefs.GetInt("Game3");
        isInit = PlayerPrefs.GetInt("IsInit");
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

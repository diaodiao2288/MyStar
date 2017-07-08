using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ParticleHalo : MonoBehaviour {
    private ParticleSystem particleSys;  //粒子系统
    private ParticleSystem.Particle[] particleArr;  //粒子数组
    private CirclePosition[] circle;  //极坐标数组
    private int tier = 10;  //速度差分层数
    private int mouseType = 0;
    private int isComplete = 0;
    private float minR;
    private float maxR;

    public int count = 10000;       // 粒子数量
    public float size = 0.03f;      // 粒子大小
    public float minRadius = 5.0f;  // 最小半径
    public float maxRadius = 12.0f; // 最大半径
    public bool clockwise = true;   // 顺时针|逆时针
    public float speed = 2f;        // 速度
    public float pingPong = 0.02f;  // 游离范围
    public Gradient colorGradient;
    public Text btnText;


    // Use this for initialization
    void Start () {
        minR = minRadius;
        maxR = maxRadius;
        particleArr = new ParticleSystem.Particle[count];
        circle = new CirclePosition[count];

        particleSys = this.GetComponent<ParticleSystem>();
        particleSys.startSpeed = 0;
        particleSys.startSize = size;
        particleSys.loop = false;
        particleSys.maxParticles = count;
        particleSys.Emit(count);
        particleSys.GetParticles(particleArr);

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[5];
        alphaKeys[0].time = 0.0f; alphaKeys[0].alpha = 1.0f;
        alphaKeys[1].time = 0.4f; alphaKeys[1].alpha = 0.4f;
        alphaKeys[2].time = 0.6f; alphaKeys[2].alpha = 1.0f;
        alphaKeys[3].time = 0.9f; alphaKeys[3].alpha = 0.4f;
        alphaKeys[4].time = 1.0f; alphaKeys[4].alpha = 0.9f;
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0].time = 0.0f; colorKeys[0].color = Color.white;
        colorKeys[1].time = 1.0f; colorKeys[1].color = Color.white;
        colorGradient.SetKeys(colorKeys, alphaKeys);

        RandomlySpread();

        btnText.color = new Color(btnText.color.r, btnText.color.g, btnText.color.b, 0);
	}

    private void RandomlySpread() {
        for (int i = 0; i < count; i++) {
            float midRadius = (maxRadius + minRadius) / 2;
            float minRate = Random.Range(1.0f, midRadius / minRadius);
            float maxRate = Random.Range(midRadius / maxRadius, 1.0f);
            float radius = Random.Range(minRadius * minRate, maxRadius * maxRate);

            // 随机每个粒子的角度
            float angle = Random.Range(0.0f, 360.0f);
            float theta = angle / 180 * Mathf.PI;

            // 随机每个粒子的游离起始时间
            float time = Random.Range(0.0f, 360.0f);
            circle[i] = new CirclePosition(radius, angle, time);
            particleArr[i].position = new Vector3(circle[i].radius * Mathf.Cos(theta), 0f, circle[i].radius * Mathf.Sin(theta));
        }
        particleSys.SetParticles(particleArr, particleArr.Length);
    }
	
	// Update is called once per frame
	void Update () {
        if ((mouseType == 0 || mouseType == 3) && Input.mousePosition.x >= Screen.width / 3 && Input.mousePosition.x <= Screen.width * 2 / 3 && Input.mousePosition.y >= Screen.height / 3 && Input.mousePosition.y <= Screen.height * 2 / 3) {
            mouseType = 1;
        } 
        else if (mouseType == 1 && minR <= minRadius / 2 && maxR <= maxRadius / 2) {
            mouseType = 2;
        }
        else if ((mouseType == 1 || mouseType == 2) && (Input.mousePosition.x < Screen.width / 3 || Input.mousePosition.x > Screen.width * 2 / 3 || Input.mousePosition.y < Screen.height / 3 || Input.mousePosition.y > Screen.height * 2 / 3)) {
            mouseType = 3;
        }
        else if (mouseType == 3 && minR >= minRadius && maxR >= maxRadius) {
            mouseType = 0;
        }
	    for (int i = 0; i < count; i++) {
            if (clockwise) {
                circle[i].angle -= (i % tier + 1) * (speed / circle[i].radius / tier);
            } else {
                circle[i].angle += (i % tier + 1) * (speed / circle[i].radius / tier);
            }
            // 粒子在半径方向上游离
            circle[i].time += Time.deltaTime;
            if (mouseType == 0) {
                minR = minRadius;
                maxR = maxRadius;
                circle[i].radius += Mathf.PingPong(circle[i].time / minR / maxR, pingPong) - pingPong / 2.0f;
            }
            else if (mouseType == 1) {
                if (i == 0) {
                    minR = minR - (minRadius - minRadius / 2) / (0.8f / Time.deltaTime);
                    maxR = maxR - (maxRadius - maxRadius / 2) / (0.8f / Time.deltaTime);
                    btnText.color = new Color(btnText.color.r, btnText.color.g, btnText.color.b, btnText.color.a + 1 / (1.6f / Time.deltaTime));
                }
                circle[i].radius -= circle[i].radius / 2 / ((0.8f / Time.deltaTime));
                circle[i].radius += Mathf.PingPong(circle[i].time / minR / maxR, pingPong) - pingPong / 2.0f;
            }
            else if (mouseType == 2) {
                minR = minRadius / 2;
                maxR = maxRadius / 2;
                circle[i].radius += Mathf.PingPong(circle[i].time / minR / maxR, pingPong) - pingPong / 2.0f;
            } else {
                if (i == 0) {
                    minR = minR + (minRadius - minRadius / 2) / (0.8f / Time.deltaTime);
                    maxR = maxR + (maxRadius - maxRadius / 2) / (0.8f / Time.deltaTime);
                    btnText.color = new Color(btnText.color.r, btnText.color.g, btnText.color.b, btnText.color.a - 1 / (1.6f / Time.deltaTime));
                }
                circle[i].radius += circle[i].radius / 2 / ((0.8f / Time.deltaTime));
                circle[i].radius += Mathf.PingPong(circle[i].time / minR / maxR, pingPong) - pingPong / 2.0f;
            }
            //circle[i].radius += Mathf.PingPong(circle[i].time / minR / maxR, pingPong) - pingPong / 2.0f;

            particleArr[i].color = colorGradient.Evaluate(circle[i].angle / 360.0f);
            // 保证angle在0~360度
            circle[i].angle = (360.0f + circle[i].angle) % 360.0f;
            float theta = circle[i].angle / 180 * Mathf.PI;
            particleArr[i].position = new Vector3(circle[i].radius * Mathf.Cos(theta), 0f, circle[i].radius * Mathf.Sin(theta));
        }
        particleSys.SetParticles(particleArr, particleArr.Length);
    }
}

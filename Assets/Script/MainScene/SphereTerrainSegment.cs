using UnityEngine;
using System.Collections;
using System;

public class SphereTerrainSegment : MonoBehaviour {
    public static SphereTerrainSegment Instance;

    public static GameObject terrain_segment;
    //private GameObject segment;

    //材质图和高度图
    //public Material diffuseMap;
    //public Texture2D heightMap;

    //顶点、uv、索引信息
    private Vector3[] vertives;
    private Vector2[] uvs;
    private int[] triangles;

    //生成信息
    private float length;
    private float minHeight = -0.1F;
    private float maxHeight = 0.1F;
    private int segmentX;
    private int segmentY;
    //private string position = "";
    private double radius;
    private float unitH;
    private static Data GlobalInstance;

    public static SphereTerrainSegment GetInstance() {
        if (Instance == null) {
            Instance = new SphereTerrainSegment();
        }
        return Instance;
    }

    /*public static GameObject genSegment() {
        return Instantiate(Resources.Load("Prefabs/Segment") , new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
    }*/

    /*public void setSegment() {
        setSegment(position, length, segmentX, segmentY, minHeight, maxHeight);
    }

    public void setSegment(string _position, float _length, int _segmentX, int _segmentY, float _minHeight, float _maxHeight) {
        //init(_length, _segmentX, _segmentY, _minHeight, _maxHeight);
        setVertives();
        drawMesh();
    }

    private void init(string _position, float _length, int _segmentX, int _segmentY, float _minHeight, float _maxHeight) {

    }*/

    //改变地形，参数，地形图控制
    public void setVertives(GameObject segment, Texture2D heightMap) {
        Mesh mesh = segment.GetComponent<MeshFilter>().mesh;
        int sum = Mathf.FloorToInt((segmentX + 1) * (segmentY + 1));
        vertives = new Vector3[sum];
        int index = 0;
        for (index = 0; index < sum; index++) {
            float tempHeight = 0;
            if (heightMap != null) {
                tempHeight = getHeight(heightMap, mesh.uv[index]);
            }
            Vector3 temp = mesh.vertices[index];
            double radioX = Mathf.Pow(temp.x, 2) / Mathf.Pow((float)radius, 2);
            double radioY = Mathf.Pow(temp.y, 2) / Mathf.Pow((float)radius, 2);
            double radioZ = Mathf.Pow(temp.z, 2) / Mathf.Pow((float)radius, 2);
            vertives[index] = new Vector3((float)(temp.x + temp.x * radioX * tempHeight), (float)(temp.y + temp.y * radioY * tempHeight), (float)(temp.z + temp.z * radioZ * tempHeight));
        }      
        uvs = mesh.uv;
        triangles = mesh.triangles;
        mesh.Clear();
        mesh.vertices = vertives;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

    }

    /*public Vector3[] getVertives() {
        Mesh mesh = segment.GetComponent<MeshFilter>().mesh;
        return mesh.vertices;
    }

    public void setUV() {

    }

    public Vector2[] getUV() {
        Mesh mesh = segment.GetComponent<MeshFilter>().mesh;
        return mesh.uv;
    }

    public void setTriangles() {

    }

    public int[] getTriangles() {
        Mesh mesh = segment.GetComponent<MeshFilter>().mesh;
        return mesh.triangles;
    }*/

    /*public void setPosition(string _position) {
        position = _position;
    }

    public string getPosition() {
        return position;
    }*/

    private Color getColor(Texture2D texture, Vector2 uv) {
        Color color = texture.GetPixel(Mathf.FloorToInt(texture.width * uv.x), Mathf.FloorToInt(texture.height * uv.y));
        return color;
    }

    private float getHeight(Texture2D texture, Vector2 uv) {
        if (texture != null) {
            Color c = getColor(texture, uv);
            float gray;
            //灰度指定算法可调，比如gray = 0.3F* c.r + 0.59F * c.g + 0.11F * c.b
            
            if (GlobalInstance.getInit() == 0) {
                gray = 0.03F * c.r + 0.059F * c.g + 0.011F * c.b;
            } else {
                gray = c.r * (0.03f + (1f - 0.03f) / (5 - GlobalInstance.getInit())) + (0.059f + (0.1f - 0.059f) / (5 - GlobalInstance.getInit())) * c.g + (0.011f + (0.05f - 0.011f) / (5 - GlobalInstance.getInit())) * c.b - 0.1f / (5 - GlobalInstance.getInit());
            }
            //float gray = c.grayscale;
            float h = unitH * gray;
            return h;
        } else {
            return 0;
        }
    }


    //材质控制
    public void drawMesh(GameObject segment, Material diffuseMap) {
        Mesh mesh = segment.GetComponent<MeshFilter>().mesh;
        if (diffuseMap == null) {
            Debug.LogWarning("No material");
        }
        segment.GetComponent<Renderer>().material = diffuseMap;
    }

    private void initVertives(GameObject segment, string _position) {
        Mesh mesh = segment.GetComponent<MeshFilter>().mesh;
        float x = length / 2;
        float y = length / 2;
        float z = length / 2;
        if (_position.Equals("up")) {
            mesh.vertices = new Vector3[] {
                new Vector3(-x, y, z),
                new Vector3(x, y, z),
                new Vector3(-x, y, -z),
                new Vector3(x, y, -z),
            };
        }
        else if (_position.Equals("down")) {
            mesh.vertices = new Vector3[] {
                new Vector3(-x, -y, -z),
                new Vector3(x, -y, -z),
                new Vector3(-x, -y, z),
                new Vector3(x, -y, z)
            };
        }
        else if (_position.Equals("front")) {
            mesh.vertices = new Vector3[] {
                new Vector3(x, y, z),
                new Vector3(-x, y, z),
                new Vector3(x, -y, z),
                new Vector3(-x, -y, z)
            };
        }
        else if (_position.Equals("back")) {
            mesh.vertices = new Vector3[] {
                new Vector3(-x, y, -z),
                new Vector3(x, y, -z),
                new Vector3(-x, -y, -z),
                new Vector3(x, -y, -z)
            };
        }
        else if (_position.Equals("left")) {
            mesh.vertices = new Vector3[] {
                new Vector3(-x, y, z),
                new Vector3(-x, y, -z),
                new Vector3(-x, -y, z),
                new Vector3(-x, -y, -z)
            };
        }
        else if (_position.Equals("right")) {
            mesh.vertices = new Vector3[] {
                new Vector3(x, y, -z),
                new Vector3(x, y, z),
                new Vector3(x, -y, -z),
                new Vector3(x, -y, z)
            };
        }

    }

    private void initUV(GameObject segment, string _position) {
        Mesh mesh = segment.GetComponent<MeshFilter>().mesh;
        if (_position.Equals("up")) {
            mesh.uv = new Vector2[] {
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(0, 0),
                new Vector2(1, 0)
            };
        }
        else if (_position.Equals("down")) {
            mesh.uv = new Vector2[] {
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(0, 0),
                new Vector2(1, 0)
            };
        }
        else if (_position.Equals("front")) {
            mesh.uv = new Vector2[] {
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(0, 0),
                new Vector2(1, 0)
            };
        }
        else if (_position.Equals("back")) {
            mesh.uv = new Vector2[] {
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(0, 0),
                new Vector2(1, 0)
            };
        }
        else if (_position.Equals("left")) {
            mesh.uv = new Vector2[] {
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(0, 0),
                new Vector2(1, 0)
            };
        }
        else if (_position.Equals("right")) {
            mesh.uv = new Vector2[] {
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(0, 0),
                new Vector2(1, 0)
            };
        }
    }

    private void initTriangles(GameObject segment, string _position) {
        Mesh mesh = segment.GetComponent<MeshFilter>().mesh;
        if (_position.Equals("up")) {
            mesh.triangles = new int[] {
                0, 1, 2,
                1, 3, 2
            };
        }
        else if (_position.Equals("down")) {
            mesh.triangles = new int[] {
                0, 1, 2,
                1, 3, 2
            };
        }
        else if (_position.Equals("front")) {
            mesh.triangles = new int[] {
                0, 1, 2,
                1, 3, 2
            };
        }
        else if (_position.Equals("back")) {
            mesh.triangles = new int[] {
                0, 1, 2,
                1, 3, 2
            };
        }
        else if (_position.Equals("left")) {
            mesh.triangles = new int[] {
                0, 1, 2,
                1, 3, 2
            };
        }
        else if (_position.Equals("right")) {
            mesh.triangles = new int[] {
                0, 1, 2,
                1, 3, 2
            };
        }
    }

    public void initMesh(GameObject segment, string position) {
        //segment = genSegment();
        //segment.transform.SetParent(parent.transform);
        if (position.Equals("")) {
            print("The position is unknown");
        } else {
            length = 50;
            segmentX = 1;
            segmentY = 1;
            unitH = maxHeight - minHeight;
            radius = Mathf.Pow(3, (float)0.5) * length / 2;
            Mesh mesh = segment.AddComponent<MeshFilter>().mesh;
            segment.AddComponent<MeshRenderer>();
            initVertives(segment, position);
            initUV(segment, position);
            initTriangles(segment, position);
            //重置法线
            mesh.RecalculateNormals();
            //重置范围
            mesh.RecalculateBounds();
        }
    }

    public void divideSegment(GameObject segment) {
        segmentX = segmentX * 2;
        segmentY = segmentY * 2;
        Mesh mesh = segment.GetComponent<MeshFilter>().mesh;
        Vector3[] vertives = divideVertives(mesh);
        Vector2[] uvs = divideUV(mesh);
        int[] triangles = divideTriangles();
        mesh.Clear();
        mesh.vertices = vertives;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    private Vector3[] divideVertives(Mesh oldMesh) {
        int sum = (segmentX + 1) * (segmentY + 1);
        int oldX = segmentX / 2;
        int oldY = segmentY / 2;
        Vector3[] old = oldMesh.vertices;
        Vector3[] now = new Vector3[sum];

        
        for (int i = 0; i < segmentX + 1; i++) {
            for (int j = 0; j < segmentY + 1; j++) {
                if (i % 2 == 0) {
                    if (j % 2 == 0) {
                        now[i * (segmentX + 1) + j] = old[i / 2 * (oldX + 1) + j / 2]; 
                    } else {
                        Vector3 temp;
                        Vector3 left = old[i / 2 * (oldX + 1) + (j - 1) / 2];
                        Vector3 right = old[i / 2 * (oldX + 1) + (j - 1) / 2 + 1];
                        temp.x = (left.x + right.x) / 2;
                        temp.y = (left.y + right.y) / 2;
                        temp.z = (left.z + right.z) / 2;
                        now[i * (segmentX + 1) + j] = temp;
                    }
                } else {
                    if (j % 2 == 0) {
                        Vector3 temp;
                        Vector3 up = old[(i - 1) / 2 * (oldX + 1) + j / 2];
                        Vector3 down = old[(i + 1) / 2 * (oldX + 1) + j / 2];
                        temp.x = (up.x + down.x) / 2;
                        temp.y = (up.y + down.y) / 2;
                        temp.z = (up.z + down.z) / 2;
                        now[i * (segmentX + 1) + j] = temp;
                    } else {
                        Vector3 temp;
                        Vector3 leftup = old[(i - 1) / 2 * (oldX + 1) + (j - 1) / 2];
                        Vector3 rightdown = old[(i + 1) / 2 * (oldX + 1) + (j + 1) / 2];
                        temp.x = (leftup.x + rightdown.x) / 2;
                        temp.y = (leftup.y + rightdown.y) / 2;
                        temp.z = (leftup.z + rightdown.z) / 2;
                        now[i * (segmentX + 1) + j] = temp;
                    }
                }
                Vector3 normalize = now[i * (segmentX + 1) + j];
                double distance = Mathf.Sqrt(Mathf.Pow(normalize.x, 2) + Mathf.Pow(normalize.y, 2) + Mathf.Pow(normalize.z, 2));
                if (distance != radius) {
                    normalize.x = (float)(normalize.x * (radius / distance));
                    normalize.y = (float)(normalize.y * (radius / distance));
                    normalize.z = (float)(normalize.z * (radius / distance));
                    now[i * (segmentX + 1) + j] = normalize;
                }
            }
        }
        return now;
    }

    private Vector2[] divideUV(Mesh oldMesh) {
        int sum = (segmentX + 1) * (segmentY + 1);
        int oldX = segmentX / 2;
        int oldY = segmentY / 2;
        Vector2[] old = oldMesh.uv;
        Vector2[] now = new Vector2[sum];
        for (int i = 0; i < segmentX + 1; i++) {
            for (int j = 0; j < segmentY + 1; j++) {
                if (i % 2 == 0) {
                    if (j % 2 == 0) {
                        now[i * (segmentX + 1) + j] = old[i / 2 * (oldX + 1) + j / 2];
                    } else {
                        Vector2 temp;
                        Vector2 left = old[i / 2 * (oldX + 1) + (j - 1) / 2];
                        Vector2 right = old[i / 2 * (oldX + 1) + (j - 1) / 2 + 1];
                        temp.x = (left.x + right.x) / 2;
                        temp.y = (left.y + right.y) / 2;
                        now[i * (segmentX + 1) + j] = temp;
                    }
                } else {
                    if (j % 2 == 0) {
                        Vector2 temp;
                        Vector2 up = old[(i - 1) / 2 * (oldX + 1) + j / 2];
                        Vector2 down = old[(i + 1) / 2 * (oldX + 1) + j / 2];
                        temp.x = (up.x + down.x) / 2;
                        temp.y = (up.y + down.y) / 2;
                        now[i * (segmentX + 1) + j] = temp;
                    } else {
                        Vector2 temp;
                        Vector2 leftup = old[(i - 1) / 2 * (oldX + 1) + (j - 1) / 2];
                        Vector2 rightdown = old[(i + 1) / 2 * (oldX + 1) + (j + 1) / 2];
                        temp.x = (leftup.x + rightdown.x) / 2;
                        temp.y = (leftup.y + rightdown.y) / 2;
                        now[i * (segmentX + 1) + j] = temp;
                    }
                }
            }
        }
        return now;
    }

    private int[] divideTriangles() {
        int sum = segmentX * segmentY * 2 * 3;
        int[] now = new int[sum];
        for (int i = 0; i < segmentX; i++) {
            for (int j = 0; j < segmentY + 1; j++) {
                if (j == 0) {
                    now[i * segmentX * 6] = i * (segmentX + 1);
                    now[i * segmentX * 6 + 1] = i * (segmentX + 1) + 1;
                    now[i * segmentX * 6 + 2] = (i + 1) * (segmentX + 1);
                }
                else if (j == segmentY) {
                    now[(i + 1) * segmentX * 6 - 3] = i * (segmentX + 1) + j;
                    now[(i + 1) * segmentX * 6 - 2] = (i + 1) * (segmentX + 1) + j;
                    now[(i + 1) * segmentX * 6 - 1] = (i + 1) * (segmentX + 1) + j - 1;
                } else {
                    now[i * segmentX * 6 + 3 + (j - 1) * 6] = i * (segmentX + 1) + j;
                    now[i * segmentX * 6 + 3 + (j - 1) * 6 + 1] = (i + 1) * (segmentX + 1) + j;
                    now[i * segmentX * 6 + 3 + (j - 1) * 6 + 2] = (i + 1) * (segmentX + 1) + j - 1;
                    now[i * segmentX * 6 + 3 + (j - 1) * 6 + 3] = i * (segmentX + 1) + j;
                    now[i * segmentX * 6 + 3 + (j - 1) * 6 + 4] = i * (segmentX + 1) + j + 1;
                    now[i * segmentX * 6 + 3 + (j - 1) * 6 + 5] = (i + 1) * (segmentX + 1) + j;
                }
            } 
        }
        return now;
    }

    void Awake() {
        GlobalInstance = Data.GetInstance();
    }

    // Use this for initialization
    void Start () {
        //segment = genSegment();
        /*setPosition("left");
        
        initMesh();
        for (int i = 0; i < 7; i++) {
            divideSegment();
        }
        drawMesh();
        setVertives();*/
    }
	
	// Update is called once per frame
	void Update () {
        //initMesh();
    }
}

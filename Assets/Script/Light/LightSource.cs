using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.gameOne;

public class LightSource : MonoBehaviour
{
    public Bezier myBezier;
    private GameObject LineRendererGameObject;
    private LineRenderer lineRenderer;
    // 是否改变方向
    bool isChanged;
    // 发射点位置
    private Vector3 original;
    // 当前发射点位置
    private Vector3 currentSource;
    // 开始点的方向
    private Vector3 startDirection;
    // 结束点的方向
    private Vector3 currentDirection;
    // 记录鼠标拖拽结束位置
    private Vector3 target;
    private List<Vector3> point = new List<Vector3>();
    private int pointNum;
    // 是否停止绘制点
    private bool end;
    // 当前是第几个光源
    private int num;
    private lightFactory factory;
    private controller controllerInstance; 

    // Use this for initialization
    void Start()
    {
        isChanged = true;
        factory = lightFactory.getInstance();
        controllerInstance = controller.getInstance();
        num = factory.getNum();
        LineRendererGameObject = GameObject.Find("source");
        lineRenderer = (LineRenderer)LineRendererGameObject.GetComponent("LineRenderer");
        original = LineRendererGameObject.transform.position;
        startDirection = Vector3.Normalize(new Vector3(-1f, 1f, 0));
        end = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isChanged)
        {
            end = false;
            // 清除前面的点
            point.Clear();
            currentDirection = startDirection;
            currentSource = original;
            RaycastHit hit;
            point.Add(original);

            while (!end)
            {
                Ray ray = new Ray(currentSource, currentDirection);
                if (Physics.Raycast(ray, out hit))
                {
                    switch (hit.transform.tag)
                    {
                        case "blackhole":
                            // 夹角小于30度直接吸入黑洞中
                            if (Vector3.Angle(currentDirection, hit.transform.position - hit.point) < 30)
                            {

                                Vector3 temp = hit.point + (Vector3.Distance(hit.transform.position, hit.point) / 2) * Vector3.Normalize(currentDirection);
                                myBezier = new Bezier(hit.point, temp, hit.transform.position);
                                for (int i = 1; i <= 50; i++)
                                {
                                    point.Add(myBezier.GetPointAtTime((float)(i * 0.02)));
                                }
                                end = true;
                                factory.remove(num);
                            }
                            else
                            {
                                float offset;
                                float angle = Vector3.Angle(currentDirection, hit.transform.position - hit.point);
                                // 扭曲的角度
                                float twistedAngle = 45 - angle / 2;
                                float distance = Vector3.Distance(hit.transform.position, hit.point);
                                Vector3 p1, p2;
                                offset = distance * Mathf.Sin(angle * Mathf.PI / 180) * Mathf.Tan(twistedAngle * Mathf.PI / 180);
                                offset += distance * Mathf.Cos(angle * Mathf.PI / 180);
                                Vector3 cross = new Vector3(-currentDirection.y, currentDirection.x, 0);
                                // 黑洞中心在光线左侧
                                if (Vector3.Dot(cross, hit.transform.position - currentSource) > 0)
                                {
                                    p1 = hit.point + offset * Vector3.Normalize(currentDirection);
                                    Vector3 temp = myRotation(hit.transform.position, p1, Vector3.forward, 270 + twistedAngle);
                                    p2 = p1 + offset * Vector3.Normalize(temp - p1);
                                    myBezier = new Bezier(hit.point, p1, p2);
                                    for (int i = 1; i <= 50; i++)
                                    {
                                        point.Add(myBezier.GetPointAtTime((float)(i * 0.02)));
                                    }
                                    currentDirection = temp - p1;
                                    currentSource = p2;
                                }
                                else
                                {
                                    p1 = hit.point + offset * Vector3.Normalize(currentDirection);
                                    Vector3 temp = myRotation(hit.transform.position, p1, Vector3.forward, 90 - twistedAngle);
                                    p2 = p1 + offset * Vector3.Normalize(temp - p1);
                                    myBezier = new Bezier(hit.point, p1, p2);
                                    for (int i = 1; i <= 50; i++)
                                    {
                                        point.Add(myBezier.GetPointAtTime((float)(i * 0.02)));
                                    }
                                    currentDirection = temp - p1;
                                    currentSource = p2;
                                }
                            }
                            break;
                        case "wormholeIn":
                            int type = 0;
                            int index = controllerInstance.pairOfwormHole(hit.transform.gameObject, type);
                            float fromAngle = controllerInstance.getInAngle(index);
                            float toAngle = controllerInstance.getOutAngle(index);
                            GameObject pair = controllerInstance.getOutHole(index);
                            Vector3 holeDirection = new Vector3(Mathf.Cos(fromAngle * Mathf.PI / 180), Mathf.Sin(fromAngle * Mathf.PI / 180), 0);
                            Vector3 inDirection = hit.point - currentSource;
                            Vector3 newPosition;
                            Vector3 newDirection;
                            Vector3 crossHoleDirection;
                            float inAngle = Vector3.Angle(inDirection, holeDirection);
                            if (inAngle < 90)
                            {
                                float distance = Vector3.Distance(hit.point, hit.transform.position);
                                if (Vector3.Angle(hit.point - hit.transform.position, new Vector3(-Mathf.Sin(fromAngle * Mathf.PI / 180), Mathf.Cos(fromAngle * Mathf.PI / 180), 0)) > 90)
                                {
                                    crossHoleDirection = new Vector3(-Mathf.Sin(toAngle * Mathf.PI / 180), Mathf.Cos(toAngle * Mathf.PI / 180), 0);
                                    newPosition = -distance * Vector3.Normalize(crossHoleDirection) + pair.transform.position;
                                    if (Vector3.Angle(inDirection, new Vector3(-Mathf.Sin(fromAngle * Mathf.PI / 180), Mathf.Cos(fromAngle * Mathf.PI / 180), 0)) > 90)
                                    {
                                        newDirection = myRotation(pair.transform.position, newPosition, Vector3.forward, 90 + inAngle);
                                        newDirection = newDirection - newPosition;
                                    }
                                    else
                                    {
                                        newDirection = myRotation(pair.transform.position, newPosition, Vector3.forward, 90 - inAngle);
                                        newDirection = newDirection - newPosition;
                                    }
                                }
                                else
                                {
                                    crossHoleDirection = new Vector3(-Mathf.Sin(toAngle * Mathf.PI / 180), Mathf.Cos(toAngle * Mathf.PI / 180), 0);
                                    newPosition = distance * Vector3.Normalize(crossHoleDirection) + pair.transform.position;
                                    if (Vector3.Angle(inDirection, new Vector3(-Mathf.Sin(fromAngle * Mathf.PI / 180), Mathf.Cos(fromAngle * Mathf.PI / 180), 0)) > 90)
                                    {
                                        newDirection = myRotation(pair.transform.position, newPosition, Vector3.forward, 270 + inAngle);
                                        newDirection = newDirection - newPosition;
                                    }
                                    else
                                    {
                                        newDirection = myRotation(pair.transform.position, newPosition, Vector3.forward, 270 - inAngle);
                                        newDirection = newDirection - newPosition;
                                    }
                                }
                                factory.newLight(num, newPosition, newDirection);
                            }
                            else
                            {
                                factory.remove(num);
                            }
                            end = true;
                            point.Add(hit.point);
                            break;
                        case "wormholeOut":
                            type = 1;
                            index = controllerInstance.pairOfwormHole(hit.transform.gameObject, type);
                            fromAngle = controllerInstance.getOutAngle(index);
                            toAngle = controllerInstance.getInAngle(index);
                            pair = controllerInstance.getInHole(index);
                            holeDirection = new Vector3(Mathf.Cos(fromAngle * Mathf.PI / 180), Mathf.Sin(fromAngle * Mathf.PI / 180), 0);
                            inDirection = hit.point - currentSource;
                            inAngle = Vector3.Angle(inDirection, holeDirection);
                            if (inAngle < 90)
                            {
                                float distance = Vector3.Distance(hit.point, hit.transform.position);
                                if (Vector3.Angle(hit.point - hit.transform.position, new Vector3(-Mathf.Sin(fromAngle * Mathf.PI / 180), Mathf.Cos(fromAngle * Mathf.PI / 180), 0)) > 90) {
                                    crossHoleDirection = new Vector3(-Mathf.Sin(toAngle * Mathf.PI / 180), Mathf.Cos(toAngle * Mathf.PI / 180), 0);
                                    newPosition = -distance * Vector3.Normalize(crossHoleDirection) + pair.transform.position;
                                    if (Vector3.Angle(inDirection, new Vector3(-Mathf.Sin(fromAngle * Mathf.PI / 180), Mathf.Cos(fromAngle * Mathf.PI / 180), 0)) > 90) {
                                        newDirection = myRotation(pair.transform.position, newPosition, Vector3.forward, 90 + inAngle);
                                        newDirection = newDirection - newPosition;
                                    }
                                    else
                                    {
                                        newDirection = myRotation(pair.transform.position, newPosition, Vector3.forward, 90 - inAngle);
                                        newDirection = newDirection - newPosition;
                                    }
                                }
                                else
                                {
                                    crossHoleDirection = new Vector3(-Mathf.Sin(toAngle * Mathf.PI / 180), Mathf.Cos(toAngle * Mathf.PI / 180), 0);
                                    newPosition = distance * Vector3.Normalize(crossHoleDirection) + pair.transform.position;
                                    if (Vector3.Angle(inDirection, new Vector3(-Mathf.Sin(fromAngle * Mathf.PI / 180), Mathf.Cos(fromAngle * Mathf.PI / 180), 0)) > 90)
                                    {
                                        newDirection = myRotation(pair.transform.position, newPosition, Vector3.forward, 270 + inAngle);
                                        newDirection = newDirection - newPosition;
                                    }
                                    else
                                    {
                                        newDirection = myRotation(pair.transform.position, newPosition, Vector3.forward, 270 - inAngle);
                                        newDirection = newDirection - newPosition;
                                    }
                                }
                                factory.newLight(num, newPosition, newDirection);
                            }
                            else
                            {
                                factory.remove(num);
                            }
                            end = true;
                            point.Add(hit.point);
                            break;
                        case "target":
                            point.Add(hit.point);
                            end = true;
                            controllerInstance.setStatus(true);
                            break;
                        default:
                            point.Add(hit.point);
                            end = true;
                            factory.remove(num);
                            break;
                    }
                }
                else
                {
                    point.Add(currentSource + 20 * currentDirection);
                    end = true;
                    factory.remove(num);
                }
            }
        }
        lineRenderer.positionCount = point.Count;
        for (int i = 0; i < point.Count; i++)
        {
            lineRenderer.SetPosition(i, point[i]);
        }
    }

    private void OnMouseDrag()
    {
        Vector3 cameraPos = Input.mousePosition;
        target = Camera.main.ScreenToWorldPoint(cameraPos);
        target.z = 0;
        startDirection = target - original;
        isChanged = true;
        
    }

    private Vector3 myRotation(Vector3 position, Vector3 center, Vector3 axis, float angle)
    {
        Vector3 point = Quaternion.AngleAxis(angle, axis) * (position - center);
        Vector3 result = center + point;
        return result;
    }
    
}

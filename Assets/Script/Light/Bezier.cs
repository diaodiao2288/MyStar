using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : System.Object
{
    private Vector3 p0;
    private Vector3 p1;
    private Vector3 p2;

    public Bezier(Vector3 _p0, Vector3 _p1, Vector3 _p2)
    {
        p0 = _p0;
        p1 = _p1;
        p2 = _p2;
    }

    public Vector3 GetPointAtTime(float t)
    {
        float _t = 1 - t;
        return _t * _t * p0 + 2 * t * _t * p1 + t * t * p2;
    }

}

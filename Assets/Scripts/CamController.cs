using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : Singleton<CamController>
{
    public float minY,maxY,minX,maxX;
    public void Run(float x, float y)
    {
        transform.position = new Vector3(Mathf.Clamp(x,minX,maxX), Mathf.Clamp(y,minY,maxY),transform.position.z);
    }
}

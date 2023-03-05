using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float speed = 0.05f;
    Vector3 targetPosition = new Vector3(0, 3, -4);
    public Transform transform;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void CameraPosOne()
    {
        Debug.Log("CameraPosOne");
        Vector3 vector = new Vector3(0, 3, -4);
        setTarget(vector);
    }

    public void CameraPosTwo()
    {
        Debug.Log("CameraPosTwo");
        Vector3 vector = new Vector3(0, 3, -2);
        setTarget(vector);
    }

    void setTarget(Vector3 target)
    {
        targetPosition = target;
    }
}

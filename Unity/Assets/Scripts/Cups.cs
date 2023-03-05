using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cups : MonoBehaviour
{
    public Transform transform;
    Vector3 targetPosition;

    public float upHeight = 2.6f;
    public float downHeight = 2.35f;
    public bool ballAttached = false;
    public float speed = .5f;
    public Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        // transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, speed * Time.deltaTime);
    }

    public void Up()
    {
        targetPosition = new Vector3(transform.position.x, upHeight, transform.position.z);
    }

    public void Down()
    {
        targetPosition = new Vector3(transform.position.x, downHeight, transform.position.z);
    }

    public void setAttached(bool tf)
    {
        ballAttached = tf;
    }

    public void setTarget(Vector3 target)
    {
        targetPosition = target;
    }

    public Vector3 getTarget()
    {
        return targetPosition;
    }

    // public void setTransform(Transform trans)
    // {
    //     transform = trans;
    // }

    // public Transform getTramsform()
    // {
    //     return transform;
    // }
}

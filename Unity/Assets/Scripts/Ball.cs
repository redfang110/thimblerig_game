using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Transform transform;
    public float speed = 1000f;
    public enum cups
    { 
        none,
        one,
        two,
        three
    }
    cups attachedTo = cups.none;

    public void setAttachedTo(cups set)
    {
        attachedTo = set;
    }

    public cups getAttchedTo()
    {
        return attachedTo;
    }

    public void setTarget(Vector3 target)
    {
        transform.position = target;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    Transform transform;

    public static float giveDistance(Transform other) {
        // float dist = Vector3.Distance(other.position, transform.position);
        // print("Distance to other: " + dist);
        // return dist;
        return 0.0f;
    }

    public Transform getTransform() {
        return transform;
    }

    void Start() {}
    void Update() {}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeOtherSun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody compRb = GetComponent<Rigidbody>();
        compRb.AddForce(2, 3, -0.5f, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

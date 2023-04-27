using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    private GameObject me;

    // Start is called before the first frame update
    void Start()
    {
        me = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Spinning();
    }
    private void Spinning()
    {
        var rot = me.transform.localEulerAngles;
        rot.y += 0.3f;
        me.transform.localEulerAngles = rot;
    }
}

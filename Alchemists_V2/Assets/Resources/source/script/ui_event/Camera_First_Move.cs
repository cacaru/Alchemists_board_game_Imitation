using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_First_Move : MonoBehaviour
{
    private Transform now_pos;
    private Vector3 pos;

    public float speed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        now_pos = this.transform;
        pos = now_pos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(now_pos.position.z <= 11.5)
        {
            pos.z += speed;
            now_pos.position = pos;
        }


        if( now_pos.position.z >= 11.5 )
        {
            transform.GetComponent<Camera_First_Move>().enabled = false;
        }
    }
}

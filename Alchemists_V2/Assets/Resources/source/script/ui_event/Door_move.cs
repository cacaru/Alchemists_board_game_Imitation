using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_move : MonoBehaviour
{
    private Transform now_pos;
    private Vector3 ori_pos;
    private Vector3 ori_rot;
    private Vector3 pos;
    private Vector3 rot;
    public float speed = 0.1f;
    public float rot_speed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        now_pos = this.transform;
        pos = now_pos.localPosition;
        ori_pos = now_pos.localPosition;
        //Debug.Log(ori_pos);
        rot = now_pos.localRotation.eulerAngles;
        ori_rot = now_pos.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        // 0.5, 0 , 0.5가 될때까지 움직임과 동시에 y축 90도가 될때까지 회전
        if( now_pos.localPosition.x <= 8.5f)
        {
            pos.x += speed;
            pos.z = Mathf.Sqrt(16*(pos.x) - 55 - (pos.x)*(pos.x)) - 2;
            rot.y += rot_speed;

            now_pos.localPosition = pos;
            now_pos.localEulerAngles = rot;
            //Debug.Log(now_pos.localPosition);
        }

        // 이동이 모두 끝나고 원상복귀 후 move 스크립트 제거
        if (now_pos.localPosition.x >= 8.5f)
        {
            now_pos.localPosition = ori_pos;
            now_pos.localEulerAngles = ori_rot;
            now_pos.GetComponent<Door_move>().enabled = false;
        }
    }
}

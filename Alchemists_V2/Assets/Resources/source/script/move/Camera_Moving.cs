using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Moving : MonoBehaviour
{
    // 움직이는 속도 조절
    public float move_speed = 15.0f;

    // 이동 관련 변수
    private Vector3 h, v;
    private Vector3 ori_pos;
    private bool start;
    private bool wall_check = false;

    // Start is called before the first frame update
    void Start()
    {
        // 이동 관련 위치 변수 조절
        ori_pos = gameObject.transform.position;
        //m_rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        Wall_check();
        // wasd 의 입력 중에만 작동하도록 -> 미끄러짐 현상 없애기
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            start = true;
        if (Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical"))
            start = false;
    }
    void FixedUpdate()
    {
        // wasf 의 입력 중에만 작동하도록 -> 미끄러짐 현상 없애기
        if (start && !wall_check)
            Moving();
    }

    private void Wall_check()
    {
        // 정면에 오브젝트의 layer가 wall이면 true, 아니면 false
        wall_check = Physics.Raycast(transform.position + new Vector3(0, 1.0f, 0), transform.forward, 0.6f, LayerMask.GetMask("Wall"));
    }

    private void Moving()
    {
        
        // 이동 할 변수 
        h = Input.GetAxis("Horizontal") * Vector3.right;
        v = Input.GetAxis("Vertical") * Vector3.forward;

        Vector3 move_direction = h + v;

        transform.Translate(move_speed * Time.deltaTime * move_direction.normalized, Space.Self);
        /*
         * rigid body 를 이용한 이동 -> 축 변환이 어려움
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 vel;
        vel.x = hor; vel.y = 0; vel.z = ver;
        vel *= move_speed;

        m_rigidbody.velocity = vel;
        */
    }

    public void Axis_rotate(Quaternion quat, float speed)
    {
        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, quat, Time.deltaTime * speed);
    }

    public void Init_pos()
    {
        gameObject.transform.position = ori_pos;
    }

}

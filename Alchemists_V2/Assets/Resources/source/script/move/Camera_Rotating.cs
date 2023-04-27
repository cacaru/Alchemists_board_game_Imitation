using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Rotating : MonoBehaviour
{
    public float camera_speed = 800.0f;

    // 카메라 관련 변수
    private float camera_x, camera_y;
    private float ori_camera_x, ori_camera_y;
    private Vector3 tmp_euler = new();

    // Start is called before the first frame update
    void Start()
    {
        // 카메라 관련 회전 변수 조절
        ori_camera_x = gameObject.transform.rotation.x;
        ori_camera_y = gameObject.transform.rotation.y;
        camera_x = ori_camera_x;
        camera_y = ori_camera_y;

        // 마우스 고정
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 움직임
        Camera_rotation();
    }

    private void Camera_rotation()
    {
        // 상/하 회전
        camera_x += -Input.GetAxis("Mouse Y") * Time.deltaTime * camera_speed;
        // 좌/우 회전
        camera_y += Input.GetAxis("Mouse X") * Time.deltaTime * camera_speed;

        // 상/ 하 최대각 고정
        camera_x = Mathf.Clamp(camera_x, -20, 20);


        // quaternion 으로 회전
        // 카메라 회전
        // 이동각을 vector3로 표현 -> x축 회전 값 : x , y축 회전값 : y
        tmp_euler.x = camera_x; tmp_euler.y = camera_y; tmp_euler.z = 0;
        //Euler로 Vector3 값을 Quaternion 값으로 전환
        Quaternion quat = Quaternion.Euler(tmp_euler);
        // 회전
        gameObject.transform.rotation =
            Quaternion.Slerp(transform.rotation, quat, camera_speed * Time.deltaTime);

        // 오브젝트 회전
        // 상/하 회전 없이 y축 회전만 진행
        tmp_euler.x = 0;
        quat = Quaternion.Euler(tmp_euler);
        GameObject.Find("Moving_obj").GetComponent<Camera_Moving>().Axis_rotate(quat, camera_speed);
    }

    public void Init_camera()
    {
        GameObject.Find("Moving_obj").GetComponent<Camera_Moving>().Init_pos();
        tmp_euler.x = ori_camera_x;
        tmp_euler.y = ori_camera_y;
        gameObject.transform.rotation = Quaternion.Euler(tmp_euler);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Rotating : MonoBehaviour
{
    public float camera_speed = 800.0f;

    // ī�޶� ���� ����
    private float camera_x, camera_y;
    private float ori_camera_x, ori_camera_y;
    private Vector3 tmp_euler = new();

    // Start is called before the first frame update
    void Start()
    {
        // ī�޶� ���� ȸ�� ���� ����
        ori_camera_x = gameObject.transform.rotation.x;
        ori_camera_y = gameObject.transform.rotation.y;
        camera_x = ori_camera_x;
        camera_y = ori_camera_y;

        // ���콺 ����
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // ���콺 ������
        Camera_rotation();
    }

    private void Camera_rotation()
    {
        // ��/�� ȸ��
        camera_x += -Input.GetAxis("Mouse Y") * Time.deltaTime * camera_speed;
        // ��/�� ȸ��
        camera_y += Input.GetAxis("Mouse X") * Time.deltaTime * camera_speed;

        // ��/ �� �ִ밢 ����
        camera_x = Mathf.Clamp(camera_x, -20, 20);


        // quaternion ���� ȸ��
        // ī�޶� ȸ��
        // �̵����� vector3�� ǥ�� -> x�� ȸ�� �� : x , y�� ȸ���� : y
        tmp_euler.x = camera_x; tmp_euler.y = camera_y; tmp_euler.z = 0;
        //Euler�� Vector3 ���� Quaternion ������ ��ȯ
        Quaternion quat = Quaternion.Euler(tmp_euler);
        // ȸ��
        gameObject.transform.rotation =
            Quaternion.Slerp(transform.rotation, quat, camera_speed * Time.deltaTime);

        // ������Ʈ ȸ��
        // ��/�� ȸ�� ���� y�� ȸ���� ����
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

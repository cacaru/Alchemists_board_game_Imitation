using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Switch : MonoBehaviour
{
    public Texture2D cursor_img;
    public Camera cam;

    // ���� ������ �˸� ����
    public bool in_round = false;

    private Vector3 center_pos;// ī�޶� ���� ����
    private Vector2 hot_spot;
    private bool mouse_on = false;
    // Start is called before the first frame update
    void Start()
    { 
        //center_pos = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2);
        hot_spot.x = cursor_img.width / 2;
        hot_spot.y = cursor_img.height / 2;
        Cursor.SetCursor(cursor_img, hot_spot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
            mouse_on = !mouse_on;

        if (mouse_on)
            Mouse_on();

        if (!mouse_on)
            Mouse_Off();

        if (!in_round)
        {
            // r Ű�� �����غ� â ����
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameObject.Find("Socket_obj").GetComponent<Socket_In_Board>().Round_ready_prepare();
            }
        }
        else
        {
            // t �� ����Ȯ���ϱ�
            if(Input.GetKeyDown(KeyCode.T))
            {
                GameObject.Find("Socket_obj").GetComponent<Socket_In_Board>().Board_act_end_confirm();
            }
        }
    }

    private void Mouse_on()
    {
        cam.GetComponent<Camera_Rotating>().enabled = false;
        GameObject.Find("Moving_obj").GetComponent<Camera_Moving>().enabled = false;

        // ���콺 ����
        //Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Mouse_Off()
    {
        cam.GetComponent<Camera_Rotating>().enabled = true;
        // ���� �ൿ ���̶�� �̵��� �Ұ��ϰ� ����Ǿ����
        if (in_round)
            GameObject.Find("Moving_obj").GetComponent<Camera_Moving>().enabled = false;
        else
            GameObject.Find("Moving_obj").GetComponent<Camera_Moving>().enabled = true;

        // ���콺 ����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    public void Outside_mouse_controll(bool check)
    {
        this.mouse_on = check;
    }
}

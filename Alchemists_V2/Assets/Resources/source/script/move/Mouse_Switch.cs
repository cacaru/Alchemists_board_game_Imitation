using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Switch : MonoBehaviour
{
    public Texture2D cursor_img;
    public Camera cam;

    // 라운드 중임을 알릴 변수
    public bool in_round = false;

    private Vector3 center_pos;// 카메라 센터 변수
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
            // r 키로 게임준비 창 띄우기
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameObject.Find("Socket_obj").GetComponent<Socket_In_Board>().Round_ready_prepare();
            }
        }
        else
        {
            // t 로 선택확정하기
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

        // 마우스 동작
        //Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Mouse_Off()
    {
        cam.GetComponent<Camera_Rotating>().enabled = true;
        // 라운드 행동 중이라면 이동이 불가하게 변경되어야함
        if (in_round)
            GameObject.Find("Moving_obj").GetComponent<Camera_Moving>().enabled = false;
        else
            GameObject.Find("Moving_obj").GetComponent<Camera_Moving>().enabled = true;

        // 마우스 고정
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    public void Outside_mouse_controll(bool check)
    {
        this.mouse_on = check;
    }
}

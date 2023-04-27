using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public Camera main;
    public Camera sub;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void On_main()
    {
        // sub 조작
        sub.enabled = false;
        sub.GetComponent<AudioListener>().enabled = false;
        sub.GetComponent<Camera_Rotating>().Init_camera();
        sub.GetComponent<Camera_Rotating>().enabled = false;
        GameObject.Find("Moving_obj").GetComponent<Camera_Moving>().enabled = false;

        // main 조작
        main.enabled = true;
        main.GetComponent<AudioListener>().enabled = true;       

        // 마우스 스위치 끄기
        gameObject.transform.GetComponent<Mouse_Switch>().enabled = false;
    }

    public void On_sub()
    {
        // main 조작
        main.enabled = false;
        main.GetComponent<AudioListener>().enabled = false;

        // sub 조작
        sub.enabled = true;
        sub.GetComponent<AudioListener>().enabled = true;
        sub.GetComponent<Camera_Rotating>().enabled = true;
        GameObject.Find("Moving_obj").GetComponent<Camera_Moving>().enabled = true;
        // sub상태에서 마우스 상태 조작
        gameObject.transform.GetComponent<Mouse_Switch>().enabled = true;
    }


}

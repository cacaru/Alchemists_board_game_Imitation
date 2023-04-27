using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat_Observer : MonoBehaviour
{
    public GameObject socket;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 엔터를 누르면 socket의 스크립트 함수 실행
        if( Input.GetKeyDown(KeyCode.Return))
        {
            socket.GetComponent<Room_Page_Control>().emit_chat();
        }
    }
}

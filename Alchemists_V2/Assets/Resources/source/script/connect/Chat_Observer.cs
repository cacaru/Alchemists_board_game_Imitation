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
        // ���͸� ������ socket�� ��ũ��Ʈ �Լ� ����
        if( Input.GetKeyDown(KeyCode.Return))
        {
            socket.GetComponent<Room_Page_Control>().emit_chat();
        }
    }
}

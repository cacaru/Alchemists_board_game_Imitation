using System.Collections;
using UnityEngine;

public class Chat_Observe_in_Board : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject.Find("Socket_obj").GetComponent<Socket_In_Board>().emit_chat();
        }
    }
}

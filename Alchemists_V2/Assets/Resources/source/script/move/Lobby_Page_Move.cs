using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby_Page_Move: MonoBehaviour
{
    public GameObject �κ�;
    public GameObject ����ȭ��;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void move_to_front()
    {
        �κ�.gameObject.SetActive(true);
        ����ȭ��.gameObject.SetActive(false);
    }

    public void move_to_explain()
    {
        �κ�.gameObject.SetActive(false);
        ����ȭ��.gameObject.SetActive(true);
    }
}

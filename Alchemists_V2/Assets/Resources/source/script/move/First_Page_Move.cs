using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First_Page_Move : MonoBehaviour
{
    public GameObject ùȭ��;
    public GameObject ����ȭ��;
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
        ùȭ��.gameObject.SetActive(true);
        ����ȭ��.gameObject.SetActive(false);
        ����ȭ��.gameObject.SetActive(false);
    }

    public void move_to_connect_ip()
    {
        ùȭ��.gameObject.SetActive(false);
        ����ȭ��.gameObject.SetActive(true);
        ����ȭ��.gameObject.SetActive(false);
    }

    public void move_to_explain()
    {
        ùȭ��.gameObject.SetActive(false);
        ����ȭ��.gameObject.SetActive(false);
        ����ȭ��.gameObject.SetActive(true);
    }
}

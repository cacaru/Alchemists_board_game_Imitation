using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First_Page_Move : MonoBehaviour
{
    public GameObject 첫화면;
    public GameObject 입장화면;
    public GameObject 설명화면;

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
        첫화면.gameObject.SetActive(true);
        입장화면.gameObject.SetActive(false);
        설명화면.gameObject.SetActive(false);
    }

    public void move_to_connect_ip()
    {
        첫화면.gameObject.SetActive(false);
        입장화면.gameObject.SetActive(true);
        설명화면.gameObject.SetActive(false);
    }

    public void move_to_explain()
    {
        첫화면.gameObject.SetActive(false);
        입장화면.gameObject.SetActive(false);
        설명화면.gameObject.SetActive(true);
    }
}

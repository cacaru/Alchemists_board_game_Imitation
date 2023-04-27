using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_explain4_page : MonoBehaviour
{
    public GameObject page_1;
    public GameObject page_2;
    public GameObject page_3;

    public GameObject next_btn;
    public GameObject pre_btn;

    private int now_page = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void next_page()
    {
        if( now_page == 3)
        {
            // ´õ °¡¼± ¾ÈµÊ
            return;
        }
        now_page += 1;
        switch (now_page)
        {
            case 2:
                pre_btn.gameObject.SetActive(true);
                next_btn.gameObject.SetActive(true);
                page_1.gameObject.SetActive(false);
                page_2.gameObject.SetActive(true);
                page_3.gameObject.SetActive(false);
                break;
            case 3:
                pre_btn.gameObject.SetActive(true);
                next_btn.gameObject.SetActive(false);
                page_1.gameObject.SetActive(false);
                page_2.gameObject.SetActive(false);
                page_3.gameObject.SetActive(true);
                break;
        }
    }

    public void pre_page()
    {
        if( now_page == 1)
        {
            return;
        }
        now_page -= 1;
        switch (now_page)
        {
            case 1:
                pre_btn.gameObject.SetActive(false);
                next_btn.gameObject.SetActive(true);
                page_1.gameObject.SetActive(true);
                page_2.gameObject.SetActive(false);
                page_3.gameObject.SetActive(false);
                break;
            case 2:
                pre_btn.gameObject.SetActive(true);
                next_btn.gameObject.SetActive(true);
                page_1.gameObject.SetActive(false);
                page_2.gameObject.SetActive(true);
                page_3.gameObject.SetActive(false);
                break;
        }
    }

}

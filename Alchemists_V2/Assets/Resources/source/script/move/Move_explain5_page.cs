using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_explain5_page : MonoBehaviour
{
    public GameObject page_1;
    public GameObject page_2;

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
        if (now_page == 2)
        {
            // ´õ °¡¼± ¾ÈµÊ
            return;
        }
        now_page += 1;

        pre_btn.gameObject.SetActive(true);
        next_btn.gameObject.SetActive(false);
        page_1.gameObject.SetActive(false);
        page_2.gameObject.SetActive(true);
    }

    public void pre_page()
    {
        if (now_page == 1)
        {
            return;
        }
        now_page -= 1;

        pre_btn.gameObject.SetActive(false);
        next_btn.gameObject.SetActive(true);
        page_1.gameObject.SetActive(true);
        page_2.gameObject.SetActive(false);

    }
}

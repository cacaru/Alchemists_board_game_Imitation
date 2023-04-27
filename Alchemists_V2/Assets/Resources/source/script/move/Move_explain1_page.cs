using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_explain1_page : MonoBehaviour
{
    // 게임 설명의 5 페이지 연동
    public GameObject page_1;
    public GameObject page_2;
    public GameObject page_3;
    public GameObject page_4;
    public GameObject page_5;

    // 이전 다음 2개의 버튼 연동
    public GameObject pre_btn;
    public GameObject nex_btn;

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
        if( now_page == 5)
        {
            // 마지막 페이지므로 작동되선 안됨
            return;
        }

        // 현재 열려있는 페이지를 끄고
        switch (now_page)
        {
            case 1:
                // 현재 1페이지라면 다음페이지에서 다시 1페이지로 넘어올 수 있게 pre_btn을 켜줌
                pre_btn.gameObject.SetActive(true);
                page_1.gameObject.SetActive(false);
                break;
            case 2:
                page_2.gameObject.SetActive(false);
                break;
            case 3:
                page_3.gameObject.SetActive(false);
                break;
            case 4:
                page_4.gameObject.SetActive(false);
                break;
        }
        // now_page를 증가시키고
        now_page += 1;

        // 증가된 now_page의 페이지를 켬
        switch (now_page)
        {
            case 2:
                page_2.gameObject.SetActive(true);
                break;
            case 3:
                page_3.gameObject.SetActive(true);
                break;
            case 4:
                page_4.gameObject.SetActive(true);
                break;
            case 5:
                // 마지막 페이지의 경우 next_btn이 보이지 않아야함
                nex_btn.gameObject.SetActive(false);
                page_5.gameObject.SetActive(true);
                break;
        }
    }

    public void prev_page()
    {
        if ( now_page == 1)
        {
            return;
        }
        // 현재 열려있는 페이지를 끄고
        switch (now_page)
        {
            case 2:
                // 2페이지면 다음이 1페이지니까 prebtn을 꺼줌
                pre_btn.gameObject.SetActive(false);
                page_2.gameObject.SetActive(false);
                break;
            case 3:
                page_3.gameObject.SetActive(false);
                break;
            case 4:
                page_4.gameObject.SetActive(false);
                break;
            case 5:
                page_5.gameObject.SetActive(false);
                break;
        }
        // now_page를 감소시키고
        now_page -= 1;

        // 증가된 now_page의 페이지를 켬
        switch (now_page)
        {
            case 1:
                page_1.gameObject.SetActive(true);
                break;
            case 2:
                page_2.gameObject.SetActive(true);
                break;
            case 3:
                page_3.gameObject.SetActive(true);
                break;
            case 4:
                // 마지막 페이지로 돌아갈 수 있도록 nex_btn을 켜줌
                nex_btn.gameObject.SetActive(true);
                page_4.gameObject.SetActive(true);
                break;
        }
    }
}

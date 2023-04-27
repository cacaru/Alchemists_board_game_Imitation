using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_explain1_page : MonoBehaviour
{
    // ���� ������ 5 ������ ����
    public GameObject page_1;
    public GameObject page_2;
    public GameObject page_3;
    public GameObject page_4;
    public GameObject page_5;

    // ���� ���� 2���� ��ư ����
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
            // ������ �������Ƿ� �۵��Ǽ� �ȵ�
            return;
        }

        // ���� �����ִ� �������� ����
        switch (now_page)
        {
            case 1:
                // ���� 1��������� �������������� �ٽ� 1�������� �Ѿ�� �� �ְ� pre_btn�� ����
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
        // now_page�� ������Ű��
        now_page += 1;

        // ������ now_page�� �������� ��
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
                // ������ �������� ��� next_btn�� ������ �ʾƾ���
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
        // ���� �����ִ� �������� ����
        switch (now_page)
        {
            case 2:
                // 2�������� ������ 1�������ϱ� prebtn�� ����
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
        // now_page�� ���ҽ�Ű��
        now_page -= 1;

        // ������ now_page�� �������� ��
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
                // ������ �������� ���ư� �� �ֵ��� nex_btn�� ����
                nex_btn.gameObject.SetActive(true);
                page_4.gameObject.SetActive(true);
                break;
        }
    }
}

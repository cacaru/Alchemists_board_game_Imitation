using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Explain_change : MonoBehaviour
{
    public TextMeshProUGUI title;
    public GameObject page_1;
    public GameObject page_2;
    public GameObject page_3;
    public GameObject page_4;
    public GameObject page_5;
    public GameObject page_6;
    public GameObject page_7;
    public GameObject page_8;
    public GameObject page_9;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void exp_1()
    {
        title.text = "���� ����";
        page_1.gameObject.SetActive(true);
        page_2.gameObject.SetActive(false);
        page_3.gameObject.SetActive(false);
        page_4.gameObject.SetActive(false);
        page_5.gameObject.SetActive(false);
        page_6.gameObject.SetActive(false);
        page_7.gameObject.SetActive(false);
        page_8.gameObject.SetActive(false);
        page_9.gameObject.SetActive(false);
    }

    public void exp_2()
    {
        title.text = "��� ���� ����";
        page_1.gameObject.SetActive(false);
        page_2.gameObject.SetActive(true);
        page_3.gameObject.SetActive(false);
        page_4.gameObject.SetActive(false);
        page_5.gameObject.SetActive(false);
        page_6.gameObject.SetActive(false);
        page_7.gameObject.SetActive(false);
        page_8.gameObject.SetActive(false);
        page_9.gameObject.SetActive(false);
    }

    public void exp_3()
    {
        title.text = "��� ��ȯ ����";
        page_1.gameObject.SetActive(false);
        page_2.gameObject.SetActive(false);
        page_3.gameObject.SetActive(true);
        page_4.gameObject.SetActive(false);
        page_5.gameObject.SetActive(false);
        page_6.gameObject.SetActive(false);
        page_7.gameObject.SetActive(false);
        page_8.gameObject.SetActive(false);
        page_9.gameObject.SetActive(false);
    }
    public void exp_4()
    {
        title.text = "���� �ȱ� ����";
        page_1.gameObject.SetActive(false);
        page_2.gameObject.SetActive(false);
        page_3.gameObject.SetActive(false);
        page_4.gameObject.SetActive(true);
        page_5.gameObject.SetActive(false);
        page_6.gameObject.SetActive(false);
        page_7.gameObject.SetActive(false);
        page_8.gameObject.SetActive(false);
        page_9.gameObject.SetActive(false);
    }
    public void exp_5()
    {
        title.text = "������ ���� ����";
        page_1.gameObject.SetActive(false);
        page_2.gameObject.SetActive(false);
        page_3.gameObject.SetActive(false);
        page_4.gameObject.SetActive(false);
        page_5.gameObject.SetActive(true);
        page_6.gameObject.SetActive(false);
        page_7.gameObject.SetActive(false);
        page_8.gameObject.SetActive(false);
        page_9.gameObject.SetActive(false);
    }
    public void exp_6()
    {
        title.text = "�м� �ݹ� ����";
        page_1.gameObject.SetActive(false);
        page_2.gameObject.SetActive(false);
        page_3.gameObject.SetActive(false);
        page_4.gameObject.SetActive(false);
        page_5.gameObject.SetActive(false);
        page_6.gameObject.SetActive(true);
        page_7.gameObject.SetActive(false);
        page_8.gameObject.SetActive(false);
        page_9.gameObject.SetActive(false);
    }
    public void exp_7()
    {
        title.text = "�м� ��ǥ ����";
        page_1.gameObject.SetActive(false);
        page_2.gameObject.SetActive(false);
        page_3.gameObject.SetActive(false);
        page_4.gameObject.SetActive(false);
        page_5.gameObject.SetActive(false);
        page_6.gameObject.SetActive(false);
        page_7.gameObject.SetActive(true);
        page_8.gameObject.SetActive(false);
        page_9.gameObject.SetActive(false);
    }
    public void exp_8()
    {
        title.text = "���� ����";
        page_1.gameObject.SetActive(false);
        page_2.gameObject.SetActive(false);
        page_3.gameObject.SetActive(false);
        page_4.gameObject.SetActive(false);
        page_5.gameObject.SetActive(false);
        page_6.gameObject.SetActive(false);
        page_7.gameObject.SetActive(false);
        page_8.gameObject.SetActive(true);
        page_9.gameObject.SetActive(false);
    }
    public void exp_9()
    {
        title.text = "��ȸ ����";
        page_1.gameObject.SetActive(false);
        page_2.gameObject.SetActive(false);
        page_3.gameObject.SetActive(false);
        page_4.gameObject.SetActive(false);
        page_5.gameObject.SetActive(false);
        page_6.gameObject.SetActive(false);
        page_7.gameObject.SetActive(false);
        page_8.gameObject.SetActive(false);
        page_9.gameObject.SetActive(true);
    }
}

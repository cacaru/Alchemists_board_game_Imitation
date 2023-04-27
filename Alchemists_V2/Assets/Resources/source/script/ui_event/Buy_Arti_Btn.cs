using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��Ƽ��Ʈ ���� ��ư�� ������ �� �۵��� �Լ�
public class Buy_Arti_Btn : MonoBehaviour
{
    private Data_Hub data_hub;
    private Material me;
    // Start is called before the first frame update
    void Start()
    {
        data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();
        me = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        me = Resources.Load<Material>("source/img/icon/Materials/btn_frame_2");
    }

    private void OnMouseExit()
    {
        me = Resources.Load<Material>("source/img/icon/Materials/btn_frame");
    }

    private void OnMouseUp()
    {
        int num = int.Parse(gameObject.name[^1].ToString()) - 1;
        // �� ������Ʈ�� �����Ϸ� �� ������ �۵��ؼ� �ȵ�
        if (num == 8)
        {
            return;
        }
        //�����ϱ�
        data_hub.Arti_num = num;
        GameObject.Find("Switch_Area").transform.Find("Buy_announce").gameObject.SetActive(true);
    }
}

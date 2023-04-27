using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alchemists_data;

public class Object_Selecter : MonoBehaviour
{
    public bool selected = false;
    private Material _target;
    private Data_Hub data_hub;
    private Board_Acting_Checker checker;
    // Start is called before the first frame update
    // ��� ���� Ȥ�� �Ǹ�, �� ��ǥ, ���� ���� ������Ʈ�� Ŭ�� �Ǿ��� ���� �۾��� �ϴ� ��ũ��Ʈ
    void Start()
    {
        data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();
        checker = GameObject.Find("Data_Controller").GetComponent<Board_Acting_Checker>();
        _target = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        //Debug.Log(gameObject.name);
        _target.SetColor("_OutlineColor", OUTLINE_COLOR.HIGHLIGHT);
        _target.SetFloat("_OutlineWidth", 1.2f);
    }

    private void OnMouseExit()
    {
        //Debug.Log(gameObject.name);
        // ������ �Ǿ� �ִ� ������Ʈ�� _OutlineWidth 1.2f, _OutlineColor : OUTLINE_COLOR.DEFAULT
        if (selected)
        {
            _target.SetFloat("_OutlineWidth", 1.2f);   
        }
        else
        {
            _target.SetFloat("_OutlineWidth", 1.0f);
        }
        _target.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);

    }

    private void OnMouseUp()
    {
        // ��� ���� : Ingre_1~5
        // ��� �Ǹ�, �뺴���� �����Ǹ�, ����, ����ȸ �ܰ��� ������Ʈ �̸� :: board_num_ingre_1~8
        // �� ��ǥ �ݹ� �̸� :: ���θ�������
        // select �ѱ��
        // �Ʒ��� ��� �� �ݵ�� �ϳ��� "��" �ɸ� :: board_num���� �����ϱ� ����
        bool board_1 = gameObject.name.StartsWith("Ingre_");
        bool board_2 = gameObject.name.StartsWith("2_");
        bool board_3 = gameObject.name.StartsWith("3_");
        bool board_7 = gameObject.name.StartsWith("7_");
        bool board_8 = gameObject.name.StartsWith("8_");
        bool board_9 = gameObject.name.StartsWith("9_");

        //��� ���� 
        if (board_1)
        {
            if (gameObject.name.Length > 8)
            {
                int ingre_index = int.Parse(gameObject.name[^3].ToString());
                //Debug.Log("ingre_num :: " + ingre_num);
                //Debug.Log("ingre_index :: " + ingre_index);
                data_hub.Select_ingre_num = int.Parse(gameObject.name[^1].ToString());
                data_hub.Select_index = (ingre_index);
                checker.Board_1_selecting_obj_checker(ingre_index);
            }
            else
            {
                data_hub.Select_ingre_num = int.Parse(gameObject.name[^1].ToString());
                data_hub.Select_index = 0;
                checker.Board_1_selecting_obj_checker(0);
            }
        }

        // ��� �Ǹ�
        if (board_2)
        {
            int ingre_num = int.Parse(gameObject.name[^1].ToString());
            data_hub.Sell_item_num = (ingre_num);
            checker.Board_2_selecting_obj_checker(ingre_num);
        }

        // �뺴���� �Ǹ��� ���� ���� // �л����� ���� ���� // �����ο��� ���� ����
        if (board_3 || board_7 || board_8 )
        {
            checker.Board_ingre_checker(int.Parse(gameObject.name[^1].ToString()));
        }

        // ���� ����ȸ�� ���� ����
        if (board_9)
        {

        }
    }
}

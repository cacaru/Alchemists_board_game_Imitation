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
    // 재료 선택 혹은 판매, 논문 발표, 실험 등의 오브젝트가 클릭 되었을 때의 작업을 하는 스크립트
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
        // 선택이 되어 있는 오브젝트면 _OutlineWidth 1.2f, _OutlineColor : OUTLINE_COLOR.DEFAULT
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
        // 재료 선택 : Ingre_1~5
        // 재료 판매, 용병에게 물약판매, 실험, 전시회 단계의 오브젝트 이름 :: board_num_ingre_1~8
        // 논문 발표 반박 이름 :: 새로만들어야함
        // select 넘기기
        // 아래의 경우 중 반드시 하나에 "만" 걸림 :: board_num으로 시작하기 때문
        bool board_1 = gameObject.name.StartsWith("Ingre_");
        bool board_2 = gameObject.name.StartsWith("2_");
        bool board_3 = gameObject.name.StartsWith("3_");
        bool board_7 = gameObject.name.StartsWith("7_");
        bool board_8 = gameObject.name.StartsWith("8_");
        bool board_9 = gameObject.name.StartsWith("9_");

        //재료 선택 
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

        // 재료 판매
        if (board_2)
        {
            int ingre_num = int.Parse(gameObject.name[^1].ToString());
            data_hub.Sell_item_num = (ingre_num);
            checker.Board_2_selecting_obj_checker(ingre_num);
        }

        // 용병에게 판매할 물약 생성 // 학생에게 물약 실험 // 스스로에게 물약 실험
        if (board_3 || board_7 || board_8 )
        {
            checker.Board_ingre_checker(int.Parse(gameObject.name[^1].ToString()));
        }

        // 물약 전시회용 물약 제조
        if (board_9)
        {

        }
    }
}

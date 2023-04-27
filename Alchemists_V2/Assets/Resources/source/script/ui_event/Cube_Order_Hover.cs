using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cube_Order_Hover : MonoBehaviour
{
    public GameObject target_cube;

    private GameObject pre_obj;
    private GameObject cube_announce;
    private Vector2 mos_pos;

    // 큐브 spin 원위치 용 변수
    private Vector3 target_cube_ori_rot;
    // Start is called before the first frame update
    void Start()
    {
        pre_obj = Resources.Load<GameObject>("source/Prefabs/Custom_Object/Cube_Order_Announce");
        target_cube_ori_rot = target_cube.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseEnter()
    {
        // 책 아래쪽의 큐브의 spin 시작
        target_cube.GetComponent<Spin>().enabled = true;

        #region 안내창 띄우기
        // 마우스 위치 인식
        mos_pos = Input.mousePosition;

        // prefab 인스턴스 생성
        cube_announce = Instantiate(pre_obj);
        cube_announce.transform.SetParent(GameObject.Find("GUI").transform.Find("Canvas").Find("Show_area").transform);
        string content = "";

        // 프리팹 위치 및 내용 지정
        if (gameObject.name.Equals("cube_1"))
        {
            content = "금화 1개를 지불하고\n 첫 번째 순서로 차례를 진행합니다.";
        }
        else if (gameObject.name.Equals("cube_2"))
        {
            content = "아무것도 받지않고\n 2번 순서로 차례를 진행합니다.";
        }
        else if (gameObject.name.Equals("cube_3"))
        {
            content = "재료 한개를 받고\n 3번 순서로 차례를 진행합니다.";
        }
        else if (gameObject.name.Equals("cube_4"))
        {
            content = "재료 2개를 받고\n 4번 순서로 차례를 진행합니다.";
        }
        else if (gameObject.name.Equals("cube_5"))
        {
            content = "호의카드 1장과 재료 1개를 받고\n 5번 순서로 차례를 진행합니다.";
        }
        else if (gameObject.name.Equals("cube_6"))
        {
            content = "호의카드 2장을 받고\n 6번 순서로 차례를 진행합니다.";
        }
        else if (gameObject.name.Equals("cube_7"))
        {
            content = "재료 3개를 받고\n 7번 순서로 차례를 진행합니다.";
        }
        else if (gameObject.name.Equals("cube_8"))
        {
            content = "호의카드 1장과 재료 2개를 받고\n 8번 순서로 차례를 진행합니다.";
        }
        else if (gameObject.name.Equals("cube_9"))
        {
            content = "호의카드 1장과 재료 1개를 받습니다.\n벌칙 존으로 임의 선택이 불가능합니다.";
        }

        cube_announce.transform.Find("Content").GetComponent<TMP_Text>().text = content;
        // 프리팹 보이기
        cube_announce.SetActive(true);
        #endregion
    }

    public void OnMouseOver()
    {
        mos_pos = Input.mousePosition;
        if(mos_pos.x + 600 > 1920)
        {
            cube_announce.transform.position = new Vector3(mos_pos.x - 310, mos_pos.y - 185, 0);
        }
        else
        {
            cube_announce.transform.position = new Vector3(mos_pos.x + 310, mos_pos.y - 185, 0);
        }
        
    }

    public void OnMouseExit()
    {
        // spin 종료
        target_cube.transform.localEulerAngles = target_cube_ori_rot;
        target_cube.GetComponent<Spin>().enabled = false;

        // 안내창 삭제
        Destroy(cube_announce);
        //Debug.Log("exit");
    }
}

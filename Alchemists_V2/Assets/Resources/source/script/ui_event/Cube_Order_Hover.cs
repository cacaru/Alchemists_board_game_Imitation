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

    // ť�� spin ����ġ �� ����
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
        // å �Ʒ����� ť���� spin ����
        target_cube.GetComponent<Spin>().enabled = true;

        #region �ȳ�â ����
        // ���콺 ��ġ �ν�
        mos_pos = Input.mousePosition;

        // prefab �ν��Ͻ� ����
        cube_announce = Instantiate(pre_obj);
        cube_announce.transform.SetParent(GameObject.Find("GUI").transform.Find("Canvas").Find("Show_area").transform);
        string content = "";

        // ������ ��ġ �� ���� ����
        if (gameObject.name.Equals("cube_1"))
        {
            content = "��ȭ 1���� �����ϰ�\n ù ��° ������ ���ʸ� �����մϴ�.";
        }
        else if (gameObject.name.Equals("cube_2"))
        {
            content = "�ƹ��͵� �����ʰ�\n 2�� ������ ���ʸ� �����մϴ�.";
        }
        else if (gameObject.name.Equals("cube_3"))
        {
            content = "��� �Ѱ��� �ް�\n 3�� ������ ���ʸ� �����մϴ�.";
        }
        else if (gameObject.name.Equals("cube_4"))
        {
            content = "��� 2���� �ް�\n 4�� ������ ���ʸ� �����մϴ�.";
        }
        else if (gameObject.name.Equals("cube_5"))
        {
            content = "ȣ��ī�� 1��� ��� 1���� �ް�\n 5�� ������ ���ʸ� �����մϴ�.";
        }
        else if (gameObject.name.Equals("cube_6"))
        {
            content = "ȣ��ī�� 2���� �ް�\n 6�� ������ ���ʸ� �����մϴ�.";
        }
        else if (gameObject.name.Equals("cube_7"))
        {
            content = "��� 3���� �ް�\n 7�� ������ ���ʸ� �����մϴ�.";
        }
        else if (gameObject.name.Equals("cube_8"))
        {
            content = "ȣ��ī�� 1��� ��� 2���� �ް�\n 8�� ������ ���ʸ� �����մϴ�.";
        }
        else if (gameObject.name.Equals("cube_9"))
        {
            content = "ȣ��ī�� 1��� ��� 1���� �޽��ϴ�.\n��Ģ ������ ���� ������ �Ұ����մϴ�.";
        }

        cube_announce.transform.Find("Content").GetComponent<TMP_Text>().text = content;
        // ������ ���̱�
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
        // spin ����
        target_cube.transform.localEulerAngles = target_cube_ori_rot;
        target_cube.GetComponent<Spin>().enabled = false;

        // �ȳ�â ����
        Destroy(cube_announce);
        //Debug.Log("exit");
    }
}

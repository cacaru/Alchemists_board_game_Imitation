using Alchemists_data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Data_Controller : MonoBehaviour
{
    #region 3d field
    // ��� ���� ������Ʈ 5��
    public GameObject ingre_1;
    public GameObject ingre_2;
    public GameObject ingre_3;
    public GameObject ingre_4;
    public GameObject ingre_5;

    // �뺴���� ���� �Ǹ�
    public GameObject adventurer_image;
    public GameObject sell_potion_1;
    public GameObject sell_potion_2;
    public GameObject sell_potion_3;

    // ���� ���� 3��
    public GameObject arti_1;
    public TMP_Text arti_comment_1;
    public TMP_Text arti_name_1;
    public TMP_Text arti_price_1;
    public TMP_Text arti_point_1;
    public GameObject arti_btn_1;
    public GameObject arti_2;
    public TMP_Text arti_comment_2;
    public TMP_Text arti_name_2;
    public TMP_Text arti_price_2;
    public TMP_Text arti_point_2;
    public GameObject arti_btn_2;
    public GameObject arti_3;
    public TMP_Text arti_comment_3;
    public TMP_Text arti_name_3;
    public TMP_Text arti_price_3;
    public TMP_Text arti_point_3;
    public GameObject arti_btn_3;

    // �� �ݹ�
    public GameObject board_5_ingre_1;
    public GameObject board_5_element_image_1;
    public GameObject board_5_stamp_1_1;
    public GameObject board_5_stamp_1_2;
    public GameObject board_5_stamp_1_3;
    public GameObject board_5_ingre_2;
    public GameObject board_5_element_image_2;
    public GameObject board_5_stamp_2_1;
    public GameObject board_5_stamp_2_2;
    public GameObject board_5_stamp_2_3;
    public GameObject board_5_ingre_3;
    public GameObject board_5_element_image_3;
    public GameObject board_5_stamp_3_1;
    public GameObject board_5_stamp_3_2;
    public GameObject board_5_stamp_3_3;
    public GameObject board_5_ingre_4;
    public GameObject board_5_element_image_4;
    public GameObject board_5_stamp_4_1;
    public GameObject board_5_stamp_4_2;
    public GameObject board_5_stamp_4_3;
    public GameObject board_5_ingre_5;
    public GameObject board_5_element_image_5;
    public GameObject board_5_stamp_5_1;
    public GameObject board_5_stamp_5_2;
    public GameObject board_5_stamp_5_3;
    public GameObject board_5_ingre_6;
    public GameObject board_5_element_image_6;
    public GameObject board_5_stamp_6_1;
    public GameObject board_5_stamp_6_2;
    public GameObject board_5_stamp_6_3;
    public GameObject board_5_ingre_7;
    public GameObject board_5_element_image_7;
    public GameObject board_5_stamp_7_1;
    public GameObject board_5_stamp_7_2;
    public GameObject board_5_stamp_7_3;
    public GameObject board_5_ingre_8;
    public GameObject board_5_element_image_8;
    public GameObject board_5_stamp_8_1;
    public GameObject board_5_stamp_8_2;
    public GameObject board_5_stamp_8_3;
    // �� ��ǥ
    public GameObject board_6_ingre_1;
    public GameObject board_6_element_image_1;
    public GameObject board_6_stamp_1_1;
    public GameObject board_6_stamp_1_2;
    public GameObject board_6_stamp_1_3;
    public GameObject board_6_ingre_2;
    public GameObject board_6_element_image_2;
    public GameObject board_6_stamp_2_1;
    public GameObject board_6_stamp_2_2;
    public GameObject board_6_stamp_2_3;
    public GameObject board_6_ingre_3;
    public GameObject board_6_element_image_3;
    public GameObject board_6_stamp_3_1;
    public GameObject board_6_stamp_3_2;
    public GameObject board_6_stamp_3_3;
    public GameObject board_6_ingre_4;
    public GameObject board_6_element_image_4;
    public GameObject board_6_stamp_4_1;
    public GameObject board_6_stamp_4_2;
    public GameObject board_6_stamp_4_3;
    public GameObject board_6_ingre_5;
    public GameObject board_6_element_image_5;
    public GameObject board_6_stamp_5_1;
    public GameObject board_6_stamp_5_2;
    public GameObject board_6_stamp_5_3;
    public GameObject board_6_ingre_6;
    public GameObject board_6_element_image_6;
    public GameObject board_6_stamp_6_1;
    public GameObject board_6_stamp_6_2;
    public GameObject board_6_stamp_6_3;
    public GameObject board_6_ingre_7;
    public GameObject board_6_element_image_7;
    public GameObject board_6_stamp_7_1;
    public GameObject board_6_stamp_7_2;
    public GameObject board_6_stamp_7_3;
    public GameObject board_6_ingre_8;
    public GameObject board_6_element_image_8;
    public GameObject board_6_stamp_8_1;
    public GameObject board_6_stamp_8_2;
    public GameObject board_6_stamp_8_3;
    // �л� ���� ��� ����
    public TMP_Text board_7_ingre_cnt_1;
    public TMP_Text board_7_ingre_cnt_2;
    public TMP_Text board_7_ingre_cnt_3;
    public TMP_Text board_7_ingre_cnt_4;
    public TMP_Text board_7_ingre_cnt_5;
    public TMP_Text board_7_ingre_cnt_6;
    public TMP_Text board_7_ingre_cnt_7;
    public TMP_Text board_7_ingre_cnt_8;
    // �ڱ� ���� ��� ����
    public TMP_Text board_8_ingre_cnt_1;
    public TMP_Text board_8_ingre_cnt_2;
    public TMP_Text board_8_ingre_cnt_3;
    public TMP_Text board_8_ingre_cnt_4;
    public TMP_Text board_8_ingre_cnt_5;
    public TMP_Text board_8_ingre_cnt_6;
    public TMP_Text board_8_ingre_cnt_7;
    public TMP_Text board_8_ingre_cnt_8;
    #endregion

    private GameObject[] board_5_ingre_list = new GameObject[9];
    private GameObject[] board_5_ele_list = new GameObject[9];
    private GameObject[] board_5_stamp_1_list = new GameObject[9];
    private GameObject[] board_5_stamp_2_list = new GameObject[9];
    private GameObject[] board_5_stamp_3_list = new GameObject[9];

    private GameObject[] board_6_ingre_list = new GameObject[9];
    private GameObject[] board_6_ele_list = new GameObject[9];
    private GameObject[] board_6_stamp_1_list = new GameObject[9];
    private GameObject[] board_6_stamp_2_list = new GameObject[9];
    private GameObject[] board_6_stamp_3_list = new GameObject[9];
    // Start is called before the first frame updat
    void Start()
    {
        #region board_5,6�� ������Ʈ�� �����ϱ� ���ϰ� �迭ȭ�ص�
        board_5_ingre_list[1] = board_5_ingre_1;
        board_5_ingre_list[2] = board_5_ingre_2;
        board_5_ingre_list[3] = board_5_ingre_3;
        board_5_ingre_list[4] = board_5_ingre_4;
        board_5_ingre_list[5] = board_5_ingre_5;
        board_5_ingre_list[6] = board_5_ingre_6;
        board_5_ingre_list[7] = board_5_ingre_7;
        board_5_ingre_list[8] = board_5_ingre_8;

        board_6_ingre_list[1] = board_6_ingre_1;
        board_6_ingre_list[2] = board_6_ingre_2;
        board_6_ingre_list[3] = board_6_ingre_3;
        board_6_ingre_list[4] = board_6_ingre_4;
        board_6_ingre_list[5] = board_6_ingre_5;
        board_6_ingre_list[6] = board_6_ingre_6;
        board_6_ingre_list[7] = board_6_ingre_7;
        board_6_ingre_list[8] = board_6_ingre_8;

        board_5_ele_list[1] = board_5_element_image_1;
        board_5_ele_list[2] = board_5_element_image_2;
        board_5_ele_list[3] = board_5_element_image_3;
        board_5_ele_list[4] = board_5_element_image_4;
        board_5_ele_list[5] = board_5_element_image_5;
        board_5_ele_list[6] = board_5_element_image_6;
        board_5_ele_list[7] = board_5_element_image_7;
        board_5_ele_list[8] = board_5_element_image_8;

        board_6_ele_list[1] = board_6_element_image_1;
        board_6_ele_list[2] = board_6_element_image_2;
        board_6_ele_list[3] = board_6_element_image_3;
        board_6_ele_list[4] = board_6_element_image_4;
        board_6_ele_list[5] = board_6_element_image_5;
        board_6_ele_list[6] = board_6_element_image_6;
        board_6_ele_list[7] = board_6_element_image_7;
        board_6_ele_list[8] = board_6_element_image_8;

        board_5_stamp_1_list[1] = board_5_stamp_1_1;
        board_5_stamp_1_list[2] = board_5_stamp_2_1;
        board_5_stamp_1_list[3] = board_5_stamp_3_1;
        board_5_stamp_1_list[4] = board_5_stamp_4_1;
        board_5_stamp_1_list[5] = board_5_stamp_5_1;
        board_5_stamp_1_list[6] = board_5_stamp_6_1;
        board_5_stamp_1_list[7] = board_5_stamp_7_1;
        board_5_stamp_1_list[8] = board_5_stamp_8_1;

        board_5_stamp_2_list[1] = board_5_stamp_1_2;
        board_5_stamp_2_list[2] = board_5_stamp_2_2;
        board_5_stamp_2_list[3] = board_5_stamp_3_2;
        board_5_stamp_2_list[4] = board_5_stamp_4_2;
        board_5_stamp_2_list[5] = board_5_stamp_5_2;
        board_5_stamp_2_list[6] = board_5_stamp_6_2;
        board_5_stamp_2_list[7] = board_5_stamp_7_2;
        board_5_stamp_2_list[8] = board_5_stamp_8_2;

        board_5_stamp_3_list[1] = board_5_stamp_1_3;
        board_5_stamp_3_list[2] = board_5_stamp_2_3;
        board_5_stamp_3_list[3] = board_5_stamp_3_3;
        board_5_stamp_3_list[4] = board_5_stamp_4_3;
        board_5_stamp_3_list[5] = board_5_stamp_5_3;
        board_5_stamp_3_list[6] = board_5_stamp_6_3;
        board_5_stamp_3_list[7] = board_5_stamp_7_3;
        board_5_stamp_3_list[8] = board_5_stamp_8_3;

        board_6_stamp_1_list[1] = board_6_stamp_1_1;
        board_6_stamp_1_list[2] = board_6_stamp_2_1;
        board_6_stamp_1_list[3] = board_6_stamp_3_1;
        board_6_stamp_1_list[4] = board_6_stamp_4_1;
        board_6_stamp_1_list[5] = board_6_stamp_5_1;
        board_6_stamp_1_list[6] = board_6_stamp_6_1;
        board_6_stamp_1_list[7] = board_6_stamp_7_1;
        board_6_stamp_1_list[8] = board_6_stamp_8_1;

        board_6_stamp_2_list[1] = board_6_stamp_1_2;
        board_6_stamp_2_list[2] = board_6_stamp_2_2;
        board_6_stamp_2_list[3] = board_6_stamp_3_2;
        board_6_stamp_2_list[4] = board_6_stamp_4_2;
        board_6_stamp_2_list[5] = board_6_stamp_5_2;
        board_6_stamp_2_list[6] = board_6_stamp_6_2;
        board_6_stamp_2_list[7] = board_6_stamp_7_2;
        board_6_stamp_2_list[8] = board_6_stamp_8_2;

        board_6_stamp_3_list[1] = board_6_stamp_1_3;
        board_6_stamp_3_list[2] = board_6_stamp_2_3;
        board_6_stamp_3_list[3] = board_6_stamp_3_3;
        board_6_stamp_3_list[4] = board_6_stamp_4_3;
        board_6_stamp_3_list[5] = board_6_stamp_5_3;
        board_6_stamp_3_list[6] = board_6_stamp_6_3;
        board_6_stamp_3_list[7] = board_6_stamp_7_3;
        board_6_stamp_3_list[8] = board_6_stamp_8_3;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // ���� ������ �����ϴ� ���� �ʱ�ȭ �� ���⼭ �սô�
    public Dictionary<string, Dictionary<int, Artifacts_Info>> Init_artifacts_info()
    {
        Dictionary<string, Dictionary<int, Artifacts_Info>> result = new();

        Dictionary<int, Artifacts_Info> tmp = new();
        Artifacts_Info tmp_info;
        int gold = 0;
        int point = 0;
        string name = "";
        string comment = "";
        string kor_name = "";

        // rank_1 �� 1~6���� �ۼ�
        #region rank_1
        // rank_1-1
        name = "discount_card";
        kor_name = "���� ī��";
        gold = 3;
        point = 1;
        comment = "������ �����ϴ� �������� ��ȭ 2���� ���� ���ϴ�. �� �������ʹ� ��ȭ 1���� ���� ���ϴ�.";
        tmp_info = new(gold, point, name, comment, kor_name);
        tmp.Add(1, tmp_info);

        // rank_1-2
        name = "haste_boots";
        kor_name = "�ӵ� ��� ��ȭ";
        gold = 4;
        point = 2;
        comment = "�ڽ��� ť�갡 �ϳ��� �ִ� �ൿ ĭ �� ������ ��ΰ� �ൿ�� ��ģ ��, �� �� �� �� �ൿ�� �� �� �ֽ��ϴ�. ����� 1ȸ ��밡���ϸ�, �뺴���� �����Ǹſ��� ����� �� �����ϴ�.";
        tmp_info = new(gold, point, name, comment, kor_name);
        tmp.Add(2, tmp_info);

        // rank_1-3
        name = "magic_mortar";
        kor_name = "������ ����";
        gold = 3;
        point = 1;
        comment = "������ ���� ��, ��� �� �� ���� �����ϴ�. �������� �ٸ������ �������� ���ϴ�.";
        tmp_info = new(gold, point, name, comment, kor_name);
        tmp.Add(3, tmp_info);

        // rank_1-4
        name = "night_vision";
        kor_name = "�����";
        gold = 3;
        point = 1;
        comment = "�ٸ� ����� ������ �Ȱų� ������ ��� �� ��� �� �ϳ��� �� �� �ֽ��ϴ�. �� ���� �������� ���ϴ�. ����� 1ȸ ��� �����մϴ�.";
        tmp_info = new(gold, point, name, comment, kor_name);
        tmp.Add(4, tmp_info);

        // rank_1-5
        name = "printing_machine";
        kor_name = "�μ��";
        gold = 4;
        point = 2;
        comment = "�м��� ��ǥ�ϰų� ������ �� ��ȭ 1���� �������� �ʽ��ϴ�.";
        tmp_info = new(gold, point, name, comment, kor_name);
        tmp.Add(5, tmp_info);

        // rank_1-6
        name = "robe_of_respect";
        kor_name = "������ ����";
        gold = 4;
        point = 0;
        comment = "�� ������ ���� �� ���� 1���� �� ����ϴ�. ������ ���忡�� �������� �ʽ��ϴ�.";
        tmp_info = new(gold, point, name, comment, kor_name);
        tmp.Add(6, tmp_info);
        #endregion
        // rank_1 �Է�
        result.Add("rank_1", tmp);
        tmp = new();    // tmp �ʱ�ȭ

        // rank_2 ����
        #region rank_2
        // rank_2 - 1
        name = "chest_of_witch";
        kor_name = "������ ��¦";
        gold = 3;
        point = 2;
        comment = "��� ȿ���� ���� : ��� ī�� 7���� �޽��ϴ�. ��� �������� �÷��̾� ������ ���� �� ���ī�带 ���� �� �����ϴ�.";
        tmp_info = new(gold, point, name, comment, kor_name);
        tmp.Add(1, tmp_info);

        // rank_2 - 2
        name = "eloquent_necklace";
        kor_name = "�޺����� �����";
        gold = 4;
        point = 0;
        comment = "��� ȿ���� ���� : �� ���� 5���� ����ϴ�.";
        tmp_info = new(gold, point, name, comment, kor_name);
        tmp.Add(2, tmp_info);

        // rank_2 - 3
        name = "hypnotic_necklace";
        kor_name = "�ָ���� �����";
        gold = 3;
        point = 1;
        comment = "��� ȿ���� ���� : ȣ�� ī�� 4���� �޽��ϴ�.";
        tmp_info = new(gold, point, name, comment, kor_name);
        tmp.Add(3, tmp_info);

        // rank_2 - 4
        name = "seal_of_authority";
        kor_name = "������ ����";
        gold = 4;
        point = 0;
        comment = "�м��� ��ǥ�ϰų� ������ �� ����, �� ���� 2���� �߰��� ����ϴ�.";
        tmp_info = new(gold, point, name, comment, kor_name);
        tmp.Add(4, tmp_info);

        // rank_2 - 5
        name = "silver_glass";
        kor_name = "�� ��";
        gold = 4;
        point = 6;
        comment = "ȿ���� ������ �������� ������ �����մϴ�.";
        tmp_info = new(gold, point, name, comment, kor_name);
        tmp.Add(5, tmp_info);

        // rank_2 - 6
        name = "thinking_hat";
        kor_name = "�����ϴ� ����";
        gold = 4;
        point = 1;
        comment = "��� ȿ���� ���� : �ڽ��� ���� ��� ī�带 �ִ� �� �� �����մϴ�. �ϳ��� ��� ī�带 �� �ֿ� ���ÿ� ������ �� �����ϴ�. ��Ḧ ������ �ʽ��ϴ�.";
        tmp_info = new(gold, point, name, comment, kor_name);
        tmp.Add(6, tmp_info);
        #endregion
        // rank_2 �Է�
        result.Add("rank_2", tmp);
        tmp = new();    // tmp �ʱ�ȭ

        // rank_3 ����
        #region rank_3
        // rank_3 - 1
        name = "bronze_cup";
        kor_name = "û����";
        gold = 4;
        point = 4;
        comment = "Ư���� ȿ���� ������ ������ �����մϴ�.";
        tmp_info = new(gold, point, name, comment, kor_name);
        tmp.Add(1, tmp_info);

        // rank_3 - 2
        name = "feather_hat";
        kor_name = "���� ����";
        gold = 3;
        point = 0;
        comment = "����ȸ ���� : ���������� ������ ������ ������ ���� �ΰ�, ���� ��� ���� ����� �������� ���� 1���� ����ϴ�.";
        tmp_info = new(gold, point, name, comment, kor_name);
        tmp.Add(2, tmp_info);

        // rank_3 - 3
        name = "glass_cabinet";
        kor_name = "���� �����";
        gold = 5;
        point = 0;
        comment = "���� ��� ����� �� �������� �����Ͽ� (������ ���� * 2)��ŭ ������ ����ϴ�.";
        tmp_info = new(gold, point, name, comment, kor_name);
        tmp.Add(3, tmp_info);

        // rank_3 - 4
        name = "golden_alter";
        kor_name = "Ȳ�� ����";
        gold = 1;
        point = 0;
        comment = "��� ��� : ��ȭ�� 1������ 8������ �� �� �ֽ��ϴ�. ������ ��ȭ��ŭ �� ������ ����ϴ�.";
        tmp_info = new(gold, point, name, comment, kor_name);
        tmp.Add(4, tmp_info);

        // rank_3 - 5
        name = "magic_mirror";
        kor_name = "���� �ſ�";
        gold = 4;
        point = 0;
        comment = "���� ���� ���� ���� �ߴ� �� 5������ ���� 1���� ȹ���մϴ�.";
        tmp_info = new(gold, point, name, comment, kor_name);
        tmp.Add(5, tmp_info);

        // rank_3 - 6
        name = "statue_of_wisdom";
        kor_name = "������ ������";
        gold = 4;
        point = 0;
        comment = "���� ���� ���� ���� �м��� ���� �ڽ��� ���帶�� ���� 1���� ȹ���մϴ�.";
        tmp_info = new(gold, point, name, comment, kor_name);
        tmp.Add(6, tmp_info);
        #endregion
        // rank_3 �Է�
        result.Add("rank_3", tmp);

        return result;
    }

    ////////////////////////////////////                  ////////////////////////////////////////////////
    //////////////////////////////////// ������Ʈ ���� �κ� ////////////////////////////////////////////////
    ////////////////////////////////////                  ////////////////////////////////////////////////

    // ���� ���� ť�� �ʱ�ȭ
    public void Init_round_order_cube()//(int max_count)
    {
        // ����� ���� �ʱ�ȭ
        GameObject.Find("Front").transform.Find("Select_order_obj").Find("Order_Cube_Selecter").GetComponent<Order_Cube_Select_Event>().selected_round_order = new int[0];

        // ����� ť����� ��� �� �ʱ�ȭ
        Color DEFAULT = new Color(82 / 255f, 82 / 255f, 82 / 255f, 1);

        GameObject _target = GameObject.Find("Select_order_obj").transform.Find("Under_3").gameObject;
        /*
        if (max_count > 3)
            _target = GameObject.Find("Select_order_obj").transform.Find("Over_4").gameObject;
        else
            _target = GameObject.Find("Select_order_obj").transform.Find("Under_3").gameObject;
        */

        _target.transform.Find("Paint_wall_1").Find("Cube_1").GetComponent<Renderer>().material.color = DEFAULT;
        _target.transform.Find("Paint_wall_1").Find("Cube_2").GetComponent<Renderer>().material.color = DEFAULT;
        _target.transform.Find("Paint_wall_1").Find("Cube_3").GetComponent<Renderer>().material.color = DEFAULT;

        _target.transform.Find("Paint_wall_2").Find("Cube_4").GetComponent<Renderer>().material.color = DEFAULT;
        _target.transform.Find("Paint_wall_2").Find("Cube_5").GetComponent<Renderer>().material.color = DEFAULT;
        _target.transform.Find("Paint_wall_2").Find("Cube_6").GetComponent<Renderer>().material.color = DEFAULT;

        _target.transform.Find("Paint_wall_3").Find("Cube_8").GetComponent<Renderer>().material.color = DEFAULT;
        _target.transform.Find("Paint_wall_3").Find("Cube_9").GetComponent<Renderer>().material.color = DEFAULT;
    }

    // ��� ���� 5��
    public void Controller_select_ingre(int[] ingredient_select_arr)
    {
        GameObject[] tmp = { ingre_1, ingre_2, ingre_3, ingre_4, ingre_5 };
        for(int i =0; i< 5; i++)
        {
            string path = ingredient_select_arr[i] switch
            {
                1 => "source/img/ingre/card_1",
                2 => "source/img/ingre/card_2",
                3 => "source/img/ingre/card_3",
                4 => "source/img/ingre/card_4",
                5 => "source/img/ingre/card_5",
                6 => "source/img/ingre/card_6",
                7 => "source/img/ingre/card_7",
                8 => "source/img/ingre/card_8",
                _ => "source/img/ingre/card_back",
            };
            tmp[i].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
            tmp[i].name = "Ingre_"+ (i) + "_" + ingredient_select_arr[i];
        }

        GameObject.Find("Room_Part_1").transform.Find("Show_Ingre_Sect").Find("Ingre_0").GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/ingre/card_back"));
    }

    // �뺴���� �����Ǹ� �̹���
    public void Controller_adventurer_marking(List<Dictionary<string, bool>> adv_data, int adv_num)
    {
        // �뺴 �̹��� �ֱ�
        string path = adv_num switch
        {
            1 => "source/img/adventurer/Materials/adventurer_1",
            2 => "source/img/adventurer/Materials/adventurer_2",
            3 => "source/img/adventurer/Materials/adventurer_3",
            4 => "source/img/adventurer/Materials/adventurer_4",
            5 => "source/img/adventurer/Materials/adventurer_5",
            6 => "source/img/adventurer/Materials/adventurer_6",
            _ => "",
        };
        adventurer_image.GetComponent<Renderer>().material = Resources.Load<Material>(path);

        // �뺴���� �Ǹ� ������ �̹��� �ֱ�
        // �������� 3:red,2:green,1:blue ��
        if (adv_data[adv_num]["red_1"])
        {
            sell_potion_3.GetComponent<Renderer>().material = Resources.Load<Material>("source/img/potion/Materials/red_+");
        }
        else if (adv_data[adv_num]["red_0"])
        {
            sell_potion_3.GetComponent<Renderer>().material = Resources.Load<Material>("source/img/potion/Materials/red_-");
        }

        if (adv_data[adv_num]["green_1"])
        {
            sell_potion_2.GetComponent<Renderer>().material = Resources.Load<Material>("source/img/potion/Materials/green_+");
        }
        else if (adv_data[adv_num]["green_0"])
        {
            sell_potion_2.GetComponent<Renderer>().material = Resources.Load<Material>("source/img/potion/Materials/green_-");
        }

        if (adv_data[adv_num]["blue_1"])
        {
            sell_potion_1.GetComponent<Renderer>().material = Resources.Load<Material>("source/img/potion/Materials/blue_+");
        }
        else if (adv_data[adv_num]["blue_0"])
        {
            sell_potion_1.GetComponent<Renderer>().material = Resources.Load<Material>("source/img/potion/Materials/blue_-");
        }
        //sell_potion_1;
        //sell_potion_2;
        //sell_potion_3;

    }

    // ���� ���� 3��
    public void Controller_selling_arti(int now_rank, int[] arti_num, Dictionary<string, Dictionary<int, Artifacts_Info>> arti_info)
    {
        bool rank_1 = false;
        bool rank_2 = false;
        bool rank_3 = false;
        string rank = "";
        switch (now_rank)
        {
            case 1: 
                rank_1 = true;
                rank = "rank_1";
                break;
            case 2: rank_2 = true;
                rank = "rank_2";
                break;
            case 3: rank_3 = true;
                rank = "rank_3";
                break;
        }
        for (int i = 0; i < 3; i++)
        {
            string path = "";
            if (rank_1)
            {
                path = arti_num[i] switch
                {
                    1 => "source/img/artifacts/rank_1/Materials/discount_card",
                    2 => "source/img/artifacts/rank_1/Materials/haste_boots",
                    3 => "source/img/artifacts/rank_1/Materials/magic_mortar",
                    4 => "source/img/artifacts/rank_1/Materials/night_vision",
                    5 => "source/img/artifacts/rank_1/Materials/printing_machine",
                    6 => "source/img/artifacts/rank_1/Materials/robe_of_respect",
                    _ => "source/img/artifacts/rank_1/Materials/rank_1_back",
                };
            }

            if (rank_2)
            {
                path = arti_num[i] switch
                {
                    1 => "source/img/artifacts/rank_2/Materials/chest_of_witch",
                    2 => "source/img/artifacts/rank_2/Materials/eloquent_necklace",
                    3 => "source/img/artifacts/rank_2/Materials/hypnotic_necklace",
                    4 => "source/img/artifacts/rank_2/Materials/seal_of_authority",
                    5 => "source/img/artifacts/rank_2/Materials/silver_glass",
                    6 => "source/img/artifacts/rank_2/Materials/thinking_hat",
                    _ => "source/img/artifacts/rank_2/Materials/rank_2_back",
                };
            }

            if (rank_3)
            {
                path = arti_num[i] switch
                {
                    1 => "source/img/artifacts/rank_3/Materials/bronze_cup",
                    2 => "source/img/artifacts/rank_3/Materials/feather_hat",
                    3 => "source/img/artifacts/rank_3/Materials/glass_cabinet",
                    4 => "source/img/artifacts/rank_3/Materials/golden_alter",
                    5 => "source/img/artifacts/rank_3/Materials/magic_mirror",
                    6 => "source/img/artifacts/rank_3/Materials/statue_of_wisdom",
                    _ => "source/img/artifacts/rank_3/Materials/rank_3_back",
                };
            }

            if (i == 0)
            {
                arti_1.GetComponent<Renderer>().material = Resources.Load<Material>(path);
                //Debug.Log(arti_info[rank][arti_num[i]].Name);
                //Debug.Log(arti_num[i]);
                if (arti_num[i] == 0)
                {
                    arti_comment_1.text = "";
                    arti_price_1.text = "";
                    arti_point_1.text = "";
                    arti_name_1.text = "����";
                    arti_btn_1.name = "empty_9";
                }
                else
                {
                    arti_comment_1.text = arti_info[rank][arti_num[i]].Comment;
                    arti_price_1.text = arti_info[rank][arti_num[i]].Gold.ToString() + "G";
                    arti_point_1.text = arti_info[rank][arti_num[i]].Point.ToString();
                    arti_name_1.text = arti_info[rank][arti_num[i]].Kor_name;
                    arti_btn_1.name = "item_" + arti_num[i].ToString();
                }
                
            }
            else if (i == 1)
            {
                arti_2.GetComponent<Renderer>().material = Resources.Load<Material>(path);
                //Debug.Log(arti_info[rank][arti_num[i]].Name);
                //Debug.Log(arti_num[i]);
                if (arti_num[i] == 0)
                {
                    arti_comment_2.text = "";
                    arti_price_2.text = "";
                    arti_point_2.text = "";
                    arti_name_2.text = "����";
                    arti_btn_2.name = "empty_9";
                }
                else
                {
                    arti_comment_2.text = arti_info[rank][arti_num[i]].Comment;
                    arti_price_2.text = arti_info[rank][arti_num[i]].Gold.ToString() + "G";
                    arti_point_2.text = arti_info[rank][arti_num[i]].Point.ToString();
                    arti_name_2.text = arti_info[rank][arti_num[i]].Kor_name;
                    arti_btn_2.name = "item_" + arti_num[i].ToString();
                }
            }
            else if (i == 2)
            {
                arti_3.GetComponent<Renderer>().material = Resources.Load<Material>(path);
                //Debug.Log(arti_info[rank][arti_num[i]].Name);
                //Debug.Log(arti_num[i]);
                if (arti_num[i] == 0)
                {
                    arti_comment_3.text = "";
                    arti_price_3.text = "";
                    arti_point_3.text = "";
                    arti_name_3.text = "����";
                    arti_btn_3.name = "empty_9";
                }
                else
                {
                    arti_comment_3.text = arti_info[rank][arti_num[i]].Comment;
                    arti_price_3.text = arti_info[rank][arti_num[i]].Gold.ToString() + "G";
                    arti_point_3.text = arti_info[rank][arti_num[i]].Point.ToString();
                    arti_name_3.text = arti_info[rank][arti_num[i]].Kor_name;
                    arti_btn_3.name = "item_" + arti_num[i].ToString();
                }
                
            }
        }
        
    }

    // �� �ݹ�
    // �� ��ǥ
    public void Controller_theory_core_init()
    {
        for(int i = 1; i < 9; i++)
        {
            string path = i switch
            {
                1 => "source/img/ingre/card_1",
                2 => "source/img/ingre/card_2",
                3 => "source/img/ingre/card_3",
                4 => "source/img/ingre/card_4",
                5 => "source/img/ingre/card_5",
                6 => "source/img/ingre/card_6",
                7 => "source/img/ingre/card_7",
                8 => "source/img/ingre/card_8",
                _ => "source/img/ingre/card_back"
            };

            board_5_ingre_list[i].gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
            board_6_ingre_list[i].gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
        }
    }

    public void Controller_theory_core_selecter(int num, bool onoff)
    {
        if (onoff)
        {
            if (num == 5)
            {
                for (int i = 1; i < 9; i++)
                {
                    if (board_5_ingre_list[i].GetComponent<Theory_Core_Selecter>()) continue;

                    board_5_ingre_list[i].AddComponent<Theory_Core_Selecter>();
                }
            }
            else if (num == 6)
            {
                for (int i = 1; i < 9; i++)
                {
                    if (board_6_ingre_list[i].GetComponent<Theory_Core_Selecter>()) continue;

                    board_6_ingre_list[i].AddComponent<Theory_Core_Selecter>();
                }
            }
        }
        else
        {
            if (num == 5)
            {
                for (int i = 1; i < 9; i++)
                {
                    Destroy(board_5_ingre_list[i].GetComponent<Theory_Core_Selecter>());
                    board_5_ingre_list[i].GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
                }
            }
            else if (num == 6)
            {
                for (int i = 1; i < 9; i++)
                {
                    Destroy(board_6_ingre_list[i].GetComponent<Theory_Core_Selecter>());
                    board_6_ingre_list[i].GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
                }
            }
        }
    }

    public void Controller_theory_field(Dictionary<int, Theory_Data> theory_data, string my_key)
    {
        for(int i = 1; i < 9; i++)
        {
            // element�� ���� �׸� -> ����ִٸ� blank �̹����� ����
            string ele_path = "";
            string pot_1_path = "", pot_2_path = "", pot_3_path = "";
            /*Debug.Log(theory_data.Count);*/
            //Debug.Log(theory_data[i].element);
            if (string.IsNullOrEmpty(theory_data[i].element.ToString()) || theory_data[i].element == 0)
            {
                ele_path = "source/img/icon/Materials/question_mark_2";
                pot_1_path = "source/img/icon/Materials/question_mark_2";
                pot_2_path = "source/img/icon/Materials/question_mark_2";
                pot_3_path = "source/img/icon/Materials/question_mark_2";
            }
            // ele�� ������� ������
            else
            {
                ele_path = theory_data[i].element switch
                {
                    1 => "source/img/ingre/Materials/rgbl010",
                    2 => "source/img/ingre/Materials/rgbl101",
                    3 => "source/img/ingre/Materials/rglb011",
                    4 => "source/img/ingre/Materials/rglb100",
                    5 => "source/img/ingre/Materials/rlgb001",
                    6 => "source/img/ingre/Materials/rlgb110",
                    7 => "source/img/ingre/Materials/rlglbl000",
                    8 => "source/img/ingre/Materials/rlglbl111",
                    _ => "source/img/icon/Materials/question_mark_2",
                };
                //Debug.Log(theory_data[i].stamp_1.color);
                // ele�� ������ point 1�� �ݵ�� ������

                // ������ �ڽ��� ���� �ƴϸ� ���� �޸����θ� ǥ��Ǿ����
                if (theory_data[i].stamp_1.user_key.Equals(my_key))
                {
                    pot_1_path = theory_data[i].stamp_1.color switch
                    {
                        "red" => "source/img/stamp/Materials/stamp_red_",
                        "blue" => "source/img/stamp/Materials/stamp_blue_",
                        "black" => "source/img/stamp/Materials/stamp_black_",
                        "white" => "source/img/stamp/Materials/stamp_white_",
                        _ => "source/img/icon/Materials/question_mark_2",
                    };
                    //Debug.Log(theory_data[i].stamp_1.point);
                    pot_1_path += theory_data[i].stamp_1.point switch
                    {
                        "point_5_1" or "point_5_2" => "5",
                        "point_3_1" or "point_3_2" or "point_3_3" => "3",
                        "question_red_1" or "question_red_2" => "red",
                        "question_green_1" or "question_green_2" => "green",
                        "question_blue_1" or "question_blue_2" => "blue",
                        _ => "source/img/icon/Materials/question_mark_2",
                    };
                }
                else
                {
                    pot_1_path = theory_data[i].stamp_1.color switch
                    {
                        "red" => "source/img/stamp/Materials/stamp_red_back",
                        "blue" => "source/img/stamp/Materials/stamp_blue_back",
                        "black" => "source/img/stamp/Materials/stamp_black_back",
                        "white" => "source/img/stamp/Materials/stamp_white_back",
                        _ => "source/img/icon/Materials/question_mark_2",
                    };
                }
                

                // point_2 �� ���� ������ �ι��� path �� ����
                if (!string.IsNullOrEmpty(theory_data[1].stamp_2.color))
                {
                    if (theory_data[i].stamp_2.user_key.Equals(my_key))
                    {
                        pot_2_path = theory_data[i].stamp_2.color switch
                        {
                            "red" => "source/img/stamp/Materials/stamp_red_",
                            "blue" => "source/img/stamp/Materials/stamp_blue_",
                            "black" => "source/img/stamp/Materials/stamp_black_",
                            "white" => "source/img/stamp/Materials/stamp_white_",
                            _ => "source/img/icon/Materials/question_mark_2",
                        };
                        pot_2_path += theory_data[i].stamp_2.point switch
                        {
                            "point_5_1" or "point_5_2" => "5",
                            "point_3_1" or "point_3_2" or "point_3_3" => "3",
                            "question_red_1" or "question_red_2" => "red",
                            "question_green_1" or "question_green_2" => "green",
                            "question_blue_1" or "question_blue_2" => "blue",
                            _ => "",
                        };
                    }
                    else
                    {
                        pot_2_path = theory_data[i].stamp_2.color switch
                        {
                            "red" => "source/img/stamp/Materials/stamp_red_back",
                            "blue" => "source/img/stamp/Materials/stamp_blue_back",
                            "black" => "source/img/stamp/Materials/stamp_black_back",
                            "white" => "source/img/stamp/Materials/stamp_white_back",
                            _ => "source/img/icon/Materials/question_mark_2",
                        };
                    }
                    
                }
                else
                {
                    pot_2_path = "source/img/icon/Materials/question_mark_2";
                }

                if (!string.IsNullOrEmpty(theory_data[i].stamp_3.color))
                {
                    if (theory_data[i].stamp_3.user_key.Equals(my_key))
                    {
                        pot_3_path = theory_data[i].stamp_3.color switch
                        {
                            "red" => "source/img/stamp/Materials/stamp_red_",
                            "blue" => "source/img/stamp/Materials/stamp_blue_",
                            "black" => "source/img/stamp/Materials/stamp_black_",
                            "white" => "source/img/stamp/Materials/stamp_white_",
                            _ => "source/img/icon/Materials/question_mark_2",
                        };
                        pot_3_path += theory_data[i].stamp_3.point switch
                        {
                            "point_5_1" or "point_5_2" => "5",
                            "point_3_1" or "point_3_2" or "point_3_3" => "3",
                            "question_red_1" or "question_red_2" => "red",
                            "question_green_1" or "question_green_2" => "green",
                            "question_blue_1" or "question_blue_2" => "blue",
                            _ => "",
                        };
                    }
                    else
                    {
                        pot_3_path = theory_data[i].stamp_3.color switch
                        {
                            "red" => "source/img/stamp/Materials/stamp_red_back",
                            "blue" => "source/img/stamp/Materials/stamp_blue_back",
                            "black" => "source/img/stamp/Materials/stamp_black_back",
                            "white" => "source/img/stamp/Materials/stamp_white_back",
                            _ => "source/img/icon/Materials/question_mark_2",
                        };
                    }
                }
                else
                {
                    pot_3_path = "source/img/icon/Materials/question_mark_2";
                }
            }
            /*
            Debug.Log(ele_path);
            Debug.Log(pot_1_path);
            Debug.Log(pot_2_path);
            Debug.Log(pot_3_path);
            */
            // ����� path�� ���� -> material�� �����ϴ� ���
            board_5_ele_list[i].GetComponent<Renderer>().material = Resources.Load<Material>(ele_path);
            board_6_ele_list[i].GetComponent<Renderer>().material = Resources.Load<Material>(ele_path);

            // ele ���� �������� ������ �ۼ� ������ question_mark
            board_5_stamp_1_list[i].GetComponent<Renderer>().material = Resources.Load<Material>(pot_1_path);
            board_6_stamp_1_list[i].GetComponent<Renderer>().material = Resources.Load<Material>(pot_1_path);

            board_5_stamp_2_list[i].GetComponent<Renderer>().material = Resources.Load<Material>(pot_2_path);
            board_6_stamp_2_list[i].GetComponent<Renderer>().material = Resources.Load<Material>(pot_2_path);

            board_5_stamp_3_list[i].GetComponent<Renderer>().material = Resources.Load<Material>(pot_3_path);
            board_6_stamp_3_list[i].GetComponent<Renderer>().material = Resources.Load<Material>(pot_3_path);

        }
        
    }

    // ��� ���� ������ �����Ǵ� �� ������Ű��
    public void Controller_ingre_count_sign(User_Data_Array data)
    {
        // �л����� �����ϱ�
        board_7_ingre_cnt_1.text = data.User_ingame_data.Ingredient.Card_1.ToString() + "��";
        board_7_ingre_cnt_2.text = data.User_ingame_data.Ingredient.Card_2.ToString() + "��";
        board_7_ingre_cnt_3.text = data.User_ingame_data.Ingredient.Card_3.ToString() + "��";
        board_7_ingre_cnt_4.text = data.User_ingame_data.Ingredient.Card_4.ToString() + "��";
        board_7_ingre_cnt_5.text = data.User_ingame_data.Ingredient.Card_5.ToString() + "��";
        board_7_ingre_cnt_6.text = data.User_ingame_data.Ingredient.Card_6.ToString() + "��";
        board_7_ingre_cnt_7.text = data.User_ingame_data.Ingredient.Card_7.ToString() + "��";
        board_7_ingre_cnt_8.text = data.User_ingame_data.Ingredient.Card_8.ToString() + "��";

        // ������ �����ϱ�
        board_8_ingre_cnt_1.text = data.User_ingame_data.Ingredient.Card_1.ToString() + "��";
        board_8_ingre_cnt_2.text = data.User_ingame_data.Ingredient.Card_2.ToString() + "��";
        board_8_ingre_cnt_3.text = data.User_ingame_data.Ingredient.Card_3.ToString() + "��";
        board_8_ingre_cnt_4.text = data.User_ingame_data.Ingredient.Card_4.ToString() + "��";
        board_8_ingre_cnt_5.text = data.User_ingame_data.Ingredient.Card_5.ToString() + "��";
        board_8_ingre_cnt_6.text = data.User_ingame_data.Ingredient.Card_6.ToString() + "��";
        board_8_ingre_cnt_7.text = data.User_ingame_data.Ingredient.Card_7.ToString() + "��";
        board_8_ingre_cnt_8.text = data.User_ingame_data.Ingredient.Card_8.ToString() + "��";
    }

    // ��밡���� ť�� ���� ǥ���ϱ�
    public void Controller_showing_cube(User_Data_Array data)
    {
        // �� �� ã��
        Sprite spr = data.User_color switch {
            "red"  => Resources.Load<Sprite>("source/img/icon/cube_red"),
            "blue" => Resources.Load<Sprite>("source/img/icon/cube_blue"),
            "black" => Resources.Load<Sprite>("source/img/icon/cube_black"),
            "white" => Resources.Load<Sprite>("source/img/icon/cube_white"),
            _ => Resources.Load<Sprite>("source/img/icon/cube_gray"),
        };

        GameObject tmp = GameObject.Find("Show_area").transform.Find("Can_use_cube_area").gameObject;
        // ��� ������Ʈ���� de_active
        for(int i = 1; i < 7; i++)
        {
            tmp.transform.Find("cube_" + i).gameObject.SetActive(false);
        }

        // ť�� ������� ǥ��
        for(int i = 0; i < data.User_ingame_data.Cube_count; i++)
        {
            tmp.transform.Find("cube_" + (i + 1)).gameObject.SetActive(true);
            tmp.transform.Find("cube_" + (i + 1)).GetComponent<Image>().sprite = spr;
        }
    }
   
    // ����ȸ ���ô� �� ť�� �� ����
    public void Controller_exhibit_cube_draw(Exhibition_Result data)
    {
        Debug.Log("in_controller");
        // first 1: red_1 /// 2:red_0  // 3 green_1 // 4 green_0 // 5 blue_1 // 6 blue_0
        // second  7           8         9              10          11          12
        Color red = new Color(186 / 255f, 48 / 255f, 48 / 255f);
        Color blue = new Color(48 / 255f, 48 / 255f, 186 / 255f);
        Color black = Color.black;
        Color white = Color.white;
        Color gray = new Color(82 / 255f, 82 / 255f, 82 / 255f);

        #region cube color setting

        // red
        if (data.first.red_1.user_key.Length > 0)
        {
            Color color = data.first.red_1.color switch
            {
                "red" => red,
                "blue" => blue,
                "black" => black,
                "white" => white,
                _ => gray,
            };
            GameObject.Find("Room_Part_9").transform.Find("Show_Sect").Find("Red_1").Find("Cube_Sect").Find("First").gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
        }

        if (data.first.red_0.user_key.Length > 0)
        {
            Color color = data.first.red_0.color switch
            {
                "red" => red,
                "blue" => blue,
                "black" => black,
                "white" => white,
                _ => gray,
            };
            GameObject.Find("Room_Part_9").transform.Find("Show_Sect").Find("Red_0").Find("Cube_Sect").Find("First").gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
        }

        if (data.second.red_1.user_key.Length > 0)
        {
            Color color = data.second.red_1.color switch
            {
                "red" => red,
                "blue" => blue,
                "black" => black,
                "white" => white,
                _ => gray,
            };
            GameObject.Find("Room_Part_9").transform.Find("Show_Sect").Find("Red_1").Find("Cube_Sect").Find("Second").gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
        }

        if (data.second.red_0.user_key.Length > 0)
        {
            Color color = data.second.red_0.color switch
            {
                "red" => red,
                "blue" => blue,
                "black" => black,
                "white" => white,
                _ => gray,
            };
            GameObject.Find("Room_Part_9").transform.Find("Show_Sect").Find("Red_0").Find("Cube_Sect").Find("Second").gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
        }


        // green
        if (data.first.green_1.user_key.Length > 0)
        {
            Color color = data.first.green_1.color switch
            {
                "red" => red,
                "blue" => blue,
                "black" => black,
                "white" => white,
                _ => gray,
            };
            GameObject.Find("Room_Part_9").transform.Find("Show_Sect").Find("Green_1").Find("Cube_Sect").Find("First").gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
        }

        if (data.first.green_0.user_key.Length > 0)
        {
            Color color = data.first.green_0.color switch
            {
                "red" => red,
                "blue" => blue,
                "black" => black,
                "white" => white,
                _ => gray,
            };
            GameObject.Find("Room_Part_9").transform.Find("Show_Sect").Find("Green_0").Find("Cube_Sect").Find("First").gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
        }

        if (data.second.green_1.user_key.Length > 0)
        {
            Color color = data.second.green_1.color switch
            {
                "red" => red,
                "blue" => blue,
                "black" => black,
                "white" => white,
                _ => gray,
            };
            GameObject.Find("Room_Part_9").transform.Find("Show_Sect").Find("Green_1").Find("Cube_Sect").Find("Second").gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
        }

        if (data.second.green_0.user_key.Length > 0)
        {
            Color color = data.second.green_0.color switch
            {
                "red" => red,
                "blue" => blue,
                "black" => black,
                "white" => white,
                _ => gray,
            };
            GameObject.Find("Room_Part_9").transform.Find("Show_Sect").Find("Green_0").Find("Cube_Sect").Find("Second").gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
        }

        //blue
        if (data.first.blue_1.user_key.Length > 0)
        {
            Color color = data.first.blue_1.color switch
            {
                "red" => red,
                "blue" => blue,
                "black" => black,
                "white" => white,
                _ => gray,
            };
            GameObject.Find("Room_Part_9").transform.Find("Show_Sect").Find("Blue_1").Find("Cube_Sect").Find("First").gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
        }

        if (data.first.blue_0.user_key.Length > 0)
        {
            Color color = data.first.blue_0.color switch
            {
                "red" => red,
                "blue" => blue,
                "black" => black,
                "white" => white,
                _ => gray,
            };
            GameObject.Find("Room_Part_9").transform.Find("Show_Sect").Find("Blue_0").Find("Cube_Sect").Find("First").gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
        }

        if (data.second.blue_1.user_key.Length > 0)
        {
            Color color = data.second.blue_1.color switch
            {
                "red" => red,
                "blue" => blue,
                "black" => black,
                "white" => white,
                _ => gray,
            };
            GameObject.Find("Room_Part_9").transform.Find("Show_Sect").Find("Blue_1").Find("Cube_Sect").Find("Second").gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
        }

        if (data.second.blue_0.user_key.Length > 0)
        {
            Color color = data.second.blue_0.color switch
            {
                "red" => red,
                "blue" => blue,
                "black" => black,
                "white" => white,
                _ => gray,
            };
            GameObject.Find("Room_Part_9").transform.Find("Show_Sect").Find("Blue_0").Find("Cube_Sect").Find("Second").gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
        }

        #endregion
    }
}

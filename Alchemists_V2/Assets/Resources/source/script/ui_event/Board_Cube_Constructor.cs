using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alchemists_data;

public class Board_Cube_Constructor : MonoBehaviour
{

    private GameObject pre_cube;

    private Color red = new Color(186/255f, 48/255f, 48/255f);
    private Color blue = new Color(48/255f, 48/255f, 186/255f);
    private Color black = Color.black;
    private Color white = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        pre_cube = Resources.Load<GameObject>("source/prefabs/Custom_Object/Board_Cube");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Draw_cubes(Dictionary<int, Dictionary<int, User_Cube_Data>> user_cube_data, int round_cont, string my_key, bool in_round)
    {
        // user_cube_data �� ���� ���忡 ť�긦 ������
        // Ÿ�� ������ ���� ����
        GameObject _target_place;
        Transform[] _child_list;
        int my_num = 0;
        // �� ��ȣ ã��
        for(int i = 0; i<user_cube_data[1].Count; i++)
        {
            if (user_cube_data[1][i].user_key.Equals(my_key))
            {
                my_num = i;
                break;
            }
        }

        // ť�� �ۼ�
        for(int i = 1; i < 9; i++)
        {
            // ť�� ���ź�
            // part_1 ~ 8 ���� ���Ŵ� ���� ����
            _target_place = GameObject.Find("Room_Part_" + i).transform.Find("Cube_Sect").gameObject;
            // title�� �����ϰ� �����Ǿ� �ִ� �ڽ� ť����� ������ ����
            _child_list = _target_place.GetComponentsInChildren<Transform>();
            // 0 �� title�̶� ���� ���� ���� -> ���� child_list�� �ݵ�� �����ϹǷ� �˻� �ʿ� x
            for (int j = 2; j < _child_list.Length; j++)
            {
                Destroy(_child_list[j].gameObject);
            }

            // ť�� ������
            // ������ user_cube_data�� ���� ���� ������ ����
            for (int j = 0; j < user_cube_data[i].Count; j++)
            {
                // ��ǥ�� i ���� ���� ��� ����
                float x=0f, y = 9.5f - j * 1.5f, z =0f, v=0f, w = 0.7f;
                switch (i)
                {
                    case 1:
                        x = 27f; z = 59f; v = 2f;
                        break;
                    case 2:
                        x = 27f; z = 87f; v = 2f;
                        break;
                    case 3:
                        x = 28f; z = 115f; v = 2f;
                        break;
                    case 4:
                        x = 22f; z = 143f; v = 2f;
                        break;
                    case 5: case 6:
                        x = -20f; z = 33f; v = -2f; w = -0.7f;
                        break;
                    case 7: case 8:
                        x = -20f; z = 89f; v = -2f; w = -0.7f;
                        break;
                }

                // ť�� �ۼ�����
                for (int k = 1; k < user_cube_data[i][j].length + 1; k++)
                {
                    GameObject temp;
                    // ť�갡 1���� �ܼ� �ۼ�
                    if (user_cube_data[i][j].cube[k].cnt == 1)
                    {
                        x += k == 1 ? 0 : v;
                        temp = Instantiate(pre_cube, new Vector3(x, y, z), Quaternion.identity);
                        temp.transform.SetParent(_target_place.transform, false);
                        // i : �����ȣ , j : �� ��ȣ, k: ť���ȣ
                        temp.name = "Cube_" + i + "_" + j + "_" + k;

                        // �� ��ȣ�� ť����  + ���� board �������� �ƴ϶��
                        if (j == my_num && !in_round)
                        {
                            // cube_highlighter�߰�
                            temp.AddComponent<Cube_highlighter>();
                        }
                        
                        // �� ���ϱ�
                        switch (user_cube_data[i][j].user_color)
                        {
                            case "red":
                                temp.GetComponent<Renderer>().material.color = red;
                                break;
                            case "blue":
                                temp.GetComponent<Renderer>().material.color = blue;
                                break;
                            case "black":
                                temp.GetComponent<Renderer>().material.color = black;
                                break;
                            case "white":
                                temp.GetComponent<Renderer>().material.color = white;
                                break;
                        }

                        // is_select�� true ��� outline ���� 0.3 �� �߰�����
                        if (user_cube_data[i][j].cube[k].is_select)
                        {
                            temp.GetComponent<Renderer>().material.SetFloat("_Outline", 0.3f);
                            temp.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                            // �� ť����
                            if (j == my_num && !in_round)
                                temp.GetComponent<Cube_highlighter>().selected = true;
                        }
                            
                    }
                    
                    // ť�� cnt�� 2�� 2���� �ʿ��� �����̹Ƿ� 2���� ť�긦 ������ �̸����� ����, 
                    if (user_cube_data[i][j].cube[k].cnt == 2)
                    {
                        x += k == 1 ? 0 : v;
                        float x_1 = x + w;
                        temp = Instantiate(pre_cube, new Vector3(x, y, z), Quaternion.identity);
                        GameObject temp_2 = Instantiate(pre_cube, new Vector3(x_1, y, z), Quaternion.identity);
                        temp.transform.SetParent(_target_place.transform, false);
                        temp_2.transform.SetParent(_target_place.transform, false);

                        // i : �����ȣ , j : �� ��ȣ, k: ť���ȣ
                        temp.name = "Cube_" + i + "_" + j + "_" + k;
                        temp_2.name = "Cube_" + i + "_" + j + "_" + k;

                        // �� ť����
                        if (j == my_num && !in_round)
                        {
                            // cube_highlighter �߰�
                            temp.AddComponent<Cube_highlighter>();
                            temp_2.AddComponent<Cube_highlighter>();
                        }

                        switch (user_cube_data[i][j].user_color)
                        {
                            case "red":
                                temp.GetComponent<Renderer>().material.color = red;
                                temp_2.GetComponent<Renderer>().material.color = red;
                                break;
                            case "blue":
                                temp.GetComponent<Renderer>().material.color = blue;
                                temp_2.GetComponent<Renderer>().material.color = blue;
                                break;
                            case "black":
                                temp.GetComponent<Renderer>().material.color = black;
                                temp_2.GetComponent<Renderer>().material.color = black;
                                break;
                            case "white":
                                temp.GetComponent<Renderer>().material.color = white;
                                temp_2.GetComponent<Renderer>().material.color = white;
                                break;
                        }

                        // is_select�� true ��� outline ���� 0.3 �� �߰�����
                        if (user_cube_data[i][j].cube[k].is_select)
                        {
                            temp.GetComponent<Renderer>().material.SetFloat("_Outline", 0.3f);
                            temp_2.GetComponent<Renderer>().material.SetFloat("_Outline", 0.3f);
                            temp.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                            temp_2.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                            // �� ť����
                            if (j == my_num && !in_round)
                            {
                                temp.GetComponent<Cube_highlighter>().selected = true;
                                temp_2.GetComponent<Cube_highlighter>().selected = true;
                            }
                                
                        }
                            
                    }
                }
            }
        }

        // ������ ��Ʈ�� ������ ���忡���� �۵��ϸ� ��
        // part_9
        if (round_cont == 6)
        {
            _target_place = GameObject.Find("Room_Part_9").transform.Find("Cube_Sect").gameObject;
            // title�� �����ϰ� �����Ǿ� �ִ� �ڽ� ť����� ������ ����
            _child_list = _target_place.GetComponentsInChildren<Transform>();
            // 0 �� �ڱ��ڽ��̶� ���� ���� ����
            for (int j = 1; j < _child_list.Length; j++)
            {
                Destroy(_child_list[j].gameObject);
            }

            // ��� ����ŭ 
            for (int i = 0; i < user_cube_data[9].Count; i++)
            {
                float x = -5.2f;
                float y = i == 0 ? 9.5f : 9.5f - (i * 1.5f);
                float z = 145f;

                for (int k = 1; k < user_cube_data[9][i].length + 1; k++)
                {
                    x += k == 1 ? 0 : -2;
                    GameObject temp = Instantiate(pre_cube, new Vector3(x, y, z), Quaternion.identity);
                    temp.transform.SetParent(_target_place.transform, false);

                    // i : �����ȣ , j : �� ��ȣ, k: ť���ȣ
                    temp.name = "Cube_9_" + i + "_" + k;

                    switch (user_cube_data[9][i].user_color)
                    {
                        case "red":
                            temp.GetComponent<Renderer>().material.color = red;
                            break;
                        case "blue":
                            temp.GetComponent<Renderer>().material.color = blue;
                            break;
                        case "black":
                            temp.GetComponent<Renderer>().material.color = black;
                            break;
                        case "white":
                            temp.GetComponent<Renderer>().material.color = white;
                            break;
                    }

                    // �� ��ȣ�� ť����
                    if (i == my_num && !in_round)
                    {
                        // cube_highlighter�߰�
                        temp.AddComponent<Cube_highlighter>();
                    }

                    if (user_cube_data[9][i].cube[k].is_select)
                    {
                        temp.GetComponent<Renderer>().material.SetFloat("_Outline", 0.3f);
                        temp.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                        // �� ť����
                        if (i == my_num && !in_round)
                            temp.GetComponent<Cube_highlighter>().selected = true;
                    }
                }

            }
        }
    }

}

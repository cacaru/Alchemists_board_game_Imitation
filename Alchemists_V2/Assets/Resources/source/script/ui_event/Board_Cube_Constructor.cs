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
        // user_cube_data 에 따라 보드에 큐브를 생성함
        // 타겟 생성을 위한 변수
        GameObject _target_place;
        Transform[] _child_list;
        int my_num = 0;
        // 내 번호 찾기
        for(int i = 0; i<user_cube_data[1].Count; i++)
        {
            if (user_cube_data[1][i].user_key.Equals(my_key))
            {
                my_num = i;
                break;
            }
        }

        // 큐브 작성
        for(int i = 1; i < 9; i++)
        {
            // 큐브 제거부
            // part_1 ~ 8 까지 제거는 전부 진행
            _target_place = GameObject.Find("Room_Part_" + i).transform.Find("Cube_Sect").gameObject;
            // title을 제외하고 생성되어 있는 자식 큐브들을 모조리 제거
            _child_list = _target_place.GetComponentsInChildren<Transform>();
            // 0 은 title이라 제거 하지 않음 -> 따라서 child_list가 반드시 존재하므로 검사 필요 x
            for (int j = 2; j < _child_list.Length; j++)
            {
                Destroy(_child_list[j].gameObject);
            }

            // 큐브 생성부
            // 하위에 user_cube_data에 따라 색을 입히며 생성
            for (int j = 0; j < user_cube_data[i].Count; j++)
            {
                // 좌표는 i 값에 따라 계속 변경
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

                // 큐브 작성시작
                for (int k = 1; k < user_cube_data[i][j].length + 1; k++)
                {
                    GameObject temp;
                    // 큐브가 1개면 단순 작성
                    if (user_cube_data[i][j].cube[k].cnt == 1)
                    {
                        x += k == 1 ? 0 : v;
                        temp = Instantiate(pre_cube, new Vector3(x, y, z), Quaternion.identity);
                        temp.transform.SetParent(_target_place.transform, false);
                        // i : 보드번호 , j : 내 번호, k: 큐브번호
                        temp.name = "Cube_" + i + "_" + j + "_" + k;

                        // 내 번호의 큐브라면  + 현재 board 진행중이 아니라면
                        if (j == my_num && !in_round)
                        {
                            // cube_highlighter추가
                            temp.AddComponent<Cube_highlighter>();
                        }
                        
                        // 색 정하기
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

                        // is_select가 true 라면 outline 값을 0.3 을 추가해줌
                        if (user_cube_data[i][j].cube[k].is_select)
                        {
                            temp.GetComponent<Renderer>().material.SetFloat("_Outline", 0.3f);
                            temp.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                            // 내 큐브라면
                            if (j == my_num && !in_round)
                                temp.GetComponent<Cube_highlighter>().selected = true;
                        }
                            
                    }
                    
                    // 큐브 cnt가 2면 2개가 필요한 행위이므로 2개의 큐브를 동일한 이름으로 생성, 
                    if (user_cube_data[i][j].cube[k].cnt == 2)
                    {
                        x += k == 1 ? 0 : v;
                        float x_1 = x + w;
                        temp = Instantiate(pre_cube, new Vector3(x, y, z), Quaternion.identity);
                        GameObject temp_2 = Instantiate(pre_cube, new Vector3(x_1, y, z), Quaternion.identity);
                        temp.transform.SetParent(_target_place.transform, false);
                        temp_2.transform.SetParent(_target_place.transform, false);

                        // i : 보드번호 , j : 내 번호, k: 큐브번호
                        temp.name = "Cube_" + i + "_" + j + "_" + k;
                        temp_2.name = "Cube_" + i + "_" + j + "_" + k;

                        // 내 큐브라면
                        if (j == my_num && !in_round)
                        {
                            // cube_highlighter 추가
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

                        // is_select가 true 라면 outline 값을 0.3 을 추가해줌
                        if (user_cube_data[i][j].cube[k].is_select)
                        {
                            temp.GetComponent<Renderer>().material.SetFloat("_Outline", 0.3f);
                            temp_2.GetComponent<Renderer>().material.SetFloat("_Outline", 0.3f);
                            temp.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                            temp_2.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                            // 내 큐브라면
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

        // 마지막 파트는 마지막 라운드에서만 작동하면 됨
        // part_9
        if (round_cont == 6)
        {
            _target_place = GameObject.Find("Room_Part_9").transform.Find("Cube_Sect").gameObject;
            // title을 제외하고 생성되어 있는 자식 큐브들을 모조리 제거
            _child_list = _target_place.GetComponentsInChildren<Transform>();
            // 0 은 자기자신이라 제거 하지 않음
            for (int j = 1; j < _child_list.Length; j++)
            {
                Destroy(_child_list[j].gameObject);
            }

            // 사람 수만큼 
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

                    // i : 보드번호 , j : 내 번호, k: 큐브번호
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

                    // 내 번호의 큐브라면
                    if (i == my_num && !in_round)
                    {
                        // cube_highlighter추가
                        temp.AddComponent<Cube_highlighter>();
                    }

                    if (user_cube_data[9][i].cube[k].is_select)
                    {
                        temp.GetComponent<Renderer>().material.SetFloat("_Outline", 0.3f);
                        temp.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                        // 내 큐브라면
                        if (i == my_num && !in_round)
                            temp.GetComponent<Cube_highlighter>().selected = true;
                    }
                }

            }
        }
    }

}

using Alchemists_data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_highlighter: MonoBehaviour
{
    // 선택되지 않고 마우스가 올라가지 않았으면 outline의 크기가 0.0f

    // 큐브에 처음 부착 될 때 정해질 변수
    public bool selected = false;
    public bool in_round = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if (in_round) return;
        highlight();
    }

    private void OnMouseExit()
    {
        if (in_round) return;
        un_highlight();
    }

    // 이 큐브가 선택되었을 때 selecter에게 번호를 보냄
    private void OnMouseUp()
    {
        if (in_round) return;
        string name = gameObject.name;
        // name :: Cube_i_j_k
        //         i : board_num
        //         j : user_num
        //         k : cube_num
        // highlight 오브젝트는 반드시 본인의 큐브만 선택가능하게 작성하므로 j는 불필요
        int[] result = { int.Parse(name[5].ToString()), int.Parse(name[9].ToString()) };

        // socket_in_board에 전송
        GameObject.Find("Socket_obj").GetComponent<Socket_In_Board>().Select_board_cube(result);
    }

    public void highlight()
    {
        if (in_round) return;
        // return :: front : board_num
        //           end   : cube_num

        // 같은 부모를 가진 같은 이름의 큐브가 존재한다면 그큐브도 highlight
        Transform[] tmp = gameObject.transform.parent.GetComponentsInChildren<Transform>();
        ArrayList cnt = new();
        for(int i = 0; i < tmp.Length; i++)
        {
            if (tmp[i].gameObject.name.Equals(gameObject.name)) cnt.Add(i);
        }
        
        if( cnt.Count >= 2 )
        {
            for(int i = 0; i < cnt.Count; i++)
            {
                tmp[(int)cnt[i]].GetComponent<Renderer>().material.SetFloat("_Outline", 0.3f);
                tmp[(int)cnt[i]].GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
            }
        }
        else
        {
            gameObject.transform.GetComponent<Renderer>().material.SetFloat("_Outline", 0.3f);
            gameObject.transform.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.HIGHLIGHT);
        }
        
    }

    public void un_highlight()
    {
        if (in_round) return;
        // 같은 부모를 가진 같은 이름의 큐브가 존재한다면 그큐브도 un highlight
        Transform[] tmp = gameObject.transform.parent.GetComponentsInChildren<Transform>();
        ArrayList cnt = new();
        for (int i = 0; i < tmp.Length; i++)
        {
            if (tmp[i].gameObject.name.Equals(gameObject.name)) cnt.Add(i);
        }

        if (cnt.Count >= 2)
        {
            for (int i = 0; i < cnt.Count; i++)
            {
                if (selected)
                    tmp[(int)cnt[i]].GetComponent<Renderer>().material.SetFloat("_Outline", 0.3f);
                else
                    tmp[(int)cnt[i]].GetComponent<Renderer>().material.SetFloat("_Outline", 0.0f);

                tmp[(int)cnt[i]].GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
            }
        }
        else
        {
            // 선택된 것은 선택된 상태로 돌려야함
            if (selected)
            {
                gameObject.transform.GetComponent<Renderer>().material.SetFloat("_Outline", 0.3f);
                gameObject.transform.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
            }
            else
            {
                gameObject.transform.GetComponent<Renderer>().material.SetFloat("_Outline", 0f);
                gameObject.transform.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
            }
        }
    }


}

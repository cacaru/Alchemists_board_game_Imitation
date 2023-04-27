using Alchemists_data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_highlighter: MonoBehaviour
{
    // ���õ��� �ʰ� ���콺�� �ö��� �ʾ����� outline�� ũ�Ⱑ 0.0f

    // ť�꿡 ó�� ���� �� �� ������ ����
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

    // �� ť�갡 ���õǾ��� �� selecter���� ��ȣ�� ����
    private void OnMouseUp()
    {
        if (in_round) return;
        string name = gameObject.name;
        // name :: Cube_i_j_k
        //         i : board_num
        //         j : user_num
        //         k : cube_num
        // highlight ������Ʈ�� �ݵ�� ������ ť�길 ���ð����ϰ� �ۼ��ϹǷ� j�� ���ʿ�
        int[] result = { int.Parse(name[5].ToString()), int.Parse(name[9].ToString()) };

        // socket_in_board�� ����
        GameObject.Find("Socket_obj").GetComponent<Socket_In_Board>().Select_board_cube(result);
    }

    public void highlight()
    {
        if (in_round) return;
        // return :: front : board_num
        //           end   : cube_num

        // ���� �θ� ���� ���� �̸��� ť�갡 �����Ѵٸ� ��ť�굵 highlight
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
        // ���� �θ� ���� ���� �̸��� ť�갡 �����Ѵٸ� ��ť�굵 un highlight
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
            // ���õ� ���� ���õ� ���·� ��������
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

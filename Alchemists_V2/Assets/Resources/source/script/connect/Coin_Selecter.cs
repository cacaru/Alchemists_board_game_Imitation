using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alchemists_data;

public class Coin_Selecter : MonoBehaviour
{
    public bool selected = false;

    private Material _target;
    private Data_Hub data_hub;
    private Board_Acting_Checker checker;
    // Start is called before the first frame update
    void Start()
    {
        _target = gameObject.GetComponent<Renderer>().material;
        data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();
        checker = GameObject.Find("Data_Controller").GetComponent<Board_Acting_Checker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        _target.SetFloat("_Outline", 0.2f);
        _target.SetColor("_OutlineColor", OUTLINE_COLOR.HIGHLIGHT);
    }

    private void OnMouseExit()
    {
        if (selected)
        {
            _target.SetFloat("_Outline", 0.2f);
        }
        else
        {
            _target.SetFloat("_Outline", 0.0f);
        }
        _target.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
    }

    private void OnMouseUp()
    {
        int num = int.Parse(gameObject.name[^1].ToString());
        data_hub.Dis_coin_num = num;
        checker.Board_3_coin_checker(num);
    }
}

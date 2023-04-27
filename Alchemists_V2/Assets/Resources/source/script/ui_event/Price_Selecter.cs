using System.Collections;
using UnityEngine;
using Alchemists_data;


public class Price_Selecter : MonoBehaviour
{
    private Material _target;
    private Data_Hub data_hub;
    private Board_Acting_Checker checker;

    public bool selected = false;

    private GameObject announce;
    // Use this for initialization
    void Start()
    {
        _target = GetComponent<Renderer>().material;
        data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();
        checker = GameObject.Find("Data_Controller").GetComponent<Board_Acting_Checker>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        _target.SetColor("_OutlineColor", OUTLINE_COLOR.HIGHLIGHT);
        _target.SetFloat("_Outline", 0.2f);
    }

    private void OnMouseExit()
    {
        if(selected)
            _target.SetFloat("_Outline", 0.2f);
        else
            _target.SetFloat("_Outline", 0.0f);
        _target.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
    }

    private void OnMouseUp()
    {
        int num = int.Parse(gameObject.name[^1].ToString());
        data_hub.Selling_price = num;
        checker.Board_3_price_checker(num);
    }
}

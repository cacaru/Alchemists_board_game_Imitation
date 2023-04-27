using System.Collections;
using UnityEngine;
using Alchemists_data;

public class Potion_Selecter : MonoBehaviour
{
    public bool selected = false;
    private Material _target;
    private Data_Hub data_hub;
    private Board_Acting_Checker checker;
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
        if (selected)
            _target.SetFloat("_Outline", 0.2f);
        else
            _target.SetFloat("_Outline", 0.0f);

        _target.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
    }

    private void OnMouseUp()
    {
        //Debug.Log(gameObject.name);
        data_hub.Sell_potion = gameObject.name;
        checker.Board_3_potion_checker(gameObject.name);
    }

}

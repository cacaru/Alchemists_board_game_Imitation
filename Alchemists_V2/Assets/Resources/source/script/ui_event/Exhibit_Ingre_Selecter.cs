using System.Collections;
using UnityEngine;
using Alchemists_data;

public class Exhibit_Ingre_Selecter : MonoBehaviour
{
    public bool selected = false;

    private Material _target;
    private Data_Hub data_hub;
    private Board_Acting_Checker checker;

    // Use this for initialization
    void Start()
    {
        data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();
        _target = gameObject.GetComponent<Renderer>().material;
        checker = GameObject.Find("Data_Controller").GetComponent<Board_Acting_Checker>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        _target.SetColor("_OutlineColor", OUTLINE_COLOR.HIGHLIGHT);
        _target.SetFloat("_OutlineWidth", 1.2f);
    }

    private void OnMouseExit()
    {
        //Debug.Log(gameObject.name);
        // 선택이 되어 있는 오브젝트면 _OutlineWidth 1.2f, _OutlineColor : OUTLINE_COLOR.DEFAULT
        if (selected)
        {
            _target.SetFloat("_OutlineWidth", 1.2f);
        }
        else
        {
            _target.SetFloat("_OutlineWidth", 1.0f);
        }
        _target.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);

    }

    private void OnMouseUp()
    {
        checker.Board_9_ingre_checker(gameObject.name);
    }
}
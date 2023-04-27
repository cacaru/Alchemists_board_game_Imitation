using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Btn_explain_hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject obj;
    public float scale_val = 1.2f;

    private Vector3 ori_val;
    private Vector3 change_val;

    public void OnPointerEnter(PointerEventData eventData)
    {
        obj.transform.localScale = change_val;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        obj.transform.localScale = ori_val;
    }

    // Start is called before the first frame update
    void Start()
    {
        change_val = new Vector3(scale_val, scale_val, scale_val);
        ori_val = obj.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

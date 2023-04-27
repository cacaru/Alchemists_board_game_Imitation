using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Reasoning_btn : MonoBehaviour
{
    public void Change_val()
    {
        GameObject me = EventSystem.current.currentSelectedGameObject;
        string now = me.transform.Find("Text").GetComponent<TMP_Text>().text;
        string change = "";

        if( now.Equals(""))
        {
            change = "X";
        }
        else if( now.Equals("O"))
        {
            change = "";
        }
        else if( now.Equals("X"))
        {
            change = "O";
        }

        me.transform.Find("Text").GetComponent<TMP_Text>().text = change;
    }
}

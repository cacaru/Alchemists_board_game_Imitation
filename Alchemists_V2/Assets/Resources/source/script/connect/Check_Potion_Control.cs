using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Alchemists_data
{
    public class Check_Potion_Control
    {
        /// <summary>
        /// 실험 결과 창에 데이터를 뿌리는 함수
        /// </summary>
        /// <param name="window">result window gameObject</param>
        /// <param name="data">my_data</param>
        public static void Control(GameObject window, Is_Check_Potion_Data data)
        {
            if (data.Blank)
                window.transform.Find("Blank").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/blank");
            else
                window.transform.Find("Blank").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/blank_back");
            if (data.Blue_0)
                window.transform.Find("Blue_0").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/blue_-");
            else
                window.transform.Find("Blue_0").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/blue_-_back");
            if (data.Blue_1)
                window.transform.Find("Blue_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/blue_+");
            else
                window.transform.Find("Blue_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/blue_+_back");
            if (data.Green_0)
                window.transform.Find("Green_0").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/green_-");
            else
                window.transform.Find("Green_0").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/green_-_back");
            if (data.Green_1)
                window.transform.Find("Green_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/green_+");
            else
                window.transform.Find("Green_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/green_+_back");
            if (data.Red_0)
                window.transform.Find("Red_0").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/red_-");
            else
                window.transform.Find("Red_0").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/red_-_back");
            if (data.Red_1)
                window.transform.Find("Red_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/red_+");
            else
                window.transform.Find("Red_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/red_+_back");
        }
    }
}
using System.Collections;
using TMPro;
using UnityEngine;

namespace Alchemists_data
{
    public class Ingre_Control : MonoBehaviour
    {
        /// <summary>
        /// 내 데이터에 맞게 재료카드의 갯수를 표시해주는 함수
        /// </summary>
        /// <param name="window">재료를 표시할 윈도우</param>
        /// <param name="data">재료 정보</param>
        public static void Control(GameObject window, Ingredient_Data data)
        {
            window.transform.Find("Ingre_val_1").GetComponent<TMP_Text>().text = data.Card_1.ToString() + "장";
            window.transform.Find("Ingre_val_2").GetComponent<TMP_Text>().text = data.Card_2.ToString() + "장";
            window.transform.Find("Ingre_val_3").GetComponent<TMP_Text>().text = data.Card_3.ToString() + "장";
            window.transform.Find("Ingre_val_4").GetComponent<TMP_Text>().text = data.Card_4.ToString() + "장";
            window.transform.Find("Ingre_val_5").GetComponent<TMP_Text>().text = data.Card_5.ToString() + "장";
            window.transform.Find("Ingre_val_6").GetComponent<TMP_Text>().text = data.Card_6.ToString() + "장";
            window.transform.Find("Ingre_val_7").GetComponent<TMP_Text>().text = data.Card_7.ToString() + "장";
            window.transform.Find("Ingre_val_8").GetComponent<TMP_Text>().text = data.Card_8.ToString() + "장";
        }

    }
}
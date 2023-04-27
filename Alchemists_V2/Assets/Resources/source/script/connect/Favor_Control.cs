using System.Collections;
using TMPro;
using UnityEngine;

namespace Alchemists_data
{
    public class Favor_Control
    {
        /// <summary>
        /// 호의 카드 데이터 뿌리기
        /// </summary>
        /// <param name="window">favar_window gameObject</param>
        /// <param name="data">my_data</param>
        public static void Control(GameObject window, Favor_Card_Data data)
        {
            // 조수
            window.transform.Find("Favor_val_1").GetComponent<TMP_Text>().text = data.Assistent.ToString() + "장";
            // 술집 점원
            window.transform.Find("Favor_val_2").GetComponent<TMP_Text>().text = data.Bar_owner.ToString() + "장";
            // 힘센 친구
            window.transform.Find("Favor_val_3").GetComponent<TMP_Text>().text = data.Big_man.ToString() + "장";
            // 관리인
            window.transform.Find("Favor_val_4").GetComponent<TMP_Text>().text = data.Caretaker.ToString() + "장";
            // 약초학자
            window.transform.Find("Favor_val_5").GetComponent<TMP_Text>().text = data.Herbalist.ToString() + "장";
            // 상인
            window.transform.Find("Favor_val_6").GetComponent<TMP_Text>().text = data.Merchant.ToString() + "장";
            // 가게 주인
            window.transform.Find("Favor_val_7").GetComponent<TMP_Text>().text = data.Shopkeeper.ToString() + "장";
            // 현자
            window.transform.Find("Favor_val_8").GetComponent<TMP_Text>().text = data.Wise_man.ToString() + "장";
        }
    }
}
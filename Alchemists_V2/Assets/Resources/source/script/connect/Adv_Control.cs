using System.Collections;
using UnityEngine;

namespace Alchemists_data
{

    public class Adv_Control
    {

        /// <summary>
        ///  용병에게 제시 할 수 있는 할인코인 목록을 보여주는 창에 데이터를 뿌리는 함수
        /// </summary>
        /// <param name="window">adv window gameObject</param>
        /// <param name="data">my_data</param>
        public static void Control(GameObject window, Discount_Adventurer_Data data)
        {
            if (data.Ad_0)
                window.transform.Find("Adv_0").gameObject.SetActive(true);
            else
                window.transform.Find("Adv_0").gameObject.SetActive(false);
            if (data.Ad_1)
                window.transform.Find("Adv_1").gameObject.SetActive(true);
            else
                window.transform.Find("Adv_1").gameObject.SetActive(false);
            if (data.Ad_2)
                window.transform.Find("Adv_2").gameObject.SetActive(true);
            else
                window.transform.Find("Adv_2").gameObject.SetActive(false);
            if (data.Ad_3)
                window.transform.Find("Adv_3").gameObject.SetActive(true);
            else
                window.transform.Find("Adv_3").gameObject.SetActive(false);
        }
    }
}
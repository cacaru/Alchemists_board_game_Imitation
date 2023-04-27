using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alchemists_data
{
    public class Stamp_Control : MonoBehaviour
    {
        /// <summary>
        /// 내 데이터에 맞게 Board scene의 stamp_window의 이미지를 지정해줌
        /// </summary>
        /// <param name="window">stamp_window</param>
        /// <param name="data">Have_Stamp_Data</param>
        /// <param name="color">my_color</param>
        public static void Control(GameObject window, Have_Stamp_Data data, string color) 
        {
            if (color.Equals("black")) Black_stamping(window, data);
            else if (color.Equals("white")) White_stamping(window, data);
            else if (color.Equals("red")) Red_stamping(window, data);
            else if (color.Equals("blue")) Blue_stamping(window, data);
        }

        private static void Black_stamping(GameObject window, Have_Stamp_Data data)
        {
            if (data.Point_3_1)
                window.transform.Find("Stamp_3_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_black_3");
            else
                window.transform.Find("Stamp_3_1").gameObject.SetActive(false);
            if (data.Point_3_2)
                window.transform.Find("Stamp_3_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_black_3");
            else
                window.transform.Find("Stamp_3_2").gameObject.SetActive(false);
            if (data.Point_3_3)
                window.transform.Find("Stamp_3_3").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_black_3");
            else
                window.transform.Find("Stamp_3_3").gameObject.SetActive(false);
            if (data.Point_5_1)
                window.transform.Find("Stamp_5_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_black_5");
            else
                window.transform.Find("Stamp_5_1").gameObject.SetActive(false);
            if (data.Point_5_2)
                window.transform.Find("Stamp_5_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_black_5");
            else
                window.transform.Find("Stamp_5_2").gameObject.SetActive(false);
            if (data.Question_blue_1)
                window.transform.Find("Stamp_blue_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_black_blue");
            else
                window.transform.Find("Stamp_blue_1").gameObject.SetActive(false);
            if (data.Question_blue_2)
                window.transform.Find("Stamp_blue_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_black_blue");
            else
                window.transform.Find("Stamp_blue_2").gameObject.SetActive(false);
            if (data.Question_green_1)
                window.transform.Find("Stamp_green_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_black_green");
            else
                window.transform.Find("Stamp_green_1").gameObject.SetActive(false);
            if (data.Question_green_2)
                window.transform.Find("Stamp_green_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_black_green");
            else
                window.transform.Find("Stamp_green_2").gameObject.SetActive(false);
            if (data.Question_red_1)
                window.transform.Find("Stamp_red_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_black_red");
            else
                window.transform.Find("Stamp_red_1").gameObject.SetActive(false);
            if (data.Question_red_2)
                window.transform.Find("Stamp_red_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_black_red");
            else
                window.transform.Find("Stamp_red_2").gameObject.SetActive(false);
        }

        private static void White_stamping(GameObject window, Have_Stamp_Data data)
        {
            if (data.Point_3_1)
                window.transform.Find("Stamp_3_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_white_3");
            else
                window.transform.Find("Stamp_3_1").gameObject.SetActive(false);
            if (data.Point_3_2)
                window.transform.Find("Stamp_3_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_white_3");
            else
                window.transform.Find("Stamp_3_2").gameObject.SetActive(false);
            if (data.Point_3_3)
                window.transform.Find("Stamp_3_3").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_white_3");
            else
                window.transform.Find("Stamp_3_3").gameObject.SetActive(false);
            if (data.Point_5_1)
                window.transform.Find("Stamp_5_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_white_5");
            else
                window.transform.Find("Stamp_5_1").gameObject.SetActive(false);
            if (data.Point_5_2)
                window.transform.Find("Stamp_5_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_white_5");
            else
                window.transform.Find("Stamp_5_2").gameObject.SetActive(false);
            if (data.Question_blue_1)
                window.transform.Find("Stamp_blue_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_white_blue");
            else
                window.transform.Find("Stamp_blue_1").gameObject.SetActive(false);
            if (data.Question_blue_2)
                window.transform.Find("Stamp_blue_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_white_blue");
            else
                window.transform.Find("Stamp_blue_2").gameObject.SetActive(false);
            if (data.Question_green_1)
                window.transform.Find("Stamp_green_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_white_green");
            else
                window.transform.Find("Stamp_green_1").gameObject.SetActive(false);
            if (data.Question_green_2)
                window.transform.Find("Stamp_green_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_white_green");
            else
                window.transform.Find("Stamp_green_2").gameObject.SetActive(false);
            if (data.Question_red_1)
                window.transform.Find("Stamp_red_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_white_red");
            else
                window.transform.Find("Stamp_red_1").gameObject.SetActive(false);
            if (data.Question_red_2)
                window.transform.Find("Stamp_red_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_white_red");
            else
                window.transform.Find("Stamp_red_2").gameObject.SetActive(false);
        }

        private static void Red_stamping(GameObject window, Have_Stamp_Data data)
        {
            if (data.Point_3_1)
                window.transform.Find("Stamp_3_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_red_3");
            else
                window.transform.Find("Stamp_3_1").gameObject.SetActive(false);
            if (data.Point_3_2)
                window.transform.Find("Stamp_3_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_red_3");
            else
                window.transform.Find("Stamp_3_2").gameObject.SetActive(false);
            if (data.Point_3_3)
                window.transform.Find("Stamp_3_3").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_red_3");
            else
                window.transform.Find("Stamp_3_3").gameObject.SetActive(false);
            if (data.Point_5_1)
                window.transform.Find("Stamp_5_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_red_5");
            else
                window.transform.Find("Stamp_5_1").gameObject.SetActive(false);
            if (data.Point_5_2)
                window.transform.Find("Stamp_5_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_red_5");
            else
                window.transform.Find("Stamp_5_2").gameObject.SetActive(false);
            if (data.Question_blue_1)
                window.transform.Find("Stamp_blue_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_red_blue");
            else
                window.transform.Find("Stamp_blue_1").gameObject.SetActive(false);
            if (data.Question_blue_2)
                window.transform.Find("Stamp_blue_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_red_blue");
            else
                window.transform.Find("Stamp_blue_2").gameObject.SetActive(false);
            if (data.Question_green_1)
                window.transform.Find("Stamp_green_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_red_green");
            else
                window.transform.Find("Stamp_green_1").gameObject.SetActive(false);
            if (data.Question_green_2)
                window.transform.Find("Stamp_green_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_red_green");
            else
                window.transform.Find("Stamp_green_2").gameObject.SetActive(false);
            if (data.Question_red_1)
                window.transform.Find("Stamp_red_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_red_red");
            else
                window.transform.Find("Stamp_red_1").gameObject.SetActive(false);
            if (data.Question_red_2)
                window.transform.Find("Stamp_red_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_red_red");
            else
                window.transform.Find("Stamp_red_2").gameObject.SetActive(false);
        }

        private static void Blue_stamping(GameObject window, Have_Stamp_Data data)
        {
            if (data.Point_3_1)
                window.transform.Find("Stamp_3_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_blue_3");
            else
                window.transform.Find("Stamp_3_1").gameObject.SetActive(false);
            if (data.Point_3_2)
                window.transform.Find("Stamp_3_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_blue_3");
            else
                window.transform.Find("Stamp_3_2").gameObject.SetActive(false);
            if (data.Point_3_3)
                window.transform.Find("Stamp_3_3").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_blue_3");
            else
                window.transform.Find("Stamp_3_3").gameObject.SetActive(false);
            if (data.Point_5_1)
                window.transform.Find("Stamp_5_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_blue_5");
            else
                window.transform.Find("Stamp_5_1").gameObject.SetActive(false);
            if (data.Point_5_2)
                window.transform.Find("Stamp_5_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_blue_5");
            else
                window.transform.Find("Stamp_5_2").gameObject.SetActive(false);
            if (data.Question_blue_1)
                window.transform.Find("Stamp_blue_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_blue_blue");
            else
                window.transform.Find("Stamp_blue_1").gameObject.SetActive(false);
            if (data.Question_blue_2)
                window.transform.Find("Stamp_blue_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_blue_blue");
            else
                window.transform.Find("Stamp_blue_2").gameObject.SetActive(false);
            if (data.Question_green_1)
                window.transform.Find("Stamp_green_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_blue_green");
            else
                window.transform.Find("Stamp_green_1").gameObject.SetActive(false);
            if (data.Question_green_2)
                window.transform.Find("Stamp_green_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_blue_green");
            else
                window.transform.Find("Stamp_green_2").gameObject.SetActive(false);
            if (data.Question_red_1)
                window.transform.Find("Stamp_red_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_blue_red");
            else
                window.transform.Find("Stamp_red_1").gameObject.SetActive(false);
            if (data.Question_red_2)
                window.transform.Find("Stamp_red_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_blue_red");
            else
                window.transform.Find("Stamp_red_2").gameObject.SetActive(false);
        }
    }

}

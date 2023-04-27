using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Alchemists_data
{
    public class Arti_Control :MonoBehaviour
    {
        private static GameObject pre_data;
        private static Data_Hub data_hub;
        /// <summary>
        ///  내가 구매한 유물을 보여주는 함수
        /// </summary>
        /// <param name="window">Canvas.Show_area.Arti_window.Arti_Area.Viewport.Content</param>
        /// <param name="data"></param>
        public static void Control(GameObject window, Artifacts_Data data)
        {
            pre_data = Resources.Load<GameObject>("source/Prefabs/Custom_Object/Arti_data");
            data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();

            // window아래의 하위 오브젝트들을 일시 제거
            Transform[] list = window.GetComponentsInChildren<Transform>();
            for(int i = 0; i < list.Length; i++)
            {
                if (list[i] != window.GetComponent<Transform>())
                {
                    Destroy(list[i].gameObject);
                }
            }
            /*
            Debug.Log(data.Discount_card);
            Debug.Log(data.Haste_boots);
            Debug.Log(data.Magic_mortar);
            Debug.Log(data.Night_vision);
            Debug.Log(data.Printing_machine);
            Debug.Log(data.Robe_of_respect);

            Debug.Log(data.Chest_of_witch);
            Debug.Log(data.Eloquent_necklace);
            Debug.Log(data.Hypnotic_necklace);
            Debug.Log(data.Seal_of_authority);
            Debug.Log(data.Silver_glass);
            Debug.Log(data.Thinking_hat);

            Debug.Log(data.Bronze_cup);
            Debug.Log(data.Feather_hat);
            Debug.Log(data.Glass_cabinet);
            Debug.Log(data.Golden_alter);
            Debug.Log(data.Magic_mirror);
            Debug.Log(data.Statue_of_wisdom);
            */

            // data 가 있으면 window에 추가
            // rank_1
            if (data.Discount_card)
            {
                GameObject tmp = Instantiate(pre_data, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(window.transform, false);

                // Image, Name, Explane, Point_val, Gold_val 설정
                tmp.transform.Find("Image").GetComponent<Image>().sprite      = Resources.Load<Sprite>("source/img/artifacts/rank_1/discount_card");
                tmp.name                                                      = data_hub.Artifacts_info["rank_1"][1].Name;
                tmp.transform.Find("Name").GetComponent<TMP_Text>().text      = data_hub.Artifacts_info["rank_1"][1].Kor_name;
                tmp.transform.Find("Explane").GetComponent<TMP_Text>().text   = data_hub.Artifacts_info["rank_1"][1].Comment;
                tmp.transform.Find("Point_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_1"][1].Point.ToString();
                tmp.transform.Find("Gold_val").GetComponent<TMP_Text>().text  = data_hub.Artifacts_info["rank_1"][1].Gold.ToString();
            }

            if (data.Haste_boots)
            {
                GameObject tmp = Instantiate(pre_data, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(window.transform, false);

                // Image, Name, Explane, Point_val, Gold_val 설정
                tmp.transform.Find("Image").GetComponent<Image>().sprite        = Resources.Load<Sprite>("source/img/artifacts/rank_1/haste_boots");
                tmp.name                                                        = data_hub.Artifacts_info["rank_1"][2].Name;
                tmp.transform.Find("Name").GetComponent<TMP_Text>().text        = data_hub.Artifacts_info["rank_1"][2].Kor_name;
                tmp.transform.Find("Explane").GetComponent<TMP_Text>().text     = data_hub.Artifacts_info["rank_1"][2].Comment;
                tmp.transform.Find("Point_val").GetComponent<TMP_Text>().text   = data_hub.Artifacts_info["rank_1"][2].Point.ToString();
                tmp.transform.Find("Gold_val").GetComponent<TMP_Text>().text    = data_hub.Artifacts_info["rank_1"][2].Gold.ToString();
            }

            if (data.Magic_mortar)
            {
                GameObject tmp = Instantiate(pre_data, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(window.transform, false);

                // Image, Name, Explane, Point_val, Gold_val 설정
                tmp.transform.Find("Image").GetComponent<Image>().sprite        = Resources.Load<Sprite>("source/img/artifacts/rank_1/magic_mortar");
                tmp.name                                                        = data_hub.Artifacts_info["rank_1"][3].Name;
                tmp.transform.Find("Name").GetComponent<TMP_Text>().text        = data_hub.Artifacts_info["rank_1"][3].Kor_name;
                tmp.transform.Find("Explane").GetComponent<TMP_Text>().text     = data_hub.Artifacts_info["rank_1"][3].Comment;
                tmp.transform.Find("Point_val").GetComponent<TMP_Text>().text   = data_hub.Artifacts_info["rank_1"][3].Point.ToString();
                tmp.transform.Find("Gold_val").GetComponent<TMP_Text>().text    = data_hub.Artifacts_info["rank_1"][3].Gold.ToString();
            }

            if (data.Night_vision)
            {
                GameObject tmp = Instantiate(pre_data, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(window.transform, false);

                // Image, Name, Explane, Point_val, Gold_val 설정
                tmp.transform.Find("Image").GetComponent<Image>().sprite        = Resources.Load<Sprite>("source/img/artifacts/rank_1/night_vision");
                tmp.name                                                        = data_hub.Artifacts_info["rank_1"][4].Name;
                tmp.transform.Find("Name").GetComponent<TMP_Text>().text        = data_hub.Artifacts_info["rank_1"][4].Kor_name;
                tmp.transform.Find("Explane").GetComponent<TMP_Text>().text     = data_hub.Artifacts_info["rank_1"][4].Comment;
                tmp.transform.Find("Point_val").GetComponent<TMP_Text>().text   = data_hub.Artifacts_info["rank_1"][4].Point.ToString();
                tmp.transform.Find("Gold_val").GetComponent<TMP_Text>().text    = data_hub.Artifacts_info["rank_1"][4].Gold.ToString();
            }

            if (data.Printing_machine)
            {
                GameObject tmp = Instantiate(pre_data, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(window.transform, false);

                // Image, Name, Explane, Point_val, Gold_val 설정
                tmp.transform.Find("Image").GetComponent<Image>().sprite        = Resources.Load<Sprite>("source/img/artifacts/rank_1/printing_machine");
                tmp.name                                                        = data_hub.Artifacts_info["rank_1"][5].Name;
                tmp.transform.Find("Name").GetComponent<TMP_Text>().text        = data_hub.Artifacts_info["rank_1"][5].Kor_name;
                tmp.transform.Find("Explane").GetComponent<TMP_Text>().text     = data_hub.Artifacts_info["rank_1"][5].Comment;
                tmp.transform.Find("Point_val").GetComponent<TMP_Text>().text   = data_hub.Artifacts_info["rank_1"][5].Point.ToString();
                tmp.transform.Find("Gold_val").GetComponent<TMP_Text>().text    = data_hub.Artifacts_info["rank_1"][5].Gold.ToString();
            }

            if (data.Robe_of_respect)
            {
                GameObject tmp = Instantiate(pre_data, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(window.transform, false);

                // Image, Name, Explane, Point_val, Gold_val 설정
                tmp.transform.Find("Image").GetComponent<Image>().sprite        = Resources.Load<Sprite>("source/img/artifacts/rank_1/robe_of_respect");
                tmp.name                                                        = data_hub.Artifacts_info["rank_1"][6].Name;
                tmp.transform.Find("Name").GetComponent<TMP_Text>().text        = data_hub.Artifacts_info["rank_1"][6].Kor_name;
                tmp.transform.Find("Explane").GetComponent<TMP_Text>().text     = data_hub.Artifacts_info["rank_1"][6].Comment;
                tmp.transform.Find("Point_val").GetComponent<TMP_Text>().text   = data_hub.Artifacts_info["rank_1"][6].Point.ToString();
                tmp.transform.Find("Gold_val").GetComponent<TMP_Text>().text    = data_hub.Artifacts_info["rank_1"][6].Gold.ToString();
            }

            // rank_2
            if (data.Chest_of_witch)
            {
                GameObject tmp = Instantiate(pre_data, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(window.transform, false);

                // Image, Name, Explane, Point_val, Gold_val 설정
                tmp.transform.Find("Image").GetComponent<Image>().sprite        = Resources.Load<Sprite>("source/img/artifacts/rank_2/chest_of_witch");
                tmp.name                                                        = data_hub.Artifacts_info["rank_2"][1].Name;
                tmp.transform.Find("Name").GetComponent<TMP_Text>().text        = data_hub.Artifacts_info["rank_2"][1].Kor_name;
                tmp.transform.Find("Explane").GetComponent<TMP_Text>().text     = data_hub.Artifacts_info["rank_2"][1].Comment;
                tmp.transform.Find("Point_val").GetComponent<TMP_Text>().text   = data_hub.Artifacts_info["rank_2"][1].Point.ToString();
                tmp.transform.Find("Gold_val").GetComponent<TMP_Text>().text    = data_hub.Artifacts_info["rank_2"][1].Gold.ToString();
            }

            if (data.Eloquent_necklace)
            {
                GameObject tmp = Instantiate(pre_data, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(window.transform, false);

                // Image, Name, Explane, Point_val, Gold_val 설정
                tmp.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_2/eloquent_necklace");
                tmp.name = data_hub.Artifacts_info["rank_2"][2].Name;
                tmp.transform.Find("Name").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][2].Kor_name;
                tmp.transform.Find("Explane").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][2].Comment;
                tmp.transform.Find("Point_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][2].Point.ToString();
                tmp.transform.Find("Gold_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][2].Gold.ToString();
            }

            if (data.Hypnotic_necklace)
            {
                GameObject tmp = Instantiate(pre_data, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(window.transform, false);

                // Image, Name, Explane, Point_val, Gold_val 설정
                tmp.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_2/hypnotic_necklace");
                tmp.name = data_hub.Artifacts_info["rank_2"][3].Name;
                tmp.transform.Find("Name").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][3].Kor_name;
                tmp.transform.Find("Explane").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][3].Comment;
                tmp.transform.Find("Point_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][3].Point.ToString();
                tmp.transform.Find("Gold_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][3].Gold.ToString();
            }

            if (data.Seal_of_authority)
            {
                GameObject tmp = Instantiate(pre_data, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(window.transform, false);

                // Image, Name, Explane, Point_val, Gold_val 설정
                tmp.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_2/seal_of_authority");
                tmp.name = data_hub.Artifacts_info["rank_2"][4].Name;
                tmp.transform.Find("Name").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][4].Kor_name;
                tmp.transform.Find("Explane").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][4].Comment;
                tmp.transform.Find("Point_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][4].Point.ToString();
                tmp.transform.Find("Gold_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][4].Gold.ToString();
            }

            if (data.Silver_glass)
            {
                GameObject tmp = Instantiate(pre_data, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(window.transform, false);

                // Image, Name, Explane, Point_val, Gold_val 설정
                tmp.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_2/silver_glass");
                tmp.name = data_hub.Artifacts_info["rank_2"][5].Name;
                tmp.transform.Find("Name").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][5].Kor_name;
                tmp.transform.Find("Explane").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][5].Comment;
                tmp.transform.Find("Point_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][5].Point.ToString();
                tmp.transform.Find("Gold_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][5].Gold.ToString();
            }

            if (data.Thinking_hat)
            {
                GameObject tmp = Instantiate(pre_data, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(window.transform, false);

                // Image, Name, Explane, Point_val, Gold_val 설정
                tmp.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_2/thinking_hat");
                tmp.name = data_hub.Artifacts_info["rank_2"][6].Name;
                tmp.transform.Find("Name").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][6].Kor_name;
                tmp.transform.Find("Explane").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][6].Comment;
                tmp.transform.Find("Point_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][6].Point.ToString();
                tmp.transform.Find("Gold_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_2"][6].Gold.ToString();
            }

            // rank_3

            if (data.Bronze_cup)
            {
                GameObject tmp = Instantiate(pre_data, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(window.transform, false);

                // Image, Name, Explane, Point_val, Gold_val 설정
                tmp.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_3/bronze_cup");
                tmp.name = data_hub.Artifacts_info["rank_3"][1].Name;
                tmp.transform.Find("Name").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][1].Kor_name;
                tmp.transform.Find("Explane").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][1].Comment;
                tmp.transform.Find("Point_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][1].Point.ToString();
                tmp.transform.Find("Gold_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][1].Gold.ToString();
            }

            if (data.Feather_hat)
            {
                GameObject tmp = Instantiate(pre_data, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(window.transform, false);

                // Image, Name, Explane, Point_val, Gold_val 설정
                tmp.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_3/feather_hat");
                tmp.name = data_hub.Artifacts_info["rank_3"][2].Name;
                tmp.transform.Find("Name").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][2].Kor_name;
                tmp.transform.Find("Explane").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][2].Comment;
                tmp.transform.Find("Point_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][2].Point.ToString();
                tmp.transform.Find("Gold_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][2].Gold.ToString();
            }

            if (data.Glass_cabinet)
            {
                GameObject tmp = Instantiate(pre_data, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(window.transform, false);

                // Image, Name, Explane, Point_val, Gold_val 설정
                tmp.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_3/glass_cabinet");
                tmp.name = data_hub.Artifacts_info["rank_3"][3].Name;
                tmp.transform.Find("Name").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][3].Kor_name;
                tmp.transform.Find("Explane").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][3].Comment;
                tmp.transform.Find("Point_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][3].Point.ToString();
                tmp.transform.Find("Gold_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][3].Gold.ToString();
            }

            if (data.Golden_alter)
            {
                GameObject tmp = Instantiate(pre_data, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(window.transform, false);

                // Image, Name, Explane, Point_val, Gold_val 설정
                tmp.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_3/golden_alter");
                tmp.name = data_hub.Artifacts_info["rank_3"][4].Name;
                tmp.transform.Find("Name").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][4].Kor_name;
                tmp.transform.Find("Explane").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][4].Comment;
                tmp.transform.Find("Point_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][4].Point.ToString();
                tmp.transform.Find("Gold_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][4].Gold.ToString();
            }
            
            if (data.Magic_mirror)
            {
                GameObject tmp = Instantiate(pre_data, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(window.transform, false);

                // Image, Name, Explane, Point_val, Gold_val 설정
                tmp.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_3/magic_mirror");
                tmp.name = data_hub.Artifacts_info["rank_3"][5].Name;
                tmp.transform.Find("Name").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][5].Kor_name;
                tmp.transform.Find("Explane").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][5].Comment;
                tmp.transform.Find("Point_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][5].Point.ToString();
                tmp.transform.Find("Gold_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][5].Gold.ToString();
            }

            if (data.Statue_of_wisdom)
            {
                GameObject tmp = Instantiate(pre_data, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(window.transform, false);

                // Image, Name, Explane, Point_val, Gold_val 설정
                tmp.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_3/statue_of_wisdom");
                tmp.name = data_hub.Artifacts_info["rank_3"][6].Name;
                tmp.transform.Find("Name").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][6].Kor_name;
                tmp.transform.Find("Explane").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][6].Comment;
                tmp.transform.Find("Point_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][6].Point.ToString();
                tmp.transform.Find("Gold_val").GetComponent<TMP_Text>().text = data_hub.Artifacts_info["rank_3"][6].Gold.ToString();
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Alchemists_data
{
    public class Board_Data_Control : MonoBehaviour
    {
        /// <summary>
        /// 사이드 메뉴에 내 데이터를 기반으로 정보를 뿌리는 함수
        /// </summary>
        /// <param name="area"> Show_area gameObject</param>
        /// <param name="data">my_data</param>
        public static void Control(GameObject area, User_Data_Array data)
        {
            // 소지금 
            area.transform.Find("Side_menu").Find("My_gold_val").GetComponent<TMP_Text>().text = data.User_ingame_data.My_gold.ToString() + "G";

            // 내 명성
            area.transform.Find("Side_menu").Find("My_fame_val").GetComponent<TMP_Text>().text = data.User_ingame_data.Point.ToString() + "점";

            // 재료 변수 설정
            Ingre_Control.Control(area.transform.Find("Ingre_window").gameObject ,data.User_ingame_data.Ingredient);
            
            // 호의카드 변수 설정
            Favor_Control.Control(area.transform.Find("Favor_window").gameObject, data.User_ingame_data.Favor_card);

            // 구매한 아티팩트가 있다면 prefabs 추가
            Arti_Control.Control(area.transform.Find("Arti_window").Find("Arti_Area").Find("Viewport").Find("Content").gameObject, data.User_ingame_data.Artifacts);
            // 인장 -> 변수가 true면 이미지 띄우고 없으면 SetActive(false)
            // 인장은 색에 따라 들어가는 이미지가 달라지므로 4개의 함수를 써봅시다.
            Stamp_Control.Control(area.transform.Find("Stamp_window").gameObject, data.User_ingame_data.Have_stamp, data.User_color);
            // 할인 제시 -> 인장과 동일
            Adv_Control.Control(area.transform.Find("Adv_window").gameObject, data.User_ingame_data.Discount_adventurer);
            // 성공한 물약 -> true면 환한 이미지로 변경
            Check_Potion_Control.Control(area.transform.Find("Result_window").gameObject, data.User_ingame_data.Is_check_poition);
        }

        public static void Ready_player_shower(GameObject area, Dictionary<int, User_Data_Array> user_data_array)
        {
            Transform[] list = area.transform.Find("Ready_area").GetComponentsInChildren<Transform>();
            string path;
            // 유저의 준비 상태에 따라 이미지를 넣은 alpha값을 조절
            // 기존의 모든 이미지의 alpha 를 80으로
            for(int i = 0; i < user_data_array.Count; i++)
            {
                path = user_data_array[i].User_color switch {
                    "red" => "source/img/icon/cube_red",
                    "blue" => "source/img/icon/cube_blue",
                    "black" => "source/img/icon/cube_black",
                    "white" => "source/img/icon/cube_white",
                    _ => "source/img/icon/cube_gray",
                };
                list[i + 1].gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
                Color tmp = list[i + 1].gameObject.GetComponent<Image>().color;
                tmp.a = 0f;
                list[i + 1].gameObject.GetComponent<Image>().color = tmp;
            }

            // is_ready가 true라면 a값을 올림
            for(int i = 0; i< user_data_array.Count; i++)
            {
                if (user_data_array[i].Is_ready.Equals("true"))
                {
                    Color tmp = list[i + 1].gameObject.GetComponent<Image>().color;
                    tmp.a = 255f;
                    list[i + 1].gameObject.GetComponent<Image>().color = tmp;
                }
            }
        }

        public static void Other_data_setting(Transform area, Dictionary<int, User_Data_Array> user_data_array, string my_key)
        {
            // 사용할 오브젝트들 선언
            GameObject _t_obj = area.Find("Other_player_info_window").gameObject;
            GameObject pre_window   = Resources.Load<GameObject>("source/Prefabs/Custom_Object/O_Info");
            GameObject pre_cube_img = Resources.Load<GameObject>("source/Prefabs/Custom_Object/cube_image");
            GameObject pre_arti_img = Resources.Load<GameObject>("source/Prefabs/Custom_Object/arti_image");

            //사용할 변수
            string path;

            for(int i = 0; i < user_data_array.Count; i++)
            {
                // 내 정보면 지나치기
                if (user_data_array[i].User_key.Equals(my_key))
                {
                    continue;
                }

                GameObject tmp = Instantiate(pre_window, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(_t_obj.transform, false);

                // 이름 설정
                tmp.transform.Find("name").GetComponent<TMP_Text>().text = user_data_array[i].User_name;

                // 색 설정
                path = user_data_array[i].User_color switch 
                { 
                    "red" => "source/img/icon/cube_red",
                    "blue" => "source/img/icon/cube_blue",
                    "black" => "source/img/icon/cube_black",
                    "white" => "source/img/icon/cube_white",
                    _ => "",
                };
                tmp.transform.Find("color_icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(path);

                // 보유 금액 표기
                tmp.transform.Find("gold_val").GetComponent<TMP_Text>().text = user_data_array[i].User_ingame_data.My_gold.ToString() + "G";
                // 보유 명성 표기
                tmp.transform.Find("fame_val").GetComponent<TMP_Text>().text = user_data_array[i].User_ingame_data.Point.ToString();
                // 호의카드 장수 표기
                tmp.transform.Find("favor_card_val").GetComponent<TMP_Text>().text = user_data_array[i].User_ingame_data.Favor_card.Total.ToString() + "장";
                // 재료카드 장수 표기
                tmp.transform.Find("ingre_card_val").GetComponent<TMP_Text>().text = user_data_array[i].User_ingame_data.Ingredient.Total.ToString() + "장";

                // 큐브 표기
                // 기존 표기 큐브 제거
                Transform[] list = tmp.transform.Find("cube_view").Find("Viewport").Find("Content").GetComponentsInChildren<Transform>();
                if (list != null)
                {
                    for(int j = 0; j < list.Length; j++)
                    {
                        Destroy(list[j].gameObject);
                    }
                }
                for (int j = 0; j < user_data_array[i].User_ingame_data.Cube_count; j++)
                {
                    GameObject img = Instantiate(pre_cube_img,new Vector3(0,0,0) ,Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("cube_view").Find("Viewport").Find("Content"), false);

                    // 큐브 색 표기
                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
                }

                #region 실험결과 영역
                // 실험 결과 표기
                if (user_data_array[i].User_ingame_data.Is_check_poition.Red_1)
                    tmp.transform.Find("test_area").Find("red_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/red_+");
                else
                    tmp.transform.Find("test_area").Find("red_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/red_+_back");

                if (user_data_array[i].User_ingame_data.Is_check_poition.Red_0)
                    tmp.transform.Find("test_area").Find("red_0").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/red_-");
                else
                    tmp.transform.Find("test_area").Find("red_0").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/red_-_back");

                if (user_data_array[i].User_ingame_data.Is_check_poition.Green_1)
                    tmp.transform.Find("test_area").Find("green_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/green_+");
                else
                    tmp.transform.Find("test_area").Find("green_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/green_+_back");

                if (user_data_array[i].User_ingame_data.Is_check_poition.Green_0)
                    tmp.transform.Find("test_area").Find("green_0").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/green_-");
                else
                    tmp.transform.Find("test_area").Find("green_0").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/green_-_back");

                if (user_data_array[i].User_ingame_data.Is_check_poition.Blue_1)
                    tmp.transform.Find("test_area").Find("blue_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/blue_+");
                else
                    tmp.transform.Find("test_area").Find("blue_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/blue_+_back");

                if (user_data_array[i].User_ingame_data.Is_check_poition.Blue_0)
                    tmp.transform.Find("test_area").Find("blue_0").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/blue_-");
                else
                    tmp.transform.Find("test_area").Find("blue_0").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/blue_-_back");

                if (user_data_array[i].User_ingame_data.Is_check_poition.Blank)
                    tmp.transform.Find("test_area").Find("blank").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/blue_-");
                else
                    tmp.transform.Find("test_area").Find("blank").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/potion/blue_-_back");
                #endregion

                #region 유물 영역
                // 구매한 아티팩트 표기
                // 모든 아티팩트를 돌아야함
                // 기존에 표기된 아티팩트들 제거
                list = tmp.transform.Find("arti_view").Find("Viewport").Find("Content").GetComponentsInChildren<Transform>();
                for(int j = 0; j < list.Length; j++)
                {
                    Destroy(list[j].gameObject);
                }

                // 아티팩트 생성
                // rank_1-1
                if (user_data_array[i].User_ingame_data.Artifacts.Discount_card)
                {
                    GameObject img = Instantiate(pre_arti_img, new Vector3(0, 0, 0), Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("arti_view").Find("Viewport").Find("Content"), false);
                    img.name = "discount_card";

                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_1/discount_card");
                }
                // 1-2
                if (user_data_array[i].User_ingame_data.Artifacts.Haste_boots)
                {
                    GameObject img = Instantiate(pre_arti_img, new Vector3(0, 0, 0), Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("arti_view").Find("Viewport").Find("Content"), false);
                    img.name = "haste_boots";

                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_1/haste_boots");
                }
                // 1-3
                if (user_data_array[i].User_ingame_data.Artifacts.Magic_mortar)
                {
                    GameObject img = Instantiate(pre_arti_img, new Vector3(0, 0, 0), Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("arti_view").Find("Viewport").Find("Content"), false);
                    img.name = "magic_mortar";

                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_1/magic_mortar");
                }
                // 1-4
                if (user_data_array[i].User_ingame_data.Artifacts.Night_vision)
                {
                    GameObject img = Instantiate(pre_arti_img, new Vector3(0, 0, 0), Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("arti_view").Find("Viewport").Find("Content"), false);
                    img.name = "Night_vision";

                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_1/night_vision");
                }
                // 1-5
                if (user_data_array[i].User_ingame_data.Artifacts.Printing_machine)
                {
                    GameObject img = Instantiate(pre_arti_img, new Vector3(0, 0, 0), Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("arti_view").Find("Viewport").Find("Content"), false);
                    img.name = "printing_machine";

                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_1/printing_machine");
                }
                // 1-6
                if (user_data_array[i].User_ingame_data.Artifacts.Robe_of_respect)
                {
                    GameObject img = Instantiate(pre_arti_img, new Vector3(0, 0, 0), Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("arti_view").Find("Viewport").Find("Content"), false);
                    img.name = "robe_of_respect";

                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_1/robe_of_respect");
                }
                // 2-1
                if (user_data_array[i].User_ingame_data.Artifacts.Chest_of_witch)
                {
                    GameObject img = Instantiate(pre_arti_img, new Vector3(0, 0, 0), Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("arti_view").Find("Viewport").Find("Content"), false);
                    img.name = "chest_of_witch";

                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_2/chest_of_witch");
                }
                // 2-2
                if (user_data_array[i].User_ingame_data.Artifacts.Eloquent_necklace)
                {
                    GameObject img = Instantiate(pre_arti_img, new Vector3(0, 0, 0), Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("arti_view").Find("Viewport").Find("Content"), false);
                    img.name = "eloquent_necklace";

                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_2/eloquent_necklace");
                }
                // 2-3
                if (user_data_array[i].User_ingame_data.Artifacts.Hypnotic_necklace)
                {
                    GameObject img = Instantiate(pre_arti_img, new Vector3(0, 0, 0), Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("arti_view").Find("Viewport").Find("Content"), false);
                    img.name = "hypnotic_necklace";

                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_2/hypnotic_necklace");
                }
                // 2-4
                if (user_data_array[i].User_ingame_data.Artifacts.Seal_of_authority)
                {
                    GameObject img = Instantiate(pre_arti_img, new Vector3(0, 0, 0), Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("arti_view").Find("Viewport").Find("Content"), false);
                    img.name = "seal_of_authority";

                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_2/seal_of_authority");
                }
                // 2-5
                if (user_data_array[i].User_ingame_data.Artifacts.Silver_glass)
                {
                    GameObject img = Instantiate(pre_arti_img, new Vector3(0, 0, 0), Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("arti_view").Find("Viewport").Find("Content"), false);
                    img.name = "silver_glass";

                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_2/silver_glass");
                }
                // 2-6
                if (user_data_array[i].User_ingame_data.Artifacts.Thinking_hat)
                {
                    GameObject img = Instantiate(pre_arti_img, new Vector3(0, 0, 0), Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("arti_view").Find("Viewport").Find("Content"), false);
                    img.name = "thinking_hat";

                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_2/thinking_hat");
                }
                // rank_3-1
                if (user_data_array[i].User_ingame_data.Artifacts.Bronze_cup)
                {
                    GameObject img = Instantiate(pre_arti_img, new Vector3(0, 0, 0), Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("arti_view").Find("Viewport").Find("Content"), false);
                    img.name = "bronze_cup";

                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_3/bronze_cup");
                }
                // 3-2
                if (user_data_array[i].User_ingame_data.Artifacts.Feather_hat)
                {
                    GameObject img = Instantiate(pre_arti_img, new Vector3(0, 0, 0), Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("arti_view").Find("Viewport").Find("Content"), false);
                    img.name = "feather_hat";

                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_3/feather_hat");
                }
                // 3-3
                if (user_data_array[i].User_ingame_data.Artifacts.Glass_cabinet)
                {
                    GameObject img = Instantiate(pre_arti_img, new Vector3(0, 0, 0), Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("arti_view").Find("Viewport").Find("Content"), false);
                    img.name = "glass_cabinet";

                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_3/glass_cabinet");
                }
                // 3-4
                if (user_data_array[i].User_ingame_data.Artifacts.Golden_alter)
                {
                    GameObject img = Instantiate(pre_arti_img, new Vector3(0, 0, 0), Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("arti_view").Find("Viewport").Find("Content"), false);
                    img.name = "golden_alter";

                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_3/golden_alter");
                }
                // 3-5
                if (user_data_array[i].User_ingame_data.Artifacts.Magic_mirror)
                {
                    GameObject img = Instantiate(pre_arti_img, new Vector3(0, 0, 0), Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("arti_view").Find("Viewport").Find("Content"), false);
                    img.name = "magic_mirror";

                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_3/magic_mirror");
                }
                //3-6
                if (user_data_array[i].User_ingame_data.Artifacts.Statue_of_wisdom)
                {
                    GameObject img = Instantiate(pre_arti_img, new Vector3(0, 0, 0), Quaternion.identity);
                    img.transform.SetParent(tmp.transform.Find("arti_view").Find("Viewport").Find("Content"), false);
                    img.name = "statue_of_wisdom";

                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/artifacts/rank_3/statue_of_wisdom");
                }
                #endregion
            }
        }
    }
}
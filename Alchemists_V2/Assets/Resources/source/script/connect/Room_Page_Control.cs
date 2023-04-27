using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using Self_Converter;
using Alchemists_data;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Room_Page_Control : MonoBehaviour
{
    private Data_Hub data_hub;

    // room_page 에서 사용될 변수
    public GameObject user_prefabs;
    public GameObject user_info_parents;
    public GameObject room_page;
    public GameObject room_announce_modal;
    public TMP_Text chat_area_obj;
    public TMP_InputField my_chat_input_obj;
    private GameObject my_field;

    private Regex nick_name_regex = new Regex(@"^([A-za-z0-9ㄱ-ㅎ가-힣][^`[]+){1,10}$");

    // Use this for initialization
    void Start()
    {
        data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();
        Watch_socket();
        if (data_hub.Im_master)
        {
            string msg = data_hub.My_name + "가 방에 참가하였습니다. - from unity As master";
            Alc_Data send_data = new Alc_Builder().User_name(data_hub.My_name)
                                                  .Room_name(data_hub.Room_name)
                                                  .Room_pw(data_hub.Room_pw)
                                                  .Count(data_hub.Max_count)
                                                  .Is_master("true")
                                                  .Is_ready("false")
                                                  .No_enter(data_hub.No_enter)
                                                  .Msg(msg)
                                                  .Create_Room_Build();
            //Debug.Log(send_data.print());
            data_hub.Socket.Emit("enter", send_data);
        }
        else
        {
            string send_msg = data_hub.My_name + "가 방에 참가하였습니다. - from unity as entrant";
            //Debug.Log(send_msg);
            Alc_Data send_data = new Alc_Builder().User_name(data_hub.My_name)
                                                  .Room_name(data_hub.Room_name)
                                                  .Is_master("false")
                                                  .Is_ready("false")
                                                  .No_enter(data_hub.No_enter)
                                                  .Msg(send_msg)
                                                  .Enter_Room_Data_Build();
            //Debug.Log(send_data.print());
            data_hub.Socket.Emit("enter", send_data);
        }

        data_hub.No_enter = false;

        //Debug.Log(data_hub.Room_name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    ///  방 나가기 함수
    /// </summary>
    // params-> room_name, my_key
    public void exit_room()
    {
        //data :: room_name, user_key
        Alc_Data send_data = new Alc_Builder().Room_name(data_hub.Room_name)
                                              .User_key(data_hub.My_key)
                                              .Common_Data_Build();

        data_hub.Socket.Emit("quit_room", send_data);
        // chat 내역 지우기
        chat_area_obj.text = "";
        // 방장 취소
        data_hub.Im_master = false;
        // 버튼 이름 초기화
        room_page.transform.Find("ready_str").GetComponent<TMP_Text>().text = "준비하기";
    }

    // 방에서 나의 색을 선택할 함수
    public void Select_color(string color)
    {
        // color== null 이면 색 해제
        // sending_data :: user_name, user_color, user_key, room_name
        // edit_color

        Alc_Data send_data = new Alc_Builder().User_name(data_hub.My_name)
                                              .User_key(data_hub.My_key)
                                              .User_color(color)
                                              .Room_name(data_hub.Room_name)
                                              .Edit_Color_Data_Build();
        //Debug.Log(send_data.print());

        data_hub.Socket.Emit("edit_color", send_data);
    }

    /// <summary>
    ///  준비 완료
    /// </summary>
    // params -> room_name, my_key, is_ready
    public void game_ready()
    {
        // 방장이 아니라면 게임 준비
        // 방장이라면 게임 시작 버튼으로 변경되어야함 
        if (data_hub.Im_master)
        {
            // 모든 유저가 준비상태가 아니면 안내를 띄워주어야함

            // 현재 유저수 구하기
            int counter = 0;
            foreach (KeyValuePair<int, User_Data_Array> item in data_hub.User_data_array)
            {
                if (item.Value.Is_ready.Equals("true")) counter++;
            }

            // 비교 후 게임 시작 / 게임 시작 불가 announce
            if (counter + 1 == data_hub.Max_count)
            {
                // 같으면 게임 시작 -> scene 이동
                //SceneManager.LoadScene("Board");

                // 게임 시작을 알림
                data_hub.Socket.Emit("move_to_board_everyone", data_hub.Room_name);
            }
            else
            {
                // 다르면 announce 띄우기
                room_announce_modal.transform.Find("wrong_text").GetComponent<TMP_Text>().text =
                    "모든 연금술사가 준비되어야\n 게임을 시작할 수 있습니다.\n게임 준비 완료까지\n 잠시 기다려주세요";
                room_announce_modal.SetActive(true);
            }
        }
        else
        {
            string t = "";
            string is_ready = "";
            foreach (KeyValuePair<int, User_Data_Array> item in data_hub.User_data_array)
            {
                if (data_hub.My_key.Equals(item.Value.User_key))
                {
                    // 색이 안정해져있으면 취소해야함
                    if (item.Value.User_color == null || item.Value.User_color == "")
                    {
                        // 색을 선택해야 고를 수 있다고 안내
                        room_announce_modal.transform.Find("wrong_text").GetComponent<TMP_Text>().text =
                            "색을 골라야\n 준비를 완료할 수 있습니다.";
                        room_announce_modal.SetActive(true);
                        return;
                    }
                    // is_ready가 false 이면 준비하기 true면 준비완료로 변경
                    if (item.Value.Is_ready.Equals("true"))
                    {
                        is_ready = "false";
                        t = "준비하기";
                    }
                    else
                    {
                        is_ready = "true";
                        t = "준비완료";
                    }
                    break;
                }
            }

            Alc_Data send_data = new Alc_Builder().Room_name(data_hub.Room_name)
                                                  .User_key(data_hub.My_key)
                                                  .Is_ready(is_ready)
                                                  .Ready_Game_Data_Build();

            data_hub.Socket.Emit("lobby_ready", send_data);

            // 버튼 이름 갱신
            room_page.transform.Find("ready_str").GetComponent<TMP_Text>().text = t;
        }
    }

    // 채팅 전송함수
    public void emit_chat()
    {
        // 빈 값은 전송 x
        if (my_chat_input_obj.text.Equals("")) return;
        Alc_Data send_data = new Alc_Builder().Speaker(data_hub.My_name)
                                              .Msg(my_chat_input_obj.text)
                                              .Type("normal")
                                              .Room_name(data_hub.Room_name)
                                              .Chat_Data_Build();
        //Debug.Log(send_data.print());

        data_hub.Socket.Emit("chat", send_data);

        my_chat_input_obj.text = "";

    }

    private void Watch_socket()
    {

        // 채팅할 때 사용될 함수
        data_hub.Socket.OnUnityThread("chat", (data) =>
        {
            //Debug.Log(data);

            // data :: speaker , msg, type
            // 방 안에서의 채팅 
            chat_area_obj.text += StoD_converter.Resp_to_Chat_Data(data);
            Scroll_to_Bottom.Scroll_to_bottom(chat_area_obj.transform.parent.parent.parent.gameObject.GetComponent<ScrollRect>());

        });
        // 방 입장시 서버에서 현재 방에 있는 모든 유저의 정보를 보내줌
        // data :: user_data_array
        data_hub.Socket.OnUnityThread("all_player", (data) =>
        {
            data_hub.User_data_array = StoD_converter.Resp_to_User_Data(data);
            //Debug.Log(user_data_array.Count);
            // 부모 아래에 오브젝트를 전부 제거하고
            if (user_info_parents != null)
            {
                Transform[] child_list = user_info_parents.GetComponentsInChildren<Transform>();
                if (child_list != null)
                {
                    for (int i = 1; i < child_list.Length; i++)
                    {
                        if (child_list[i] != user_info_parents.transform)
                        {
                            Destroy(child_list[i].gameObject);
                        }
                    }
                }
                // 추가
                for (var i = 0; i < data_hub.User_data_array.Count; i++)
                {
                    int x = (i + 1) % 2 == 0 ? 280 : -280;
                    int y = (i + 1) <= 2 ? 135 : -135;
                    GameObject temp = Instantiate(user_prefabs, new Vector3(x, y, 0), Quaternion.identity);
                    temp.transform.SetParent(user_info_parents.transform, false);
                    temp.transform.Find("user_name").GetComponent<TMP_Text>().text = data_hub.User_data_array[i].User_name;

                    bool he_has_color = false;
                    string path = "";
                    string color = data_hub.User_data_array[i].User_color;

                    // 내 유저 정보라면 색을 고를 수 있어야하므로 색을 선택하기 위한 버튼을 생성
                    // 아니라면 그냥 img만 보여줌
                    //Debug.Log(data_hub.My_key);
                    if (data_hub.User_data_array[i].User_key.Equals(data_hub.My_key))
                    {
                        //큐브 버튼 4개에 이벤트 추가
                        temp.transform.Find("btn_area").transform.Find("black").gameObject.GetComponent<Button>().onClick.AddListener(() => Select_color("black"));
                        temp.transform.Find("btn_area").transform.Find("blue").gameObject.GetComponent<Button>().onClick.AddListener(() => Select_color("blue"));
                        temp.transform.Find("btn_area").transform.Find("red").gameObject.GetComponent<Button>().onClick.AddListener(() => Select_color("red"));
                        temp.transform.Find("btn_area").transform.Find("white").gameObject.GetComponent<Button>().onClick.AddListener(() => Select_color("white"));

                        // 큐브 색 선택 이벤트 추가
                        temp.transform.Find("btn_img").gameObject.GetComponent<Button>().onClick.AddListener(() => Select_color(""));

                        if (data_hub.User_data_array[i].User_color != "")
                        {
                            he_has_color = true;
                        }
                        else
                        {
                            temp.transform.Find("btn_img").gameObject.SetActive(false);
                            temp.transform.Find("btn_area").gameObject.SetActive(true);
                        }

                        // temp 저장해두기
                        my_field = temp;

                        // 내가 방장인 변수 저장해두기
                        if (data_hub.User_data_array[i].Is_master.Equals("true")) data_hub.Im_master = true;
                        else data_hub.Im_master = false;

                        // 준비 완료 상태 알림
                        if (data_hub.User_data_array[i].Is_ready == "true")
                        {
                            temp.transform.Find("is_master").gameObject.SetActive(true);
                            temp.transform.Find("is_master").GetComponent<TMP_Text>().text = "준비 완료";
                            temp.transform.Find("btn_img").GetComponent<Button>().enabled = false;
                        }
                        else
                        {
                            temp.transform.Find("is_master").gameObject.SetActive(false);
                        }
                        // 방장이면 방장 표시 및 버튼 변경
                        if (data_hub.User_data_array[i].Is_master == "true")
                        {
                            temp.transform.Find("is_master").gameObject.SetActive(true);
                            room_page.transform.Find("ready_str").GetComponent<TMP_Text>().text = "게임시작";
                        }
                    }
                    else
                    {
                        // 다른 플레이어의 btn_img 버튼 컴포넌트 삭제
                        Destroy(temp.transform.Find("btn_img").GetComponent<Button>());
                        if (data_hub.User_data_array[i].User_color != null)
                        {
                            he_has_color = true;
                        }
                        temp.transform.Find("btn_img").gameObject.SetActive(true);

                        // 준비 완료 상태 알림
                        if (data_hub.User_data_array[i].Is_ready == "true")
                        {
                            temp.transform.Find("is_master").gameObject.SetActive(true);
                            temp.transform.Find("is_master").GetComponent<TMP_Text>().text = "준비 완료";
                        }
                        else
                        {
                            temp.transform.Find("is_master").gameObject.SetActive(false);
                        }

                        // 방장이면 표시해줌 -> is_ready에서 false로 바뀌니 추후에 변경해주는걸로 서순...
                        if (data_hub.User_data_array[i].Is_master.Equals("true"))
                        {
                            temp.transform.Find("is_master").gameObject.SetActive(true);
                        }
                    }


                    // btn 이미지에 색 추가
                    if (he_has_color)
                    {
                        if (data_hub.User_data_array[i].User_color.Equals("red"))
                            path = "source/img/icon/cube_red";
                        else if (data_hub.User_data_array[i].User_color.Equals("black"))
                            path = "source/img/icon/cube_black";
                        else if (data_hub.User_data_array[i].User_color.Equals("blue"))
                            path = "source/img/icon/cube_blue";
                        else if (data_hub.User_data_array[i].User_color.Equals("white"))
                            path = "source/img/icon/cube_white";

                        temp.transform.Find("btn_img").gameObject.SetActive(true);
                    }
                    else
                    {
                        path = "source/img/icon/cube_gray";
                    }

                    temp.transform.Find("btn_img").GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
                }
            }

        });

        // 같은 색을 선택하면 값이 바뀌지 않고 안내창이 나오는 함수
        data_hub.Socket.OnUnityThread("same_color", (res) =>
        {
            room_announce_modal.transform.Find("wrong_text").GetComponent<TMP_Text>().text =
                    "다른 연금술사와\n같은 색을 고를 수 없습니다.\n다른 색을 골라주세요!";
            room_announce_modal.SetActive(true);

            // btn_area로 화면을 돌려야함
            my_field.GetComponent<Btn_cube_select>().Show_btn_area();
        });

        // 방 나갔을 때 페이지를 옮길 함수
        data_hub.Socket.OnUnityThread("move_room_list", (res) =>
        {
            // 페이지를 옮기고
            SceneManager.LoadScene("Lobby");
        });

        // 게임 시작을 위해 Scene 전환
        data_hub.Socket.OnUnityThread("everyone_move_to_board", (res) =>
        {
            data_hub.Is_in_room = false;
            SceneManager.LoadScene("Board");
        });
    }
}

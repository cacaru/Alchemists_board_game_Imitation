using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using Self_Converter;
using Alchemists_data;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lobby_Control : MonoBehaviour
{
    private Data_Hub data_hub;

    // 서버 연결 이후 페이지 이동
    public GameObject lobby;
    public GameObject create_room;

    // 방 만들기
    public TMP_InputField room_name_obj;
    public TMP_InputField room_pw_obj;
    public TMP_Text room_count_obj;
    public TMP_Text announce_in_create_room;

    // 방 목록 받는 변수
    private Dictionary<int, Room_List> room_list; // name, count, max_count  // count : 현재 인원수
    public GameObject room_list_prefabs;
    public GameObject room_parents;

    // 방 비밀번호 입력창
    public GameObject pw_input_modal;
    public TMP_InputField pw_input_field;
    public GameObject pw_input_alert_modal;


    private Regex nick_name_regex = new Regex(@"^([A-za-z0-9ㄱ-ㅎ가-힣][^`[]+){1,10}$");

    // Use this for initialization
    void Start()
    {
        data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();
        Watch_socket();
        data_hub.Socket.Emit("enter_room_gate", data_hub.My_name);
    }

    // Update is called once per frame
    void Update()
    {

    }


    // 방만들기 페이지로 이동
    public void Move_create_room()
    {
        lobby.SetActive(false);
        create_room.SetActive(true);
    }

    // 로비로 돌아가기
    public void Move_lobby()
    {
        create_room.SetActive(false);
        lobby.SetActive(true);
    }

    /// <summary>
    ///  방 만들기
    /// </summary>
    public void Create_alche_room()
    {
        Regex pw_regex = new Regex(@"^([A-za-z0-9ㄱ-ㅎ가-힣][^`[]+){0,10}$");
        bool room_name_check = nick_name_regex.IsMatch(room_name_obj.text);
        bool room_pw_check = pw_regex.IsMatch(room_pw_obj.text);
        int count = int.Parse(room_count_obj.text.Replace("인", ""));

        data_hub.Max_count = count;

        if (room_name_check && room_pw_check)
        {
            // 필요 정보 가공 및 데이터 저장
            data_hub.Room_name = room_name_obj.text;
            //Debug.Log(data_hub.Room_name);
            data_hub.Im_master = true;
            data_hub.Room_pw = room_pw_obj.text;

            // 방 이름/ 비밀번호 inputfield 텍스트 초기화
            room_name_obj.text = "";
            room_pw_obj.text = "";

            // 방 이름/ 비밀번호 오류 안내창 초기화
            announce_in_create_room.text = "형식 안내\\n\\n이름과 비밀번호는\\n특수문자를 제외한\\n숫자,영어,한글 조합으로\\n최대 10자로\\n작성해주세요.";

            // 방에 입장함을 알리며 page를 방페이지로 이동
            SceneManager.LoadScene("Room_page");
        }
        else
        {
            if (!room_name_check && room_pw_check)
            {
                announce_in_create_room.text = "방 이름 형식이\n 잘못되었습니다.";
            }
            else if (!room_name_check && !room_pw_check)
            {
                announce_in_create_room.text = "방 이름과 비밀번호 형식이\n 잘못되었습니다.";
            }
            else if (room_name_check && !room_pw_check)
            {
                announce_in_create_room.text = "방 비밀번호 형식이\n 잘못되었습니다.";
            }
            return;
        }
    }

    /// <summary>
    /// 방 입장시 비밀번호 입력창 띄우기
    /// </summary>
    /// <param name="room_name"></param>
    public void show_pw_area(string room_name)
    {
        pw_input_modal.transform.Find("room_name").GetComponent<TMP_Text>().text = room_name;
        pw_input_modal.gameObject.SetActive(true);
    }

    // pw 입력창에서 나가기
    public void out_pw_area()
    {
        pw_input_field.text = "";
        pw_input_modal.transform.Find("room_name").GetComponent<TMP_Text>().text = "";
        pw_input_modal.gameObject.SetActive(false);
    }

    // 방 입장하기
    public void enter_room()
    {
        string room_name = pw_input_modal.transform.Find("room_name").GetComponent<TMP_Text>().text;
        string room_pw = pw_input_field.text;

        /*      
         *      Debug.Log(room_name);
                Debug.Log(room_pw);
         */
        // 서버와 통신
        // 비밀번호가 맞는지 확인해야함
        // send_data :: room_name, room_pw
        Alc_Data send_data = new Alc_Builder().Room_name(room_name)
                                              .Room_pw(room_pw)
                                              .Simple_Room_Data_Build();

        data_hub.Socket.Emit("check_pw", send_data);
    }


    // socket on 모아놓는 함수
    private void Watch_socket()
    {
        
        // 로비에 들어갔을 때 로비에 나와있는 방 정보 받기
        // data :: room_list -> { [name, count, max_count],[name, count, max_count] , *** }
        data_hub.Socket.OnUnityThread("enter_room_success", (data) =>
        {
            if (data.ToString().Length > 4)
                room_list = StoD_converter.Resp_to_Room_list(data);
            else
                room_list = null;

            // 방 목록 오브젝트 전부 제거
            if (room_parents != null)
            {
                Transform[] child_list = room_parents.GetComponentsInChildren<Transform>();
                if (child_list != null)
                {
                    for (int i = 1; i < child_list.Length; i++)
                    {
                        if (child_list[i] != room_parents.transform)
                        {
                            Destroy(child_list[i].gameObject);
                        }
                    }
                }

                // 오브젝트 추가
                if (room_list != null)
                {
                    for (int i = 0; i < room_list.Count; i++)
                    {
                        // 방 이름 임시 저장
                        string room_name = room_list[i].Room_name;
                        // 방 목록 생성 시작
                        GameObject temp = Instantiate(room_list_prefabs, new Vector3(0, 0, 0), Quaternion.identity);
                        temp.transform.SetParent(room_parents.transform, false);
                        temp.transform.Find("title").GetComponent<TMP_Text>().text = room_name;
                        temp.transform.Find("room_people").GetComponent<TMP_Text>().text = room_list[i].Count + "/" + room_list[i].Max_count;
                        //Debug.Log(temp.transform.Find("title").GetComponent<TMP_Text>().text);

                        // 버튼에 이벤트 추가
                        temp.transform.Find("enter_room_btn").gameObject.GetComponent<Button>().onClick.AddListener(() => this.show_pw_area(room_name));
                    }
                }
            }

        });


        // 방 입장시 비밀번호가 옳았을 때 돌아올 함수
        data_hub.Socket.OnUnityThread("ok_pw", (data) =>
        {
            // enter 함수를 emit 하면서 방 페이지로 이동해야함
            // emit에 필요한 변수
            // user_name, room_name, is_master=false,is_ready=false,msg=user_name+"가 방에 참가하였습니다 - by unity"
            data_hub.Room_name = pw_input_modal.transform.Find("room_name").GetComponent<TMP_Text>().text;
            // max_count 저장
            for (int i = 0; i < room_list.Count; i++)
            {
                if (room_list[i].Room_name.Equals(data_hub.Room_name))
                {
                    data_hub.Max_count = room_list[i].Max_count;
                    break;
                }
            }
            data_hub.Im_master = false;

            // 기존 방 페이지의 pw입력창과 입력된 값 초기화
            pw_input_alert_modal.SetActive(false);
            pw_input_field.text = "";
            pw_input_modal.SetActive(false);

            // 방에 입장함을 알리며 page를 방페이지로 이동
            SceneManager.LoadScene("Room_page");
        });

        // 방 입장시 비밀번호가 틀릴 때 돌아올 함수
        data_hub.Socket.OnUnityThread("wrong_pw", (data) =>
        {
            // 비밀번호 틀림 안내창 띄우기
            pw_input_alert_modal.SetActive(true);
        });
    }

}

using Alchemists_data;
using Self_Converter;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Socket_in_End : MonoBehaviour
{
    private Data_Hub data_hub;
    // Use this for initialization

    private bool want_restart = false;
    void Start()
    {
        data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();
        // 마지막 페이지의 오브젝트들 넣기
        gameObject.GetComponent<End_Page_Object_Controller>().Draw_end_page(data_hub.Game_result_data);
        Watch_socket();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 다시하기 버튼
    // 다시하기 취소 버튼
    // 방 나가기 버튼
    // 모든 버튼의 emit 데이터는 아래와 동일
    // data :: room_name, user_key,
    public void Out_game()
    {
        Alc_Data send_data = new Alc_Builder().User_key(data_hub.My_key)
                                              .Room_name(data_hub.Room_name)
                                              .Common_Data_Build();

        data_hub.Socket.Emit("out_game", send_data);

        // 게임에서 나가는 것이므로 first_page로 이동
        SceneManager.LoadScene("Lobby");
    }

    public void Want_restart()
    {
        if (!want_restart)
        {
            // 다시 하고 싶다는 의사 전달 
            // 추후에 인원이 다 안차더라도 게임 로비로 이동됨
            // 로비로 이동될 떄 게임방 주인은 순위 가장 높은 사람으로 선정
            Alc_Data send_data = new Alc_Builder().User_key(data_hub.My_key)
                                          .Room_name(data_hub.Room_name)
                                          .Common_Data_Build();

            data_hub.Socket.Emit("restart_checking", send_data);

            GameObject.Find("Fix_Sect").transform.Find("restart_text").gameObject.GetComponent<TMP_Text>().text = "취소 하기";
            want_restart = true;
        }
        else
        {
            // 다시하기 취소
            Alc_Data send_data = new Alc_Builder().User_key(data_hub.My_key)
                                          .Room_name(data_hub.Room_name)
                                          .Common_Data_Build();

            data_hub.Socket.Emit("restart_cancel", send_data);
            GameObject.Find("Fix_Sect").transform.Find("restart_text").gameObject.GetComponent<TMP_Text>().text = "다시 하기";
            want_restart = false;
        }
    }
    
    // socket on 모음
    private void Watch_socket()
    {
        data_hub.Socket.OnUnityThread("restart_check", (resp) => {
            // resp == [[1]]
            GameObject.Find("Fix_Sect").transform.Find("Restart_want_Sect").Find("restart_user_count").GetComponent<TMP_Text>().text = resp.ToString().Replace("[", "").Replace("]", "");
        });

        data_hub.Socket.OnUnityThread("restart_cancel_check", (resp) => {
            // resp == [[1]]
            GameObject.Find("Fix_Sect").transform.Find("Restart_want_Sect").Find("restart_user_count").GetComponent<TMP_Text>().text = resp.ToString().Replace("[", "").Replace("]", "");
        });

        data_hub.Socket.OnUnityThread("cant_restart", (resp) => {
            Alc_Data send_data = new Alc_Builder().Room_name(data_hub.Room_name)
                                                  .Simple_Room_Data_Build();

            data_hub.Socket.Emit("lobby_move", send_data);
            // lobby로 이동
            SceneManager.LoadScene("Lobby");
        });

        data_hub.Socket.OnUnityThread("restart", (resp) => {
            /*
              room_pw : room_data[data.room_name].left_game_result_data.room_pw,
              count : room_data[data.room_name].left_game_result_data.count,
              master_key : room_data[data.room_name].left_game[0],
            [{"room_pw":"","count":1,"master_key":"123123123"}]
             */
            // 정보를 받고 
            string[] list = resp.ToString().Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"", "").Split(",");
            data_hub.Room_pw = list[0].Split(":")[1];
            data_hub.Max_count = int.Parse(list[1].Split(":")[1]);
            //Debug.Log(list[1].Split(":")[1]);
            //Debug.Log(data_hub.Max_count);

            string master_key = list[2].Split(":")[1];
            if (data_hub.My_key.Equals(master_key))
            {
                data_hub.Im_master = true;
            }
            else
            {
                data_hub.Im_master = false;
            }

            Alc_Data send_data = new Alc_Builder().Room_name(data_hub.Room_name)
                                                  .Simple_Room_Data_Build();

            data_hub.Socket.Emit("lobby_move", send_data);

            // room_page로 이동
            SceneManager.LoadScene("Room_page");
        });

    }

}

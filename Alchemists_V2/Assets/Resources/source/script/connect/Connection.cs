using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using Self_Converter;
using UnityEngine.SceneManagement;

public class Connection : MonoBehaviour
{
    // 모든 scene에서 사용될 변수 객체에 접근
    private Data_Hub data_hub;

    #region first_page 안에서만 접근해야함

    // connect_ip
    public TMP_InputField connect_ip;
    public TMP_InputField nick_name;
    public GameObject annouce_board;
    public TMP_Text annoucne;
    public GameObject loading_panel; // 서버 연결 대기 

    #endregion

    // board 안에서 사용될 변수
    // 큐브로 순서 선택부터 시작해야함

    // 특수문자 제외한 이름 받을 정규식
    private Regex nick_name_regex = new Regex(@"^([A-za-z0-9ㄱ-ㅎ가-힣][^`[]+){1,10}$");

    private void Start()
    {
        data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void disconnect_socket()
    {
        data_hub.Socket.Disconnect();
        Debug.Log(data_hub.Socket.Disconnected);
        loading_panel.SetActive(false);
    }

    /// <summary>
    /// 연결 시작
    /// </summary>
    public void Connect_start()
    {
        // ip 정규식
        Regex ip_regex = new Regex(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

        // 지금 받아져 있는 값
        string ip_check = connect_ip.text;
        string nick_check = nick_name.text;

        /*
         * Debug.Log(ip_regex.IsMatch(checking_text));
         * Debug.Log(nick_name_regex.IsMatch(nick_name_text));
         */
        bool ip_correct = ip_regex.IsMatch(ip_check);
        bool nick_correct = nick_name_regex.IsMatch(nick_check);

        if (ip_correct && nick_correct)
        {
            // ip 연결
            var uri = new Uri("http://" + ip_check + ":3000");
            data_hub.Socket = new SocketIOUnity(uri);

            data_hub.Socket.Connect();

            // 서버 연결중 화면을 띄울 부분
            // 연결중 image 내부 요소의 취소하기 버튼을 누르면
            // socket변수를 초기화 -> disconnect, 후 new SocketIOUnity('')? null로 초기와 어떻게하지?

            if (!data_hub.Socket.Connected)
            {
                loading_panel.SetActive(true);
            }
            data_hub.My_name = nick_name.text;
            Connecting();
        }
        else
        {
            if (!ip_correct && nick_correct)
            {
                annoucne.text = "아이피 형식이\n 잘못되었습니다.";
            }
            else if (!ip_correct && !nick_correct)
            {
                annoucne.text = "아이피와 이름 형식이\n 잘못되었습니다.";
            }
            else if (ip_correct && !nick_correct)
            {
                annoucne.text = "이름 형식이\n 잘못되었습니다.";
            }
            annouce_board.SetActive(true);
            return;
        }
    }

    private void Connecting()
    {
        // 최초에 서버에 연결이 성공되면 받을 함수
        // res : trash data
        data_hub.Socket.OnUnityThread("Connect_unity", (res) =>
        {
            //Debug.Log(res);
            // 서버에 연결 되었으면 연결 중 image 끔
            //
            //Debug.Log(data_hub.Socket.Id);
            if (data_hub.Socket.Id != null)
            {
                data_hub.My_key = data_hub.Socket.Id;
            }

            // 로비로 이동
            SceneManager.LoadScene("Lobby");
        });
    }

    public void test()
    {
        string str = "{1:{name:qwe,score:28,grade:1},2:{name:ㅂㅈㄷ,score:28,grade:1},3:{name:,score:,grade:},4:{name:,score:,grade:}}";

        StoD_converter.Resp_to_Game_Result_Data(str);
        
    }

}

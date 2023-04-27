using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using Self_Converter;
using UnityEngine.SceneManagement;

public class Connection : MonoBehaviour
{
    // ��� scene���� ���� ���� ��ü�� ����
    private Data_Hub data_hub;

    #region first_page �ȿ����� �����ؾ���

    // connect_ip
    public TMP_InputField connect_ip;
    public TMP_InputField nick_name;
    public GameObject annouce_board;
    public TMP_Text annoucne;
    public GameObject loading_panel; // ���� ���� ��� 

    #endregion

    // board �ȿ��� ���� ����
    // ť��� ���� ���ú��� �����ؾ���

    // Ư������ ������ �̸� ���� ���Խ�
    private Regex nick_name_regex = new Regex(@"^([A-za-z0-9��-����-�R][^`[]+){1,10}$");

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
    /// ���� ����
    /// </summary>
    public void Connect_start()
    {
        // ip ���Խ�
        Regex ip_regex = new Regex(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

        // ���� �޾��� �ִ� ��
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
            // ip ����
            var uri = new Uri("http://" + ip_check + ":3000");
            data_hub.Socket = new SocketIOUnity(uri);

            data_hub.Socket.Connect();

            // ���� ������ ȭ���� ��� �κ�
            // ������ image ���� ����� ����ϱ� ��ư�� ������
            // socket������ �ʱ�ȭ -> disconnect, �� new SocketIOUnity('')? null�� �ʱ�� �������?

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
                annoucne.text = "������ ������\n �߸��Ǿ����ϴ�.";
            }
            else if (!ip_correct && !nick_correct)
            {
                annoucne.text = "�����ǿ� �̸� ������\n �߸��Ǿ����ϴ�.";
            }
            else if (ip_correct && !nick_correct)
            {
                annoucne.text = "�̸� ������\n �߸��Ǿ����ϴ�.";
            }
            annouce_board.SetActive(true);
            return;
        }
    }

    private void Connecting()
    {
        // ���ʿ� ������ ������ �����Ǹ� ���� �Լ�
        // res : trash data
        data_hub.Socket.OnUnityThread("Connect_unity", (res) =>
        {
            //Debug.Log(res);
            // ������ ���� �Ǿ����� ���� �� image ��
            //
            //Debug.Log(data_hub.Socket.Id);
            if (data_hub.Socket.Id != null)
            {
                data_hub.My_key = data_hub.Socket.Id;
            }

            // �κ�� �̵�
            SceneManager.LoadScene("Lobby");
        });
    }

    public void test()
    {
        string str = "{1:{name:qwe,score:28,grade:1},2:{name:������,score:28,grade:1},3:{name:,score:,grade:},4:{name:,score:,grade:}}";

        StoD_converter.Resp_to_Game_Result_Data(str);
        
    }

}

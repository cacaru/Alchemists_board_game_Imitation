using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using Self_Converter;
using UnityEngine.SceneManagement;

public class Connection : MonoBehaviour
{
    // ¸ðµç scene¿¡¼­ »ç¿ëµÉ º¯¼ö °´Ã¼¿¡ Á¢±Ù
    private Data_Hub data_hub;

    #region first_page ¾È¿¡¼­¸¸ Á¢±ÙÇØ¾ßÇÔ

    // connect_ip
    public TMP_InputField connect_ip;
    public TMP_InputField nick_name;
    public GameObject annouce_board;
    public TMP_Text annoucne;
    public GameObject loading_panel; // ¼­¹ö ¿¬°á ´ë±â 

    #endregion

    // board ¾È¿¡¼­ »ç¿ëµÉ º¯¼ö
    // Å¥ºê·Î ¼ø¼­ ¼±ÅÃºÎÅÍ ½ÃÀÛÇØ¾ßÇÔ

    // Æ¯¼ö¹®ÀÚ Á¦¿ÜÇÑ ÀÌ¸§ ¹ÞÀ» Á¤±Ô½Ä
    private Regex nick_name_regex = new Regex(@"^([A-za-z0-9¤¡-¤¾°¡-ÆR][^`[]+){1,10}$");

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
    /// ¿¬°á ½ÃÀÛ
    /// </summary>
    public void Connect_start()
    {
        // ip Á¤±Ô½Ä
        Regex ip_regex = new Regex(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

        // Áö±Ý ¹Þ¾ÆÁ® ÀÖ´Â °ª
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
            // ip ¿¬°á
            var uri = new Uri("http://" + ip_check + ":3000");
            data_hub.Socket = new SocketIOUnity(uri);

            data_hub.Socket.Connect();

            // ¼­¹ö ¿¬°áÁß È­¸éÀ» ¶ç¿ï ºÎºÐ
            // ¿¬°áÁß image ³»ºÎ ¿ä¼ÒÀÇ Ãë¼ÒÇÏ±â ¹öÆ°À» ´©¸£¸é
            // socketº¯¼ö¸¦ ÃÊ±âÈ­ -> disconnect, ÈÄ new SocketIOUnity('')? null·Î ÃÊ±â¿Í ¾î¶»°ÔÇÏÁö?

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
                annoucne.text = "¾ÆÀÌÇÇ Çü½ÄÀÌ\n Àß¸øµÇ¾ú½À´Ï´Ù.";
            }
            else if (!ip_correct && !nick_correct)
            {
                annoucne.text = "¾ÆÀÌÇÇ¿Í ÀÌ¸§ Çü½ÄÀÌ\n Àß¸øµÇ¾ú½À´Ï´Ù.";
            }
            else if (ip_correct && !nick_correct)
            {
                annoucne.text = "ÀÌ¸§ Çü½ÄÀÌ\n Àß¸øµÇ¾ú½À´Ï´Ù.";
            }
            annouce_board.SetActive(true);
            return;
        }
    }

    private void Connecting()
    {
        // ÃÖÃÊ¿¡ ¼­¹ö¿¡ ¿¬°áÀÌ ¼º°øµÇ¸é ¹ÞÀ» ÇÔ¼ö
        // res : trash data
        data_hub.Socket.OnUnityThread("Connect_unity", (res) =>
        {
            //Debug.Log(res);
            // ¼­¹ö¿¡ ¿¬°á µÇ¾úÀ¸¸é ¿¬°á Áß image ²û
            //
            //Debug.Log(data_hub.Socket.Id);
            if (data_hub.Socket.Id != null)
            {
                data_hub.My_key = data_hub.Socket.Id;
            }

            // ·Îºñ·Î ÀÌµ¿
            SceneManager.LoadScene("Lobby");
        });
    }

    public void test()
    {
        string str = "{1:{name:qwe,score:28,grade:1},2:{name:¤²¤¸¤§,score:28,grade:1},3:{name:,score:,grade:},4:{name:,score:,grade:}}";

        StoD_converter.Resp_to_Game_Result_Data(str);
        
    }

}

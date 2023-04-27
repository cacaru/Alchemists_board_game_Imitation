using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Favor_Card_Use_Window_Control : MonoBehaviour
{
    public GameObject favor_use_window;
    public GameObject effect;

    public GameObject ingre_sect;
    public GameObject board_num_sect;

    private Data_Hub data_hub;
    // Start is called before the first frame update
    void Start()
    {
        data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Using_window_close()
    {
        // ���� ������� �Ⱥ��̰� �ϱ�
        ingre_sect.SetActive(false);
        board_num_sect.SetActive(false);
        favor_use_window.transform.Find("Use").gameObject.SetActive(false);

        // ��� ���� ����
        ingre_sect.transform.Find("card_1").gameObject.SetActive(true);
        ingre_sect.transform.Find("card_2").gameObject.SetActive(true);
        ingre_sect.transform.Find("card_3").gameObject.SetActive(true);
        ingre_sect.transform.Find("card_4").gameObject.SetActive(true);
        ingre_sect.transform.Find("card_5").gameObject.SetActive(true);
        ingre_sect.transform.Find("card_6").gameObject.SetActive(true);
        ingre_sect.transform.Find("card_7").gameObject.SetActive(true);
        ingre_sect.transform.Find("card_8").gameObject.SetActive(true);

        // ������ �Ⱥ��̰�
        favor_use_window.SetActive(false);
    }

    public void Using_window_open(string name)
    {
        data_hub.Favor_card_name = name;

        // ����ϱ� ��ư Ȱ��ȭ ���� üũ �� ���� ����
        switch (name)
        {
            case "assistent":
                effect.GetComponent<TMP_Text>().text = "�̹� ���忡 �ൿť�� 1���� �߰��˴ϴ�.";
                if (data_hub.My_data.User_ingame_data.Favor_card.Assistent > 0)
                {
                    favor_use_window.transform.Find("Use").gameObject.SetActive(true);
                }
                break;
            case "bar_owner":
                effect.GetComponent<TMP_Text>().text = "�뺴���� ���� �Ǹ� �ൿ���� ��Ȯ�� ��ġ�� ������ ����� ���� 1�� �߰��� ����ϴ�.\n��ġ���� �ʾƵ� �Ǹſ� �����Ѵٸ� ��ȭ�� 1�� �� �޽��ϴ�.";
                if (data_hub.My_data.User_ingame_data.Favor_card.Bar_owner > 0)
                {
                    favor_use_window.transform.Find("Use").gameObject.SetActive(true);
                }
                break;
            case "big_man":
                effect.GetComponent<TMP_Text>().text = "�ൿĭ�� �ϳ� �����ؼ� �ڽ��� ť�긦 ���� �� �ٷ� �ű�ϴ�.";
                board_num_sect.SetActive(true);
                if (data_hub.My_data.User_ingame_data.Favor_card.Big_man > 0)
                {
                    favor_use_window.transform.Find("Use").gameObject.SetActive(true);
                }
                break;
            case "caretaker":
                effect.GetComponent<TMP_Text>().text = "�̹� ���忡 �뺴���� ���� �Ǹ� ��, �ڽſ��� ���� ���� �ൿ�� �� �� �ֽ��ϴ�.";
                if (data_hub.My_data.User_ingame_data.Favor_card.Caretaker > 0)
                {
                    favor_use_window.transform.Find("Use").gameObject.SetActive(true);
                }
                break;
            case "herbalist":
                effect.GetComponent<TMP_Text>().text = "���ī�� 3���� �ް�, 2���� �����ϴ�.";
                if (data_hub.My_data.User_ingame_data.Favor_card.Herbalist > 0)
                {
                    favor_use_window.transform.Find("Use").gameObject.SetActive(true);
                }
                // �ִ� ����� �����ϹǷ� �ִ� ��� ��ϸ� true ������ false
                if (data_hub.My_data.User_ingame_data.Ingredient.Card_1 > 0)
                    ingre_sect.transform.Find("card_1").gameObject.SetActive(true);
                if (data_hub.My_data.User_ingame_data.Ingredient.Card_2 > 0)
                    ingre_sect.transform.Find("card_2").gameObject.SetActive(true);
                if (data_hub.My_data.User_ingame_data.Ingredient.Card_3 > 0)
                    ingre_sect.transform.Find("card_3").gameObject.SetActive(true);
                if (data_hub.My_data.User_ingame_data.Ingredient.Card_4 > 0)
                    ingre_sect.transform.Find("card_4").gameObject.SetActive(true);
                if (data_hub.My_data.User_ingame_data.Ingredient.Card_5 > 0)
                    ingre_sect.transform.Find("card_5").gameObject.SetActive(true);
                if (data_hub.My_data.User_ingame_data.Ingredient.Card_6 > 0)
                    ingre_sect.transform.Find("card_6").gameObject.SetActive(true);
                if (data_hub.My_data.User_ingame_data.Ingredient.Card_7 > 0)
                    ingre_sect.transform.Find("card_7").gameObject.SetActive(true);
                if (data_hub.My_data.User_ingame_data.Ingredient.Card_8 > 0)
                    ingre_sect.transform.Find("card_8").gameObject.SetActive(true);

                ingre_sect.SetActive(true);
                break;
            case "merchant":
                effect.GetComponent<TMP_Text>().text = "�ڽ��� �뺴���� ���� �Ǹ� �ൿ ������ ù ������� �Ǹ� ������ ��ȭ 1���� �߰��� ����ϴ�.\nù ������ �ƴ϶� �Ǹ� ������ 3���� ���� �� ���ϴ� ���� ������ �� �ֽ��ϴ�.";
                if (data_hub.My_data.User_ingame_data.Favor_card.Merchant > 0)
                {
                    favor_use_window.transform.Find("Use").gameObject.SetActive(true);
                }
                break;
            case "shopkeeper":
                effect.GetComponent<TMP_Text>().text = "�������� �� �� ��ȭ 1���� ���� ���ϴ�.";
                if (data_hub.My_data.User_ingame_data.Favor_card.Shopkeeper > 0)
                {
                    favor_use_window.transform.Find("Use").gameObject.SetActive(true);
                }
                break;
            case "wise_man":
                effect.GetComponent<TMP_Text>().text = "��Ḧ �Ǹ��� ��, ��ȭ 1���� �߰��� ����ϴ�.";
                if (data_hub.My_data.User_ingame_data.Favor_card.Wise_man > 0)
                {
                    favor_use_window.transform.Find("Use").gameObject.SetActive(true);
                }
                break;
        }

        favor_use_window.SetActive(true);

    }
}

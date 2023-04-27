using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Result_Sect_Open : MonoBehaviour
{
    private Vector3 ori_pos;
    private Vector3 pos;

    private float speed = 6f;

    private bool off = false;
    private bool end = true;
    // Use this for initialization
    void Start()
    {
        ori_pos = gameObject.transform.localPosition;
        pos = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Open()
    {
        StartCoroutine(Shower());
    }

    IEnumerator Shower()
    {
        while (end)
        {
            if (!off)
            {
                if (pos.x < -810f)
                {
                    pos.x += speed;
                    gameObject.transform.localPosition = pos;
                    yield return new WaitForFixedUpdate();
                }

                else if (pos.x >= -810f)
                {
                    pos.x = -810f;
                    gameObject.transform.localPosition = pos;
                    yield return new WaitForSeconds(3f);
                    off = true;
                }
            }
            else
            {
                if (pos.x > -1120f)
                {
                    pos.x -= speed;
                    gameObject.transform.localPosition = pos;
                    yield return new WaitForFixedUpdate();
                }
                else if (pos.x <= ori_pos.x)
                {
                    pos.x = ori_pos.x;
                    gameObject.transform.localPosition = pos;
                    Init();
                    break;
                }
            }
        }
        yield break;
    }

    public void Init()
    {
        // 보통은 반드시 모든 값이 바뀌므로 끄기만 하면 됨
        gameObject.transform.Find("Normal_Form").gameObject.SetActive(false);

        // 반박은 stamp 자료를 초기화 해줘야 데이터가 겹칠 일이 없음
        gameObject.transform.Find("Refute_Form").gameObject.SetActive(false);
        gameObject.transform.Find("Refute_Form").Find("Success_Area").gameObject.SetActive(false);
        gameObject.transform.Find("Refute_Form").Find("Success_Area").Find("Stamp_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_gray");
        gameObject.transform.Find("Refute_Form").Find("Success_Area").Find("Stamp_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_gray");
        gameObject.transform.Find("Refute_Form").Find("Success_Area").Find("Stamp_3").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_gray");

        // 사용 변수 초기화
        off = false;
    }
}

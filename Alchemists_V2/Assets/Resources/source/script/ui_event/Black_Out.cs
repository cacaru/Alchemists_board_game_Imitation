using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Black_Out : MonoBehaviour
{
    public Image fade_image;
    public float speed = 4f;

    private bool fade_start = false;
    private bool fade_ending = false;

    private bool to_board = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // fade out => 검은화면화
        if (fade_start)
        {
            Vector3 tmp_sc = fade_image.transform.localScale;
            if (tmp_sc.x > 26f)
            {
                // 카메라 변경
                if (to_board)
                    GameObject.Find("Camera_Controller").GetComponent<Camera_Controller>().On_sub();
                
                else
                    GameObject.Find("Camera_Controller").GetComponent<Camera_Controller>().On_main();

                // fade
                fade_start = false;
                fade_ending = true;
                return;
            }

            tmp_sc.x += speed; tmp_sc.y += speed; tmp_sc.z += speed;
            fade_image.transform.localScale = tmp_sc;
        }

        // 다시 scale 0으로
        if (fade_ending)
        {
            Vector3 tmp_sc = fade_image.transform.localScale;
            if (tmp_sc.x == 0f || tmp_sc.x < 0f)
            {
                fade_start = false;
                fade_ending = false;

                GameObject.Find("Switch_Area").transform.Find("Fade_Image").gameObject.SetActive(false);
            }

            tmp_sc.x -= speed; tmp_sc.y -= speed; tmp_sc.z -= speed;
            fade_image.transform.localScale = tmp_sc;
        }
    }

    public void Start_fade(bool to_board)
    {
        GameObject.Find("Switch_Area").transform.Find("Fade_Image").gameObject.SetActive(true);
        this.to_board = to_board;
        fade_start = true;
    }
}

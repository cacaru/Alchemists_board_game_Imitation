using UnityEngine;
using UnityEngine.UI;

public class Btn_cube_select : MonoBehaviour
{

    public GameObject btn_area;
    public Image btn_img;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void select_color(string color)
    {
        if(!btn_area.activeSelf)
        {
            btn_area.SetActive(true);
            btn_img.gameObject.SetActive(false);
            btn_img.sprite = null;
        }
        else
        {
            btn_area.SetActive(false);

            switch (color)
            {
                case "black":
                    btn_img.sprite = Resources.Load<Sprite>("source/img/icon/cube_black") as Sprite;
                    break;
                case "blue":
                    btn_img.sprite = Resources.Load<Sprite>("source/img/icon/cube_blue") as Sprite;
                    break;
                case "red":
                    btn_img.sprite = Resources.Load<Sprite>("source/img/icon/cube_red") as Sprite;
                    break;
                case "white":
                    btn_img.sprite = Resources.Load<Sprite>("source/img/icon/cube_white") as Sprite;
                    break;
            }
            btn_img.gameObject.SetActive(true);
        }
        
    }

    public void Show_btn_area()
    {
        btn_area.SetActive(true);
        btn_img.gameObject.SetActive(false);
    }
}

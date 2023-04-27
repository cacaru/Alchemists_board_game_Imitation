using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mouse_Value_Changer : MonoBehaviour
{
    public Slider rotate_changer;
    public Slider speed_changer;

    public TMP_Text rotate_text;
    public TMP_Text speed_text;

    public GameObject camera_rotating;
    public GameObject camera_moving;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotate_text.text = rotate_changer.value.ToString();
        speed_text.text = speed_changer.value.ToString();
    }

    public void Rotate_change()
    {
        camera_rotating.GetComponent<Camera_Rotating>().camera_speed = rotate_changer.value;
    }

    public void Speed_change()
    {
        camera_moving.GetComponent<Camera_Moving>().move_speed = speed_changer.value;
    }
}

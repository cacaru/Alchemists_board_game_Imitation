using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_Controller : MonoBehaviour
{
    public GameObject option_window;

    private bool option_open_checker = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (option_open_checker)
            {
                close_option();
                option_open_checker = false;
            }
            else
            {
                open_option();
                option_open_checker = true;
            }
        }
    }

    private void open_option()
    {
        option_window.SetActive(true);
    }

    public void close_option()
    {
        option_window.SetActive(false);
    }
}

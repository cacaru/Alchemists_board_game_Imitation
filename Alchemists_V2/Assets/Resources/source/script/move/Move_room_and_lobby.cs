using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_room_and_lobby : MonoBehaviour
{
    public GameObject �κ�;
    public GameObject �游���;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void back_to_lobby()
    {
        �游���.SetActive(false);
        �κ�.SetActive(true);
    }

    public void create_room()
    {
        �κ�.SetActive(false);
        �游���.SetActive(true);
    }
}

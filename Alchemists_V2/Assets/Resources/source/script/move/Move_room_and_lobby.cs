using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_room_and_lobby : MonoBehaviour
{
    public GameObject 로비;
    public GameObject 방만들기;

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
        방만들기.SetActive(false);
        로비.SetActive(true);
    }

    public void create_room()
    {
        로비.SetActive(false);
        방만들기.SetActive(true);
    }
}

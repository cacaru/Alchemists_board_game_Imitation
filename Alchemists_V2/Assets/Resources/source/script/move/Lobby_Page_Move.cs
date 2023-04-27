using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby_Page_Move: MonoBehaviour
{
    public GameObject 로비;
    public GameObject 설명화면;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void move_to_front()
    {
        로비.gameObject.SetActive(true);
        설명화면.gameObject.SetActive(false);
    }

    public void move_to_explain()
    {
        로비.gameObject.SetActive(false);
        설명화면.gameObject.SetActive(true);
    }
}

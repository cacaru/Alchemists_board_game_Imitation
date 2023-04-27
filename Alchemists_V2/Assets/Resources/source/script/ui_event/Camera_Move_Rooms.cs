using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move_Rooms : MonoBehaviour
{
    private Transform now_pos;
    private Vector3 pos;

    public float speed = .07f;

    private bool move_to_front = false;
    private bool move_to_board = false;
    // Start is called before the first frame update
    void Start()
    {
        now_pos = this.transform;
        pos = now_pos.position;
    }
    // camera.x -5.15
    // camera.y 6.17
    // camera.z move 11 <-> 43
    // Update is called once per frame
    void Update()
    {
        if (move_to_board)
        {
            if (now_pos.position.z >= 43f)
            {
                pos.z = 43f;
                now_pos.position = pos;
                move_to_board = false;
                gameObject.GetComponent<Camera_Move_Rooms>().enabled = false;
                return;
            }
            pos.z += speed;
            now_pos.position = pos;
        }

        if (move_to_front)
        {
            if (now_pos.position.z <= 11f)
            {
                pos.z = 11f;
                now_pos.position = pos;
                move_to_front = false;
                gameObject.GetComponent<Camera_Move_Rooms>().enabled = false;
                return;
            }
            pos.z -= speed;
            now_pos.position = pos;
        }
    }

    public void Move_to_board() { move_to_board = true; }
    public void Move_to_front() { move_to_front = true; }
}

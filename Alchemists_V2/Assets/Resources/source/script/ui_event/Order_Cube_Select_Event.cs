using UnityEngine;

public class Order_Cube_Select_Event : MonoBehaviour
{
    // 큐브 색 변환
    private int my_color_code;                  // 색 번호에 맞게 색을 변환
    private int max_count;                      // 방의 최대 인원
    private Vector3 m_m_pos;                    // 현재 내 마우스 커서의 화면상 위치

    private GameObject pre_target;              // 지난 타겟
    private Color DEFAULT = new Color(82 / 255f, 82 / 255f, 82 / 255f, 1);

    // 접근해서 전송할 변수
    public int cube_num = 0;

    // 직접 접근해서 수정할 변수
    public int[] selected_round_order;
    void Start()
    {
        // my_color_code 받아오기
        my_color_code = GameObject.Find("Data_Hub").GetComponent<Data_Hub>().My_color_code;
        max_count = GameObject.Find("Data_Hub").GetComponent<Data_Hub>().Max_count;
    }

   void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            m_m_pos = Input.mousePosition;
            // 카메라에서 스크린에 마우스 클릭 위치를 통과하는 광선을 반환합니다.
            Ray ray = Camera.main.ScreenPointToRay(m_m_pos);
            RaycastHit hit;

            // 광선으로 충돌된 collider가 hit에 들어감
            if (Physics.Raycast(ray, out hit))
            {
                // 9번 큐브는 선택할수 없도록 막아야함
                if (hit.collider.name.Equals("Cube_9") || hit.collider.name.Equals("cube_9"))
                {
                    return;
                }

                GameObject _target_obj = null;
                Material _target;
                bool be_target = false;
                // 이름이 Cube_ or cube_ 로 시작하면 큐브 인것
                // Cube_로 시작하면 해당 오브젝트를 바로 색칠
                if (hit.collider.name.StartsWith("Cube_"))
                {
                    _target_obj = hit.collider.gameObject;
                    be_target = true;
                }
                   
                // cube_ 로 시작하면 Cube_ 로 변환한 오브젝트를 찾아 색칠
                if (hit.collider.name.StartsWith("cube_"))
                {
                    _target_obj = GameObject.Find(hit.collider.name.Replace("c", "C"));
                    be_target = true;
                }

                if (be_target)
                {
                    // selected_cube_order에 들어있는 번호들의 큐브들은 선택할 수 없게 막아야하므로 여기서 터뜨림
                    for(int i = 0; i < selected_round_order.Length; i++)
                    {
                        if (int.Parse(_target_obj.name.Substring(5, 1)) == selected_round_order[i])
                            return;
                        // max_count<=3 일때 7번 큐브는 8번 큐브와도 같음
                        if (max_count <= 3)
                        {
                            if (int.Parse(_target_obj.name.Substring(5, 1)) == 8 && selected_round_order[i] == 7)
                                return;
                        }
                    }

                    // target에 색을 입히는 과정
                    // 이전에 색을 입힌 target이 있었으면 색 해제
                    if (pre_target != null)
                    {
                        pre_target.GetComponent<Renderer>().material.color = DEFAULT;
                    }

                    // 이전에 색을 입힌 target이 지금과 같다면 if문 탈출
                    if (pre_target == _target_obj)
                    {
                        // 다음번 선택에서는 다시 색이 칠해질 수 있도록 초기화
                        pre_target = null;

                        // 다시 선택한 것이므로 번호도 초기화
                        cube_num = 0;
                        return;
                    }
                        
                    _target = _target_obj.GetComponent<Renderer>().material;
                    switch (my_color_code)
                    {
                        case 1:
                            _target.color = Color.red;
                            break;
                        case 2:
                            _target.color = Color.blue;
                            break;
                        case 3:
                            _target.color = Color.black;
                            break;
                        case 4:
                            _target.color = Color.white;
                            break;
                        default:
                            _target.color = Color.green;
                            break;
                    }

                    pre_target = _target_obj;
                    // 현재 선택된 큐브의 번호를 저장
                    cube_num = int.Parse(_target_obj.name.Replace("Cube_", ""));
                    //Debug.Log(cube_num);

                    // 누른 결과를 서버에 전송
                    GameObject.Find("Socket_obj").GetComponent<Socket_In_Board>().Round_order_cube_select(cube_num);
                }
            }

        }
       
    }

}
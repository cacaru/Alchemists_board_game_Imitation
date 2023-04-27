using UnityEngine;

public class Order_Cube_Select_Event : MonoBehaviour
{
    // ť�� �� ��ȯ
    private int my_color_code;                  // �� ��ȣ�� �°� ���� ��ȯ
    private int max_count;                      // ���� �ִ� �ο�
    private Vector3 m_m_pos;                    // ���� �� ���콺 Ŀ���� ȭ��� ��ġ

    private GameObject pre_target;              // ���� Ÿ��
    private Color DEFAULT = new Color(82 / 255f, 82 / 255f, 82 / 255f, 1);

    // �����ؼ� ������ ����
    public int cube_num = 0;

    // ���� �����ؼ� ������ ����
    public int[] selected_round_order;
    void Start()
    {
        // my_color_code �޾ƿ���
        my_color_code = GameObject.Find("Data_Hub").GetComponent<Data_Hub>().My_color_code;
        max_count = GameObject.Find("Data_Hub").GetComponent<Data_Hub>().Max_count;
    }

   void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            m_m_pos = Input.mousePosition;
            // ī�޶󿡼� ��ũ���� ���콺 Ŭ�� ��ġ�� ����ϴ� ������ ��ȯ�մϴ�.
            Ray ray = Camera.main.ScreenPointToRay(m_m_pos);
            RaycastHit hit;

            // �������� �浹�� collider�� hit�� ��
            if (Physics.Raycast(ray, out hit))
            {
                // 9�� ť��� �����Ҽ� ������ ���ƾ���
                if (hit.collider.name.Equals("Cube_9") || hit.collider.name.Equals("cube_9"))
                {
                    return;
                }

                GameObject _target_obj = null;
                Material _target;
                bool be_target = false;
                // �̸��� Cube_ or cube_ �� �����ϸ� ť�� �ΰ�
                // Cube_�� �����ϸ� �ش� ������Ʈ�� �ٷ� ��ĥ
                if (hit.collider.name.StartsWith("Cube_"))
                {
                    _target_obj = hit.collider.gameObject;
                    be_target = true;
                }
                   
                // cube_ �� �����ϸ� Cube_ �� ��ȯ�� ������Ʈ�� ã�� ��ĥ
                if (hit.collider.name.StartsWith("cube_"))
                {
                    _target_obj = GameObject.Find(hit.collider.name.Replace("c", "C"));
                    be_target = true;
                }

                if (be_target)
                {
                    // selected_cube_order�� ����ִ� ��ȣ���� ť����� ������ �� ���� ���ƾ��ϹǷ� ���⼭ �Ͷ߸�
                    for(int i = 0; i < selected_round_order.Length; i++)
                    {
                        if (int.Parse(_target_obj.name.Substring(5, 1)) == selected_round_order[i])
                            return;
                        // max_count<=3 �϶� 7�� ť��� 8�� ť��͵� ����
                        if (max_count <= 3)
                        {
                            if (int.Parse(_target_obj.name.Substring(5, 1)) == 8 && selected_round_order[i] == 7)
                                return;
                        }
                    }

                    // target�� ���� ������ ����
                    // ������ ���� ���� target�� �־����� �� ����
                    if (pre_target != null)
                    {
                        pre_target.GetComponent<Renderer>().material.color = DEFAULT;
                    }

                    // ������ ���� ���� target�� ���ݰ� ���ٸ� if�� Ż��
                    if (pre_target == _target_obj)
                    {
                        // ������ ���ÿ����� �ٽ� ���� ĥ���� �� �ֵ��� �ʱ�ȭ
                        pre_target = null;

                        // �ٽ� ������ ���̹Ƿ� ��ȣ�� �ʱ�ȭ
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
                    // ���� ���õ� ť���� ��ȣ�� ����
                    cube_num = int.Parse(_target_obj.name.Replace("Cube_", ""));
                    //Debug.Log(cube_num);

                    // ���� ����� ������ ����
                    GameObject.Find("Socket_obj").GetComponent<Socket_In_Board>().Round_order_cube_select(cube_num);
                }
            }

        }
       
    }

}
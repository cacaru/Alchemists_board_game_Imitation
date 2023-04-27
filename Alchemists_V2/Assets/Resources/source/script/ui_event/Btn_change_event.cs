using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Btn_change_event : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Sprite on_enter_img;
    public Sprite default_img;

    Image img;
    private Transform my_trans;

    public float rotate_angle = 0.125f;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        my_trans = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        my_trans.Rotate(new Vector3(0, 0, rotate_angle));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        img.sprite = on_enter_img;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.sprite = default_img;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        img.sprite = default_img;
    }
}

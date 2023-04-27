using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Windows : MonoBehaviour
{
    private Animator pre_ani;
    private Animator ani;
    //���ο� �˸�â ����
    public void Open_windows(GameObject wd)
    {
        if( pre_ani != null && wd.GetComponent<Animator>() != pre_ani )
        {
            Closing_pre();
        }
        else if( wd.GetComponent<Animator>() == pre_ani)
        {
            return;
        }

        ani = wd.GetComponent<Animator>();
        ani.SetBool("Open", true);
        pre_ani = ani;
    }

    // �ܺο��� ���� ���� ������ �ݱ� ���
    public void Close_windows(GameObject wd)
    {
        pre_ani = wd.GetComponent<Animator>();
        Closing_pre();
    }

    // �ݱ�
    private void Closing_pre()
    {
        pre_ani.SetBool("Open", false);
        StartCoroutine(Delay_deactive(pre_ani));
    }

    // ���� �ݴ°� ���������� object��ٸ���
    IEnumerator Delay_deactive(Animator pre_ani)
    {
        bool closed_end = false;
        
        while (!closed_end)
        {
            if (!pre_ani.IsInTransition(0))
                closed_end = pre_ani.GetCurrentAnimatorStateInfo(0).IsName("Closed");
            
            yield return new WaitForEndOfFrame();
        }

    }
}

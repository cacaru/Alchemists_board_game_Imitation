using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Side : MonoBehaviour
{
    private Animator ani;

    public void Toggle(GameObject menu)
    {
        ani = menu.GetComponent<Animator>();

        Playing_ani(menu);
    }

    private void Playing_ani(GameObject menu)
    {
        ani.SetBool("Open", !ani.GetBool("Open"));
        StartCoroutine(Delay_deactive());
    }

    IEnumerator Delay_deactive()
    {
        bool closed_end = false;
        bool closing = true;

        while (!closed_end && closing) 
        {
            if (!ani.IsInTransition(0))
                closed_end = ani.GetCurrentAnimatorStateInfo(0).IsName("Closed");

            yield return new WaitForEndOfFrame();
        }

    }
}

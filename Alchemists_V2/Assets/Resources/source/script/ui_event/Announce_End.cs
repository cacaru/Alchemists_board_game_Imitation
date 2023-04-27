using UnityEngine;

public class Announce_End : MonoBehaviour
{
    public GameObject announce;
    
    public void z_announce_end()
    {
        announce.SetActive(false);
    }
}

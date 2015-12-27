using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

    KTpoint blue;
    KTpoint red;

    void Start()
    {
        blue = GameObject.Find("KT1").GetComponent<KTpoint>();
        red = GameObject.Find("KT2").GetComponent<KTpoint>();

    }

   public void victory()
    {
        Time.timeScale = 0.2f;
    }
    public void defeat()
    {
        Time.timeScale = 0.2f;
    }

}

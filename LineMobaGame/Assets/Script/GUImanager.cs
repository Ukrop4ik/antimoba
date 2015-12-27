using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUImanager : MonoBehaviour {

    [SerializeField] private Button ADboost;
    [SerializeField] private Button ADboostPassive;
    [SerializeField] private Image BOT;
    [SerializeField] private Image MID;
    [SerializeField] private Image TOP;
    [SerializeField] private Text BOTtext;
    [SerializeField] private Text MIDtext;
    [SerializeField] private Text TOPtext;
    public Sprite[] sprites;
    [SerializeField] private GameObject[] KT;
    public float KT3timer = 10f;
    public float KT4timer = 10f;
    public float KT5timer = 10f;
    public float KT1timer = 10f;
    public float KT2timer = 10f;

    public GameObject[] units;

    public void UnitADBoost()
    {
        units = GameObject.FindGameObjectsWithTag("BlueMob");

        for (int i = 0; i < units.Length; i++)
        {
            units[i].GetComponent<UnitLogic>().ADboost();
            Debug.Log(units[i].name + " усилен!!");
        }
    }
    public void UnitADBoostPassive()
    {
        units = GameObject.FindGameObjectsWithTag("BlueMob");
        for (int i = 0; i < units.Length; i++)
        {
            units[i].GetComponent<UnitLogic>().DAMAGEboost += 0.2f;
            Debug.Log(units[i].name + " усилен навсегда!!");
        }
    }

    void Update()
    {

        if (KT[0].gameObject.GetComponent<KTpoint>().capture == true)
        {
            KT3timer -= Time.deltaTime;
            if (KT3timer <= 0)
            {
                KT3timer = 10f;
                KT[0].gameObject.GetComponent<KTpoint>().KTvisual();

            }
        }
        else if (KT[0].gameObject.GetComponent<KTpoint>().capture == false)
        {
            KT3timer += Time.deltaTime;
            if (KT3timer >= 10)
            {
                KT3timer = 10f;
            }
        }

        if (KT[1].gameObject.GetComponent<KTpoint>().capture == true)
        {
            KT4timer -= Time.deltaTime;
            if (KT4timer <= 0)
            {
                KT4timer = 10f;
                KT[1].gameObject.GetComponent<KTpoint>().KTvisual();
                
            }
        }
        else if (KT[1].gameObject.GetComponent<KTpoint>().capture == false)
        {
            KT4timer += Time.deltaTime;
            if (KT4timer >= 10)
            {
                KT4timer = 10f;
            }
        }

        if (KT[2].gameObject.GetComponent<KTpoint>().capture == true)
        {
            KT5timer -= Time.deltaTime;
            if (KT5timer <= 0)
            {
                KT5timer = 10f;
                KT[2].gameObject.GetComponent<KTpoint>().KTvisual();
                
            }
        }

        if (KT[2].gameObject.GetComponent<KTpoint>().capture == false)
        {
            KT5timer += Time.deltaTime;
            if (KT5timer >= 10)
            {
                KT5timer = 10f;
            }
        }

        //KT1
        if (KT[3].gameObject.GetComponent<KTpoint>().capture == true)
        {
            KT1timer -= Time.deltaTime;
            if (KT1timer <= 0)
            {
                KT1timer = 10f;
                KT[3].gameObject.GetComponent<KTpoint>().KTvisual();

            }
        }
        //KT2
        if (KT[4].gameObject.GetComponent<KTpoint>().capture == true)
        {
            KT2timer -= Time.deltaTime;
            if (KT2timer <= 0)
            {
                KT2timer = 10f;
                KT[4].gameObject.GetComponent<KTpoint>().KTvisual();

            }
        }
      
        BOTtext.text = KT4timer.ToString("0");
        MIDtext.text = KT5timer.ToString("0");
        TOPtext.text = KT3timer.ToString("0");

        if (KT[0].gameObject.GetComponent<KTpoint>().image == 1)
        {
            TOP.sprite = sprites[7];
        }
        if (KT[0].gameObject.GetComponent<KTpoint>().image == 2)
        {
            TOP.sprite = sprites[6];
        }
        if (KT[0].gameObject.GetComponent<KTpoint>().image == 3)
        {
            TOP.sprite = sprites[8];
        }
        if (KT[1].gameObject.GetComponent<KTpoint>().image == 1)
        {
            BOT.sprite = sprites[1];
        }
        if (KT[1].gameObject.GetComponent<KTpoint>().image == 2)
        {
            BOT.sprite = sprites[0];
        }
        if (KT[1].gameObject.GetComponent<KTpoint>().image == 3)
        {
            BOT.sprite = sprites[2];
        }
        if (KT[2].gameObject.GetComponent<KTpoint>().image == 1)
        {
            MID.sprite = sprites[4];
        }
        if (KT[2].gameObject.GetComponent<KTpoint>().image == 2)
        {
            MID.sprite = sprites[3];
        }
        if (KT[2].gameObject.GetComponent<KTpoint>().image == 3)
        {
            MID.sprite = sprites[5];
        }

    }
}

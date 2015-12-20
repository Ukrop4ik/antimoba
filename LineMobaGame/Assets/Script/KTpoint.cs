using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class KTpoint : MonoBehaviour {


    List<GameObject> blueList = new List<GameObject>();
    List<GameObject> redList = new List<GameObject>();

    [SerializeField]GameObject part1;
    [SerializeField]GameObject part2;
    [SerializeField]GameObject manager;
    ParticleSystem _part1;
    ParticleSystem _part2;
    public int image = 1;

    public Color red;
    public Color blue;
    public Color neutral;

    int BlueModCount;
    int RedModCount;

    float tic = 1f;
    bool tictac = true;
    bool inistart = true;

   public bool capture = false;
   public int KTpointStatus;
    /* 1 - neutral
    2 - blue
    3  - red
    */

    void OnTriggerStay(Collider other)
    {
        if (!blueList.Contains(other.gameObject) && other.gameObject.tag == "BlueMob")
        {
            blueList.Add(other.gameObject);
            //Debug.Log("BlueMob in " + gameObject.name + " count = " + blueList.Count);
        }

        if (!redList.Contains(other.gameObject) && other.gameObject.tag == "RedMob")
        {
            redList.Add(other.gameObject);
            //Debug.Log("BlueMob in " + gameObject.name + " count = " + blueList.Count);
        }

        if (other && inistart == true)
        {
            inistart = false;
        }

        if (gameObject.name == "KT5" && redList.Count == 0 && _part2.startColor == blue)
        {
            
                GameObject KT2 = GameObject.Find("KT2");
                other.gameObject.GetComponent<UnitLogic>().targets[1] = KT2;
       
        }
        if (gameObject.name == "KT5" && blueList.Count == 0 && _part2.startColor == red)
        {

            GameObject KT2 = GameObject.Find("KT1");
            other.gameObject.GetComponent<UnitLogic>().targets[1] = KT2;

        }
        if (gameObject.name == "KT3" && redList.Count == 0 && _part2.startColor == blue)
        {

            GameObject KT2 = GameObject.Find("KT2");
            other.gameObject.GetComponent<UnitLogic>().targets[1] = KT2;

        }
        if (gameObject.name == "KT3" && blueList.Count == 0 && _part2.startColor == red)
        {

            GameObject KT2 = GameObject.Find("KT1");
            other.gameObject.GetComponent<UnitLogic>().targets[1] = KT2;

        }
        if (gameObject.name == "KT4" && redList.Count == 0 && _part2.startColor == blue)
        {

            GameObject KT2 = GameObject.Find("KT2");
            other.gameObject.GetComponent<UnitLogic>().targets[1] = KT2;

        }
        if (gameObject.name == "KT4" && blueList.Count == 0 && _part2.startColor == red)
        {

            GameObject KT2 = GameObject.Find("KT1");
            other.gameObject.GetComponent<UnitLogic>().targets[1] = KT2;

        }


    }

    void OnTriggerExit(Collider other)
    {

    }

    void Start()
    {
        KTpointStatus = 1;
        _part1 = part1.gameObject.GetComponent<ParticleSystem>();
        _part2 = part2.gameObject.GetComponent<ParticleSystem>();
        
    }

    void Update()
    {

        if (tictac == true)
        {
            tic -= Time.deltaTime;
            if (tic <= 0)
            {
                tictac = false;

                RemoveUnitsinList();

                BlueModCount = blueList.Count;
               // Debug.Log("RedMob in " + gameObject.name + " count = " + RedModCount);
                RedModCount = redList.Count;
               // Debug.Log("BlueMob in " + gameObject.name + " count = " + BlueModCount);

                tictac = true;
                tic = 1f;
            }
        }

        if (red != null && inistart == false )
        {
            if (BlueModCount == 0 && RedModCount != 0 && KTpointStatus != 3)
            {
                capture = true;
                KTpointStatus = 3;

            }
            
            if (BlueModCount != 0 && RedModCount == 0 && KTpointStatus != 2)
            {
                capture = true;
                KTpointStatus = 2;

            }
            
            if (BlueModCount == 0 && RedModCount == 0 && KTpointStatus != 1)
            {
                capture = true;
                KTpointStatus = 1;
 
            }
            
        }
        

    }

    void RemoveUnitsinList()
    {
        foreach (GameObject obj in blueList.Select(x => x).ToArray())
            if (obj == null)
            {
                blueList.Remove(obj);
            }
        foreach (GameObject obj in redList.Select(x => x).ToArray())
            if (obj == null)
            {
                redList.Remove(obj);
            }
    }

    public void KTvisual()
    {
        switch (KTpointStatus)
        {
            case 3:
                capture = false;
                _part1.startColor = red;
                _part2.startColor = red;
                image = 3;
                break;

            case 2:
                capture = false;
                _part1.startColor = blue;
                _part2.startColor = blue;
                image = 2;
                break;
            case 1:
                capture = false;
                _part1.startColor = neutral;
                _part2.startColor = neutral;
                image = 1;
                break;
        }
    }
}

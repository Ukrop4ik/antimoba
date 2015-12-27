using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class KTpoint : MonoBehaviour {


    List<GameObject> blueList = new List<GameObject>();
    List<GameObject> redList = new List<GameObject>();
    [SerializeField]EndGame endgame;
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
    bool battle;
    bool empty;
    bool bluewin;
    bool redwin;



    void OnTriggerStay(Collider other)
    {
        if (!blueList.Contains(other.gameObject) && other.gameObject.tag == "BlueMob" && !other.gameObject.name.Contains("Mage"))
        {
            blueList.Add(other.gameObject);
            //Debug.Log("BlueMob in " + gameObject.name + " count = " + blueList.Count);
        }

        if (!redList.Contains(other.gameObject) && other.gameObject.tag == "RedMob" && !other.gameObject.name.Contains("Mage"))
        {
            redList.Add(other.gameObject);
            //Debug.Log("BlueMob in " + gameObject.name + " count = " + blueList.Count);
        }

        if ((gameObject.name == "KT1" || gameObject.name == "KT2") && inistart == true)
        {
            MainPointIni();        
        }

        if (other && inistart == true)
        {
            inistart = false;
        }

        if (gameObject.name == "KT5" && redList.Count == 0 && _part2.startColor == blue)
        {
            
                GameObject KT2 = GameObject.Find("KT2");
                other.gameObject.GetComponent<UnitLogic>().targets[0] = KT2;
       
        }
        if (gameObject.name == "KT5" && blueList.Count == 0 && _part2.startColor == red)
        {

            GameObject KT2 = GameObject.Find("KT1");
            other.gameObject.GetComponent<UnitLogic>().targets[0] = KT2;

        }
        if (gameObject.name == "KT3" && redList.Count == 0 && _part2.startColor == blue)
        {

            GameObject KT2 = GameObject.Find("KT2");
            other.gameObject.GetComponent<UnitLogic>().targets[0] = KT2;

        }
        if (gameObject.name == "KT3" && blueList.Count == 0 && _part2.startColor == red)
        {

            GameObject KT2 = GameObject.Find("KT1");
            other.gameObject.GetComponent<UnitLogic>().targets[0] = KT2;

        }
        if (gameObject.name == "KT4" && redList.Count == 0 && _part2.startColor == blue)
        {

            GameObject KT2 = GameObject.Find("KT2");
            other.gameObject.GetComponent<UnitLogic>().targets[0] = KT2;

        }
        if (gameObject.name == "KT4" && blueList.Count == 0 && _part2.startColor == red)
        {

            GameObject KT2 = GameObject.Find("KT1");
            other.gameObject.GetComponent<UnitLogic>().targets[0] = KT2;

        }



    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BlueMob")
        {
            blueList.Remove(other.gameObject);
        }
        if (other.gameObject.tag == "RedMob")
        {
            redList.Remove(other.gameObject);
        }
    }

    void Start()
    {
        KTpointStatus = 1;
        _part1 = part1.gameObject.GetComponent<ParticleSystem>();
        _part2 = part2.gameObject.GetComponent<ParticleSystem>();



    }

    void Update()
    {

        RemoveUnitsinList();
        BlueModCount = blueList.Count;
        RedModCount = redList.Count;


        if (red != null && inistart == false)
        {


            battle = BlueModCount != 0 && RedModCount != 0;
            empty = BlueModCount == 0 && RedModCount == 0;
            redwin = BlueModCount == 0 && RedModCount != 0;
            bluewin = BlueModCount != 0 && RedModCount == 0;

            if (redwin && KTpointStatus != 3)
            {
                capture = true;
                  

            }


            if (bluewin && KTpointStatus != 2)
            {
                capture = true;
                

            }
            else if (battle || empty)
            {
                capture = false;
            }
            




        }
       



    }

    void RemoveUnitsinList()
    {
        foreach (GameObject obj in blueList.Select(x => x).ToArray())
            if (obj.GetComponent<UnitLogic>().kill == true)
            {
                blueList.Remove(obj);
            }
        foreach (GameObject obj in redList.Select(x => x).ToArray())
            if (obj.GetComponent<UnitLogic>().kill == true)
            {
                redList.Remove(obj);
            }
    }

    public void KTvisual()
    {
        int a = 0;


            if (redwin)
            {
                a = 3;
            }
            if (bluewin)
            {
                a = 2;
            }

        if (a != 0)
        {
            switch (a)
            {
                case 3:
                    capture = false;
                    KTpointStatus = 3;
                    _part1.startColor = red;
                    _part2.startColor = red;
                    EndGame();
                    image = 3;
                    a = 0;
                    break;

                case 2:
                    capture = false;
                    KTpointStatus = 2;
                    _part1.startColor = blue;
                    _part2.startColor = blue;
                    EndGame();
                    image = 2;
                    a = 0;
                    break;
                case 1:
                    capture = false;
                    _part1.startColor = neutral;
                    _part2.startColor = neutral;
                    image = 1;
                    a = 0;
                    break;
            }
        }
    }

    void MainPointIni()
    {
        if (gameObject.name == "KT1")
        {
            _part1.startColor = blue;
            _part2.startColor = blue;
            KTpointStatus = 2;
        }
        if (gameObject.name == "KT2")
        {
            _part1.startColor = red;
            _part2.startColor = red;
            KTpointStatus = 3;
        }
    }

    void EndGame()
    {
        if (_part1.startColor == red && gameObject.name == "KT1")
        {
            endgame.defeat();
        }
        if (_part1.startColor == blue && gameObject.name == "KT2")
        {
            endgame.victory();
        }
    }
}

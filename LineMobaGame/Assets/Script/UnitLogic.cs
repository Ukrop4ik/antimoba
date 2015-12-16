using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class UnitLogic : MonoBehaviour
{

    public float UnitID;
    
    public GameObject _logic;

    public int firstPoint;
    public float HP; // здоровье
    public float DAMAGE; // урон
    public float KDA; // перезарядка

    //бусты
    public float HPboost = 1f; // множитель здоровья
    public float DAMAGEboost = 1f; // множитель урона
    public float KDAboost = 1f; // множитель скорости перезарядки

    public bool isEnemy = false;
    public bool canAttack = false;

    public bool kill = false;
    public float KillTime;

    float HPCase;
    public float HPbuffer;

    public bool initial = true;

    Animator anim;
    NavMeshAgent agent;
    public GameObject bar;
    public UnitLogic enemyStat;
    public string targetname;



    public GameObject[] enemis;
    public GameObject[] targets;
    public GameObject[] kt;





    // Use this for initialization
    void Start()
    {
        //  bar = gameObject.transform.transform.Find("Image") .gameObject;

        UnitID = Random.Range(1f, 100f);
        gameObject.name += UnitID;

        agent = GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Run", true);


        HP = 100f * HPboost;
        DAMAGE = 10f;
        KDA = 2f;
        KillTime = 4f;
        HPCase = HP;
        HPbuffer = HP;

   

        // триггер коллайдер
        var collider = gameObject.AddComponent<SphereCollider>();
        collider.radius = 0.01524855f;
        collider.center = new Vector3(0, 0.07f, 0);
        collider.isTrigger = true;



    }

    void Update()
    {
        //выбор точки маршрута
        if (firstPoint != 0)
        {
            switch (firstPoint)
            {
                case 1:
                    targets[1] = GameObject.Find("KT5");
                    agent.destination = targets[1].transform.position;
                    break;
                case 2:
                    targets[1] = GameObject.Find("KT3");
                    agent.destination = targets[1].transform.position;
                    break;
                case 3:
                    targets[1] = GameObject.Find("KT4");
                    agent.destination = targets[1].transform.position;
                    break;

            }
        }


        // нанесение урона 
        if (enemis[0] != null && isEnemy == true && enemis[0].tag != "DeadBody")
        {
            float _damage;

            if (canAttack == true)
            {
                anim.SetBool("Attack", true);
                _damage = hit(DAMAGE, DAMAGEboost); 
                enemis[0].GetComponent<UnitLogic>().HP -= _damage;
                
                canAttack = false;




            }
            

        }


        if (firstPoint != 0)
        {
            var collider = gameObject.GetComponent<SphereCollider>();
            collider.radius = 0.1524855f;
        }


        //смерть
        if (HP <= 0) { gameObject.tag = "DeadBody"; death();  HP = 0;  }

        // счетчик перед смертью
        if (kill == true)
        {

            KillTime -= Time.deltaTime;
            if (KillTime <= 0f)
            {
                Destroy(gameObject);
            }
        }

        // счетчик кулдауна атаки
        if (canAttack == false)
        {
            KDA -= Time.deltaTime * KDAboost;
            if (KDA <= 0)
            {
                canAttack = true;
                KDA = 2f;
            }
        }

        // передача ХП в бар
        if (HP < HPCase)
        {
            bar.GetComponent<HPBAR>().changeWidth();
            HPCase = HP;

        }

    }

    


    void death()
    {

        anim.SetBool("Kill", true);

        kill = true;
        isEnemy = false;
        canAttack = false;

    }

    float hit(float a, float b)
    {
        float Damage = a * b;
        return Damage;
    }
    // обьект попал в агро зону моба
    void OnTriggerStay(Collider other)
    {





        if (initial == true)
        {
            kt = GameObject.FindGameObjectsWithTag("KT");
            enemis = new GameObject[1];
            targets = new GameObject[2];
            initial = false;
        }
        if (anim == null)
        {
            return;
        }

        // заметили врага
        if ((enemis[0] == null && other.tag != gameObject.tag) && other.tag != "KT" && other.tag != "Spaun" && other.tag != "DeadBody")
        {
            enemis[0] = other.gameObject;
            targetname = other.gameObject.name;
            isEnemy = true;

            // узнали путь к врагу, запомнив старый
            if (targets[0] != other.gameObject && targets[1] != other.gameObject && other.gameObject.tag != gameObject.tag)
            {
                targets[0] = targets[1];
                targets[1] = enemis[0].gameObject;
            }


            // идем к врагу
            agent.destination = targets[1].transform.position;
            if (enemis[0] == other.gameObject && other.tag == "DeadBody")
            {
                enemis[0] = null;
                targets[1] = targets[0];
                targets[0] = null;

                agent.destination = targets[1].transform.position;

            }

        }
        else if (enemis[0] == null && other.gameObject.tag != "RedMob") { isEnemy = false; anim.SetBool("Attack", false); }




    }
    void OnTriggerExit(Collider other)
    {
        if (enemis[0] == other.gameObject && other.tag == "DeadBody")
        {
            enemis[0] = null;
            targets[1] = targets[0];
            targets[0] = null;
 
            agent.destination = targets[1].transform.position;

        }
    }


    void TargetSelect()
    {

    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class UnitLogic : MonoBehaviour
{



    public int firstPoint = 0;
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
    UnitLogic enemyStat;



    public GameObject[] enemis;
    public GameObject[] targets;





    // Use this for initialization
    void Start()
    {
        //  bar = gameObject.transform.transform.Find("Image") .gameObject;



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
        if (firstPoint == 0)
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
        if (enemis[1] != null)
        {
            float _damage;

            if (canAttack == true)
            {
                _damage = hit(enemyStat.DAMAGE, enemyStat.DAMAGEboost);
                anim.SetBool("Attack", true);
                HP -= _damage;
                canAttack = false;
            }
            
        }


        if (firstPoint != 0)
        {
            var collider = gameObject.GetComponent<SphereCollider>();
            collider.radius = 0.1524855f;
        }


        //смерть
        if (HP <= 0) { death(); gameObject.tag = "DeadBody"; HP = 0; }

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

    
    bool Damage(float a)
    {
        HP -= a;
        return HP <= 0;
    }
    void death()
    {

        anim.SetBool("Kill", true);

        kill = true;
        isEnemy = false;
        canAttack = true;

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
            enemis = new GameObject[2];
            targets = new GameObject[2];
            initial = false;
        }

        if (anim == null)
        {
            return;
        }

        // заметили врага
        if ((enemis.Equals(other.gameObject) == false  && other.tag != gameObject.tag) && (other.tag == "RedMob" || other.tag == "BlueMob"))
        {
            enemis[1] = other.gameObject;
            GetEnemyLogic();

            // узнали путь к врагу, запомнив старый
            if (targets[0] != other.gameObject && targets[1] != other.gameObject && other.gameObject.tag != gameObject.tag)
            {
                targets[0] = targets[1];
                targets[1] = enemis[1].gameObject;
            }


            // идем к врагу
            agent.destination = targets[1].transform.position;

        }




    }
    void OnTriggerExit(Collider other)
    {
        if (enemis[1] == other.gameObject && other.tag == "DeadBody" || enemis[1] == null)
        {
            enemis[1] = null;
            targets[1] = targets[0];
            targets[0] = null;
            agent.destination = targets[1].transform.position;

        }
    }

    void GetEnemyLogic()
    {
        enemyStat = enemis[1].GetComponent<UnitLogic>();
    }

    void TargetSelect()
    {

    }
}

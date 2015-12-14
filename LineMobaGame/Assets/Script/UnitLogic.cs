using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UnitLogic : MonoBehaviour
{

   

    public int firstPoint = 0;
    public float HP; // здоровье
    public float DAMAGE; // урон
    public float KDA; // перезарядка

    //бусты
    public float HPboost = 1; // множитеь здоровья
    public float DAMAGEboost = 1; // множитель урона
    public float KDAboost = 1; // множитель скорости перезарядки

    public bool isEnemy = false;
    public bool canAttack = false;

    public float HPCase;
    public float HPbuffer;

    Animator anim;
    NavMeshAgent agent;
    public GameObject bar;



    // Use this for initialization
    void Start()
    {
      //  bar = gameObject.transform.transform.Find("Image") .gameObject;

        

        agent = GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Run", true);


        HP = 100f;
        DAMAGE = 10f;
        KDA = 1f;
        HPCase = HP;
        HPbuffer = HP;

        Debug.Log(HPCase);

        // триггер коллайдер
        var collider = gameObject.AddComponent<SphereCollider>();
        collider.radius = 0.01524855f;
        collider.center = new Vector3(0, 0.07f, 0);
        collider.isTrigger = true;





        // определяем принадлежность моба к стороне


    }

    void Update()
    {
        
        GameObject target;
        switch (firstPoint)
        {
            case 1:
                target = GameObject.Find("KT5");
                agent.destination = target.transform.position;
                break;
            case 2:
                target = GameObject.Find("KT3");
                agent.destination = target.transform.position;
                break;
            case 3:
                target = GameObject.Find("KT4");
                agent.destination = target.transform.position;
                break;

        }
        if (firstPoint != 0)
        {
            var collider = gameObject.GetComponent<SphereCollider>();
            collider.radius = 0.1524855f;
        }


        //смерть
        if (HP <= 0) { death(); }

        // счетчик кулдауна атаки
        if (canAttack == false)
        {
            KDA -= Time.deltaTime * KDAboost;
            if (KDA <= 0)
            {
                canAttack = true;
                KDA = 1f;
            }
        }

        // передача ХП в бар
        if (HP < HPCase)
        {
            bar.GetComponent<HPBAR>().changeWidth();
            HPCase = HP;
            
        }

    }

    // метод нанесения урона
    bool Damage(float a)
    {
        HP -= a;
        
        
        
        return HP <= 0;
    }
    void death()
    {
        Destroy(gameObject);
    }

    // обьект попал в агро зону моба
    void OnTriggerStay(Collider other)
    {
        if (anim == null)
        {
            return;
        }
        var posslist = new List<string>();
        posslist.Add("RedMob");
        posslist.Add("BlueMob");

        if (posslist.Contains(gameObject.tag) && posslist.Contains(other.tag))
        {
            if (gameObject.tag != other.tag)
            {
                anim.SetBool("Attack", true);
                
                if (canAttack == true)
                {
                 
                    var unit = other.GetComponent<UnitLogic>();
                    isEnemy = !unit.Damage(DAMAGE * DAMAGEboost);
                    canAttack = false;
                    if (!isEnemy)
                    {
                        anim.SetBool("Attack", false);
                    }
                }
            }
        }
        if (!isEnemy)
        {
            anim.SetBool("Attack", false);
        }
        //"ditto";



    }


}

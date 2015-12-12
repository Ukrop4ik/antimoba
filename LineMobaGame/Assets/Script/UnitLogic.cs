using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitLogic : MonoBehaviour {

    public bool isEnemy = false;

    public int firstPoint;
    public float HP; // здоровье
    public float DAMAGE; // урон
    public float KDA; // перезарядка

    //бусты
    public float HPboost = 1; // множитеь здоровья
    public float DAMAGEboost = 1; // множитель урона
    public float KDAboost = 1; // множитель скорости перезарядки

    public bool canAttack = false;

    


    // Use this for initialization
    void Start () {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        GameObject target;

        

        HP = 100f;
        DAMAGE = 10f;
        KDA = 1f;

       
        
        

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

        // определяем принадлежность моба к стороне
   

    }

    void Update()
    {
        //смерть
        if (HP <= 0) { death();}

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

    }

    // метод нанесения урона
    void Damage(float a)
    {
        HP -= a;
        
    }
    void death()
    {
        Destroy(gameObject);
    }

    // обьект попал в агро зону моба
    void OnTriggerStay(Collider other)
    {
        if (gameObject.tag != other.tag && other.name != "Terrain" )
        {
            isEnemy = true;
            if (canAttack == true && isEnemy)
            {
                Debug.Log("Атакуют!" + other.name);
                Damage(DAMAGE * DAMAGEboost);
            }
            
        }
        else isEnemy = false;



    }


}

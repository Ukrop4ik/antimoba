using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Events;

[System.Serializable]
public class UnitLogic : MonoBehaviour
{
    public int Line;

    float targetdist;
    float enemydist;

    public float UnitID;
    
    public GameObject _logic;

    public int firstPoint;
    public float HP; // здоровье
    public float DAMAGE; // урон
    public float KDA; // перезарядка

    //бусты
    public float HPboost = 1f; // множитель здоровья
    public float DAMAGEboost = 1f; // множитель урона
    float boost = 0.2f; //шаг множителя
    public float KDAboost = 1f; // множитель скорости перезарядки
    public float spellspeed;

    [SerializeField] private bool isEnemy = false;
    [SerializeField] private bool canAttack = false;
    [SerializeField] GameObject staff;

    public bool kill = false;
    private float KillTime;

    public float HPCase;
    public float HPbuffer;

    private bool initial = true;
    private bool gotostart = true;

    Animator anim;
    NavMeshAgent agent;
    [SerializeField] private GameObject bar;
    public UnitLogic enemyStat;
    public string targetname;
    private SphereCollider collider;
    private SpellSystem SpellSystem;



    public GameObject[] enemis;
    public GameObject[] targets;
    public GameObject[] kt;





    void Awake()
    {
        HP = 100f * HPboost;
        DAMAGE = 10f;
        KDA = 2f;

        KillTime = 2f;
        HPCase = HP;
        HPbuffer = HP;
    }

    // Use this for initialization
    void Start()
    {
        //  bar = gameObject.transform.transform.Find("Image") .gameObject;

        UnitID = Random.Range(1f, 100f);
        gameObject.name += UnitID;

        agent = GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Run", true);

        // триггер коллайдер
        StartCoroutine(colliderCreate());
        StartCoroutine(targetsCreate());

        kt = new GameObject[4];

        kt[0] = GameObject.Find("KT5");
        kt[1] = GameObject.Find("KT3");
        kt[2] = GameObject.Find("KT4");

        if (gameObject.tag == "BlueMob")
        {

            kt[3] = GameObject.Find("KT2");
            
        }
        if (gameObject.tag == "RedMob")
        {

            kt[3] = GameObject.Find("KT1");

        }

        SpellSystem = GameObject.Find("SpellSystem").GetComponent<SpellSystem>();



    }

    void Update()
    {
        if (targets[1] != null)
        {
            targetdist = Vector3.Distance(gameObject.transform.position, targets[1].transform.position);
        }
        if (enemis[0] != null)
        {
            enemydist = Vector3.Distance(gameObject.transform.position, enemis[0].transform.position);
        }

        if (enemis[0] == null && targets[0] != null)
        {
            isEnemy = false; anim.SetBool("Attack", false);
            targets[1] = targets[0];
        }

        if (targets[1] != null)
        {
            agent.destination = targets[1].transform.position;
            
            anim.SetBool("Run", true);
        }
        if (targetdist <= 2.05f)
        {
            anim.SetBool("Run", false);
        }







        //выбор точки маршрута
        if (firstPoint != 0 && gotostart == true)
        {
            switch (firstPoint)
            {
                case 1:
                    targets[1] = GameObject.Find("KT5");
                    targets[0] = GameObject.Find("KT5");
                    agent.destination = targets[1].transform.position;
                    gotostart = false;
                    Line = 2;
                    break;
                case 2:
                    targets[1] = GameObject.Find("KT3");
                    targets[0] = GameObject.Find("KT3");
                    agent.destination = targets[1].transform.position;
                    gotostart = false;
                    Line = 1;
                    break;
                case 3:
                    targets[1] = GameObject.Find("KT4");
                    targets[0] = GameObject.Find("KT4");
                    agent.destination = targets[1].transform.position;
                    gotostart = false;
                    Line = 3;
                    break;

            }
        }

        if (gameObject.tag == "BlueMob")




        if (enemis[0] == null && gotostart == false && targets[0] != null)
        {

            targets[1] = targets[0];

            

        }


        // нанесение урона воином
        if (gameObject.name.Contains("MobBlue") || gameObject.name.Contains("MobRed"))
        {
            if (enemis[0] != null && isEnemy == true && enemis[0].tag != "DeadBody")
            {
                float _damage;

                if (canAttack == true)
                {
                    
                    anim.SetBool("Attack", true);
                    _damage = hit(DAMAGE, DAMAGEboost);
                    enemis[0].GetComponent<UnitLogic>().HP -= _damage;
                    gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targets[1].transform.rotation, Time.deltaTime * 2f);
                    canAttack = false;
                }


            }
        }
        // логика атаки мага
        else if (enemis[0] != null && isEnemy == true && enemis[0].tag != "DeadBody")
        {
            if (canAttack == true)
            {
                GameObject spell = SpellSystem.spells[0];
                spellspeed = 10f;
                float _damage = hit(DAMAGE, DAMAGEboost);
                anim.SetBool("Attack", true);
                spellcast(spell, staff, enemis[0], _damage, spellspeed);
                canAttack = false;
            }
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

        targets[0] = null;
        targets[1] = null;
        enemis[0] = null;
       

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
            if (enemis[0] != null)
            {
                targets[1] = gameObject;
            }

        }

        if (other.gameObject.name.Contains("Mage") && other.tag != gameObject.tag && targets[1] == null)
        {
            targets[1] = other.gameObject;
        }

    }
    void OnTriggerExit(Collider other)
    {
    }


    public void ADboost()
    {
        DAMAGEboost += boost;
        Debug.Log("Юнит усилен на: " + (DAMAGEboost - 1));
        
    }

    private IEnumerator colliderCreate()
    {
        collider = gameObject.GetComponent<SphereCollider>();
        yield return new WaitForSeconds(0.5f);
        collider.enabled = true;
    }
    private IEnumerator targetsCreate()
    {
        enemis = new GameObject[1];
        targets = new GameObject[2];
        yield return new WaitForSeconds(0.5f);

        
    }

    // для магов
    void spellcast( GameObject spell, GameObject start, GameObject target, float damage, float speed)
    {
        GameObject _spell = Instantiate(spell, staff.transform.position, Quaternion.LookRotation(Vector3.forward)) as GameObject;
        _spell.GetComponent<Spell>().damage = damage;
        _spell.GetComponent<Spell>().speed = speed;
        _spell.GetComponent<Spell>().starttarget = gameObject;
        _spell.GetComponent<Spell>().target = target;
    }

}

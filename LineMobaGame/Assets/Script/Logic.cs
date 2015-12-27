using UnityEngine;
using System.Collections;

public class Logic : MonoBehaviour
{


    public GameObject mySpaun;
    public GameObject mySpaunTarget;

    public GameObject mySpaun1;
    public GameObject mySpaunTarget1;

    public GameObject mySpaun2;
    public GameObject mySpaunTarget2;

    public GameObject enemySpaun;
    public GameObject enemySpaunTarget;

    public GameObject enemySpaun1;
    public GameObject enemySpaunTarget1;

    public GameObject enemySpaun2;
    public GameObject enemySpaunTarget2;

    public GameObject enemyunits;
    public GameObject units;
    public GameObject RedMage;
    public GameObject BlueMage;

    float KDA = 30f;

    public GameObject[] spaun = new GameObject[6];

    // Use this for initialization
    void Start()
    {

        
        StartCoroutine(CreateUnit());

        
    }



    /* Спаун юнита, передем КТ, тип юнита, сторону */
    public IEnumerator CreateUnit()
    {


        for (int i = 0; i < 1; i++)
        {

            UnitSpaun(RedMage, enemySpaun, enemySpaunTarget, 1);
            UnitSpaun(RedMage, enemySpaun1, enemySpaunTarget1, 2);
            UnitSpaun(RedMage, enemySpaun2, enemySpaunTarget2, 3);

            UnitSpaun(BlueMage, mySpaun, mySpaunTarget, 1);
            UnitSpaun(BlueMage, mySpaun1, mySpaunTarget1, 2);
            UnitSpaun(BlueMage, mySpaun2, mySpaunTarget2, 3);


            UnitSpaun(units, mySpaun, mySpaunTarget, 1);
            UnitSpaun(units, mySpaun1, mySpaunTarget1, 2);
            UnitSpaun(units, mySpaun2, mySpaunTarget2, 3);


            UnitSpaun(enemyunits, enemySpaun, enemySpaunTarget, 1);
            UnitSpaun(enemyunits, enemySpaun1, enemySpaunTarget1, 2);
            UnitSpaun(enemyunits, enemySpaun2, enemySpaunTarget2, 3);
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        }

        

    }

    void Update()
    {
        KDspawn();
    }
    void KDspawn()
    {
        KDA -= Time.deltaTime;
        if (KDA <= 0)
        {
            StartCoroutine(CreateUnit());
            KDA = 30f;
        }
    }

    void UnitSpaun(GameObject unit, GameObject spaunPoint, GameObject target, int Point)
    {
        Vector3 _spaunpoint = spaunPoint.transform.position;
        Vector3 from = unit.transform.position;
        Vector3 to = target.transform.position;
        
        GameObject ind = Instantiate(unit, _spaunpoint, Quaternion.FromToRotation(from, to)) as GameObject;


            UnitLogic logic = ind.GetComponent<UnitLogic>();
            logic.firstPoint = Point;
        
        
    }

}
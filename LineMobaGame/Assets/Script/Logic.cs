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

    float KDA = 30f;

    // Use this for initialization
    void Start()
    {

        CreateUnit();

    }



    /* Спаун юнита, передем КТ, тип юнита, сторону */
    public void CreateUnit()
    {

        for (int a = 0; a < 10; a++)
        {
            UnitSpaun(units, mySpaun, mySpaunTarget, 1);
            UnitSpaun(units, mySpaun1, mySpaunTarget1, 2);
            UnitSpaun(units, mySpaun2, mySpaunTarget2, 3);
            UnitSpaun(enemyunits, enemySpaun, enemySpaunTarget, 1);
            UnitSpaun(enemyunits, enemySpaun1, enemySpaunTarget1, 2);
            UnitSpaun(enemyunits, enemySpaun2, enemySpaunTarget2, 3);

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
            CreateUnit();
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
        Debug.Log(logic.firstPoint);
    }

}
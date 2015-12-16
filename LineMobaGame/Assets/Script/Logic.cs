using UnityEngine;
using System.Collections;

public class Logic : MonoBehaviour
{


    public GameObject mySpaun;
    public GameObject mySpaun1;
    public GameObject mySpaun2;
    public GameObject enemySpaun;
    public GameObject enemySpaun1;
    public GameObject enemySpaun2;

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
        for (int a = 0; a < 1; a++)
        {
            Instantiate(units, mySpaun.transform.position, Quaternion.LookRotation(Vector3.forward));
            Instantiate(units, mySpaun1.transform.position, Quaternion.LookRotation(Vector3.forward));
            Instantiate(units, mySpaun2.transform.position, Quaternion.LookRotation(Vector3.forward));

            Instantiate(enemyunits, enemySpaun.transform.position, Quaternion.LookRotation(Vector3.back));
            Instantiate(enemyunits, enemySpaun1.transform.position, Quaternion.LookRotation(Vector3.forward));
            Instantiate(enemyunits, enemySpaun2.transform.position, Quaternion.LookRotation(Vector3.back));

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


}
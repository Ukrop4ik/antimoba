using UnityEngine;
using System.Collections;


public class SpaunPoint : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {

        if (gameObject.name.Contains("mySpaun1") || gameObject.name.Contains("enemySpaun1"))
        {
            if (other.GetComponent<UnitLogic>().firstPoint == 0)
            {
                other.GetComponent<UnitLogic>().firstPoint = 1;
            }
   
        }
        if (gameObject.name.Contains("mySpaun2") || gameObject.name.Contains("enemySpaun2"))
        {
            if (other.GetComponent<UnitLogic>().firstPoint == 0)
            {
                other.GetComponent<UnitLogic>().firstPoint = 2;
            }
        }
        if (gameObject.name.Contains("mySpaun3") || gameObject.name.Contains("enemySpaun3"))
        {
            if (other.GetComponent<UnitLogic>().firstPoint == 0)
            {
                other.GetComponent<UnitLogic>().firstPoint = 3;
            }
        }



    }
}

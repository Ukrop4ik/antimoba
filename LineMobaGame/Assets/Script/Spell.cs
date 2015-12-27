using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

    public GameObject starttarget;
    public GameObject target;
    public float damage = 0;
    public float speed = 0;
    float dist;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
        dist = Vector3.Distance(gameObject.transform.position, target.transform.position);

        if (dist <= 1f)
        {
            target.GetComponent<UnitLogic>().HP -= damage;
            Destroy(gameObject);
        }
        
    }
    




}

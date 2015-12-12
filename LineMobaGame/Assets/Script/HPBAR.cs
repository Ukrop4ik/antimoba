using UnityEngine;


public class HPBAR : MonoBehaviour {

    
// test 1
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.forward = -Camera.main.transform.forward;

       
    }
}

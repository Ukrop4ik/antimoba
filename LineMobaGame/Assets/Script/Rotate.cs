using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    //[SerializeField] GameObject center;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.Rotate(0, 50 * Time.deltaTime, 0);

	}
}

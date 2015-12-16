using UnityEngine;
using System.Collections;

public class HPBAR : MonoBehaviour {

    GameObject image;
    float maxWidth = 1f;
    float curWidth;
    UnitLogic Unit;
    // 1% = 0.0013

    void Start () {

         Unit = transform.parent.parent.GetComponent<UnitLogic>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.forward = -Camera.main.transform.forward;
      

       
    }
   public void changeWidth()
    {
        
        curWidth = ( Unit.HP / Unit.HPbuffer);
        maxWidth =  curWidth;
        
        var rect = gameObject.GetComponent<RectTransform>();
        rect. localScale = new Vector3(maxWidth, 1 , 1);
        


    }
}

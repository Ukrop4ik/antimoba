using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class HPBAR : MonoBehaviour {

    Image image;
    UnitLogic Unit;
    public float HPtext;
 

    float maxWidth = 1f;
    float curWidth;
    CanvasRenderer  rend;
    Color curcolor;
    [SerializeField] Text text;

    Animator anim;


    void Start () {
      
        HPtext = 0;
        Unit = transform.parent.parent.GetComponent<UnitLogic>();
        image = gameObject.GetComponent<Image>();

        

        if (text)
        {
            text.text = "";
            anim = text.GetComponent<Animator>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.forward = -Camera.main.transform.forward;
        if (text)
        {
            text.transform.forward = -Camera.main.transform.forward;
            text.transform.rotation = Camera.main.transform.rotation;
        }
        


    }
   public void changeWidth()
    {
        

        StartCoroutine("textHP");
        curWidth = ( Unit.HP / Unit.HPbuffer);
        maxWidth =  curWidth;
        
        var rect = gameObject.GetComponent<RectTransform>();
        rect. localScale = new Vector3(maxWidth, 1 , 1);

        var fromColor = Color.red;
        var toColor = Color.green;

        if (curWidth <= 0.4f)
        {
            image.color = Color.Lerp(fromColor, toColor, curWidth);
        }


    }

    public IEnumerator textHP()
    {
        if (transform.parent.parent.tag == "RedMob")
        {
            HPtext = Mathf.Round(Unit.HPCase - Unit.HP);
            anim.SetBool("Move", true);

            if (HPtext != 0 && text)
            {

                text.text = "- " + HPtext.ToString();


            }
            yield return new WaitForSeconds(1f);

            if (text)
            {
                text.text = "";
                anim.SetBool("Move", false);
            }

            yield return 0;

            yield return 0;
        }
        yield return 0;
    }
    


}

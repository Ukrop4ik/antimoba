using UnityEngine;
using System.Collections;

public class StatControl : MonoBehaviour {

    //private float _redModBoost;
    //private float _blueMobBust;

    public float redModBoost { get; private set; }
    public float blueMobBust { get; private set; }

    void Start ()
    {
        redModBoost = 1f;
        blueMobBust = 1f;
	
	}

    public float redBoost(float bosst)
    {
        blueMobBust += bosst;
        return blueMobBust;
    }
	

}

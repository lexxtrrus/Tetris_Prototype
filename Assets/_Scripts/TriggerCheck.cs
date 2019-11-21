using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    private TriggerCheck() { }
    private static TriggerCheck _instance;
    public static TriggerCheck Instance
    {
        get
        {
            if (_instance == null) { _instance = new TriggerCheck(); }
            return _instance;
        }
        set { _instance = value; }
    }

    private GameObject _lastTriggerEnter;
    public GameObject LastTriggerEnter
    {
        get
        {
            return _lastTriggerEnter;
        }
        set
        {
            _lastTriggerEnter = value;
        }
    }


    private void Awake()
    {
        Instance = this;
    }
}

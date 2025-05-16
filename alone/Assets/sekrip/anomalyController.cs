using System.Collections.Generic;
using UnityEngine;

public class anomalyController : MonoBehaviour
{
    public GameObject[] _anomaly;
    public enum playtime
    {
        minute5,
        minute6
    }
    public playtime settime;
    private float timer;
    private List<GameObject> _inactive;
    private float anomalytimer;

    void Awake()
    {
        for(int i= 0 ; i<_anomaly.Length;i++)
        {
            _anomaly[i].SetActive(false);
        }
        _inactive = new List<GameObject>(_anomaly);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (settime==playtime.minute5)
        {
            timer = 300f;
        } else if(settime==playtime.minute6)
        {
            timer = 360f;
        }
        anomalyreset();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            anomalytimer -= Time.deltaTime;

            if (anomalytimer <= 0)
            {
                spawnanomaly();
                anomalyreset();
            }
        }
    }

    public int getindex()
    {
        int index;
        index = UnityEngine.Random.Range(0,_anomaly.Length);
        return index;
    }

    void anomalyreset()
{
    if (timer >= 240)
    {
        anomalytimer = 45;
    }
    else if (timer >= 180)
    {
        anomalytimer = 40;
    }
    else if (timer >= 120)
    {
        anomalytimer = 35; 
    }
    else if (timer >= 60)
    {
        anomalytimer = 30;
    }
    else
    {
        anomalytimer = 25; // fallback for final minute
    }
}

    public void spawnanomaly()
    {

    if (_anomaly.Length > 0)
    {
        bool anomalyspawned = false;
        while (!anomalyspawned) //to called the anomaly that haven't active yet.
        {
        int index = getindex();    
        if (_anomaly[index] != null && _inactive[index] != null) // to make sure that no same anomaly's called
        {
        _inactive[index].SetActive(true);
        _inactive[index] = null; // so the next time the same object called will be rejected by the if statement
        anomalyspawned = true; //to get out from the loop.
        }
        }
    }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : Skill
{

    GameObject RollPS;
    GameObject RockPS;
    float RollDestoryTimer;

    private void Start()
    {
        RollPS = transform.GetChild(0).gameObject;
        RockPS = transform.GetChild(1).gameObject;
    }

    // Start is called before the first frame update
    void Update()
    {
        if (RollPS != null) {
            RollDestoryTimer += Time.deltaTime;
            if (RollDestoryTimer >= 1.6f)
            {
                Destroy(RollPS);
            }
        }
        
        StartExistenceTimer();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blizzard : Skill
{
    bool IsFrozenDone = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartExistenceTimer();
    }
    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.tag == "Empty")
        {
            Empty target = other.GetComponent<Empty>();
            Debug.Log("获取到目标");
            HitAndKo(target);
            if (!IsFrozenDone)
            {
                if (Random.Range(0, 9) + (float)player.LuckPoint >= 9)
                {
                    target.Frozen(2.5f, 1);
                    target.playerUIState.StatePlus(2);
                }
                IsFrozenDone = true;
            }
        }
    }
}

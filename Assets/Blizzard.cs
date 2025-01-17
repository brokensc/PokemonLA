using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blizzard : Skill
{
    bool IsFrozenDone = false;

    float ColliderSizeChangeTimer;
    CircleCollider2D BlizzardCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        BlizzardCollider2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        StartExistenceTimer();

        ColliderSizeChangeTimer += Time.deltaTime;
        if (ColliderSizeChangeTimer <= 1.1f ) { BlizzardCollider2D.radius = Mathf.Clamp(ColliderSizeChangeTimer * 2.3f, 0, 2.3f); }
        if (ColliderSizeChangeTimer >= 5.5f ) { BlizzardCollider2D.radius = Mathf.Clamp((6.5f-ColliderSizeChangeTimer) * 2.3f, 0, 2.3f); }
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

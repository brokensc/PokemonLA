using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    //声明玩家对象，动画管理者，以及射弹的存在时间和伤害
    public PlayerControler player;
    public float ExistenceTime;
    public float Damage;
    public float SpDamage;
    public bool IsDamageChangeable;
    public Animator animator;
    public float MaxRange;

    //声明技能的属性
    public int SkillType;
    public string SkillName;
    public string SkillChineseName;
    public string SkillDiscribe;

    //一个布尔值表示攻击是否已发生，用于非多段伤害
    bool isHitDone = false;


    //声明2个变量，表示技能的冷却时间，以及技能可以击退敌人的距离
    public float KOPoint;
    public float ColdDown;

    public int[] SkillTag;
    //Tag1:接触类 Tag2:非接触类 Tag3:爪类 Tag4:牙类 Tag5:声音类
    public bool isDirection;
    public bool isMoveWithPlayer;
    public bool isMultipleDamage;


    // Start is called before the first frame update
    public void StartExistenceTimer()
    {
        ExistenceTime -= Time.deltaTime;
        if (ExistenceTime <= 0)
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void HitAndKo(Empty target)
    {

        if (isMultipleDamage || !isHitDone) {
            if(Damage == 0)
            {
                if(Random.Range(0.0f , 1.0f ) >= 0.04f + 0.01f * player.LuckPoint)
                {
                    target.EmptyHpChange(0, (SpDamage * (SkillType == player.PlayerType01 ? 1.5f : 1) * (SkillType == player.PlayerType02 ? 1.5f : 1) * (player.PlayerTeraTypeJOR == 0 ? (SkillType == player.PlayerTeraType ? 1.5f : 1) : (SkillType == player.PlayerTeraTypeJOR ? 1.5f : 1)) * (2 * player.Level + 10) * player.SpAAbilityPoint) / (250 * target.SpdAbilityPoint) + 2, SkillType);
                }
                else
                {
                    target.EmptyHpChange(0, (SpDamage * (SkillType == player.PlayerType01 ? 1.5f : 1) * (SkillType == player.PlayerType02 ? 1.5f : 1) * (player.PlayerTeraTypeJOR == 0 ? (SkillType == player.PlayerTeraType ? 1.5f : 1) : (SkillType == player.PlayerTeraTypeJOR ? 1.5f : 1)) * 1.5f * (2 * player.Level + 10) * player.SpAAbilityPoint) / (250 * target.SpdAbilityPoint) + 2, SkillType);
                }
                

            }
            else if(SpDamage == 0)
            {
                if (Random.Range(0.0f, 1.0f) >= 0.04f + 0.01f * player.LuckPoint)
                {
                    target.EmptyHpChange((Damage * (SkillType == player.PlayerType01 ? 1.5f : 1) * (SkillType == player.PlayerType02 ? 1.5f : 1) * (player.PlayerTeraTypeJOR == 0 ? (SkillType == player.PlayerTeraType ? 1.5f : 1) : (SkillType == player.PlayerTeraTypeJOR ? 1.5f : 1)) * (2 * player.Level + 10) * player.AtkAbilityPoint) / (250 * target.DefAbilityPoint) + 2, 0, SkillType);
                }
                else
                {
                    target.EmptyHpChange((Damage * (SkillType == player.PlayerType01 ? 1.5f : 1) * (SkillType == player.PlayerType02 ? 1.5f : 1) * (player.PlayerTeraTypeJOR == 0 ? (SkillType == player.PlayerTeraType ? 1.5f : 1) : (SkillType == player.PlayerTeraTypeJOR ? 1.5f : 1)) * 1.5f * (2 * player.Level + 10) * player.AtkAbilityPoint) / (250 * target.DefAbilityPoint) + 2, 0, SkillType);
                }
                
            }
            target.EmptyKnockOut(KOPoint);
            isHitDone = true;
            if (player.playerData.IsPassiveGetList[26] && Random.Range(0.0f, 1.0f) + (float)player.LuckPoint / 30 > 0.6f)
            {
                if (SkillTag != null)
                {
                    foreach (int i in SkillTag)
                    {
                        if (i == 1) { target.EmptyToxicDone(1); }
                    }
                }
            }
            if (player.playerData.IsPassiveGetList[25] && Random.Range(0.0f, 1.0f) + (float)player.LuckPoint / 30 > 0.8f)
            {
                target.Fear(3.0f, 1);
            }
        }

    }

}

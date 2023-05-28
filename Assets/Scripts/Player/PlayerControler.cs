using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : Pokemon
{
    // Start is called before the first frame update

    public string PlayerNameChinese;
    public Sprite PlayerHead;


    //����һ��2D����������Ի��Сɽ���ĸ������
    new Rigidbody2D rigidbody2D;

    //����������������ȡ������Ϣ
    private float horizontal;
    private float vertical;

    //����һ��2D�����������Դ洢����Ķ�ά����
    Vector2 position;

    public delegate void ComeInANewRoom(PlayerControler player);
    public ComeInANewRoom ComeInANewRoomEvent;

    public delegate void ClearThisRoom(PlayerControler player);
    public ClearThisRoom ClearThisRoomEvent;

    public delegate void UpdataPassiveItem(PlayerControler player);
    public UpdataPassiveItem UpdataPassiveItemEvent;

    //����һ����ά������ʾ������������ұ�,��һ����ʾλ����,
    Vector2 look = new Vector2(1, 0);
    Vector2 move;
    Vector2 Direction;

    //����һ�����ͱ����������ֵ��һ�����α�����������ֵ,�Լ�һ�����ͱ���������������ֵ��������������
    public int maxHp;
    public int Hp
    {
        get { return nowHp; }
    }
    int nowHp;

    //����һ�����α������ڽ�Ǯ,�Լ�һ�����ͱ����������ڽ�Ǯ��������������
    public int Money
    {
        get { return nowMoney; }
    }
    int nowMoney;

    //����һ�����α�������ʯͷ,�Լ�һ�����ͱ�����������ʯͷ��������������
    public int Stone
    {
        get { return nowStone; }
    }
    int nowStone;

    //����һ�����α�ʾ��ǰ�ȼ������ֵ��һ�����α�����ʾ�ȼ����Լ�һ������ֵ����һ�����α������ھ���ֵ,�Լ�һ�����ͱ����������ھ���ֵ��������������
    int[] Exp = { 10, 23, 47, 76, 114, 158, 212, 271, 339, 413, 497, 586, 684, 788, 902, 1021, 1149, 1283, 1427, 1576, 1734, 1898, 2072, 2251, 2439, 2633, 2837, 3046, 3264, 3488, 3722, 3961, 4209, 4463, 4727, 4996, 5274, 5558, 5852, 6151, 6459, 6773, 7097, 7426, 7764, 8108, 8462, 8821, 9189, 9563, 9947, 10336, 10734, 11138, 11552, 11971, 12399, 12833, 13277, 13726, 14184, 14648, 15122, 15601, 16089, 16583, 17087, 17596, 18114, 18638, 19172, 19711, 20259, 20813, 21377, 21946, 22524, 23108, 23702, 24301, 24909, 25523, 26147, 26776, 27414, 28058, 28712, 29371, 30039, 30713, 31397, 32086, 32784, 33488, 34202, 34921, 35649, 36383, 37127 };
    public int maxEx;
    public int Level;
    public int LevelForSkill=1;
    public int Ex
    {
        get { return nowEx; }
    }
    int nowEx;
    public int iCount;


    //����һ�������ͱ��������޵�ʱ�䣬һ�������ͱ�����Ϊ�޵�ʱ���ʱ����һ�������ͱ����ж��Ƿ��޵�
    public float TimeInvincible;
    float InvincileTimer = 0.0f;
    public bool isInvincible = false;




    //����һ�������ͱ���������������ֵ��
    public float KnockOutPoint
    {
        get { return konckout; }
        set { konckout = value; }
    }
    float konckout = 6.5f;
    public Vector2 KnockOutDirection
    {
        get { return koDirection; }
        set { koDirection = value; }
    }
    Vector2 koDirection = new Vector2(0f,0f);



    //���������������ݣ���ʾ��ɫ����������ֵ,�Լ����ǰ����ֵ
    public int HpPlayerPoint;
    public int AtkPlayerPoint;
    public int SpAPlayerPoint;
    public int DefPlayerPoint;
    public int SpdPlayerPoint;
    public int SpeedPlayerPoint;
    public int MoveSpePlayerPoint;
    public int LuckPlayerPoint;

    public int AtkAbilityPoint { get { return AtkAbility; } set { AtkAbility = value; } }
    int AtkAbility;
    public int SpAAbilityPoint { get { return SpAAbility; } set { SpAAbility = value; } }
    int SpAAbility;
    public int DefAbilityPoint { get { return DefAbility; } set { DefAbility = value; } }
    int DefAbility;
    public int SpdAbilityPoint { get { return SpDAbility; } set { SpDAbility = value; } }
    int SpDAbility;
    public int SpeedAbilityPoint { get { return SpeedAbility; } set { SpeedAbility = value; } }
    int SpeedAbility;


    //������ҵ���������
    public int PlayerType01;
    public int PlayerType02;
    public int PlayerTeraType;
    public int PlayerTeraTypeJOR;


    //����һ����Ϸ���󣬱�ʾ��ҵļ���1,�Լ�����1����ȴ��ʱ���ͼ���1�Ƿ���ȴ,�Ƿ���ʹ�ü���
    public Skill Skill01;
    float Skill01Timer = 0;
    bool isSkill01CD = false;
    bool isSkill01lunch = false;
    bool isSkill = false;
    public SkillBar01 skillBar01;

    //����һ����Ϸ���󣬱�ʾ��ҵļ���1,�Լ�����1����ȴ��ʱ���ͼ���1�Ƿ���ȴ,�Ƿ���ʹ�ü���
    public Skill Skill02;
    float Skill02Timer = 0;
    bool isSkill02CD = false;
    bool isSkill02lunch = false;
    public SkillBar01 skillBar02;

    //����һ����Ϸ���󣬱�ʾ��ҵļ���1,�Լ�����1����ȴ��ʱ���ͼ���1�Ƿ���ȴ,�Ƿ���ʹ�ü���
    public Skill Skill03;
    float Skill03Timer = 0;
    bool isSkill03CD = false;
    bool isSkill03lunch = false;
    public SkillBar01 skillBar03;

    //����һ����Ϸ���󣬱�ʾ��ҵļ���1,�Լ�����1����ȴ��ʱ���ͼ���1�Ƿ���ȴ,�Ƿ���ʹ�ü���
    public Skill Skill04;
    float Skill04Timer = 0;
    bool isSkill04CD = false;
    bool isSkill04lunch = false;
    public SkillBar01 skillBar04;

    public GameObject spaceItem;
    public GameObject SpaceItemList;
    public Image SpaceItemImage;


    //����һ�������ͱ��������������ɫѧϰ����ʽ�ĵȼ�,�Լ�һ�����α�����⵱ǰ�ȼ��Ƿ�ϰ�ü���
    protected int[] GetSkillLevel;
    int levelChecker = 0;
    public List<Skill> SkillList = new List<Skill>();
    public UIPanelGwtNewSkill uIPanelGwtNewSkill;


    public Vector3Int NowRoom = new Vector3Int (0, 0, 0);
    public bool InANewRoom;
    public float NewRoomTimer;

    public SpaceItemUseUI spaceitemUseUI;
    public GameObject PassiveItemGetUI;
    public bool isTP;
    public bool isTPMove;

    public int LuckPoint;

    public bool isSpaceItemCanBeUse;

    public PlayerData playerData;

    PlayerControler ThisPlayer;

    public int NatureIndex;

    bool isDie;

    //��ʼ����ҵı�Ҫ����
    protected void Instance()
    {
        //��ǰ�������ֵ����һ��ʱ���������ֵ
        //��ǰ����ֵ�����������ֵ
        //��ʼ����ǰѪ�������Ѫ��������UI
        playerData = GetComponent<PlayerData>();


        //���Сɽ���ĸ�������Ͷ������
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        maxEx = Exp[Level-1];

        skillBar01.player = gameObject.GetComponent<PlayerControler>();
        skillBar02.player = gameObject.GetComponent<PlayerControler>();
        skillBar03.player = gameObject.GetComponent<PlayerControler>();
        skillBar04.player = gameObject.GetComponent<PlayerControler>();

        ThisPlayer = GetComponent<PlayerControler>();

        NatureIndex = Random.Range(0, 25);
        InstanceNature(NatureIndex);
        ReFreshAbllityPoint();
        nowHp = maxHp;
        UIHealthBar.Instance.MaxHpText.text = string.Format("{000}", maxHp);
        UIHealthBar.Instance.NowHpText.text = string.Format("{000}", nowHp);
        UIExpBar.Instance.Leveltext.text = Level.ToString();
        ComeInANewRoomEvent += ChangeRoomBgm;
    }


    protected void InstanceNewSkillPanel()
    {
        uIPanelGwtNewSkill.PokemonNameChinese = PlayerNameChinese;
    }



    // Update is called once per frame
    protected void UpdatePlayer()
    {
        if (!isDie)
        {
            //ÿ֡��ȡһ��ʮ�ּ��İ�����Ϣ
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");


            //ÿ֡���һ�ε�ǰ�Ƿ�Ϊ�޵�״̬������ǣ����ʱ����ʱ�������ʱ��ʱ��С��0�����Ϊ���޵�״̬
            if (isInvincible)
            {
                InvincileTimer -= Time.deltaTime;

                //���޵�ʱ���ʱ�����е�ǰ0��15���ڱ�����
                if (InvincileTimer > TimeInvincible - 0.15f)
                {

                    Vector2 position = rigidbody2D.position;
                    position.x = Mathf.Clamp(position.x + koDirection.x * 2.2f * konckout * Time.deltaTime, NowRoom.x * 30 - 12, NowRoom.x * 30 + 12);
                    position.y = Mathf.Clamp(position.y + koDirection.y * 2.2f * konckout * Time.deltaTime, NowRoom.y * 24 - 7.3f, NowRoom.y * 24 + 7.3f);
                    rigidbody2D.position = position;
                }
                if (InvincileTimer <= 0)
                {
                    isInvincible = false;
                }
            }


            if (isStateInvincible)
            {
                StateInvincileTimer -= Time.deltaTime;
                if (StateInvincileTimer <= 0)
                {
                    isStateInvincible = false;
                }
            }



            //������q��ʱ����skill01

            if (Input.GetKeyDown(KeyCode.Q) && isSkill01CD == false && Skill01 != null && !isSleepDone && !isSkill)
            {
                //���������е���8֡ʱ�ᷢ�似��1��������1����CD
                animator.SetTrigger("Skill");
                isSkill01CD = true;
                isSkill = true;
                isSkill01lunch = true;
                skillBar01.isCDStart = true;
            }
            //�������1��cd�ڼ䣬cd��ʱ��ʱ�俪ʼ���ӣ�����ʱ������Ϊ�ɷ���״̬����ʱ������
            if (isSkill01CD && !isSleepDone)
            {
                Skill01Timer += Time.deltaTime;
                if (Skill01Timer >= (isParalysisDone ? 1.8f : 1.0f) * Skill01.ColdDown * (1 - (SpeedAbilityPoint / 500)))
                {
                    isSkill01CD = false;
                    Skill01Timer = 0;
                }
            }

            //������w��ʱ����skill02

            if (Input.GetKeyDown(KeyCode.W) && isSkill02CD == false && Skill02 != null && !isSleepDone && !isSkill)
            {
                //���������е���8֡ʱ�ᷢ�似��2��������2����CD
                animator.SetTrigger("Skill");
                isSkill02CD = true;
                isSkill = true;
                isSkill02lunch = true;
                skillBar02.isCDStart = true;
            }
            //�������2��cd�ڼ䣬cd��ʱ��ʱ�俪ʼ���ӣ�����ʱ������Ϊ�ɷ���״̬����ʱ������
            if (isSkill02CD && !isSleepDone)
            {
                Skill02Timer += Time.deltaTime;
                if (Skill02Timer >= (isParalysisDone ? 1.8f : 1.0f) * Skill02.ColdDown * (1 - (SpeedAbilityPoint / 500)))
                {
                    isSkill02CD = false;
                    Skill02Timer = 0;
                }
            }

            //������e��ʱ����skill03

            if (Input.GetKeyDown(KeyCode.E) && isSkill03CD == false && Skill03 != null && !isSleepDone && !isSkill)
            {
                //���������е���8֡ʱ�ᷢ�似��3��������3����CD
                animator.SetTrigger("Skill");
                isSkill03CD = true;
                isSkill = true;
                isSkill03lunch = true;
                skillBar03.isCDStart = true;
            }
            //�������3��cd�ڼ䣬cd��ʱ��ʱ�俪ʼ���ӣ�����ʱ������Ϊ�ɷ���״̬����ʱ������
            if (isSkill03CD && !isSleepDone)
            {
                Skill03Timer += Time.deltaTime;
                if (Skill03Timer >= (isParalysisDone ? 1.8f : 1.0f) * Skill03.ColdDown * (1 - (SpeedAbilityPoint / 500)))
                {
                    isSkill03CD = false;
                    Skill03Timer = 0;
                }
            }

            //������r��ʱ����skill04

            if (Input.GetKeyDown(KeyCode.R) && isSkill04CD == false && Skill04 != null && !isSleepDone && !isSkill)
            {
                //���������е���8֡ʱ�ᷢ�似��4��������4����CD
                animator.SetTrigger("Skill");
                isSkill04CD = true;
                isSkill = true;
                isSkill04lunch = true;
                skillBar04.isCDStart = true;
            }
            //�������1��cd�ڼ䣬cd��ʱ��ʱ�俪ʼ���ӣ�����ʱ������Ϊ�ɷ���״̬����ʱ������
            if (isSkill04CD && !isSleepDone)
            {
                Skill04Timer += Time.deltaTime;
                if (Skill04Timer >= (isParalysisDone ? 1.8f : 1.0f) * Skill04.ColdDown * (1 - (SpeedAbilityPoint / 500)))
                {
                    isSkill04CD = false;
                    Skill04Timer = 0;
                }
            }
            if (InANewRoom == true)
            {
                NewRoomTimer += Time.deltaTime;

                RestoreStrengthAndTeraType();
                if (ComeInANewRoomEvent != null && !isComeInANewRoomEvent)
                {
                    ComeInANewRoomEvent(this);
                    isComeInANewRoomEvent = true;
                }
                CheckStateInInputNewRoom();
                isSpaceItemCanBeUse = false;
                if (NewRoomTimer >= 1.2f)
                {
                    isToxicDoneInNewRoom = false;
                    isBornDoneInNewRoom = false;
                    InANewRoom = false;
                    isStrengthAndTeraTypeBeRestore = false;
                    isComeInANewRoomEvent = false;
                    NewRoomTimer = 0;
                    isSpaceItemCanBeUse = true;
                }
            }


            if (isSpaceItemCanBeUse && Input.GetKeyDown(KeyCode.Space) && spaceItem != null)
            {
                spaceitemUseUI.UIAnimationStart(spaceItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);
                UseSpaceItem.UsedSpaceItem(this);
            }

            if (UpdataPassiveItemEvent != null)
            {
                UpdataPassiveItemEvent(ThisPlayer);
            }
        }
    }
    bool isComeInANewRoomEvent;
    bool isToxicDoneInNewRoom;
    bool isBornDoneInNewRoom;
    void CheckStateInInputNewRoom()
    {
        if (isToxicDone && !isToxicDoneInNewRoom)
        {
            KnockOutPoint = 1;
            ChangeHp((float)(-maxHp/20),0,0);
            isToxicDoneInNewRoom = true;
        }
        if (isBurnDone && !isBornDoneInNewRoom)
        {
            KnockOutPoint = 1;
            ChangeHp((float)(-maxHp / 20), 0, 0);
            isBornDoneInNewRoom = true;
        }
    }

    bool isStrengthAndTeraTypeBeRestore = false;

    void RestoreStrengthAndTeraType()
    {
        
        if (!isStrengthAndTeraTypeBeRestore)
        {
            PlayerTeraTypeJOR = 0;
            playerData.RestoreJORSata();
            isStrengthAndTeraTypeBeRestore = true;
        }
    }




    void ChangeRoomBgm(PlayerControler player)
    {
        if(NowRoom == MapCreater.StaticMap.PCRoomPoint)
        {
            BackGroundMusic.StaticBGM.ChangeBGMToPC();
        }
        else if(NowRoom == MapCreater.StaticMap.StoreRoomPoint)
        {
            BackGroundMusic.StaticBGM.ChangeBGMToStore();
        }
        else if (NowRoom == MapCreater.StaticMap.BossRoomPoint)
        {
            if (MapCreater.StaticMap.RRoom[NowRoom].isClear == 0) { BackGroundMusic.StaticBGM.ChangeBGMToBossWin(); }
            else { BackGroundMusic.StaticBGM.ChangeBGMToBoss(); }
        }
        else
        {
            BackGroundMusic.StaticBGM.ChangeBGMToTown();
        }
    }


    protected void FixedUpdatePlayer()
    {
        //2D����position���ڸ������������,֮����position��xy�������ʮ�ּ�x�ٶ�x��λʱ�䣬����ø����λ�õ���position
        if (!isDie)
        {
            if (!isSkill && !isTP)
            {
                position = rigidbody2D.position;
                position.x = position.x + horizontal * speed * Time.deltaTime;
                position.y = position.y + vertical * speed * Time.deltaTime;
                rigidbody2D.position = position;
            }


            //λ�Ʊ���Ϊʮ�ּ�����ֵ
            move = new Vector2(horizontal, vertical);

            //��������λ��ʱ���Ըı䶯���������λ�ƶ�������ı�
            if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                move = new Vector2(horizontal, vertical);
                if (move.x > move.y && -move.x > move.y)
                {
                    look.Set(0, -1);
                }
                else if (move.x > move.y && -move.x <= move.y)
                {
                    look.Set(1, 0);
                }
                else if (move.x <= move.y && -move.x > move.y)
                {
                    look.Set(-1, 0);
                }
                else if (move.x <= move.y && -move.x <= move.y)
                {
                    look.Set(0, 1);
                }
            }
            animator.SetFloat("LookX", look.x);
            animator.SetFloat("LookY", look.y);
            animator.SetFloat("Speed", move.magnitude);
        }
    }


    public void RefreshSkillCD()
    {
        if (isSkill01CD) { Skill01Timer = (isParalysisDone ? 1.8f : 1.0f) * Skill01.ColdDown * (1 - (SpeedAbilityPoint / 500)); skillBar01.CDPlus((isParalysisDone ? 1.8f : 1.0f) * Skill01.ColdDown * (1 - (SpeedAbilityPoint / 500))); }
        if (isSkill02CD) { Skill02Timer = (isParalysisDone ? 1.8f : 1.0f) * Skill02.ColdDown * (1 - (SpeedAbilityPoint / 500)); skillBar02.CDPlus((isParalysisDone ? 1.8f : 1.0f) * Skill02.ColdDown * (1 - (SpeedAbilityPoint / 500))); }
        if (isSkill03CD) { Skill03Timer = (isParalysisDone ? 1.8f : 1.0f) * Skill03.ColdDown * (1 - (SpeedAbilityPoint / 500)); skillBar03.CDPlus((isParalysisDone ? 1.8f : 1.0f) * Skill03.ColdDown * (1 - (SpeedAbilityPoint / 500))); }
        if (isSkill04CD) { Skill04Timer = (isParalysisDone ? 1.8f : 1.0f) * Skill04.ColdDown * (1 - (SpeedAbilityPoint / 500)); skillBar04.CDPlus((isParalysisDone ? 1.8f : 1.0f) * Skill04.ColdDown * (1 - (SpeedAbilityPoint / 500))); }
    }


    //����һ�����ݵȼ����㵱ǰ�������ֵ�ĺ���
    protected void MaxHpForLevel(int level)
    {
        maxHp = level + 10 + (int)(((float)level*HpPlayerPoint*2) / 100.0f);
    }

    protected int AbilityForLevel(int level , int Ability)
    {
        return (Ability * 2 * level) / 100 + 5;
    }




    public void EndureStart()
    {
        playerData.isEndure = true;
        playerUIState.StatePlus(8);
    }
    public void EndureOver()
    {
        playerData.isEndure = false;
        playerUIState.StateDestory(8);

    }

    //����һ���ı������ĺ���ChangeHp���ı�ĵ���ΪChangePoint�����ı����Ϊ��ʱ�����޵�ʱ�䣬���ı����Ϊ��ʱ�������޵�ʱ��
    public void ChangeHp(float ChangePoint , float ChangePointSp , int SkillType)
    {

        if (ChangePoint > 0 || ChangePointSp > 0)
        {
            
            nowHp = Mathf.Clamp(nowHp + (int)(ChangePoint+ChangePointSp), 0, maxHp);

            //Ѫ������ʱ��Ѫ��UI�����ǰѪ����������Ѫ�������ĺ���
            UIHealthBar.Instance.Per = (float)nowHp / (float)maxHp;
            UIHealthBar.Instance.NowHpText.text = string.Format("{000}", nowHp);
            UIHealthBar.Instance.ChangeHpUp();
        }
        else
        {
            //����޵н��������޵еĻ���Ϊ���޵�״̬���޵�ʱ���ʱ��ʱ������Ϊ�޵�ʱ��
            if (isInvincible)
            {
                return;
            }
            else
            {
                if(SkillType != 19)
                {
                    nowHp = Mathf.Clamp(nowHp + (int)((ChangePoint / DefAbilityPoint + ChangePointSp / SpdAbilityPoint - 2) * (Type.TYPE[SkillType][PlayerType01] * Type.TYPE[SkillType][PlayerType02] * (PlayerTeraTypeJOR == 0 ? Type.TYPE[SkillType][PlayerTeraType] : Type.TYPE[SkillType][PlayerTeraTypeJOR]  ) )*((playerData.TypeDefAlways[SkillType] + playerData.TypeDefJustOneRoom[SkillType]) > 0 ? Mathf.Pow(1.2f, (playerData.TypeDefAlways[SkillType] + playerData.TypeDefJustOneRoom[SkillType])) : Mathf.Pow(0.8f, (playerData.TypeDefAlways[SkillType] + playerData.TypeDefJustOneRoom[SkillType])))), (nowHp > 1) ? (playerData.isEndure ? 1 : 0) : 0, maxHp);
                   
                }
                else
                {
                    nowHp = Mathf.Clamp(nowHp + Mathf.Clamp((int)ChangePoint ,-100000 , -1), (nowHp > 1)?(playerData.isEndure?1:0):0, maxHp);
                }
                isInvincible = true;
                InvincileTimer = TimeInvincible;

                if (isSleepDone) { SleepRemove(); }
                UIHealthBar.Instance.Per = (float)nowHp / (float)maxHp;
                UIHealthBar.Instance.ChangeHpDown();
                UIHealthBar.Instance.NowHpText.text = string.Format("{000}", nowHp);

                if(nowHp <= 0) { isDie = true; animator.SetTrigger("Die");rigidbody2D.bodyType = RigidbodyType2D.Static;}
                //Ѫ������ʱ��Ѫ��UI�����ǰѪ����������Ѫ�������ĺ���

                //���������Ķ�������������
                animator.SetTrigger("Hit");
            }
        }
    }





    public void CallDieMask()
    {
        TPMask.In.BlackTime = 0;
        TPMask.In.TPStart = true;
        TPMask.In.transform.GetChild(0).gameObject.SetActive(true);
        TPMask.In.transform.GetChild(0).GetComponent<DiePanel>().Die(PlayerNameChinese);
    }



    //����һ���ı��Ǯ�ĺ���ChangeMoney
    public void ChangeMoney(int ChangePoint)
    {

        //�ı��Ǯ��������Ϊ99������Ϊ0��֮����UI���������Ǯ�ĸı�ֵ��������UI�ı��Ǯ���ĺ���
        nowMoney = Mathf.Clamp(nowMoney + ChangePoint + (playerData.IsPassiveGetList[10]?1:0), 0, 99);
        UIMoneyBar.Instance._Money += ChangePoint + (playerData.IsPassiveGetList[10] ? 1 : 0);
        UIMoneyBar.Instance.MoneyChange();
        
    }

    public void ChangeStone(int ChangePoint)
    {

        //�ı�ʯͷ��������Ϊ99������Ϊ0��֮����UI���������Ǯ�ĸı�ֵ��������UI�ı��Ǯ���ĺ���
        nowStone = Mathf.Clamp(nowStone + ChangePoint, 0, 99);
        UIMoneyBar.Instance._Stone += ChangePoint;
        UIMoneyBar.Instance.StoneChange();

    }




    public void ChangeHPW(Vector2Int HWP)
    {
        switch (HWP.x) {
            case 1:
                playerData.HPHardWorkAlways += (HWP.y) * 0.25f + (playerData.IsPassiveGetList[24] ? 0.15f : 0) + (playerData.IsPassiveGetList[18] ? 0.4f : 0) + (playerData.IsPassiveGetList[29] ? 0.4f : 0);
                break;
            case 2:
                playerData.AtkHardWorkAlways += (HWP.y) * 0.25f + (playerData.IsPassiveGetList[24] ? 0.15f : 0) + (playerData.IsPassiveGetList[19] ? 0.4f : 0) + (playerData.IsPassiveGetList[29] ? 0.4f : 0);
                break;
            case 3:
                playerData.DefHardWorkAlways += (HWP.y) * 0.25f + (playerData.IsPassiveGetList[24] ? 0.15f : 0) + (playerData.IsPassiveGetList[20] ? 0.4f : 0) + (playerData.IsPassiveGetList[29] ? 0.4f : 0);
                break;
            case 4:
                playerData.SpAHardWorkAlways += (HWP.y) * 0.25f + (playerData.IsPassiveGetList[24] ? 0.15f : 0) + (playerData.IsPassiveGetList[21] ? 0.4f : 0) + (playerData.IsPassiveGetList[29] ? 0.4f : 0);
                break;
            case 5:
                playerData.SpDHardWorkAlways += (HWP.y) * 0.25f + (playerData.IsPassiveGetList[24] ? 0.15f : 0) + (playerData.IsPassiveGetList[22] ? 0.4f : 0) + (playerData.IsPassiveGetList[29] ? 0.4f : 0);
                break;
            case 6:
                playerData.SpeHardWorkAlways += (HWP.y) * 0.25f + (playerData.IsPassiveGetList[24] ? 0.15f : 0) + (playerData.IsPassiveGetList[23] ? 0.4f : 0) + (playerData.IsPassiveGetList[29] ? 0.4f : 0);
                break;
        }

        float LuckHPDPlusPer = Random.Range(0.0f, 1.0f);
        if (!playerData.IsPassiveGetList[17])
        {
            if (LuckHPDPlusPer >= 0.85f && LuckHPDPlusPer <= 0.95f)
            {
                playerData.LuckHardWorkAlways += 0.5f;
            }
            else if (LuckHPDPlusPer > 0.95f && LuckHPDPlusPer <= 0.985f)
            {
                playerData.LuckHardWorkAlways += 0.75f;
            }
            else if (LuckHPDPlusPer > 0.985f && LuckHPDPlusPer <= 1.0f)
            {
                playerData.LuckHardWorkAlways += 1.25f;
            }
        }
        else
        {
            if (LuckHPDPlusPer >= 0.6f && LuckHPDPlusPer <= 0.86f)
            {
                playerData.LuckHardWorkAlways += 0.5f;
            }
            else if (LuckHPDPlusPer > 0.86f && LuckHPDPlusPer <= 0.96f)
            {
                playerData.LuckHardWorkAlways += 0.75f;
            }
            else if (LuckHPDPlusPer > 0.96f && LuckHPDPlusPer <= 1.0f)
            {
                playerData.LuckHardWorkAlways += 1.25f;
            }
        }

        ReFreshAbllityPoint();
    }





    //����һ���ı侭��ֵ�ĺ���ChangeEx
    public void ChangeEx(int ChangePoint)
    {

        //���ı�ľ���ֵΪ��ʱ���Ըı�
        if (ChangePoint > 0)
        {
            //�ı侭��ֵ
            nowEx = Mathf.Clamp((int)(nowEx + (ChangePoint * (playerData.IsPassiveGetList[12]?1.25:1) * (playerData.IsPassiveGetList[29] ? 1.25 : 1) )), 0, 80000);

            //�����ǰ����ֵС���������ֵ��������UI����仯���
            if (nowEx < maxEx)
            {
                UIExpBar.Instance.Per = (float)nowEx / (float)maxEx;
                UIExpBar.Instance.ExpUp();
            }
            //�����ǰ����ֵ���������ֵ�����ӵȼ������ٵ�ǰ����ֵ�����ı������ֵ����������������������UI    
            else
            {
                for (; nowEx >= maxEx; UIExpBar.Instance.Icount++)
                {
                    Level++;

                    nowEx = nowEx - maxEx;
                    maxEx = Exp[Level - 1];
                    int HpBewton = maxHp - nowHp;
                    ReFreshAbllityPoint();
                    nowHp = maxHp-HpBewton;
                    
                    HpBewton = 0;
                    UIHealthBar.Instance.Per = (float)nowHp / (float)maxHp;
                    UIHealthBar.Instance.NowHpText.text = string.Format("{000}", nowHp);
                    UIHealthBar.Instance.MaxHpText.text = string.Format("{000}", maxHp);
                    
                    UIHealthBar.Instance.ChangeHpUp();

                }

                UIExpBar.Instance.Per = (float)nowEx / (float)maxEx;
                UIExpBar.Instance.ExpUpOverflow();
            }
            
        }

    }

    public void ReFreshAbllityPoint()
    {
        MaxHpForLevel(Level);
        maxHp = (int)((maxHp + playerData.HPHardWorkAlways + playerData.HPHardWorkJustOneRoom) * Mathf.Pow(1.2f, (playerData.HPBounsAlways + playerData.HPBounsJustOneRoom)));
        AtkAbility = (int)((AbilityForLevel(Level, AtkPlayerPoint) + playerData.AtkHardWorkAlways + playerData.AtkHardWorkJustOneRoom) * Mathf.Pow(1.2f , (playerData.AtkBounsAlways + playerData.AtkBounsJustOneRoom)));
        SpAAbility = (int)((AbilityForLevel(Level, SpAPlayerPoint) + playerData.SpAHardWorkAlways + playerData.SpAHardWorkJustOneRoom) * Mathf.Pow(1.2f , (playerData.SpABounsAlways + playerData.SpABounsJustOneRoom))); 
        DefAbility = (int)((AbilityForLevel(Level, DefPlayerPoint) + playerData.DefHardWorkAlways + playerData.DefHardWorkJustOneRoom) * Mathf.Pow(1.2f , (playerData.DefBounsAlways + playerData.DefBounsJustOneRoom))); 
        SpDAbility = (int)((AbilityForLevel(Level, SpdPlayerPoint) + playerData.SpDHardWorkAlways + playerData.SpDHardWorkJustOneRoom) * Mathf.Pow(1.2f , (playerData.SpDBounsAlways + playerData.SpDBounsJustOneRoom)));
        SpeedAbility = (int)((AbilityForLevel(Level, SpeedPlayerPoint) + playerData.SpeHardWorkAlways + playerData.SpeHardWorkJustOneRoom) * Mathf.Pow(1.2f , (playerData.SpeBounsAlways + playerData.SpeBounsJustOneRoom)));
        speed = Mathf.Clamp(!isSleepDone?((MoveSpePlayerPoint + playerData.MoveSpeHardWorkAlways + playerData.MoveSpeHardWorkJustOneRoom) * Mathf.Pow(1.2f, (playerData.MoveSpwBounsAlways + playerData.MoveSpeBounsJustOneRoom))):0.5f , 0.2f , 10);
        LuckPoint = (int)((LuckPlayerPoint + playerData.LuckHardWorkAlways + playerData.LuckHardWorkJustOneRoom) * Mathf.Pow(1.2f, (playerData.LuckBounsAlways + playerData.LuckBounsJustOneRoom)));
        UIHealthBar.Instance.MaxHpText.text = maxHp.ToString();
        UIHealthBar.Instance.NowHpText.text = nowHp.ToString();
        float x = (UIHealthBar.Instance.Per - (float)nowHp / (float)maxHp);
        if (x > 0) {
            UIHealthBar.Instance.Per = (float)nowHp / (float)maxHp;
            UIHealthBar.Instance.ChangeHpDown();
        }else if (x < 0)
        {
            UIHealthBar.Instance.Per = (float)nowHp / (float)maxHp;
            UIHealthBar.Instance.ChangeHpUp();
        }
        
        
    }




    public void LearnNewSkill()
    {
        if (LevelForSkill == GetSkillLevel[levelChecker])
        {
            if (levelChecker < SkillList.Count)
            {
                if (Skill01 == null || Skill02 == null || Skill03 == null || Skill04 == null)
                {
                    uIPanelGwtNewSkill.NewSkillPanzelJump(SkillList[levelChecker]);
                }
                else
                {
                    uIPanelGwtNewSkill.NewSkillPanzelJump(SkillList[levelChecker]);
                    Debug.Log(SkillList[levelChecker]);
                }
            }
            else { Debug.Log("Error : SkillList OverFlow"); }
            levelChecker++;
        }
    }

    public void GetNewSkill(Skill NewSkill , int SkillNumber)
    {
        switch (SkillNumber)
        {
            case 1:
                Skill01 = NewSkill;
                skillBar01.GetSkill(Skill01);
                break;
            case 2:
                Skill02 = NewSkill;
                skillBar02.GetSkill(Skill02);
                break;
            case 3:
                Skill03 = NewSkill;
                skillBar03.GetSkill(Skill03);
                break;
            case 4:
                Skill04 = NewSkill;
                skillBar04.GetSkill(Skill04);
                break;
        }
    }




    //����һ������������ʱ������ʾ����ʹ�ü��ܣ��޷��ƶ���״̬
    public void SkillNow()
    {
        isSkill = false;
    }


    //����һ��������������ʱ���似��01
    public void GetAnimationSkill01Launch()
    {
        if (isSkill01lunch) {
            LaunchSkill01(look);
            isSkill01lunch = false;
        }
        if (isSkill02lunch)
        {
            LaunchSkill02(look);
            isSkill02lunch = false;
        }
        if (isSkill03lunch)
        {
            LaunchSkill03(look);
            isSkill03lunch = false;
        }
        if (isSkill04lunch)
        {
            LaunchSkill04(look);
            isSkill04lunch = false;
        }
    }

    //����һ�������������ô˺���ʱ������ʽ�������ɼ���1
    protected void LaunchSkill01(Vector2 Direction)
    {
        Skill skillObj = null;
        if (!Skill01.isDirection) {
            if (Direction.Equals(new Vector2(1, 0))) {
                skillObj = Instantiate(Skill01, rigidbody2D.position + (Vector2.up * 0.4f) + (Direction * 1), Quaternion.Euler(0, 0, 0));
            } else if (Direction.Equals(new Vector2(-1, 0)))
            {
                skillObj = Instantiate(Skill01, rigidbody2D.position + (Vector2.up * 0.4f) + (Direction * 1), Quaternion.Euler(0, 0, 180));
            }
            else if (Direction.Equals(new Vector2(0, 1)))
            {
                skillObj = Instantiate(Skill01, rigidbody2D.position + (Vector2.up * 0.4f) + (Direction * 1), Quaternion.Euler(0, 0, 90));
            }
            else if (Direction.Equals(new Vector2(0, -1)))
            {
                skillObj = Instantiate(Skill01, rigidbody2D.position + (Vector2.up * 0.4f) + (Direction * 1), Quaternion.Euler(0, 0, 270));
            }
        }
        else
        {
            skillObj = Instantiate(Skill01, rigidbody2D.position, Quaternion.identity, Skill01.isMoveWithPlayer ? null : transform);
        }
        skillObj.player = this;
    }

    protected void LaunchSkill02(Vector2 Direction)
    {
        Skill skillObj = null;
        if (!Skill02.isDirection) {
            if (Direction.Equals(new Vector2(1, 0)))
            {
                skillObj = Instantiate(Skill02, rigidbody2D.position + (Vector2.up * 0.4f) + (Direction * 1), Quaternion.Euler(0, 0, 0));
            }
            else if (Direction.Equals(new Vector2(-1, 0)))
            {
                skillObj = Instantiate(Skill02, rigidbody2D.position + (Vector2.up * 0.4f) + (Direction * 1), Quaternion.Euler(0, 0, 180));
            }
            else if (Direction.Equals(new Vector2(0, 1)))
            {
                skillObj = Instantiate(Skill02, rigidbody2D.position + (Vector2.up * 0.4f) + (Direction * 1), Quaternion.Euler(0, 0, 90));
            }
            else if (Direction.Equals(new Vector2(0, -1)))
            {
                skillObj = Instantiate(Skill02, rigidbody2D.position + (Vector2.up * 0.4f) + (Direction * 1), Quaternion.Euler(0, 0, 270));
            }
        }
        else
        {
            skillObj = Instantiate(Skill02, rigidbody2D.position, Quaternion.identity, Skill02.isMoveWithPlayer?null:transform);
        }
        skillObj.player = this;
    }

    protected void LaunchSkill03(Vector2 Direction)
    {
        Skill skillObj = null;
        if (!Skill03.isDirection)
        {
            if (Direction.Equals(new Vector2(1, 0)))
            {
                skillObj = Instantiate(Skill03, rigidbody2D.position + (Vector2.up * 0.4f) + (Direction * 1), Quaternion.Euler(0, 0, 0));
            }
            else if (Direction.Equals(new Vector2(-1, 0)))
            {
                skillObj = Instantiate(Skill03, rigidbody2D.position + (Vector2.up * 0.4f) + (Direction * 1), Quaternion.Euler(0, 0, 180));
            }
            else if (Direction.Equals(new Vector2(0, 1)))
            {
                skillObj = Instantiate(Skill03, rigidbody2D.position + (Vector2.up * 0.4f) + (Direction * 1), Quaternion.Euler(0, 0, 90));
            }
            else if (Direction.Equals(new Vector2(0, -1)))
            {
                skillObj = Instantiate(Skill03, rigidbody2D.position + (Vector2.up * 0.4f) + (Direction * 1), Quaternion.Euler(0, 0, 270));
            }
        }
        else
        {
            skillObj = Instantiate(Skill03, rigidbody2D.position, Quaternion.identity, Skill03.isMoveWithPlayer ? null : transform);
        }
        skillObj.player = this;
    }

    protected void LaunchSkill04(Vector2 Direction)
    {
        Skill skillObj = null;
        if (!Skill04.isDirection)
        {
            if (Direction.Equals(new Vector2(1, 0)))
            {
                skillObj = Instantiate(Skill04, rigidbody2D.position + (Vector2.up * 0.4f) + (Direction * 1), Quaternion.Euler(0, 0, 0));
            }
            else if (Direction.Equals(new Vector2(-1, 0)))
            {
                skillObj = Instantiate(Skill04, rigidbody2D.position + (Vector2.up * 0.4f) + (Direction * 1), Quaternion.Euler(0, 0, 180));
            }
            else if (Direction.Equals(new Vector2(0, 1)))
            {
                skillObj = Instantiate(Skill04, rigidbody2D.position + (Vector2.up * 0.4f) + (Direction * 1), Quaternion.Euler(0, 0, 90));
            }
            else if (Direction.Equals(new Vector2(0, -1)))
            {
                skillObj = Instantiate(Skill04, rigidbody2D.position + (Vector2.up * 0.4f) + (Direction * 1), Quaternion.Euler(0, 0, 270));
            }
        }
        else
        {
            skillObj = Instantiate(Skill04, rigidbody2D.position, Quaternion.identity, Skill04.isMoveWithPlayer ? null : transform);
        }
        skillObj.player = this;
    }

    Vector3Int TpVector3;
    public void TP(Vector3Int TPVector3)
    {
        TpVector3 = TPVector3;
        animator.SetTrigger("TP");
        isTP = true;
        isTPMove = true;
    }

    
    public void TPStart()
    {
        TPMask.In.TPStart = true;
        TPMask.In.BlackTime = 1.15f;
    }
    public void TPDoit()
    {
        gameObject.transform.position = new Vector3(30.0f * TpVector3.x, 24.0f * TpVector3.y - 2.0f, 0);
        GameObject Maincamera = GameObject.FindWithTag("MainCamera");
        Maincamera.transform.position = new Vector3(30.0f * TpVector3.x, 24.0f * TpVector3.y + 0.7f, -10);
        UiMiniMap.Instance.MiniMapMove(new Vector3(NowRoom.x - TpVector3.x, NowRoom.y - TpVector3.y, 0));
        NowRoom = TpVector3;
        InANewRoom = true;
        UiMiniMap.Instance.SeeMapOver();
    }
    public void TPEnd()
    {
        isTP = false;
        isTPMove = false;
    }

    void InstanceNature(int NatureIndex)
    {
        switch (NatureIndex)
        {
            case 0:    break;
            case 1:  playerData.AtkBounsAlways++; playerData.DefBounsAlways--; break;
            case 2:  playerData.AtkBounsAlways++; playerData.SpABounsAlways--; break;
            case 3:  playerData.AtkBounsAlways++; playerData.SpDBounsAlways--; break;
            case 4:  playerData.AtkBounsAlways++; playerData.SpeBounsAlways--; break;

            case 5:  playerData.DefBounsAlways++; playerData.AtkBounsAlways--; break;
            case 6:    break;
            case 7:  playerData.DefBounsAlways++; playerData.SpABounsAlways--; break;
            case 8:  playerData.DefBounsAlways++; playerData.SpDBounsAlways--; break;
            case 9:  playerData.DefBounsAlways++; playerData.SpeBounsAlways--; break;

            case 10: playerData.SpABounsAlways++; playerData.AtkBounsAlways--; break;
            case 11: playerData.SpABounsAlways++; playerData.DefBounsAlways--; break;
            case 12:   break;
            case 13: playerData.SpABounsAlways++; playerData.SpDBounsAlways--; break;
            case 14: playerData.SpABounsAlways++; playerData.SpeBounsAlways--; break;

            case 15: playerData.SpDBounsAlways++; playerData.AtkBounsAlways--; break;
            case 16: playerData.SpDBounsAlways++; playerData.DefBounsAlways--; break;
            case 17: playerData.SpDBounsAlways++; playerData.SpABounsAlways--; break;
            case 18:   break;
            case 19: playerData.SpDBounsAlways++; playerData.SpeBounsAlways--; break;

            case 20: playerData.SpeBounsAlways++; playerData.AtkBounsAlways--; break;
            case 21: playerData.SpeBounsAlways++; playerData.DefBounsAlways--; break;
            case 22: playerData.SpeBounsAlways++; playerData.SpABounsAlways--; break;
            case 23: playerData.SpeBounsAlways++; playerData.SpDBounsAlways--; break;
            case 24:   break;
        }
    }

}
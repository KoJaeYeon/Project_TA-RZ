using System.Collections;
using System;
using System.ComponentModel;
using UnityEngine;
using Zenject;
using QFX.SFX;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IHit
{
    #region InJect
    [Inject] public DataManager dataManager { get; }
    [Inject] public CameraRoot cameraRoot { get; }
    [Inject] public DrainSystem drainSystem { get; }
    [Inject] public PoolManager poolManager { get; }
    [Inject] public SaveManager saveManager { get; }
    [Inject] public UIEvent uiEvent { get; }
    #endregion

    #region PlayerComponent
    private PlayerInputSystem _inputSystem;
    private PlayerStateMachine _state;
    private SFX_MotionCloner _cloner;
    private Camera _camera;
    private BlueChipSystem _blueChipSystem;


    public IInteractable Interactable { get; set; }
    public PC_Common_Stat _playerStat { get; private set; } = new PC_Common_Stat();
    public PC_PassiveData PlayerPassiveData { get; private set; } = new PC_PassiveData();
    public Save_PlayerData SavePlayerData { get; set; } = new Save_PlayerData();
    public PC_Level _PC_Level { get; private set; } = new PC_Level();
    public Camera MainCamera { get { return _camera; } }
    public BlueChipSystem BlueChipSystem { get { return _blueChipSystem; } } 
    #endregion

    #region PropChanged
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    #region PlayerValue
    [Header("공격 사거리 배율 조절")]
    [Range(0, 2005)][SerializeField] float[] playerAttackRange = new float[4] {1,1,1,1};
    public float[] PlayerAttackRange { get { return playerAttackRange; } }
    [Header("플레이어 넉백 거리 조절")]
    [SerializeField] float knockback_Horizontal_Distance = 1f;
    [SerializeField] float knockback_Vertical_Distance = 1f;
    public float Knockback_Horizontal_Distance { get { return knockback_Horizontal_Distance; } }
    public float Knockback_Vertical_Distance { get { return knockback_Vertical_Distance; } }

    [Header("카메라 감도 조절")]
    [Range(0,1f)][SerializeField] float rotate_Camera_Speed = 1f;
    [Range(0,15F)][SerializeField] float rotate_Camera_Max = 15f;
    public float Rotate_Camera_Speed { get { return rotate_Camera_Speed; } set { rotate_Camera_Speed = value; } }
    public float Rotate_Camera_Max { get { return rotate_Camera_Max; } }

    public SFX_MotionCloner Cloner { get { return _cloner; } }
    int _currentAmmo;
    float _currentHP;
    float _currentSkill;
    float _currentStamina;
    float _currentAtk;
    float _currentSpeed;
    float _passiveAtk_Power;
    float _attackMultiplier = 1;
    float _hpMultiplier = 1;

    public bool IsActiveStaminaRecovery { get; set; } = true;
    bool _isPlayerAlive = true;
    public int[] BluechipSkillLevels { get; set; } = new int[4];
    public bool[] IsSkillAcitve { get; set; } = new bool[4] { false, false, false, false };
    public float[] _skillCounption { get; private set; } = new float[4] { 25, 50, 75, 100 };
    public bool IsSkillAnimationEnd { get; set; } = true;
    public int CurrentLevel { get; set; }
    public bool _playerReady { get; set; }

    //블루칩 기능
    #region
    public bool IsPlayerFourthAttackDrainAvailable { get; set; } = false;
    #endregion
    public float CurrentAtk
    {
        get
        {
            float atk_mul = SavePlayerData.PassiveDieMode == 2 ? _attackMultiplier : 1;
            return _currentAtk * atk_mul * PlayerPassiveData.BAttack;
        }
        set
        {
            if (_currentAtk == value)
                return;

            _currentAtk = value;
            OnPropertyChanged(nameof(CurrentAtk));
        }
    }

    public float CurrentSpeed
    {
        get { return _currentSpeed * PlayerPassiveData.AddMove; }
        set
        {
            if(_currentSpeed == value)
                return;

            _currentSpeed = value;
            OnPropertyChanged(nameof(CurrentSpeed));
        }
    }

    public float CurrentHP
    {
        get { return _currentHP; }
        set
        {
            if (_currentHP == value)
                return;

            _currentHP = value > HP ? HP : value;

            OnPropertyChanged(nameof(CurrentHP));
        }
    }

    public float CurrentSkill
    {
        get { return _currentSkill; }
        set
        {
            if (value <= 0)
            {
                value = 0;
            }
            else if (value > 100)
            {
                value = 100;
            }

            if (_currentSkill == value)
                return;

            //스킬 사용 시 자원 획득 불가
            foreach (bool isActive in IsSkillAcitve)
            {
                if (isActive == true) return;
            }

            _currentSkill = value;
            OnPropertyChanged(nameof(CurrentSkill));
        }
    }
    public float CurrentStamina
    {
        get { return _currentStamina; }
        set
        {
            if (value <= 0)
            {
                value = 0;
            }
            else if (value > 100)
            {
                value = 100;
            }

            if (_currentStamina == value)
                return;

            _currentStamina = value;

            if (_currentStamina == 0)
            {
                StartCoroutine(StaminaDelay());
            }

            OnPropertyChanged(nameof(CurrentStamina));
        }
    }

    public float CurrentStaminaRecovery
    {
        get { return _playerStat.Stamina_Gain + PlayerPassiveData.AddStaRecovery; }
    }

    public int CurrentAmmo
    {
        get { return _currentAmmo; }
        set
        {
            if (value <= 0)
            {
                value = 0;
            }
            else if (value > CurrentResourceOwn)
            {
                value = CurrentResourceOwn;
            }
            if (_currentAmmo == value)
                return;

            _currentAmmo = value;

            if (_currentAmmo == CurrentResourceOwn)
            {
                drainSystem.OnSetActiveDrainSystem(false);
            }
            else
            {
                drainSystem.OnSetActiveDrainSystem(true);
            }
            OnPropertyChanged(nameof(CurrentAmmo));
        }
    }
    public int CurrentResourceOwn
    {
        get { return _playerStat.Resource_Own_Num + PlayerPassiveData.AddOwnNum; }
    }
    public float HP
    {
        get
        {
            float hp_mul = SavePlayerData.PassiveDieMode == 2 ? _hpMultiplier : 1;
            return _playerStat.HP * PlayerPassiveData.AddHP * hp_mul;
        }
        set
        {
            if (_playerStat.HP == value)
                return;

            _playerStat.HP = value;
            OnPropertyChanged(nameof(HP));
        }
    }

    public float PassiveAtk_Power
    {
        get { return _passiveAtk_Power * PlayerPassiveData.EAttack; }
        set
        {
            if(_passiveAtk_Power == value)
            {
                return;
            }

            _passiveAtk_Power = value;
            OnPropertyChanged(nameof(PassiveAtk_Power));
        }
    }

    public bool IsPlayerAlive
    {
        get { return _isPlayerAlive; }
        set
        {
            if (_isPlayerAlive == value)
                return;

            _isPlayerAlive = value;
            OnPropertyChanged(nameof(IsPlayerAlive));
        }
    }

    public bool IsNext { get; set; }
    #endregion

    private void Awake()
    {
        InitializeComponent();
        InitializePlayer();
        InitializeState();
    }

    private void Update()
    {
        StaminaRecovery();
        if(Input.GetKeyDown(KeyCode.N))
        {
            CurrentSkill++;
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            Debug.Log(SavePlayerData);
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            CurrentSkill = 25;
        }
        else if(Input.GetKeyDown(KeyCode.F5))
        {
            CurrentSkill = 25;
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            CurrentSkill = 50;
        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            CurrentSkill = 75;
        }
        else if (Input.GetKeyDown(KeyCode.F8))
        {
            CurrentSkill = 100;
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            CurrentHP += 10;
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            CurrentHP += 100;
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            CurrentAmmo += 1;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            CurrentAmmo += 10;
        }
        else if (Input.GetKeyDown(KeyCode.F10))
        {
            SceneManager.LoadScene(0);
        }
        else if (Input.GetKeyDown(KeyCode.F11))
        {
            IsPlayerFourthAttackDrainAvailable = !IsPlayerFourthAttackDrainAvailable;
        }
        else if (Input.GetKeyDown(KeyCode.F12))
        {
            SceneManager.LoadScene(1);
        }
    }

    private void InitializePlayer()
    {
        Load_SaveData();        
        StartCoroutine(LoadStat());
        StartCoroutine(LoadSkillCounsumption());
    }

    private void InitializeComponent()
    {
        _cloner = gameObject.GetComponentInChildren<SFX_MotionCloner>();
        _state = gameObject.AddComponent<PlayerStateMachine>();
        _inputSystem = gameObject.AddComponent<PlayerInputSystem>();
        _blueChipSystem = gameObject.GetComponentInChildren<BlueChipSystem>();
        _camera = Camera.main;
    }

    private void InitializeState()
    {
        _state.AddState(State.Idle, new PlayerIdle(this));
        _state.AddState(State.Run, new PlayerRun(this));
        _state.AddState(State.Dash, new PlayerDash(this));
        _state.AddState(State.Drain, new PlayerDrain(this));        
        _state.AddState(State.FirstComboAttack, new PlayerFirstComboAttack(this));
        _state.AddState(State.SecondComboAttack, new PlayerSecondComboAttack(this));
        _state.AddState(State.ThirdComboAttack, new PlayerThirdComboAttack(this));
        _state.AddState(State.FourthComboAttack, new PlayerFourthComboAttack(this));
        _state.AddState(State.Skill, new PlayerSkill(this));
        _state.AddState(State.Hit, new PlayerHit(this));    
        _state.AddState(State.KnockBack, new PlayerKnockBack(this));
        _state.AddState(State.Death, new PlayerDeath(this));
    }
    #region LocoMotion
    public void Interact()
    {
        Interactable?.Interact();
    }

    public bool StaminaCheck()
    {
        return CurrentStamina > 0;
    }
    IEnumerator StaminaDelay()
    {
        IsActiveStaminaRecovery = false;
        yield return new WaitForSeconds(3f);
        IsActiveStaminaRecovery = true;
    }

    private void StaminaRecovery()
    {
        if (IsActiveStaminaRecovery == true)
        {
            CurrentStamina += CurrentStaminaRecovery * Time.deltaTime;
        }
    }

    public bool SkillCheck()
    {
        return CurrentSkill >= _skillCounption[0];
    }

    public void Set_PC_Level(PC_Level _PC_Level)
    {
        this._PC_Level = _PC_Level;
    }

    public void AllgnToCamera()
    {
        transform.rotation = cameraRoot.transform.rotation;

        cameraRoot.transform.rotation = transform.rotation;
    }
    #endregion
    #region Achievement
    public void OnCalled_Achieve_BossKilled()
    {
        if (SavePlayerData.BossKilled == true) return;

        SavePlayerData.BossKilled = true;
        string achieveText = dataManager.GetString("UI_Achievement_Text_Content_0");
        uiEvent.ActiveAchievementUI(achieveText);
    }

    public void OnCalled_Achieve_Charged()
    {
        if (SavePlayerData.Charged == true) return;

        SavePlayerData.Charged = true;
        string achieveText = dataManager.GetString("UI_Achievement_Text_Content_1");
        uiEvent.ActiveAchievementUI(achieveText);
    }

    public void OnCalled_Achieve_NoHitBossKilled()
    {
        if (SavePlayerData.NoHitBossKilled == true) return;

        SavePlayerData.NoHitBossKilled = true;
        string achieveText = dataManager.GetString("UI_Achievement_Text_Content_2");
        uiEvent.ActiveAchievementUI(achieveText);
    }

    public void OnCalled_Achieve_AllUnlockPassive()
    {
        if (SavePlayerData.AllUnlockPassive == true) return;

        foreach(int i in SavePlayerData.passiveIndex)
        {
            if (i != 2) return;
        }

        SavePlayerData.AllUnlockPassive = true;
        string achieveText = dataManager.GetString("UI_Achievement_Text_Content_3");
        uiEvent.ActiveAchievementUI(achieveText);
    }

    public void OnCalled_Achieve_RedChip()
    {
        if (SavePlayerData.RedChip == true) return;

        SavePlayerData.RedChip = true;
        string achieveText = dataManager.GetString("UI_Achievement_Text_Content_4");
        uiEvent.ActiveAchievementUI(achieveText);
    }

    public void OnCalled_Achieve_MonsterKilled()
    {
        SavePlayerData.Kill += 1;
        if (SavePlayerData.EnemyKilled == true) return;

        int needKill = int.Parse(dataManager.GetStringValue("Achievement_Content_5"));
        if(SavePlayerData.Kill >= needKill)
        {
            SavePlayerData.EnemyKilled = true;
            string achieveText = string.Format(dataManager.GetString("UI_Achievement_Text_Content_5"), needKill.ToString());
            uiEvent.ActiveAchievementUI(achieveText);
        }
    }

    public void OnCalled_Achieve_ResourceGet()
    {
        SavePlayerData.Resource += 1;
        if (SavePlayerData.ResourceGet == true) return;

        int needResource = int.Parse(dataManager.GetStringValue("Achievement_Content_6"));
        if (SavePlayerData.Resource >= needResource)
        {
            SavePlayerData.ResourceGet = true;
            string achieveText = string.Format(dataManager.GetString("UI_Achievement_Text_Content_6"), needResource.ToString());
            uiEvent.ActiveAchievementUI(achieveText);
        }
    }
    #endregion
    #region Quest
    public Action<bool> DashQuest { get; private set; }
    public Action<bool> HitQuest { get; private set; }
    public Action<bool> SkillQuest { get; private set; }

    public void RemoveAllQuest()
    {
        DashQuest = null;
        HitQuest = null;
        SkillQuest = null;
    }
    public bool ClearQuest()
    {
        if(DashQuest == null && HitQuest == null && SkillQuest == null ) return false;

        DashQuest?.Invoke(true);
        HitQuest?.Invoke(true);
        SkillQuest?.Invoke(true);
        RemoveAllQuest();
        return true;
    }

    public void RegisterDashQuest(Action<bool> callback)
    {
        DashQuest += callback;
    }

    public void RegisterHitQuest(Action<bool> callback)
    {
        HitQuest += callback;
    }

    public void RegisterSkillQuest(Action<bool> callback)
    {
        SkillQuest += callback;
    }


    #endregion
    #region PlayerLoad
    void Load_SaveData()
    {
        PlayerPassiveData._player = this;
        if (saveManager.saveIndex == -1) return;
        SavePlayerData = saveManager.Load(saveManager.saveIndex);
    }
    IEnumerator LoadStat()
    {
        // 패시브 스킬 데이터 로드
        yield return new WaitWhile(() => {
            return dataManager.GetData("G251") == null;
        });
        var data = dataManager.GetData("G251") as Passive2_Value;
        _attackMultiplier = data.Value[0];
        _hpMultiplier = data.Value[1];

        yield return new WaitWhile(() => {
            Debug.Log("Player의 데이터를 받아오는 중입니다.");
            return dataManager.GetStat("P101") == null;
        });

        var stat = dataManager.GetStat("P101") as PC_Common_Stat;
        _playerStat = stat;
        Debug.Log("Player의 스탯을 성공적으로 받아왔습니다.");
        CurrentAtk = _playerStat.Atk_Power;
        CurrentHP = _playerStat.HP;
        HP = _playerStat.HP;
        CurrentSpeed = _playerStat.Move_Speed;
        CurrentStamina = 100;
        CurrentSkill = 0;
        CurrentAmmo = 0;
        PassiveAtk_Power = _playerStat.PassiveAtk_Power;

        OnPropertyChanged(nameof(CurrentResourceOwn));

        _playerReady = true;

        yield break;
    }

    IEnumerator LoadSkillCounsumption()
    {
        while (true)
        {
            var data = dataManager.GetData("S201") as PC_Skill;
            if (data == null)
            {
                Debug.Log("Player의 스킬 소모값을 받아오지 못했습니다.");
                yield return new WaitForSeconds(1f);
            }
            else
            {
                _skillCounption[0] = data.Skill_Gauge_Consumption;
                for (int i = 1; i < 4; i++)
                {
                    string idStr = $"S20{1 + i}";
                    data = dataManager.GetData(idStr) as PC_Skill;
                    _skillCounption[i] = data.Skill_Gauge_Consumption;
                }
                Debug.Log("Player의 스킬 소모값을 성공적으로 받아왔습니다.");
                yield break;
            }

        }
    }
    public void OnSave_PlayerData()
    {
        saveManager.Save(SavePlayerData);
    }
    #endregion
    #region Gizmos
    //Ground, AttackGizmos
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        if (_state.CurrentState is PlayerFirstComboAttack)
        {
            PlayerFirstComboAttack firstCombo = _state.CurrentState as PlayerFirstComboAttack;

            Vector3 boxposition = firstCombo._boxPosition;
            Vector3 boxSize = firstCombo._boxSize;
            Quaternion boxrotation = transform.rotation;
            float _attackRange_Multiplier = firstCombo._attackRange_Multiplier;

            Matrix4x4 originalMatrix = Gizmos.matrix;

            Gizmos.matrix = Matrix4x4.TRS(boxposition, boxrotation, Vector3.one);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, boxSize * _attackRange_Multiplier);

            Gizmos.matrix = originalMatrix; 
        }
        else if(_state.CurrentState is PlayerSecondComboAttack)
        {
            PlayerSecondComboAttack secondCombo = _state.CurrentState as PlayerSecondComboAttack;

            Vector3 boxposition = secondCombo._boxPosition;
            Vector3 boxSize = secondCombo._boxSize;
            Quaternion boxrotation = transform.rotation;
            float _attackRange_Multiplier = secondCombo._attackRange_Multiplier;

            Matrix4x4 originalMatrix = Gizmos.matrix;

            Gizmos.matrix = Matrix4x4.TRS(boxposition, boxrotation, Vector3.one);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, boxSize* _attackRange_Multiplier);

            Gizmos.matrix = originalMatrix;
        }
        else if(_state.CurrentState is PlayerThirdComboAttack)
        {
            PlayerThirdComboAttack thirdCombo = _state.CurrentState as PlayerThirdComboAttack;

            Gizmos.color = Color.red;

            // 부채꼴의 중심 방향
            Vector3 forward = transform.forward;
            float angle = thirdCombo._angle -10;

            // 부채꼴의 외곽을 그리기 위한 각도 계산
            for (int i = 0; i <= thirdCombo._segments; i++)
            {
                if (i != 0 && i != thirdCombo._segments) continue;
                float currentAngle = -angle / 2 + (angle / thirdCombo._segments) * i;
                Quaternion rotation = Quaternion.AngleAxis(currentAngle, transform.up);

                // 외곽 지점 계산
                Vector3 direction = rotation * forward;
                Vector3 endPoint = transform.position + direction * thirdCombo._range;

                // 기즈모 선 그리기
                Gizmos.DrawLine(transform.position, endPoint);
            }

            // 부채꼴의 바닥 원호를 그리기 위한 계산
            for (int i = 0; i < thirdCombo._segments; i++)
            {
                float angle1 = -angle  / 2 + (angle  / thirdCombo._segments) * i;
                float angle2 = -angle / 2 + (angle / thirdCombo._segments) * (i + 1);

                Quaternion rot1 = Quaternion.AngleAxis(angle1, transform.up);
                Quaternion rot2 = Quaternion.AngleAxis(angle2, transform.up);

                Vector3 point1 = transform.position + (rot1 * forward) * thirdCombo._range;
                Vector3 point2 = transform.position + (rot2 * forward) * thirdCombo._range;

                Gizmos.DrawLine(point1, point2);
            }

            Vector3 bottom = transform.position - Vector3.up * thirdCombo._height /2;
            Vector3 top = transform.position + Vector3.up * thirdCombo._height / 2;

            Gizmos.DrawLine(bottom, bottom + forward * thirdCombo._range);
            Gizmos.DrawLine(top, top + forward * thirdCombo._range);

            Gizmos.DrawLine(bottom + forward * thirdCombo._range, top + forward * thirdCombo._range);

            Gizmos.DrawWireSphere(transform.position, thirdCombo._range);
        }

        else if(_state.CurrentState is PlayerFourthComboAttack)
        {
            PlayerFourthComboAttack fourthCombo = _state.CurrentState as PlayerFourthComboAttack;

            Vector3 boxposition = fourthCombo._boxPosition;
            Vector3 boxSize = fourthCombo._boxSize;
            Quaternion boxrotation = transform.rotation;
            float _attackRange_Multiplier = fourthCombo._attackRange_Multiplier;

            Matrix4x4 originalMatrix = Gizmos.matrix;

            Gizmos.matrix = Matrix4x4.TRS(boxposition, boxrotation, Vector3.one);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, boxSize * _attackRange_Multiplier);

            Gizmos.matrix = originalMatrix;
        }

        Vector3 GizmoPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Gizmos.DrawWireSphere(GizmoPosition, 0.2f);

    }
    #endregion
    #region Hit
    public void Hit(float damage, float paralysisTime, Transform attackTrans)
    {
        if (CheckAttackIsAvailable(attackTrans.position) == false)
        {
            return;
        }

        if (_currentHP <= 0)
        {
            _state.ChangeState(State.Death);
            return;
        }
        else
        {
            CurrentHP -= damage;

            HitQuest?.Invoke(false);

            PlayerHit.Pc_Stiff_Time = _playerStat.Damaged_Stiff_T;

            _state.OnDamagedStateChange();
        }
    }
    public void ApplyKnockback(float knockBackTime, Transform otherTrans)
    {
        if (CheckAttackIsAvailable(otherTrans.position) == false)
        {
            return;
        }

        PlayerKnockBack._knockBackPosition = otherTrans.position;
        PlayerKnockBack.Pc_Knock_Back_Time = _playerStat.Damaged_KnockBack_T;

        _state.OnKnockBackStateChange();

    }

    bool CheckAttackIsAvailable(Vector3 attackPos)
    {
        if (_isPlayerAlive == false)
        {
            return false;
        }
        else if (IsSkillAcitve[2] == true)
        {
            Vector3 directionToAttack = attackPos - transform.position;

            // 방향이 전방인지 확인 (플레이어의 전방은 transform.forward와 비교)
            float dotProduct = Vector3.Dot(transform.forward, directionToAttack.normalized);

            // dotProduct가 0보다 크면 전방
            if (dotProduct >= 0)
            {
                return false;
            }
        }

        return true;
    }
    #endregion
}

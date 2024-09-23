public class PC_PassiveData
{
    public Player _player {get; set;}
    private float _bAttack = 1;
    private float _eAttack = 1;
    private float _addHp = 1;
    private float _addMove = 1;
    private float _addStaRecovery;
    private int _addOwnNum;
    public float BAttack
    {
        get { return _bAttack; }
        set
        {
            _bAttack = value;
        }
    }
    public float EAttack
    {
        get { return _eAttack; }
        set
        {
            _eAttack = value;
        }
    }
    public float AddHP
    {
        get { return _addHp; }
        set
        {
            _addHp = value;
            _player.CurrentHP = _player.HP;
        }
    }
    public float AddMove
    {
        get { return _addMove; }
        set
        {
            _addMove = value;
        }
    }
    public float AddStaRecovery
    {
        get { return _addStaRecovery; }
        set
        {
            _addStaRecovery = value;
        }
    }
    public int AddOwnNum
    {
        get { return _addOwnNum; }
        set
        {
            _addOwnNum = value;
            _player.OnPropertyChanged(nameof(Player.CurrentResourceOwn));
        }

    }
}

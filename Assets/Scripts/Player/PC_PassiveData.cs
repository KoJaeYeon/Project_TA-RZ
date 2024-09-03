public class PC_PassiveData
{
    public Player _player {get; set;}
    private int _addOwnNum;
    public int addOwnNum
    {
        get { return _addOwnNum; }
        set
        {
            _addOwnNum = value;
            _player.OnPropertyChanged(nameof(Player.CurrentResourceOwn));
        }

    }
}

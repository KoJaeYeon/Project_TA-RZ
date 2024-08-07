using ViewModel;

public class PlayerUIViewModel : ViewModelBase
{
    private float _hp;
    private float _stamina;
    private float _skills;
    private int _nowbullets;
    private float _maxHp;
    public float Hp
    {
        get { return _hp; }
        set
        {
            if (_hp == value)
                return;

            _hp = value;
            OnPropertyChanged(nameof(Hp));
        }
    }

    public float Stamina
    {
        get { return _stamina; }
        set
        {
            if (_stamina == value)
                return;

            _stamina = value;
            OnPropertyChanged(nameof(Stamina));
        }
    }

    public float Skills
    {
        get { return _skills; }
        set
        {
            if (_skills == value)
                return;

            _skills = value;
            OnPropertyChanged(nameof(Skills));
        }
    }

    public int Nowbullets
    {
        get { return _nowbullets; }
        set
        {
            if (_nowbullets == value)
                return;

            _nowbullets = value;
            OnPropertyChanged(nameof(Nowbullets));
        }
    }

}

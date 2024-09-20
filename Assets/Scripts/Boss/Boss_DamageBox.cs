using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Boss_DamageBox : MonoBehaviour
{
    private enum Pattern
    { 
        Gimmick,
        RootAttack,
        RootSmash,
        Rush,
        Swing,
        Smash
    }

    [SerializeField] private Pattern _pattern;

    private float asdf = 1f;

    [SerializeField] BossController _boss;

    [Inject] Player _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (_pattern)
            {
                case Pattern.Gimmick:
                    _player.Hit(_boss.gimmickDamage, asdf, _boss.transform);
                    break;

                case Pattern.RootAttack:
                    _player.Hit(_boss.rootDamage, asdf, _boss.transform);
                    break;

                case Pattern.RootSmash:
                    _player.Hit(_boss.smashDamage, asdf, _boss.transform);
                    break;

                case Pattern.Rush:
                    _player.Hit(_boss.rushDamage, asdf, _boss.transform);
                    break;

                case Pattern.Swing:
                    _player.Hit(_boss.swingDamage, asdf, _boss.transform);
                    break;

                case Pattern.Smash:
                    _player.Hit(_boss.smashDamage, asdf, _boss.transform);
                    break;
            }
            Debug.Log(this.gameObject.name + other.gameObject.name);
            _boss.isPlayerHit = true;
        }
    }
}

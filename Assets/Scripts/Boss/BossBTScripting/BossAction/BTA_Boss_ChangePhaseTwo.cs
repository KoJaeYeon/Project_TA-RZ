using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_Boss_ChangePhaseTwo : BossAction
{
    public override void OnStart()
    {
        _owner.InActiveOnPhaseTwo();
    }
}

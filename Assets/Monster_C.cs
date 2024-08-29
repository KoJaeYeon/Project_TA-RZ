using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_C : Monster
{
    [SerializeField] private GameObject atkPrefab;
    [SerializeField] private GameObject explosionPrefab;

    [SerializeField] private float growDuration;
    public float LastAttackTime { get; set; }
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        idStr = "E103";
    }

    public void StartAtk()
    {
        atkPrefab.SetActive(true);
    }
}

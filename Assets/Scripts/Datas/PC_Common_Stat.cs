using System;

public struct PC_Common_Stat
{
    public int Id { get; set; }
    public string Type { get; set; }
    public float AttackPower { get; set; }
    public float Health { get; set; }
    public float MoveSpeed { get; set; }
    public int AmmoCapacity { get; set; }
    public float StaminaRecoveryRate { get; set; }

    public PC_Common_Stat(int id, string type, float attackPower, float health, float moveSpeed, int ammoCapacity, float staminaRecoveryRate)
    {
        Id = id;
        Type = type;
        AttackPower = attackPower;
        Health = health;
        MoveSpeed = moveSpeed;
        AmmoCapacity = ammoCapacity;
        StaminaRecoveryRate = staminaRecoveryRate;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Type: {Type}, Attack Power: {AttackPower}, Health: {Health}, Move Speed: {MoveSpeed}, Ammo Capacity: {AmmoCapacity}, Stamina Recovery Rate: {StaminaRecoveryRate}";
    }
}
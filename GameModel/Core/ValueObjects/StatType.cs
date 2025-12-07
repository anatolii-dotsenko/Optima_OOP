namespace GameModel.Core.ValueObjects
{
    public enum StatType
    {
        Health,
        MaxHealth,
        Attack,      // Physical Attack
        MagicPower,  // Magic Attack
        Armor,       // Flat Physical Defense
        Resistance,  // % Physical Mitigation (0-100)
        MagicResist, // % Magic Mitigation (0-100)
        Penetration, // % Resistance Negation
        Speed        // For turn order
    }
}
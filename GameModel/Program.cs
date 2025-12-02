using System;
using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    // ---------------------------------------------------------
    // Helper Structures
    // ---------------------------------------------------------

    /// <summary>
    /// Represents a special move or spell using a Delegate for the effect.
    /// </summary>
    /// <param name="Name">Name of the ability.</param>
    /// <param name="Effect">The logic to execute (User, Target).</param>
    public record Ability(string Name, Action<Character, Character> Effect);

    /// <summary>
    /// Represents an item that modifies stats or grants abilities.
    /// </summary>
    public class Item
    {
        public string Name { get; init; }
        public int BonusHealth { get; init; }
        public int BonusAttack { get; init; }
        public int BonusArmor { get; init; }
        public Ability? GrantedAbility { get; init; }

        public Item(string name, int health = 0, int attack = 0, int armor = 0, Ability? ability = null)
        {
            Name = name;
            BonusHealth = health;
            BonusAttack = attack;
            BonusArmor = armor;
            GrantedAbility = ability;
        }
    }

    // ---------------------------------------------------------
    // Core Character Logic
    // ---------------------------------------------------------

    /// <summary>
    /// Abstract base class defining common character behaviors.
    /// </summary>
    public abstract class Character
    {
        public string Name { get; }
        public int Health { get; protected set; }
        public int Armor { get; protected set; }
        public int AttackPower { get; protected set; }
        
        protected List<Ability> Abilities { get; } = new();

        protected Character(string name, int health, int armor, int attack)
        {
            Name = name;
            Health = health;
            Armor = armor;
            AttackPower = attack;
        }

        /// <summary>
        /// Deals damage to the target based on AttackPower and target's Armor.
        /// </summary>
        public void Attack(Character target)
        {
            int damage = Math.Max(0, AttackPower - target.Armor);
            target.TakeDamage(damage);
            Console.WriteLine($"{Name} attacks {target.Name} for {damage} dmg.");
        }

        /// <summary>
        /// Reduces health ensuring it doesn't drop below zero.
        /// </summary>
        public void TakeDamage(int amount) => Health = Math.Max(0, Health - amount);

        /// <summary>
        /// Restores health points.
        /// </summary>
        public void Heal(int amount)
        {
            Health += amount;
            Console.WriteLine($"{Name} heals for {amount} HP. Current: {Health}");
        }

        /// <summary>
        /// Temporarily increases armor (simplified logic).
        /// </summary>
        public void Defend(int amount)
        {
            Armor += amount;
            Console.WriteLine($"{Name} increases armor by {amount}.");
        }

        /// <summary>
        /// Equips an item, applying its stats and abilities immediately.
        /// </summary>
        public void Equip(Item item)
        {
            Health += item.BonusHealth;
            AttackPower += item.BonusAttack;
            Armor += item.BonusArmor;
            if (item.GrantedAbility != null) Abilities.Add(item.GrantedAbility);
            
            Console.WriteLine($"{Name} equipped {item.Name}.");
        }

        /// <summary>
        /// Executes a specific ability from the character's list.
        /// </summary>
        public void UseAbility(string abilityName, Character target)
        {
            var ability = Abilities.FirstOrDefault(a => a.Name == abilityName);
            if (ability != null)
            {
                Console.WriteLine($"{Name} casts {abilityName}!");
                ability.Effect(this, target);
            }
            else
            {
                Console.WriteLine($"{Name} doesn't know {abilityName}.");
            }
        }

        /// <summary>
        /// Executes the character's class-specific special move.
        /// </summary>
        public abstract void UniqueAbility(Character target);
    }

    // ---------------------------------------------------------
    // Concrete Classes
    // ---------------------------------------------------------

    /// <summary>
    /// High armor character with a heavy strike.
    /// </summary>
    public class Warrior : Character
    {
        public Warrior(string name) : base(name, 120, 10, 15) { }

        public override void UniqueAbility(Character target)
        {
            // Power Strike: Deals double damage ignoring armor logic locally
            int dmg = AttackPower * 2;
            target.TakeDamage(dmg);
            Console.WriteLine($"{Name} uses Power Strike on {target.Name} for {dmg} dmg!");
        }
    }

    /// <summary>
    /// Low armor character with high magic damage.
    /// </summary>
    public class Mage : Character
    {
        public Mage(string name) : base(name, 80, 3, 10) { }

        public override void UniqueAbility(Character target)
        {
            // Fireball: Fixed high damage
            target.TakeDamage(25);
            Console.WriteLine($"{Name} casts Fireball on {target.Name} for 25 dmg!");
        }
    }
}
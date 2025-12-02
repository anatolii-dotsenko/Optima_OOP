using GameModel.Characters;
using GameModel.Items;
using GameModel.Logging;
using GameModel.Combat;

class Program
{
    static void Main()
    {
        ICombatLogger logger = new ConsoleLogger();
        CombatSystem combat = new CombatSystem(logger);

        // Create characters
        var warrior = new Warrior("Thorin");
        var mage = new Mage("Elira");

        // Equip items
        warrior.EquipItem(new Sword());
        warrior.EquipItem(new Shield());

        mage.EquipItem(new MagicAmulet());

        // Show start
        logger.Write("=== BATTLE STARTS ===");

        // Round 1
        combat.Attack(warrior, mage);
        combat.UseAbility(mage, warrior, "Fireball");

        // Round 2
        combat.UseAbility(warrior, mage, "Power Strike");
        combat.Heal(mage, 10);

        // Round 3
        combat.Attack(warrior, mage);
        combat.Attack(mage, warrior);

        logger.Write("=== BATTLE ENDS ===");
    }
}

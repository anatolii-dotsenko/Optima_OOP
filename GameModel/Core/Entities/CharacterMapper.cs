using GameModel.Content.Characters;
using GameModel.Content.Items;
using GameModel.Core.Data;
using GameModel.Core.ValueObjects;
using GameModel.Infrastructure.Network.Dtos;

namespace GameModel.Core.Entities
{
    public static class CharacterMapper
    {
        /// <summary>
        /// Maps external API data (Genshin Impact) to an internal game entity.
        /// </summary>
        public static Character MapToGameEntity(GenshinCharacterDto dto)
        {
            Character character;

            // Simplified mapping: Catalyst users -> Mages, everyone else -> Warriors
            if (dto.Weapon.Equals("Catalyst", StringComparison.OrdinalIgnoreCase))
            {
                character = new Mage(dto.Name);
            }
            else
            {
                character = new Warrior(dto.Name);
            }

            // Generate stats based on Rarity (3-5 stars) to make imported characters unique
            int baseHp = dto.Rarity * 30;
            int baseAtk = dto.Rarity * 5;
            int baseDef = dto.Rarity * 2;

            // Use the exposed method to override default class stats
            character.SetBaseStat(StatType.MaxHealth, baseHp);
            character.SetBaseStat(StatType.Health, baseHp);
            character.SetBaseStat(StatType.Attack, baseAtk);
            character.SetBaseStat(StatType.Armor, baseDef);

            return character;
        }

        /// <summary>
        /// Rehydrates a Character entity from a saved Data Transfer Object (DTO).
        /// Used by the Save/Load system.
        /// </summary>
        public static Character MapFromData(CharacterData data)
        {
            Character character;

            // 1. Restore the correct class type
            if (data.ClassType.Contains("Mage", StringComparison.OrdinalIgnoreCase))
            {
                character = new Mage(data.Name);
            }
            else
            {
                // Default to Warrior if unknown or explicitly Warrior
                character = new Warrior(data.Name);
            }

            // 2. Restore Stats
            foreach (var stat in data.BaseStats)
            {
                character.SetBaseStat(stat.Key, stat.Value);
            }

            // Ensure current health is consistent (handle death state if needed)
            character.SetBaseStat(StatType.Health, data.CurrentHealth);

            // 3. Restore Inventory (Simple Factory Logic)
            // In a larger system, this would be a separate ItemFactory or ID-based lookup.
            foreach (var itemName in data.InventoryItems)
            {
                if (itemName.Contains("Sword", StringComparison.OrdinalIgnoreCase))
                    character.EquipItem(new Sword());
                else if (itemName.Contains("Shield", StringComparison.OrdinalIgnoreCase))
                    character.EquipItem(new Shield());
                else if (itemName.Contains("Wand", StringComparison.OrdinalIgnoreCase))
                    character.EquipItem(new LightningWand());
                else if (itemName.Contains("Amulet", StringComparison.OrdinalIgnoreCase))
                    character.EquipItem(new MagicAmulet());
            }

            return character;
        }
    }
}
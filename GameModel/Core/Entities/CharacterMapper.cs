using System;
using GameModel.Content.Characters;
using GameModel.Core.ValueObjects;
using GameModel.Infrastructure.Network.Dtos;

namespace GameModel.Core.Entities
{
    public static class CharacterMapper
    {
        public static Character MapToGameEntity(GenshinCharacterDto dto)
        {
            Character character;

            // Mapping logic: Catalyst users become Mages, others become Warriors (simplified)
            if (dto.Weapon.Equals("Catalyst", StringComparison.OrdinalIgnoreCase))
            {
                character = new Mage(dto.Name);
            }
            else
            {
                character = new Warrior(dto.Name);
            }

            // Generate stats based on Rarity (3-5 stars)
            // Example: 5 star = 150 HP, 4 star = 120 HP
            int baseHp = dto.Rarity * 30; 
            int baseAtk = dto.Rarity * 5; 

            // Reflection or specific method needed to modify protected stats outside constructor
            // Assuming we add a public method strictly for initialization or use a specific subclass
            // For now, let's pretend we can modify them via a new method in Character or just keep defaults + bonus
                        
            return character;
        }
    }
}
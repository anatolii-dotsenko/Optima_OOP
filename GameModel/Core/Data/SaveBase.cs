using System;
using System.Collections.Generic;

namespace GameModel.Core.Data
{
    public class SaveBase
    {
        public DateTime SaveDate { get; set; }
        public List<CharacterData> Characters { get; set; } = new();
        // Other aspects can be added, for example, world state or history
    }
}
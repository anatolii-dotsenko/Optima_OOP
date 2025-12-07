using System.Collections.Generic;
using GameModel.Core.Contracts;

namespace GameModel.Infrastructure.CLI.Strategies
{
    public class TextStrategy : BaseStrategy
    {
        public override string Name => "Text Editor Mode";

        public TextStrategy()
        {
            // Commands will be registered by GameBuilder
        }
    }
}
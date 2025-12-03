namespace GameModel.Commands
{
    /// <summary>
    /// Contract for all executable commands in the game.
    /// Implementations must be stateless or dependency-injected.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// The keyword users type to invoke this command (e.g., "attack", "heal").
        /// </summary>
        string Keyword { get; }

        /// <summary>
        /// Brief description of what the command does.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Executes the command with the given arguments.
        /// </summary>
        /// <param name="args">Command-line arguments (e.g., target name, amount).</param>
        void Execute(string[] args);
    }
}

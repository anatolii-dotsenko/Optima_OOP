using System;

namespace GameModel.Text
{
    /// <summary>
    /// Інтерфейс для будь-якого текстового елемента.
    /// Додано Id та Name для підтримки CLI команд.
    /// </summary>
    public interface IText
    {
        Guid Id { get; }
        string Name { get; }
        
        /// <summary>
        /// Посилання на батьківський контейнер для команди 'up'.
        /// </summary>
        Container? Parent { get; set; }

        string Render();
    }
}
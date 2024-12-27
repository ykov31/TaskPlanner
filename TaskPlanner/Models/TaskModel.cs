using System;

namespace TaskPlanner.Models
{
    public class TaskModel
    {
        public int Id { get; set; } // Уникальный идентификатор задачи
        public string Title { get; set; } // Название задачи
        public string Description { get; set; } // Описание задачи
        public int Priority { get; set; } // Приоритет: 1 - Высокий, 2 - Средний, 3 - Низкий
        public DateTime DueDate { get; set; } // Срок выполнения задачи
    }
}

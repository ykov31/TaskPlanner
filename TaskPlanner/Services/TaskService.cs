using Microsoft.Data.Sqlite;
using System;
using System.Collections.ObjectModel;
using TaskPlanner.Models;

namespace TaskPlanner.Services
{
    public class TaskService
    {
        private const string ConnectionString = "Data Source=tasks.db";

        // Конструктор: создаем таблицу, если ее нет
        public TaskService()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Tasks (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Description TEXT,
                    Priority INTEGER NOT NULL,
                    DueDate TEXT NOT NULL
                )";
            var command = new SqliteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();
        }

        // Получение всех задач
        public ObservableCollection<TaskModel> GetTasks()
        {
            var tasks = new ObservableCollection<TaskModel>();
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string selectQuery = "SELECT * FROM Tasks ORDER BY Priority, DueDate";
            var command = new SqliteCommand(selectQuery, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                tasks.Add(new TaskModel
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    Priority = reader.GetInt32(3),
                    DueDate = DateTime.Parse(reader.GetString(4))
                });
            }

            return tasks;
        }

        // Добавление новой задачи
        public void AddTask(TaskModel task)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string insertQuery = "INSERT INTO Tasks (Title, Description, Priority, DueDate) VALUES (@Title, @Description, @Priority, @DueDate)";
            var command = new SqliteCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@Title", task.Title);
            command.Parameters.AddWithValue("@Description", task.Description);
            command.Parameters.AddWithValue("@Priority", task.Priority);
            command.Parameters.AddWithValue("@DueDate", task.DueDate.ToString("yyyy-MM-dd"));
            command.ExecuteNonQuery();
        }

        // Обновление существующей задачи
        public void UpdateTask(TaskModel task)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string updateQuery = "UPDATE Tasks SET Title = @Title, Description = @Description, Priority = @Priority, DueDate = @DueDate WHERE Id = @Id";
            var command = new SqliteCommand(updateQuery, connection);
            command.Parameters.AddWithValue("@Id", task.Id);
            command.Parameters.AddWithValue("@Title", task.Title);
            command.Parameters.AddWithValue("@Description", task.Description);
            command.Parameters.AddWithValue("@Priority", task.Priority);
            command.Parameters.AddWithValue("@DueDate", task.DueDate.ToString("yyyy-MM-dd"));
            command.ExecuteNonQuery();
        }

        // Удаление задачи
        public void DeleteTask(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string deleteQuery = "DELETE FROM Tasks WHERE Id = @Id";
            var command = new SqliteCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }
    }
}

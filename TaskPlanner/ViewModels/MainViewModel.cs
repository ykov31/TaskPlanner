using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using TaskPlanner.Models;
using TaskPlanner.Services;

namespace TaskPlanner.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly TaskService _taskService;

        [ObservableProperty]
        private ObservableCollection<TaskModel> tasks;

        [ObservableProperty]
        private TaskModel selectedTask;

        public MainViewModel()
        {
            _taskService = new TaskService();
            Tasks = _taskService.GetTasks();
        }

        [RelayCommand]
        private void AddTask()
        {
            var newTask = new TaskModel { Priority = 3, DueDate = DateTime.Now.AddDays(1) };
            var addWindow = new Views.AddTaskWindow(newTask);

            if (addWindow.ShowDialog() == true)
            {
                _taskService.AddTask(newTask);
                Tasks.Add(newTask);
            }
        }

        [RelayCommand]
        private void EditTask()
        {
            if (SelectedTask == null) return;

            var editWindow = new Views.AddTaskWindow(SelectedTask);
            if (editWindow.ShowDialog() == true)
            {
                _taskService.UpdateTask(SelectedTask);
            }
        }

        [RelayCommand]
        private void DeleteTask()
        {
            if (SelectedTask == null) return;

            _taskService.DeleteTask(SelectedTask.Id);
            Tasks.Remove(SelectedTask);
        }
    }
}

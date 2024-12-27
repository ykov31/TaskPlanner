using System.Windows;
using TaskPlanner.Models;

namespace TaskPlanner.Views
{
    public partial class AddTaskWindow : Window
    {
        public AddTaskWindow(TaskModel task)
        {
            InitializeComponent();
            DataContext = task;
        }
    }
}

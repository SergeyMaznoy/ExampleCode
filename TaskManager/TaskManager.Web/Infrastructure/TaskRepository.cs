using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Domain;
using WebMatrix.WebData;
using System.Data.Entity;

namespace TaskManager.Web.Infrastructure
{
    public class TaskRepository : ITaskRepository
    {
        TasksDatabase _context;

        public TaskRepository(TasksDatabase context)
        {
            _context = context;
        }

        IQueryable<Task> ITaskRepository.All
        {
            get { return _context.Tasks; }
        }

        void ITaskRepository.InsertOrUpdate(Task task)
        {
            if (task.Id == default(int))
            {
                _context.Tasks.Add(task);
            }
            else
            {
                _context.Entry(task).State = EntityState.Modified; 
            }
        }

        void ITaskRepository.Remove(Task task)
        {
            _context.Entry(task).State = EntityState.Deleted;
        }

        void ITaskRepository.Save()
        {
            _context.SaveChanges();
        }
    }
}
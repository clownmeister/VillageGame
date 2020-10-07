using Industry;
using Manager;
using UnityEngine;

namespace Task
{
    public abstract class AbstractTask
    {
        protected readonly IndustryCategory taskCategory;
        protected readonly TaskManager taskManager;
        public abstract GameObject Assignee { get; set; }
        public abstract void Start(GameObject assignee);
        public abstract void Cancel();
        public abstract void Finish();

        public virtual bool IsAssigned()
        {
            return null != Assignee;
        }

        protected AbstractTask(TaskManager taskManager, IndustryCategory taskTaskCategory)
        {
            this.taskManager = taskManager;
            this.taskCategory = taskTaskCategory;
        }
    }
}
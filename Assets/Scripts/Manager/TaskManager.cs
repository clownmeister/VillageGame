using System;
using Industry;
using Resource;
using Task;
using UnityEngine;

namespace Manager
{
    public class TaskManager : MonoBehaviour
    {
        //debug
        public GameObject debugHarvester;
        //debug cooldown for assign task later create ticksystem
        public float debugAssignCooldownSeconds = 1;
        public float debugLastAssigmentMark = 0;
        
        public TaskList planned;
        public TaskList inProgress;

        private void Start()
        {
            planned = new TaskList();
            inProgress = new TaskList();
        }

        private void Update()
        {
            if (Time.time > debugLastAssigmentMark)
            {
                Debug.Log("Task assign check");
                debugLastAssigmentMark = Time.time + debugAssignCooldownSeconds;
                TryAssignTask();
            }
            
        }

        private void TryAssignTask()
        {
            if (planned.Count != 0 && !planned[0].IsAssigned())
            {
                foreach (AbstractTask task in inProgress)
                {
                    if (task.Assignee == debugHarvester)
                    {
                        return;
                    }
                }
                StartTask(planned[0], debugHarvester);
            }
        }
        
        public void PlanHarvestResourceTask(IndustryCategory taskCategory, ResourceNode resourceEntity)
        {
            foreach (AbstractTask task in planned)
            {
                if (task.GetType() == typeof(HarvestTask) && ((HarvestTask)task).ResourceNode == resourceEntity)
                {
                    Debug.LogWarning("Trying to create harvest task for already planned resource.");
                    return;
                }
            }
            switch (taskCategory)
            {
                case IndustryCategory.Mining:
                    planned.Add(new HarvestTask(this, taskCategory, resourceEntity));
                    break;
                case IndustryCategory.Woodcutting:
                    planned.Add(new HarvestTask(this, taskCategory, resourceEntity));
                    break;
                case IndustryCategory.Farming:
                    break;
                case IndustryCategory.Cooking:
                    break;
                case IndustryCategory.Construction:
                    break;
                case IndustryCategory.Repair:
                    break;
                case IndustryCategory.Researching:
                    break;
                case IndustryCategory.Hunting:
                    break;
                case IndustryCategory.Crafting:
                    break;
                case IndustryCategory.Smelting:
                    break;
                case IndustryCategory.Blacksmithing:
                    break;
                case IndustryCategory.Electronics:
                    break;
                case IndustryCategory.Tailoring:
                    break;
                case IndustryCategory.Gathering:
                    break;
                case IndustryCategory.Art:
                    break;
                case IndustryCategory.Medical:
                    break;
                case IndustryCategory.Social:
                    break;
                case IndustryCategory.Handling:
                    break;
                case IndustryCategory.Logistics:
                    break;
                case IndustryCategory.Cleaning:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(taskCategory), taskCategory, null);
            }
            Debug.Log("Planned tasks: " + planned.Count);
        }

        public void StartTask(AbstractTask task, GameObject assignee)
        {
            if (null != task && planned.Contains(task) && !task.IsAssigned())
            {
                planned.Remove(task);
                inProgress.Add(task);
                task.Start(assignee);
            }
        }

        public void CancelTask(AbstractTask task)
        {
            inProgress.Remove(task);
            planned.Add(task);
        }

        public void FinishTask(AbstractTask task)
        {
            planned.Remove(task);
            inProgress.Remove(task);
        }
    }
}
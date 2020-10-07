using Industry;
using UnityEngine;

namespace Task
{
    [CreateAssetMenu(fileName = "NewTaskIndustryActionData", menuName = "Tasks/TaskIndustryActionData", order = 0)]

    public class TaskIndustryActionData : ScriptableObject
    {
        public string taskActionName;
        public IndustryCategory category;
        public Sprite taskActionButtonImage;
        public Sprite taskActionCursorImage;
    }
}
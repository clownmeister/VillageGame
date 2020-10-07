using System.Collections.Generic;
using Industry;
using Resource;
using Task;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace UserInterface
{
    public class QuickTaskBar : AbstractElementUI
    {
        public override Transform ParentUI { get; set; }
        public GameObject actionPrefabUI;
        
        private List<TaskIndustryActionData> _activeTaskActionData;
        private Transform _actionListParentUI;

        public override void Init()
        {
            base.Init();
            _actionListParentUI = ParentUI.Find("_actionListParent");
            _activeTaskActionData = new List<TaskIndustryActionData>();
            UpdateElementsUI();
        }

        public override void UpdateElementsUI()
        {
            foreach (Transform child in _actionListParentUI)
            {
                child.GetComponent<Button>().onClick.RemoveAllListeners();
                Destroy(child.gameObject);
            }

            foreach (TaskIndustryActionData activeActionData in _activeTaskActionData)
            {
                GameObject newAction = GameObject.Instantiate(actionPrefabUI, _actionListParentUI);
                Text actionText = newAction.GetComponentInChildren<Text>();
                Image actionImage = newAction.GetComponentsInChildren<Image>()[1];
                Button actionButton = newAction.GetComponent<Button>();

                actionText.text = activeActionData.taskActionName.ToUpper();
                actionImage.sprite = activeActionData.taskActionButtonImage;
                actionButton.onClick.AddListener(() => CreateQuickTask(activeActionData.category));
            }
        }
        
        public override void ChangeState(bool state)
        {
            
        }

        public void UpdateData(List<ResourceNode> entities)
        {
            _activeTaskActionData.Clear();
            
            foreach (ResourceNode resourceEntity in entities)
            {
                TaskIndustryActionData data = resourceEntity.taskActionData;
                if (!_activeTaskActionData.Contains(data))
                {
                    _activeTaskActionData.Add(data);
                }
            }
            
            UpdateElementsUI();
        }

        public void CreateQuickTask(IndustryCategory taskCategory)
        {
            Debug.Log("test create task");
            foreach (ResourceNode entity in selectableManager.SelectedResourceEntities)
            {
                //TODO WORKOUT GENERIC TASK
                taskManager.PlanHarvestResourceTask(taskCategory, entity);
            }
        }
    }
}
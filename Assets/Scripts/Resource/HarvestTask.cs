using Entity;
using Industry;
using Manager;
using Task;
using UnityEngine;
using Navigation;

namespace Resource
{
    public class HarvestTask : AbstractTask
    {
        private readonly ResourceNode _resourceNode;
        public ResourceNode ResourceNode => _resourceNode;

        private Navigator _navigator;
        private HarvestTaskHandler _harvester;

        public override GameObject Assignee { get; set; }

        public HarvestTask(
            TaskManager taskManager,
            IndustryCategory taskTaskCategory,
            ResourceNode resourceNode
        )
            : base(taskManager, taskTaskCategory)
        {
            _resourceNode = resourceNode;
        }

        private void OnNavigationCancel()
        {
            Cancel();
        }

        private void OnNavigationFinish()
        {
            _harvester.ResourceNode = _resourceNode;
            _harvester.Handle();
            Debug.Log("Harvested " + _resourceNode.resourceData.resourceName + " resource node");
            Finish();
        }

        private void NavigationStartListening()
        {
            _navigator.onFinished.AddListener(OnNavigationFinish);
            _navigator.onCanceled.AddListener(OnNavigationCancel);
        }

        private void NavigationStopListening()
        {
            _navigator.onFinished.RemoveListener(OnNavigationFinish);
            _navigator.onCanceled.RemoveListener(OnNavigationCancel);
        }
        
        public override void Start(GameObject assignee)
        {
            _harvester = assignee.GetComponent<Human>().Harvester;
            _navigator = assignee.GetComponent<Navigator>();
            
            Collider collider = _resourceNode.GetComponent<Collider>();
            
            if (null == collider || null == _harvester || null == _navigator)
            {
                Debug.LogWarning("Invalid task data");
                Cancel();
                return;
            }

            if (!_navigator.StartMoving(_resourceNode.transform.position, collider))
            {
                Debug.LogWarning("Invalid task path");
                Cancel();
                return;
            }

            NavigationStartListening();

            Assignee = assignee;
        }

        public override void Cancel()
        {
            Assignee = null;
            taskManager.CancelTask(this);
            NavigationStopListening();
            Debug.LogWarning("Canceled task");
        }

        public override void Finish()
        {
            Assignee = null;
            taskManager.FinishTask(this);
            NavigationStopListening();
            Debug.Log("Finished task");
        }
    }
}
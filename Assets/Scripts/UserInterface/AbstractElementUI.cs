using System;
using Manager;
using UnityEngine;

namespace UserInterface
{
    public abstract class AbstractElementUI : MonoBehaviour
    {
        protected TaskManager taskManager;
        protected SelectableManager selectableManager;
        public abstract Transform ParentUI { get; set; }

        public void Start()
        {
            taskManager = FindObjectOfType<TaskManager>();
            selectableManager = FindObjectOfType<SelectableManager>();
            Init();
        }

        public virtual void Init()
        {
            ParentUI = this.transform;
        }
        public abstract void UpdateElementsUI();
        public abstract void ChangeState(bool state);
    }
}
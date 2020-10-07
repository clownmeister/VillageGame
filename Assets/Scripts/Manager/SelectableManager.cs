using System.Collections.Generic;
using Entity;
using Resource;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Manager
{
    [RequireComponent(typeof(UserInterfaceManager))]
    public class SelectableManager : MonoBehaviour
    {
        private UserInterfaceManager _interfaceManager;
        private EventSystem _eventSystem;

        private List<IEntity> _selectedEntities;
        private List<AbstractWorkerEntity> _selectedHumanoidEntities;
        private List<ResourceNode> _selectedResourceEntities;

        public List<ResourceNode> SelectedResourceEntities => _selectedResourceEntities;

        public LayerMask navigationLayerMask;
        public LayerMask selectionLayerMask;

        private void Start()
        {
            _interfaceManager = GetComponent<UserInterfaceManager>();
            _eventSystem = FindObjectOfType<EventSystem>();

            _selectedEntities = new List<IEntity>();
            _selectedHumanoidEntities = new List<AbstractWorkerEntity>();
            _selectedResourceEntities = new List<ResourceNode>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                bool addMod = Input.GetKey(KeyCode.LeftShift);
                bool redMod = Input.GetKey(KeyCode.LeftControl);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, 100, selectionLayerMask))
                {
                    if (!_eventSystem.IsPointerOverGameObject())
                    {
                        //TODO: SOLVE UI LAYER. ADD ONCLICK LISTENER AND CREATE TASK
                        HandleHit(hit, addMod, redMod);
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, 1000, navigationLayerMask))
                {
                    if (!_eventSystem.IsPointerOverGameObject())
                    {
                        foreach (AbstractWorkerEntity humanoid in _selectedHumanoidEntities)
                        {
                            humanoid.Navigator.StartMoving(hit.point);
                        }
                    }
                }
            }
        }

        private void HandleHit(RaycastHit hit, bool addToGroup = false, bool removeFromGroup = false)
        {
            if (hit.transform.GetComponent<IEntity>() is IEntity generalEntity)
            {
                ResolveEntity(generalEntity, addToGroup, removeFromGroup);
            }
            else
            {
                if (!addToGroup && !removeFromGroup)
                {
                    ClearAllSelected();
                }
            }

            _interfaceManager.UpdateQuickTaskBar(_selectedResourceEntities);
        }

        private void ClearAllSelected()
        {
            foreach (IEntity entity in _selectedEntities)
            {
                if (null != entity)
                {
                    entity.RadiusGraphics.EnableDraw(false);
                }
            }

            _selectedEntities.Clear();
            _selectedHumanoidEntities.Clear();
            _selectedResourceEntities.Clear();
        }

        private void ResolveEntity(IEntity entity, bool addToGroup = false, bool removeFromGroup = false)
        {
            //TODO: create entityList with private lists per type. Move logic from here
            if (addToGroup && !_selectedEntities.Contains(entity))
            {
                _selectedEntities.Add(entity);
                entity.RadiusGraphics.EnableDraw(true);
                EntityToTypeList(entity);
            }
            else if (removeFromGroup && _selectedEntities.Contains(entity))
            {
                _selectedEntities.Remove(entity);
                entity.RadiusGraphics.EnableDraw(false);
                EntityToTypeList(entity, true);
            }
            else
            {
                ClearAllSelected();
                _selectedEntities.Add(entity);
                entity.RadiusGraphics.EnableDraw(true);
                EntityToTypeList(entity);
            }
        }

        private void EntityToTypeList(IEntity entity, bool remove = false)
        {
            if (entity.GetType() == typeof(AbstractWorkerEntity))
            {
                switch (remove)
                {
                    case false:
                        _selectedHumanoidEntities.Add((AbstractWorkerEntity) entity);
                        break;
                    case true:
                        _selectedHumanoidEntities.Remove((AbstractWorkerEntity) entity);
                        break;
                }
            }
            else if (entity.GetType() == typeof(ResourceNode))
            {
                switch (remove)
                {
                    case false:
                        _selectedResourceEntities.Add((ResourceNode) entity);
                        break;
                    case true:
                        _selectedResourceEntities.Remove((ResourceNode) entity);
                        break;
                }
            }
        }
    }
}
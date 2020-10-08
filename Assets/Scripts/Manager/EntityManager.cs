using System;
using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace Manager
{
    public class EntityManager
    {
        private List<IEntity> EntityList { get; set; }
        private List<AbstractWorkerEntity> WorkerList { get; set; }

        public IEntity Get(int id)
        {
            foreach (IEntity entity in EntityList)
            {
                if (entity.Id == id)
                {
                    return entity;
                }
            }

            throw new UnityException("Entity with id:" + id + " not found");
        }

        public List<IEntity> GetEntities(int[] ids)
        {
            List<IEntity> result = new List<IEntity>();
            foreach (int id in ids)
            {
                result.Add(Get(id));
            }

            return result;
        }

        public void AddEntity(IEntity entity)
        {
            if (HasEntity(entity))
            {
                EntityList.Add(entity);
            }

            switch (entity)
            {
                case AbstractWorkerEntity worker:
                    WorkerList.Add(worker);
                    break;
            }
        }

        public void RemoveEntity(IEntity entity)
        {
            if (HasEntity(entity))
            {
                EntityList.Remove(entity);
            }

            switch (entity)
            {
                case AbstractWorkerEntity worker:
                    WorkerList.Remove(worker);
                    break;
            }
        }

        public bool HasEntity(IEntity entity)
        {
            return EntityList.Contains(entity);
        }
    }
}
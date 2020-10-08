using Resource;
using UnityEngine;

namespace Entity
{
    public abstract class AbstractWorkerEntity : AbstractMovingEntity
    {
        public bool IsAssigned { get; set; }
        public HarvestTaskHandler Harvester { get; set; }

        public override void Start()
        {
            base.Start();
            Harvester = new HarvestTaskHandler(this);
        }
    }
}
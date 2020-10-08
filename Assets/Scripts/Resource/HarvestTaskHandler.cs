using System;
using Entity;
using Manager;
using Task;

namespace Resource
{
    public class HarvestTaskHandler : AbstractTaskHandler
    {
        private AbstractWorkerEntity _worker;

        public HarvestTaskHandler(AbstractWorkerEntity worker)
        {
            this._worker = worker;
        }

        public ResourceNode ResourceNode { get; set; }

        public override void Handle()
        {
            if (null == ResourceNode)
            {
                throw new Exception("Can not access ResourceNode before initialization.");
            }

            Harvest();
        }

        private void Harvest()
        {
            //waitForTicks based on harvestSpeed * resource.Hardness
            // yield return new WaitForSeconds(harvestSpeed * resourceNode.resourceData.hardness);
            ResourceNode.HarvestYield(_worker.transform.position);
        }
    }
}
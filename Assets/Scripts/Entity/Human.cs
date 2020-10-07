using Resource;
using Task;

namespace Entity
{
    public class Human : AbstractWorkerEntity
    {
        public HarvestTaskHandler HarvestTaskHandler { get; }

        public Human(HarvestTaskHandler harvestTaskHandler)
        {
            HarvestTaskHandler = harvestTaskHandler;
        }
    }
}
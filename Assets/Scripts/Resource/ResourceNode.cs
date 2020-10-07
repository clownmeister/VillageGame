using Entity;
using Industry;
using Task;
using UnityEngine;

namespace Resource
{
    public class ResourceNode : AbstractEntity
    {
        public TaskIndustryActionData taskActionData;
        public ResourceData resourceData;

        public void HarvestYield(Vector3 itemSpawnLocation)
        {
            //TODO: max yield / harvesters industry skills
            var harvested = (int) MaxHealth;
            var newItem = new Item.Item(harvested, resourceData.resourceItemData);
            
            newItem.Drop(itemSpawnLocation);
            Die();
        }

        public override void Die()
        {
            Destroy(gameObject, 0);
        }
    }
}
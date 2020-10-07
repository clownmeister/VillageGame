using UnityEngine;

namespace Item
{
    public class Item
    {
        public int amount;
        public ItemData itemData;
        public ItemObject itemObject;

        public Item(int amount, ItemData itemData)
        {
            this.amount = amount;
            this.itemData = itemData;
        }

        private void DestroyObject()
        {
            if (itemObject != null)
            {
                itemObject.Destroy();
            }
        }

        private void InstantiateObject(Vector3 position)
        {
            itemObject = Object.Instantiate(itemData.objectPrefab, position, Quaternion.identity).GetComponent<ItemObject>();
            itemObject.item = this;
        }

        public void PickUp()
        {
            DestroyObject();
        }

        public void Drop(Vector3 position)
        {
            InstantiateObject(position);
        }
    }
}

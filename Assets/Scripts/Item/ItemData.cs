using Manager;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "NewItemData", menuName = "Items/ItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public int maxStackSize;
        public GameObject objectPrefab;
    }
}

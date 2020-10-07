using Item;
using UnityEngine;

namespace Resource
{
    [CreateAssetMenu(fileName = "NewResourceData", menuName = "Resources/ResourceData", order = 0)]

    public class ResourceData : ScriptableObject
    {
        public string resourceName;
        public float hardness;
        public ItemData resourceItemData;
    }
}

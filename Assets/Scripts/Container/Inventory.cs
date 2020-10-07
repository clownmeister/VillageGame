using System.Collections.Generic;
using UnityEngine;

namespace Container
{
    public class Inventory : MonoBehaviour
    {
        private List<Item.Item> _items;
        public int itemSize;

        private void Start()
        {
            _items = new List<Item.Item>();
        }

        public bool AddItem(Item.Item item)
        {
            if (_items.Count >= itemSize)
            {
                return false;
            }

            if (_items.Contains(item))
            {
                return false;
            }

            _items.Add(item);
            return true;
        }

        public bool RemoveItem(Item.Item item)
        {
            if (!_items.Contains(item))
            {
                return false;
            }
            _items.Remove(item);
            return true;
        }
    }
}

using UnityEngine;

namespace Item
{
    public class ItemObject : MonoBehaviour
    {
        public global::Item.Item item;
        

        public void Destroy()
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}

using Navigation;
using UnityEngine;

namespace Entity
{
    [RequireComponent(typeof(RadiusGraphics))]
    public abstract class AbstractEntity : MonoBehaviour, IEntity
    {
        public RadiusGraphics RadiusGraphics { get; set; }
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        public virtual void Start()
        {
            RadiusGraphics = GetComponent<RadiusGraphics>();
        }

        public virtual void Die()
        {
            Destroy(this.transform.gameObject);
        }
    }
}
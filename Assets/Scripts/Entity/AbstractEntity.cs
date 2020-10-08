using Manager;
using Navigation;
using UnityEngine;

namespace Entity
{
    [RequireComponent(typeof(RadiusGraphics))]
    public abstract class AbstractEntity : MonoBehaviour, IEntity
    {
        public int Id { get; set; }
        public RadiusGraphics RadiusGraphics { get; set; }
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }

        public virtual void Start()
        {
            Id = GetInstanceID();
            RadiusGraphics = GetComponent<RadiusGraphics>();
            TickSystemManager.TickEvent += OnTickUpdate;
        }

        public virtual void OnTickUpdate()
        {
        }

        public virtual void Die()
        {
            TickSystemManager.TickEvent -= OnTickUpdate;
            Destroy(this.transform.gameObject);
        }
    }
}
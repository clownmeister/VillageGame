using Navigation;

namespace Entity
{
    public interface IEntity
    {
        int Id { get; set; }

        RadiusGraphics RadiusGraphics
        {
            get;
            set;
        }

        float MaxHealth
        {
            get;
            set;
        }

        float CurrentHealth
        {
            get;
            set;
        }

        void Start();
        void OnTickUpdate();
        void Die();
    }
}

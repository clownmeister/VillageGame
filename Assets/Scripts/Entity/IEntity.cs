using Navigation;

namespace Entity
{
    public interface IEntity
    {
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
        void Die();
    }
}

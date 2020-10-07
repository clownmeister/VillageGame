using Navigation;
using UnityEngine;

namespace Entity
{
    [RequireComponent(typeof(Navigation.Navigator))]
    public abstract class AbstractMovingEntity : AbstractEntity
    {
        public Navigator Navigator { get; set; }

        public override void Start()
        {
            base.Start();
            Navigator = GetComponent<Navigation.Navigator>();
        }
    }
}

using Resource;
using Task;
using UnityEngine;

namespace Entity
{
    public class Human : AbstractWorkerEntity
    {
        public override void OnTickUpdate()
        {
            Debug.Log("Tick");
        }
    }
}
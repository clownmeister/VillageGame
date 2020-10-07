using UnityEngine;

namespace ExampleImplementation
{
    [RequireComponent(typeof(Camera))]
    public class ExampleTopDownCamera : MonoBehaviour
    {
        private Camera _camera;
        public float speed = 7;
        public float zoomSpeed = 1;
        public float runSpeedModifier = 2.5f;

        private void Start()
        {
            this._camera = GetComponent<Camera>();
        }

        private void Update() 
        {
            float actualSpeed = speed;
            Vector3 moveDirection = Vector3.zero;

            if (Input.GetKey(KeyCode.LeftControl))
            {
                actualSpeed = speed * runSpeedModifier;
            }

            if (Input.GetKey(KeyCode.W))
            {
                moveDirection += new Vector3(0, 0, 1);
            }

            if (Input.GetKey(KeyCode.S))
            {
                moveDirection += new Vector3(0, 0, -1);
            }

            if (Input.GetKey(KeyCode.A))
            {
                moveDirection += new Vector3(-1, 0, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                moveDirection += new Vector3(1, 0, 0);
            }

            moveDirection = Vector3.Normalize(moveDirection);
            
            var position = transform.position;
            position += moveDirection * (actualSpeed * Time.deltaTime);
            position += new Vector3(0,-Input.mouseScrollDelta.y * zoomSpeed,0);
            transform.position = position;
        }
    }
}
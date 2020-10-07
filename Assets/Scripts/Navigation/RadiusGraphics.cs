using UnityEngine;
using UnityEngine.Rendering;

namespace Navigation
{
    [RequireComponent(typeof(LineRenderer))]
    public class RadiusGraphics : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
     
        public enum ColorType
        {
            Primary = 1,
            Secondary = 2,
            Attack = 3,
            HoverSecondary = 102,
            HoverAttack = 103,
        }

        public bool isEnabledOnStart = false;
        
        [Range(0.1f, 3)]
        public float radius = 1.0f;

        [Range(3, 128)]
        public int numSegments = 64;
        
        [Range(0.01f, 0.1f)]
        public float width = 0.1f;
        
        public Color colorPrimary = new Color(0, 1f, 0, 1);
        public Color colorSecondary = new Color(1f, 1f, 0, 1);
        public Color colorHoverSecondary = new Color(1f, 1f, 0.5f, 0.5f);
        public Color colorAttack = new Color(1f, 0, 0, 1);
        public Color colorHoverAttack = new Color(1f, 0.3f, 0.3f, 0.5f);

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            EnableDraw(isEnabledOnStart);
            StartDraw();
        }

        public void EnableDraw(bool enable)
        {
            _lineRenderer.enabled = enable;
        }

        public void ChangeColor(ColorType colorType)
        {
            switch (colorType)
            {
                case ColorType.Primary:
                    _lineRenderer.startColor = colorPrimary;
                    _lineRenderer.endColor = colorPrimary;
                    break;
                case ColorType.Secondary:
                    _lineRenderer.startColor = colorSecondary;
                    _lineRenderer.endColor = colorSecondary;
                    break;
                case ColorType.Attack:
                    _lineRenderer.startColor = colorAttack;
                    _lineRenderer.endColor = colorAttack;
                    break;
                case ColorType.HoverSecondary:
                    _lineRenderer.startColor = colorHoverSecondary;
                    _lineRenderer.endColor = colorHoverSecondary;
                    break;
                case ColorType.HoverAttack:
                    _lineRenderer.startColor = colorHoverAttack;
                    _lineRenderer.endColor = colorHoverAttack;
                    break;
            }
        }
        
        public void StartDraw ( ) {
            _lineRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Particles/Unlit"));
            ChangeColor(ColorType.Primary);
            
            _lineRenderer.startWidth = width;
            _lineRenderer.endWidth = width;
            _lineRenderer.positionCount = numSegments + 1;
            _lineRenderer.useWorldSpace = false;
            _lineRenderer.shadowCastingMode = ShadowCastingMode.Off;
            _lineRenderer.receiveShadows = false;

            float deltaTheta = (float) (2.0 * Mathf.PI) / numSegments;
            float theta = 0f;

            for (int i = 0 ; i < numSegments + 1 ; i++) {
                float x = radius * Mathf.Cos(theta);
                float z = radius * Mathf.Sin(theta);
                Vector3 pos = new Vector3(x, 0, z);
                _lineRenderer.SetPosition(i, pos);
                theta += deltaTheta;
            }
        }
    }
}

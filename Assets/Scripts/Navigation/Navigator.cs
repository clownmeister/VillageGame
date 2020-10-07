using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Navigation
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Navigator : MonoBehaviour
    {
        //debug properties
        private const float LogDelayInSeconds = 2f;
        public bool debug = false;

        //private properties
        private NavMeshAgent _navMeshAgent;
        private Collider _targetCollider;
        private float _nextLog = 0;
        private bool _isMoving = false;

        //public properties
        public Animator animator;
        public bool preventSwarmPushing = true;

        public bool IsMoving
        {
            get => _isMoving;
            set
            {
                _isMoving = value;
                if (animator)
                {
                    animator.SetBool("isRunning", value);
                }
            }
        }

        public UnityEvent onFinished;
        public UnityEvent onStarted;
        public UnityEvent onCanceled;

        //public hidden properties
        [HideInInspector] public Vector3 lastDestination;
        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (debug)
            {
                LogState();
            }

            if (ShouldFinish())
            {
                ResetPath();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (preventSwarmPushing)
            {
                HandleSwarmTouching(other.gameObject);
            }
        
            if (null != _targetCollider && _targetCollider == other.collider)
            {
                ResetPath();
            }
        }

        private void OnCollisionStay(Collision other)
        {
            if (preventSwarmPushing)
            {
                HandleSwarmTouching(other.gameObject);
            }
            
            if (null != _targetCollider && _targetCollider == other.collider)
            {
                ResetPath();
            }
        }

        private bool ShouldFinish()
        {
            if (IsMoving && _navMeshAgent.remainingDistance < 0.6f)
            {
                return true;
            }

            return false;
        }

        private void LogState()
        {
            if (Time.time > _nextLog)
            {
                Debug.Log("Current navigator is working: " + _navMeshAgent.hasPath);
                _nextLog = Time.time + LogDelayInSeconds;
            }
        }

        private void HandleSwarmTouching(GameObject neighbour)
        {
            Navigator neighbourNavController = neighbour.GetComponent<Navigator>();
            if (IsMoving && neighbourNavController != null)
            {
                if (lastDestination == neighbourNavController.lastDestination && !neighbourNavController.IsMoving)
                {
                    ResetPath();
                }
            }
        }

        private void SetPath(NavMeshPath path, Collider targetCollider = null)
        {
            _navMeshAgent.SetPath(path);
            _targetCollider = targetCollider;
            IsMoving = true;
            onStarted.Invoke();
        }

        private void ResetPath(bool canceledPath = false)
        {
            _navMeshAgent.ResetPath();
            _targetCollider = null;
            IsMoving = false;
            if (canceledPath)
            {
                onCanceled.Invoke();
            }
            else
            {
                onFinished.Invoke();
            }
        }

        public bool StartMoving(Vector3 targetPosition, Collider targetCollider = null)
        {
            NavMeshPath newPath = new NavMeshPath();

            if (NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, newPath))
            {
                if (newPath.status != NavMeshPathStatus.PathComplete)
                {
                    if (debug)
                    {
                        Debug.LogWarning("Path invalid");
                    }

                    ResetPath(true);
                    return false;
                }

                SetPath(newPath, targetCollider);
                lastDestination = targetPosition;
                return true;
            }

            return false;
        }
    }
}
using UnityEngine;
using SimpleEventBus.Disposables;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Seek the target specified using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}SeekIcon.png")]
    public class Seek : NavMeshMovement
    {
        [Tooltip("The GameObject that the agent is seeking")]
        public SharedGameObject target;
        [Tooltip("If target is null then use the target position")]
        public SharedVector3 targetPosition;
        
        private CompositeDisposable _subscriptions;
        private GameObject _character;

        private void Initialize(CharacterInstantiatedEvent eventData)
        {
            if (gameObject.tag == "Enemy")
            {
                target = eventData.Character;
            }
        }

        private void OnDestroy()
        {
            _subscriptions?.Dispose();
        }
        
        public override void OnStart()
        {
            _subscriptions = new CompositeDisposable
            {
                EventStreams.Game.Subscribe<CharacterInstantiatedEvent>(Initialize)
            };
            if (gameObject.tag == "Enemy")
            {
                target = GameObject.FindGameObjectWithTag("Player");
            }
            base.OnStart();

            SetDestination(Target());
        }

        // Seek the destination. Return success once the agent has reached the destination.
        // Return running if the agent hasn't reached the destination yet
        public override TaskStatus OnUpdate()
        {
            //target = GameObject.FindGameObjectWithTag("Player");
            if (HasArrived()) {
                return TaskStatus.Success;
            }

            SetDestination(Target());

            return TaskStatus.Running;
        }
        
        // Return targetPosition if target is null
        private Vector3 Target()
        {
            if (target.Value != null) {
                return target.Value.transform.position;
            }
            return targetPosition.Value;
        }

        public override void OnReset()
        {
            base.OnReset();
            target = null;
            targetPosition = Vector3.zero;
        }
    }
}
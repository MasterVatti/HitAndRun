using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
    [TaskCategory("Unity/CharacterController")]
    [TaskDescription("Stores the height of the CharacterController. Returns Success.")]
    public class GetHeight : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The height of the CharacterController")]
        [RequiredField]
        public SharedFloat storeValue;

        private UnityEngine.CharacterController characterController;
        private GameObject prevGameObject;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                characterController = currentGameObject.GetComponent<UnityEngine.CharacterController>();
                prevGameObject = currentGameObject;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (characterController == null) {
                Debug.LogWarning("CharacterController is null");
                return TaskStatus.Failure;
            }
            storeValue.Value = characterController.height;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            storeValue = 0;
        }
    }
}
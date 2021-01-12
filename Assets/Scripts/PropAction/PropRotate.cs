using UnityEngine;

namespace RPG.PropAction
{
    public class PropRotate : MonoBehaviour
    {
        [SerializeField] Vector3 rotations = Vector3.zero;

        private void Update()
        {
            transform.Rotate(rotations, Space.World);
        }
    }
}

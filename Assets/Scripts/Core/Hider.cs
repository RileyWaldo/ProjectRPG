using UnityEngine;

namespace RPG.Core
{
    [RequireComponent(typeof(BoxCollider))]
    public class Hider : MonoBehaviour
    {
        [SerializeField] float range = 10f;

        bool hide = false;
        GameObject player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            if((player.transform.position - transform.position).sqrMagnitude <= range * range)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth/2, Camera.main.pixelHeight/2, 0f));
                RaycastHit hit;
                bool changeState = hide;
                hide = GetComponent<BoxCollider>().Raycast(ray, out hit, (player.transform.position - Camera.main.transform.position).magnitude);

                if(hide != changeState)
                {
                    foreach (Transform child in transform)
                    {
                        child.gameObject.SetActive(!hide);
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}

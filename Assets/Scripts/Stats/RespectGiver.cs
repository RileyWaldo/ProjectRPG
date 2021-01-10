using UnityEngine;

namespace RPG.Stats
{
    public class RespectGiver : MonoBehaviour
    {
        [SerializeField] string faction = "";
        [SerializeField] int respectValue = 0;

        public void GiveRespect()
        {
            Respect respect = GameObject.FindGameObjectWithTag("Player").GetComponent<Respect>();
            respect.GiveRespect(faction, respectValue);
        }
    }
}

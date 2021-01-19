using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Core;

namespace RPG.Stats
{
    public class Respect : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        [SerializeField] List<Faction> factions = new List<Faction>();

        public void GiveRespect(string factionType, int respectGained)
        {
            foreach(Faction faction in factions)
            {
                if (faction.faction == factionType)
                    faction.respect += respectGained;
            }
        }

        public int GetRespect(string factionType)
        {
            foreach(Faction faction in factions)
            {
                if (faction.faction == factionType)
                    return faction.respect;
            }
            Debug.Log("Couldn't find faction respect level.");
            return 0;
        }

        public object CaptureState()
        {
            return factions;
        }

        public void RestoreState(object state)
        {
            List<Faction> newState = state as List<Faction>;

            if (newState == null)
                return;

            factions.Clear();

            foreach (Faction faction in newState)
            {
                factions.Add(faction);
            }
        }

        public bool? Evaluate(PredicateType predicate, string[] parameters)
        {
            switch(predicate)
            {
                case PredicateType.HasRespectGreaterThan:
                    return GetRespect(parameters[0]) >= int.Parse(parameters[1]);

                case PredicateType.HasRespectLessThan:
                    return GetRespect(parameters[0]) <= int.Parse(parameters[1]);
            }
            return null;
        }

        [System.Serializable]
        private class Faction
        {
            public string faction = "";
            public int respect = 0;
        }
    }
}

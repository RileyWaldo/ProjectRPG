using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    [System.Serializable]
    public class Condition
    {
        [SerializeField] Disjunction[] and = default;

        public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
        {
            foreach(Disjunction disjunction in and)
            {
                if(!disjunction.Check(evaluators))
                {
                    return false;
                }
            }
            return true;
        }

        [System.Serializable]
        class Disjunction
        {
            [SerializeField] Predicate[] or = default;

            public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
            {
                foreach(Predicate pred in or)
                {
                    if(pred.Check(evaluators))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        [System.Serializable]
        class Predicate
        {
            [SerializeField] bool not = false;
            [SerializeField] PredicateType predicate = PredicateType.None;
            [SerializeField] string[] parameters = default;

            public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
            {
                foreach (var evaluator in evaluators)
                {
                    bool? result = evaluator.Evaluate(predicate, parameters);
                    if (result == null)
                    {
                        continue;
                    }

                    if (result == not)
                        return false;
                }
                return true;
            }
        }
    }
}

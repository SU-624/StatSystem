using System.Collections.Generic;
using UnityEngine;

namespace StatSystem.Runtime
{
    [CreateAssetMenu(fileName = "StatDatabase", menuName = "StatSystem/StatDatabase", order = 0)]

    public class StatDatabase : ScriptableObject
    {
        public List<StatDefinition> stats;
    }
}


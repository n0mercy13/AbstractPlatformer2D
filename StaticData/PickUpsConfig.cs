using Codebase.Logic;
using System;
using UnityEngine;

namespace Codebase.StaticData
{
    [Serializable]
    public class PickUpsConfig 
    {
        [field: SerializeField] public Coin CoinPrefab { get; private set; }
        [field: SerializeField] public MedicalKit MedicalKitPrefab { get; private set; }
    }

}

using System;
using UnityEngine;

namespace Codebase.StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "StaticData")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerConfig PlayerConfig { get; private set; }
        [field: SerializeField] public EnemyConfig EnemyConfig { get; private set; }
        [field: SerializeField] public PickUpsConfig PickUpsConfig { get; private set; }
        [field: SerializeField] public AudioConfig AudioConfig { get; private set; }
        [field: SerializeField] public UIConfig UIConfig { get; private set; }

        private void OnValidate()
        {
            if(PlayerConfig == null)
                throw new ArgumentNullException(nameof(PlayerConfig));

            if(EnemyConfig == null) 
                throw new ArgumentNullException(nameof(EnemyConfig));

            if(PickUpsConfig == null)
                throw new ArgumentNullException(nameof(PickUpsConfig));
            
            if(AudioConfig == null)
                throw new ArgumentNullException(nameof(AudioConfig));

            if (UIConfig == null)
                throw new ArgumentNullException(nameof(UIConfig));
        }
    }
}

using UnityEngine;

namespace Codebase.StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "StaticData")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerConfig PlayerConfig { get; private set; }
        [field: SerializeField] public EnemyConfig EnemyConfig { get; private set; }
    }
}

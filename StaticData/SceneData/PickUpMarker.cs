using UnityEngine;

namespace Codebase.StaticData
{
    public class PickUpMarker : MonoBehaviour 
	{
		[field: SerializeField,Range(0.0f, 1.0f)] public float SpawnChance { get; private set; } = 1.0f;
	}
}

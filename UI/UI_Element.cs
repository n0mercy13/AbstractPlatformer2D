using UnityEngine;
using Codebase.Logic;

namespace Codebase.UI
{
    public class UI_Element : MonoBehaviour
    {
        [field: SerializeField] public UIElementTypes Type { get; private set; }
    }
}

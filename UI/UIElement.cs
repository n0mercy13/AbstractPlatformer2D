using UnityEngine;
using Codebase.Logic;

namespace Codebase.UI
{
    public class UIElement : MonoBehaviour
    {
        [field: SerializeField] public UIElementTypes Type { get; private set; }
    }
}

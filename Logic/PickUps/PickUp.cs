using UnityEngine;

namespace Codebase.Logic
{
    public class PickUp : MonoBehaviour
    {
        [SerializeField, Range(0, 100)] private int _value;

        public int Collect()
        {
            Destroy(gameObject);

            return _value;
        }
    } 
}

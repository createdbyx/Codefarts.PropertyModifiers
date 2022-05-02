namespace Codefarts.PropertyModifiers
{
#if UNITY_5
    using UnityEngine;

    /// <summary>
    /// A generic base class for a property modifier implementation.
    /// </summary>
    /// <typeparam name="T">
    /// The type used as the property value.
    /// </typeparam>
    public partial class BasicPropertyModifier<T> : ScriptableObject
    {
        /// <summary>
        /// Holds the value for the <see cref="Value"/> property.
        /// </summary>
        [SerializeField]
        protected T value;

        /// <summary>
        /// Holds the value for the <see cref="Type"/> property.
        /// </summary>
        [SerializeField]
        protected ModifierType type;
    }
#endif
}
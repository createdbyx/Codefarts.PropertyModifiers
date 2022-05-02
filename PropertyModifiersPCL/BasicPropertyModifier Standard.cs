namespace Codefarts.PropertyModifiers
{
#if !UNITY_5
    /// <summary>
    /// A generic base class for a property modifier implementation.
    /// </summary>
    /// <typeparam name="T">
    /// The type used as the property value.
    /// </typeparam>
    public partial class BasicPropertyModifier<T>
    {
        /// <summary>
        /// Holds the value for the <see cref="Value"/> property.
        /// </summary>
        protected T value;

        /// <summary>
        /// Holds the value for the <see cref="Type"/> property.
        /// </summary>
        protected ModifierType type;
    }
#endif
}
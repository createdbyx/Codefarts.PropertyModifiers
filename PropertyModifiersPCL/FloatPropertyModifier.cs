namespace Codefarts.PropertyModifiers
{
    /// <summary>
    /// Provides a float based property modifier.
    /// </summary>
    /// <seealso cref="PropertyModifiers.BasicPropertyModifier{float}" />
    public class FloatPropertyModifier : BasicPropertyModifier<float>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatPropertyModifier"/> class.
        /// </summary>
        /// <param name="type">The type of modifier.</param>
        /// <param name="value">The modifier value.</param>
        public FloatPropertyModifier(ModifierType type, float value)
        {
            this.Type = type;
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatPropertyModifier"/> class.
        /// </summary>
        public FloatPropertyModifier()
        {
        }
    }
}
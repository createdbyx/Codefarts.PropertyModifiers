namespace Codefarts.PropertyModifiers
{
    /// <summary>
    /// Provides a integer based property modifier.
    /// </summary>
    /// <seealso cref="PropertyModifiers.BasicPropertyModifier{int}" />
    public class IntegerPropertyModifier : BasicPropertyModifier<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerPropertyModifier"/> class.
        /// </summary>
        /// <param name="type">The type of modifier.</param>
        /// <param name="value">The modifier value.</param>
        public IntegerPropertyModifier(ModifierType type, int value)
        {
            this.Type = type;
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerPropertyModifier"/> class.
        /// </summary>
        public IntegerPropertyModifier()
        {
        }
    }
}
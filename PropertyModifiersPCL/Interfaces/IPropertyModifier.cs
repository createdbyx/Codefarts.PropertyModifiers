/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.PropertyModifiers
{
    using System.ComponentModel;

    /// <summary>
    /// The IPropertyModifier interface.
    /// </summary>
    /// <typeparam name="T">
    /// The type used as the property value.
    /// </typeparam>
    public interface IPropertyModifier<T> : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the type of modification.
        /// </summary>
        ModifierType Type { get; set; }

        /// <summary>
        /// Gets or sets the value for the modification.
        /// </summary>
        T Value { get; set; }
    }
}
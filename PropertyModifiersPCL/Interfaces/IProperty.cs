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
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    /// <summary>
    /// The IProperty interface.
    /// </summary>
    /// <typeparam name="T">
    /// The type used as the property value.
    /// </typeparam>
    public interface IProperty<T> : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the base value for the property.
        /// </summary>
        T BaseValue { get; set; }

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> of property modifier objects.
        /// </summary>
        ObservableCollection<IPropertyModifier<T>> Modifiers { get; }

        /// <summary>
        /// Gets the modified value of the property.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Gets the minimum allowable value of the <see cref="Value"/> property.
        /// </summary>
        T Minimum { get; set; }

        /// <summary>
        /// Gets the maximum allowable value of the <see cref="Value"/> property.
        /// </summary>
        T Maximum { get; set; }
    }
}
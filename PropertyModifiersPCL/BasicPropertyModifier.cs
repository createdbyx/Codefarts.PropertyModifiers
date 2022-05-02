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
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// A generic base class for a property modifier implementation.
    /// </summary>
    /// <typeparam name="T">
    /// The type used as the property value.
    /// </typeparam>
    public partial class BasicPropertyModifier<T> : IPropertyModifier<T>
    {
        /// <summary>
        /// The cached property arguments.
        /// </summary>
        protected IDictionary<string, PropertyChangedEventArgs> propertyArgs = new Dictionary<string, PropertyChangedEventArgs>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicPropertyModifier{T}"/> class.
        /// </summary>
        /// <param name="value">
        /// The value of the modification.
        /// </param>
        public BasicPropertyModifier(T value)
        {
            this.value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicPropertyModifier{T}"/> class.
        /// </summary>
        public BasicPropertyModifier()
        {
        }

        /// <summary>
        /// Gets or sets type of modification to perform.
        /// </summary>
        public virtual ModifierType Type
        {
            get
            {
                return this.type;
            }

            set
            {
                var changed = this.type != value;
                this.type = value;
                if (changed)
                {
                    this.OnPropertyChanged("Type");
                }
            }
        }

        /// <summary>
        /// Gets or sets value used during modification.
        /// </summary>
        public virtual T Value
        {
            get
            {
                return this.value;
            }

            set
            {
                var changed = !object.Equals(this.value, value);
                this.value = value;
                if (changed)
                {
                    this.OnPropertyChanged("Value");
                }
            }
        }

        /// <summary>
        /// Provides a <see cref="INotifyPropertyChanged.PropertyChanged"/> implementation.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Used to invoke the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                if (!this.propertyArgs.ContainsKey(propertyName))
                {
                    this.propertyArgs[propertyName] = new PropertyChangedEventArgs(propertyName);
                }

                handler(this, this.propertyArgs[propertyName]);
            }
        }
    }
}
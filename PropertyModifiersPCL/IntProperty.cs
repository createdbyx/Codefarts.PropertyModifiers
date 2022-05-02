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
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;

    /// <summary>
    /// Integer based property.
    /// </summary>
    public class IntProperty : IProperty<int>
    {
        /// <summary>
        /// The modifier list.
        /// </summary>
        private readonly ObservableCollection<IPropertyModifier<int>> modifiers = new ObservableCollection<IPropertyModifier<int>>();

        /// <summary>
        /// The cached property arguments.
        /// </summary>
        private IDictionary<string, PropertyChangedEventArgs> propertyArgs = new Dictionary<string, PropertyChangedEventArgs>();

        /// <summary>
        /// The base value for the property.
        /// </summary>
        private int baseValue;

        /// <summary>
        /// The cached value returned by the <see cref="Value"/> property.
        /// </summary>
        private int cachedValue;

        /// <summary>
        /// The cached value returned by the <see cref="Minimum"/> property.
        /// </summary>
        private int minimum = int.MinValue;

        /// <summary>
        /// The cached value returned by the <see cref="Maximum"/> property.
        /// </summary>
        private int maximum = int.MaxValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntProperty"/> class.
        /// </summary>
        public IntProperty()
        {
            this.modifiers = new ObservableCollection<IPropertyModifier<int>>();
            this.modifiers.CollectionChanged += this.ModifiersCollectionChanged;
        }

        /// <summary>
        /// Handles changes to the <see cref="Modifiers"/> property.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void ModifiersCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.HookIntoModifierChanges(e.NewItems, true);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    this.HookIntoModifierChanges(e.OldItems, false);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    this.HookIntoModifierChanges(e.OldItems, false);
                    this.HookIntoModifierChanges(e.NewItems, true);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    this.HookIntoModifierChanges(this.modifiers, false);
                    this.HookIntoModifierChanges(this.modifiers, true);
                    break;
            }

            this.Value = this.CalculateValue();
        }

        private void HookIntoModifierChanges(IList items, bool hook)
        {
            foreach (var item in items)
            {
                var modifier = item as IPropertyModifier<float>;
                if (modifier != null)
                {
                    if (hook)
                    {
                        modifier.PropertyChanged += this.ModifierChanged;
                    }
                    else
                    {
                        modifier.PropertyChanged -= this.ModifierChanged;
                    }
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntProperty"/> class.
        /// </summary>
        /// <param name="value">
        /// Specifies the value of the <see cref="BaseValue"/> property.
        /// </param>
        public IntProperty(int value)
            : this()
        {
            this.baseValue = value;
            this.cachedValue = value;
        }

        /// <summary>
        /// Provides a <see cref="INotifyPropertyChanged.PropertyChanged"/> implementation.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the base value of the property.
        /// </summary>
        public virtual int BaseValue
        {
            get
            {
                return this.baseValue;
            }

            set
            {
                var changed = this.baseValue != value;
                this.baseValue = value;
                if (changed)
                {
                    this.OnPropertyChanged("BaseValue");
                }

                this.Value = this.CalculateValue();
            }
        }

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> of property modifier objects.
        /// </summary>
        public ObservableCollection<IPropertyModifier<int>> Modifiers
        {
            get
            {
                return this.modifiers;
            }
        }

        /// <summary>
        /// Gets the modified value of the property.
        /// </summary>
        public virtual int Value
        {
            get
            {
                return this.cachedValue;
            }

            private set
            {
                value = value < this.Minimum ? this.Minimum : value;
                value = value > this.Maximum ? this.Maximum : value;
                var changed = this.cachedValue != value;
                this.cachedValue = value;
                if (changed)
                {
                    this.OnPropertyChanged("Value");
                }
            }
        }

        /// <summary>
        /// Gets the minimum allowable value of the <see cref="IProperty{T}.Value"/> property.
        /// </summary>
        public virtual int Minimum
        {
            get
            {
                return this.minimum;
            }

            set
            {
                var changed = this.minimum != value;
                this.minimum = value;
                if (changed)
                {
                    this.OnPropertyChanged("Minimum");
                }

                this.Value = this.Value;
            }
        }

        /// <summary>
        /// Gets the maximum allowable value of the <see cref="IProperty{T}.Value"/> property.
        /// </summary>
        public virtual int Maximum
        {
            get
            {
                return this.maximum;
            }

            set
            {
                var changed = this.maximum != value;
                this.maximum = value;
                if (changed)
                {
                    this.OnPropertyChanged("Maximum");
                }

                this.Value = this.Value;
            }
        }

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

        /// <summary>
        /// Calculates the value for the <see cref="Value"/> property.
        /// </summary>
        /// <returns>Returns the calculated value.</returns>
        private int CalculateValue()
        {
            // get the base value
            var value = this.baseValue;

            // process all modifiers
            for (var index = 0; index < this.modifiers.Count - 1; index++)
            {
                var modifier = this.modifiers[index];
                switch (modifier.Type)
                {
                    case ModifierType.Addition:
                        value += modifier.Value;
                        break;

                    case ModifierType.Subtraction:
                        value -= modifier.Value;
                        break;

                    case ModifierType.Multiply:
                        value *= modifier.Value;
                        break;

                    case ModifierType.Divide:
                        value /= modifier.Value;
                        break;

                    case ModifierType.Absolute:
                        return modifier.Value;
                }
            }

            return value;
        }

        /// <summary>
        /// Handles <see cref="INotifyPropertyChanged.PropertyChanged"/> events of modifiers that have been added to this property.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void ModifierChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Type":
                case "Value":
                    this.Value = this.CalculateValue();
                    break;
            }
        }
    }
}
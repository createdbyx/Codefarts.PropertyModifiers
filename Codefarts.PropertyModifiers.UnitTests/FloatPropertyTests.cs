namespace Codefarts.PropertyModifiers.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /// <summary>
    /// Summary description for FloatPropertyTests
    /// </summary>
    [TestClass]
    public class FloatPropertyTests
    {
        [TestMethod]
        public void PropertyChangedEvents()
        {
            var property = new FloatProperty(5);
            Assert.AreEqual(5, property.BaseValue);
            Assert.AreEqual(5, property.Value);

            var baseValueChanges = 0;
            var valueChanges = 0;
            property.PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    case "BaseValue":
                        baseValueChanges++;
                        break;

                    case "Value":
                        valueChanges++;
                        break;
                }
            };

            property.BaseValue = 2;
            Assert.AreEqual(1, baseValueChanges);
            Assert.AreEqual(1, valueChanges);

            Assert.AreEqual(2, property.BaseValue);
            Assert.AreEqual(2, property.Value);
        }

        [TestMethod]
        public void PropertyChangedEventsWithModifiers()
        {
            var property = new FloatProperty(5);
            Assert.AreEqual(5, property.BaseValue);
            Assert.AreEqual(5, property.Value);

            var baseValueChanges = 0;
            var valueChanges = 0;
            property.PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    case "BaseValue":
                        baseValueChanges++;
                        break;

                    case "Value":
                        valueChanges++;
                        break;
                }
            };

            property.Modifiers.Add(new FloatPropertyModifier(ModifierType.Subtraction, 3)); 
            Assert.AreEqual(0, baseValueChanges);
            Assert.AreEqual(1, valueChanges);

            Assert.AreEqual(5, property.BaseValue);
            Assert.AreEqual(2, property.Value);
        }

        [TestMethod]
        public void ConstructorValue()
        {
            var property = new FloatProperty(5);
            Assert.AreEqual(5, property.BaseValue);
            Assert.AreEqual(5, property.Value);
        }

        [TestMethod]
        public void ConstructorBaseValue()
        {
            var property = new FloatProperty(5);
            Assert.AreEqual(5, property.BaseValue);
        }

        [TestMethod]
        public void AdditionModifier()
        {
            var property = new FloatProperty(5);
            property.Modifiers.Add(new BasicPropertyModifier<float>(5) { Type = ModifierType.Addition });
            Assert.AreEqual(5, property.BaseValue);
            Assert.AreEqual(10, property.Value);
        }

        [TestMethod]
        public void SubtractionModifier()
        {
            var property = new FloatProperty(5);
            property.Modifiers.Add(new BasicPropertyModifier<float>(3) { Type = ModifierType.Subtraction });
            Assert.AreEqual(5, property.BaseValue);
            Assert.AreEqual(2, property.Value);
        }

        [TestMethod]
        public void MultiplacationModifier()
        {
            var property = new FloatProperty(5);
            property.Modifiers.Add(new BasicPropertyModifier<float>(3) { Type = ModifierType.Multiply });
            Assert.AreEqual(5, property.BaseValue);
            Assert.AreEqual(15, property.Value);
        }

        [TestMethod]
        public void DivisionModifier()
        {
            var property = new FloatProperty(5);
            property.Modifiers.Add(new BasicPropertyModifier<float>(2) { Type = ModifierType.Divide });
            Assert.AreEqual(5, property.BaseValue);
            Assert.AreEqual(2.5, property.Value);
        }

        [TestMethod]
        public void DivisionAdditionModifier()
        {
            var property = new FloatProperty(5);
            property.Modifiers.Add(new BasicPropertyModifier<float>(2) { Type = ModifierType.Divide });
            property.Modifiers.Add(new BasicPropertyModifier<float>(4) { Type = ModifierType.Addition });
            Assert.AreEqual(5, property.BaseValue);
            Assert.AreEqual(6.5, property.Value);
        }

        [TestMethod]
        public void AbsoluteModifier()
        {
            var property = new FloatProperty(5);
            property.Modifiers.Add(new BasicPropertyModifier<float>(2) { Type = ModifierType.Divide });
            property.Modifiers.Add(new BasicPropertyModifier<float>(4) { Type = ModifierType.Addition });
            property.Modifiers.Add(new BasicPropertyModifier<float>(3) { Type = ModifierType.Absolute });
            Assert.AreEqual(5, property.BaseValue);
            Assert.AreEqual(3, property.Value);
        }

        /* Old Priority test code
         
        [TestMethod]
        public void FloatPropertyModifierPriority()
        {
            var property = new FloatProperty(5);
            property.Modifiers.Add(new BasicPropertyModifier<float>(2) { Type = ModifierType.Divide });
            property.Modifiers.Add(new BasicPropertyModifier<float>(4) { Type = ModifierType.Addition });
            property.Modifiers.Add(new BasicPropertyModifier<float>(1) { Type = ModifierType.Subtraction, Priority = int.MaxValue });
            Assert.AreEqual(3, property.Modifiers.Count);
            Assert.AreEqual(5, property.BaseValue);
            Assert.AreEqual(4, property.Value);
        }

        [TestMethod]
        public void FloatPropertyAbsoluteModifierAtEndWithLowerPriority()
        {
            var property = new FloatProperty(5);
            property.Modifiers.Add(new BasicPropertyModifier<float>(2) { Type = ModifierType.Divide });
            property.Modifiers.Add(new BasicPropertyModifier<float>(4) { Type = ModifierType.Addition });
            property.Modifiers.Add(new BasicPropertyModifier<float>(1) { Type = ModifierType.Subtraction, Priority = int.MaxValue });
            property.Modifiers.Add(new BasicPropertyModifier<float>(11) { Type = ModifierType.Absolute, Priority = int.MaxValue - 1 });
            Assert.AreEqual(4, property.Modifiers.Count);
            Assert.AreEqual(5, property.BaseValue);
            Assert.AreEqual(11, property.Value);
        }

        [TestMethod]
        public void FloatPropertyAbsoluteModifierAtStartWithLowerPriority()
        {
            var property = new FloatProperty(5);
            property.Modifiers.Add(new BasicPropertyModifier<float>(11) { Type = ModifierType.Absolute, Priority = int.MaxValue - 1 });
            property.Modifiers.Add(new BasicPropertyModifier<float>(2) { Type = ModifierType.Divide });
            property.Modifiers.Add(new BasicPropertyModifier<float>(4) { Type = ModifierType.Addition });
            property.Modifiers.Add(new BasicPropertyModifier<float>(1) { Type = ModifierType.Subtraction, Priority = int.MaxValue });
            Assert.AreEqual(4, property.Modifiers.Count);
            Assert.AreEqual(5, property.BaseValue);
            Assert.AreEqual(11, property.Value);
        }

        [TestMethod]
        public void FloatPropertyReversePriority()
        {
            var property = new FloatProperty(5);
            property.Modifiers.Add(new BasicPropertyModifier<float>(1) { Type = ModifierType.Absolute, Priority = 5 });
            property.Modifiers.Add(new BasicPropertyModifier<float>(1) { Type = ModifierType.Divide, Priority = 4 });
            property.Modifiers.Add(new BasicPropertyModifier<float>(1) { Type = ModifierType.Addition, Priority = 3 });
            property.Modifiers.Add(new BasicPropertyModifier<float>(1) { Type = ModifierType.Subtraction, Priority = 2 });

            var list = new List<IPropertyModifier<float>>(property.Modifiers);

            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(5, property.BaseValue);
            Assert.AreEqual(1, property.Value);
            for (var i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(i + 2, list[i].Priority);
            }
        }

        [TestMethod]
        public void FloatPropertyForwardPriority()
        {
            var property = new FloatProperty(5);
            property.Modifiers.Add(new BasicPropertyModifier<float>(1) { Type = ModifierType.Absolute, Priority = 2 });
            property.Modifiers.Add(new BasicPropertyModifier<float>(1) { Type = ModifierType.Divide, Priority = 3 });
            property.Modifiers.Add(new BasicPropertyModifier<float>(1) { Type = ModifierType.Addition, Priority = 4 });
            property.Modifiers.Add(new BasicPropertyModifier<float>(1) { Type = ModifierType.Subtraction, Priority = 5 });

            var list = new List<IPropertyModifier<float>>(property.Modifiers);

            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(5, property.BaseValue);
            Assert.AreEqual(1, property.Value);
            for (var i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(i + 2, list[i].Priority);
            }
        }
        */
    }
}

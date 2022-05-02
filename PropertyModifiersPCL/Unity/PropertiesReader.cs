/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/
#if UNITY_5 || UNITY_2017
namespace Codefarts.PropertyModifiers.Unity
{
    using Codefarts.PropertyModifiers.Scripts.Character;
    using System;
    using System.Xml;

    using Codefarts.ContentManager;

    using ReadState = Codefarts.ContentManager.ReadState;

    /// <summary>
    /// Provides a <see cref="IReader{T}"/> implementation for reading <see cref="CharacterProperties"/> types.
    /// </summary>
    public class PropertiesReader : IReader<string>
    {
        /// <summary>
        /// Gets the <see cref="IReader{T}.Type"/> that this reader implementation returns.
        /// </summary>
        public Type Type
        {
            get
            {
                return typeof(CharacterProperties);
            }
        }

        /// <summary>
        /// Reads a file and returns a type representing the data.
        /// </summary>
        /// <param name="key">The id to be read.</param>
        /// <param name="content">A reference to the content manager that invoked the read.</param>
        /// <returns>Returns a type representing the data.</returns>
        public object Read(string key, ContentManager<string> content)
        {
            var doc = new XmlDocument();
            doc.Load(key);

            if (doc.DocumentElement == null || doc.DocumentElement.Name.ToLower() != "properties")
            {
                throw new XmlException("Root element is not 'properties'");
            }

            var props = new CharacterProperties();

            var propertyList = doc.DocumentElement.SelectNodes("property");
            if (propertyList == null)
            {
                return props;
            }

            foreach (XmlNode node in propertyList)
            {
                if (node.Attributes == null)
                {
                    throw new XmlException("Property element is missing attributes.");
                }

                var name = node.Attributes["name"];
                var value = node.Attributes["value"];

                if (name == null || string.IsNullOrEmpty(name.Name.Trim()))
                {
                    throw new XmlException("Missing 'name' attribute or attribute has no value.");
                }

                if (value == null)
                {
                    throw new XmlException("Missing 'value' attribute.");
                }

                float baseValue;
                if (!float.TryParse(value.InnerText, out baseValue))
                {
                    throw new XmlException("Could not parse 'value' attribute.");
                }

                // add the property
                props.Add(name.Name, new FloatProperty(baseValue));
            }

            return props;
        }

        /// <summary>
        /// Determines if the reader can read the data.
        /// </summary>
        /// <param name="key">The id to be read.</param>
        /// <param name="content">A reference to the content manager that invoked the read.</param>
        /// <returns>Returns true if the data can be read by this reader; otherwise false.</returns>
        public bool CanRead(string key, ContentManager<string> content)
        {
            var doc = new XmlDocument();
            try
            {
                doc.Load(key);
                return doc.DocumentElement != null && doc.DocumentElement.Name.ToLower() == "properties";
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Reads a file asynchronously and returns a type representing the data.
        /// </summary>
        /// <param name="key">The id to be read.</param>
        /// <param name="content">A reference to the content manager that invoked the read.</param>
        /// <param name="completedCallback">Specifies a callback that will be invoked when the read is complete.</param>
        public void ReadAsync(string key, ContentManager<string> content, Action<ReadAsyncArgs<string, object>> completedCallback)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(cb =>
            {
                var data = this.Read(key, content);
                if (completedCallback != null)
                {
                    completedCallback(new ReadAsyncArgs<string, object>() { Progress = 100, Key = key, Result = data, State = ReadState.Completed });
                }
            });
        }
    }
}
#endif
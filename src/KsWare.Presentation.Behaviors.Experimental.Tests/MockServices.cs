// Source: https://gist.github.com/SchreinerK/73002d709fe6ee365cd3d7b43186300c#file-mockservices-cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

// ReSharper disable LocalizableElement
// ReSharper disable CheckNamespace

namespace KsWare.TestTools
{

	internal static class MockServices
	{

		// http://www.rvenables.com/instantiating-classes-with-internal-constructors-roberts-c-musings/

		/// <summary>
		/// Instantiates T without calling a constructor.
		/// Works well with otherwise uninstantiable objects.
		/// </summary>
		/// <typeparam name="T">Anything that does NOT derive
		/// from ContextBoundObject.</typeparam>
		/// <param name="values">A dictionary of values to initialize
		/// the object in place of a constructor.</param>
		/// <returns>The newly created and instantiated object.</returns>
		public static T Create<T>(Dictionary<string, object> values)
		{
			if (values == null)
				throw new ArgumentNullException(nameof(values), $"Parameter '{nameof(values)}' is null!");

			return Fill(CreateBlank<T>(), values);
		}

		private static T CreateBlank<T>()
		{
			if (typeof(ContextBoundObject).IsAssignableFrom(typeof(T)))
				throw new ArgumentException("Types derives from ContextBoundObject!");

			return (T)FormatterServices
				.GetUninitializedObject(typeof(T));
		}

		public static T Fill<T>(T source, Dictionary<string, object> values)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source), $"Parameter '{nameof(source)}' is null!");

			if (values == null)
				throw new ArgumentNullException(nameof(values), $"Parameter '{nameof(values)}' is null!");

			if (values.Count == 0)
				return source;

			source = FillFields(source, values);

			source = FillProperties(source, values);

			return source;
		}

		public static T FillFields<T>(T source, Dictionary<string, object> values)
		{
			var type = typeof(T);

			while (type != null)
			{
				var eventFields = type.GetFields(
					BindingFlags.NonPublic |
					BindingFlags.Public |
					BindingFlags.Instance |
					BindingFlags.Static |
					BindingFlags.DeclaredOnly);

				if (eventFields.Length > 0)
					foreach (var fieldInfo in eventFields)
						if (values.ContainsKey(fieldInfo.Name)
							&& fieldInfo.FieldType.IsInstanceOfType(values[fieldInfo.Name]))
							fieldInfo.SetValue(source, values[fieldInfo.Name]);
				type = type.BaseType;
			}

			return source;
		}

		public static T FillProperties<T>(T source, Dictionary<string, object> values)
		{
			var type = typeof(T);

			while (type != null)
			{
				var propertyFields = type.GetProperties(
					BindingFlags.NonPublic |
					BindingFlags.Public |
					BindingFlags.Instance |
					BindingFlags.Static |
					BindingFlags.GetProperty);

				if (propertyFields.Any())
					foreach (var propertyField in propertyFields)
					{
						if (values.ContainsKey(propertyField.Name) && propertyField.CanWrite)
							propertyField.SetValue(source, values[propertyField.Name]);
						if (values.ContainsKey(propertyField.Name) && !propertyField.CanWrite)
						{
							var field = typeof(T).GetField($"<{propertyField.Name}>k__BackingField",
								BindingFlags.Instance | BindingFlags.NonPublic);
							field?.SetValue(source, values[propertyField.Name]);
						}
					}

				type = type.BaseType;
			}

			return source;
		}

	}

}
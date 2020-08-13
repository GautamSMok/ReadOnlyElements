using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReadOnlyElements
{
	public class ReadOnlyElement<T>
	{
		private readonly T _value;

		private ReadOnlyElement()
		{
			//avoid public instantiation
		}

		public T Value
		{
			get
			{
				return _value;
			}
		}

		private ReadOnlyElement(T element)
		{
			_value = element;
		}

		private static bool CheckForReadOnlyObject()
		{
			bool isReadOnly = false;

			isReadOnly = !typeof(T).GetProperties().Any(prop => prop.CanWrite);

			if (!isReadOnly)
			{
				throw new NotSupportedException("object is not readonly");
			}


			return isReadOnly;
		}

		private static bool AreAllFieldsReadOnly()
		{
			var typeInfo = typeof(T).GetTypeInfo();
			return typeInfo.DeclaredFields.All(d => d.Attributes.ToString().Contains("InitOnly"));
		}

		public static System.Collections.ObjectModel.ReadOnlyCollection<ReadOnlyElement<T>> GetReadOnlyList(params T[] elements)
		{
			return GetReadOnlyList(elements.ToList());
		}

		public static System.Collections.ObjectModel.ReadOnlyCollection<ReadOnlyElement<T>> GetReadOnlyList(IEnumerable<T> elements)
		{
			if (typeof(T).IsValueType || (AreAllFieldsReadOnly() && CheckForReadOnlyObject()))
			{
				List<ReadOnlyElement<T>> list = new List<ReadOnlyElement<T>>();
				elements.ToList().ForEach(e => {
					list.Add(new ReadOnlyElement<T>(e));
				});

				System.Collections.ObjectModel.ReadOnlyCollection<ReadOnlyElement<T>> readOnlyList =
					new System.Collections.ObjectModel.ReadOnlyCollection<ReadOnlyElement<T>>(list);
				return readOnlyList;
			}
			else
			{
				throw new NotSupportedException("Readonly elements are missing");
			}
		}

		public static ReadOnlyElement<T> GetReadOnlyValue(T value)
		{
			return new ReadOnlyElement<T>(value);
		}
	}
}

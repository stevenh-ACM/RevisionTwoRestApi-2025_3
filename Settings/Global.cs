#nullable disable

namespace RevisionTwoApp.RestApi.Settings;

internal static class Global
{
    // Static property accessible from anywhere
    internal static object[ ] GlobalProperties = default;

    internal static void InitializeGlobalProperties(int size)
    {
        GlobalProperties = new object[ size ];
    }
    internal static object GetGlobalProperty(int index)
    {
        if (index < 0 || index >= GlobalProperties.Length)
        {
            throw new IndexOutOfRangeException("Index is out of range of the global properties array.");
        }
        return GlobalProperties[ index ];
    }

    internal static object GetGlobalProperty(int index, Type type)
    {
        if (index < 0 || index >= GlobalProperties.Length)
        {
            throw new IndexOutOfRangeException("Index is out of range of the global properties array.");
        }
        if (GlobalProperties[ index ] != null && GlobalProperties[ index ].GetType() != type)
        {
            throw new InvalidCastException($"Value at index {index} is not of type {type.Name}.");
        }
        return GlobalProperties[ index ];
    }
    internal static object GetGlobalProperty(int index, Type type, bool allowNull = false)
    {
        if (index < 0 || index >= GlobalProperties.Length)
        {
            throw new IndexOutOfRangeException("Index is out of range of the global properties array.");
        }
        if (!allowNull && GlobalProperties[ index ] == null)
        {
            throw new ArgumentNullException(nameof(GlobalProperties), "Value cannot be null unless allowNull is true.");
        }
        if (GlobalProperties[ index ] != null && GlobalProperties[ index ].GetType() != type)
        {
            throw new InvalidCastException($"Value at index {index} is not of type {type.Name}.");
        }
        return GlobalProperties[ index ];
    }

    internal static void SetGlobalProperty(int index, object value)
    {
        if (index < 0 || index >= GlobalProperties.Length)
        {
            throw new IndexOutOfRangeException("Index is out of range of the global properties array.");
        }
        GlobalProperties[ index ] = value;
    }

    internal static void ClearGlobalProperties( )
    {
        GlobalProperties = new object[ GlobalProperties.Length ];
    }

    internal static void PrintGlobalProperties( )
    {
        for (int i = 0; i < GlobalProperties.Length; i++)
        {
            Console.WriteLine($"GlobalProperties[{i}]: {GlobalProperties[ i ]}");
        }
    }

    internal static void PrintGlobalPropertiesTypes( )
    {
        for (int i = 0; i < GlobalProperties.Length; i++)
        {
            Console.WriteLine($"GlobalProperties[{i}] Type: {GlobalProperties[ i ]?.GetType().Name ?? "null"}");
        }
    }

    internal static void ResetGlobalProperties( )
    {
        for (int i = 0; i < GlobalProperties.Length; i++)
        {
            GlobalProperties[ i ] = null;
        }
    }

    internal static void SetGlobalProperties(object[ ] properties)
    {
        if (properties == null || properties.Length != GlobalProperties.Length)
        {
            throw new ArgumentException("Provided properties array must match the size of the global properties array.");
        }
        GlobalProperties = properties;
    }

    internal static void SetGlobalProperty(int index, string value)
    {
        if (index < 0 || index >= GlobalProperties.Length)
        {
            throw new IndexOutOfRangeException("Index is out of range of the global properties array.");
        }
        GlobalProperties[ index ] = value;
    }

    internal static void SetGlobalProperty(int index, int value)
    {
        if (index < 0 || index >= GlobalProperties.Length)
        {
            throw new IndexOutOfRangeException("Index is out of range of the global properties array.");
        }
        GlobalProperties[ index ] = value;
    }

    internal static void SetGlobalProperty(int index, bool value)
    {
        if (index < 0 || index >= GlobalProperties.Length)
        {
            throw new IndexOutOfRangeException("Index is out of range of the global properties array.");
        }
        GlobalProperties[ index ] = value;
    }

    internal static void SetGlobalProperty(int index, object value, Type type)
    {
        if (index < 0 || index >= GlobalProperties.Length)
        {
            throw new IndexOutOfRangeException("Index is out of range of the global properties array.");
        }
        if (value != null && value.GetType() != type)
        {
            throw new ArgumentException($"Value must be of type {type.Name}.");
        }
        GlobalProperties[ index ] = value;
    }

    internal static void SetGlobalProperty(int index, object value, Type type, bool allowNull = false)
    {
        if (index < 0 || index >= GlobalProperties.Length)
        {
            throw new IndexOutOfRangeException("Index is out of range of the global properties array.");
        }
        if (!allowNull && value == null)
        {
            throw new ArgumentNullException(nameof(value), "Value cannot be null unless allowNull is true.");
        }
        if (value != null && value.GetType() != type)
        {
            throw new ArgumentException($"Value must be of type {type.Name}.");
        }
        GlobalProperties[ index ] = value;
    }

    internal static void SetGlobalProperty(int index, object value, Type type, bool allowNull = false, bool checkType = true)
    {
        if (index < 0 || index >= GlobalProperties.Length)
        {
            throw new IndexOutOfRangeException("Index is out of range of the global properties array.");
        }
        if (!allowNull && value == null)
        {
            throw new ArgumentNullException(nameof(value), "Value cannot be null unless allowNull is true.");
        }
        if (checkType && value != null && value.GetType() != type)
        {
            throw new ArgumentException($"Value must be of type {type.Name}.");
        }
        GlobalProperties[ index ] = value;
    }
    internal static void SetGlobalProperty(int index, object value, Type type, bool allowNull = false, bool checkType = true, bool throwOnError = true)
    {
        if (index < 0 || index >= GlobalProperties.Length)
        {
            if (throwOnError)
            {
                throw new IndexOutOfRangeException("Index is out of range of the global properties array.");
            }
            return;
        }
        if (!allowNull && value == null)
        {
            if (throwOnError)
            {
                throw new ArgumentNullException(nameof(value), "Value cannot be null unless allowNull is true.");
            }
            return;
        }
        if (checkType && value != null && value.GetType() != type)
        {
            if (throwOnError)
            {
                throw new ArgumentException($"Value must be of type {type.Name}.");
            }
            return;
        }
        GlobalProperties[ index ] = value;
    }

    internal static void SetGlobalProperty(int index, object value, Type type, bool allowNull = false, bool checkType = true, bool throwOnError = true, bool logError = false)
    {
        if (index < 0 || index >= GlobalProperties.Length)
        {
            if (throwOnError)
            {
                throw new IndexOutOfRangeException("Index is out of range of the global properties array.");
            }
            if (logError)
            {
                Console.WriteLine($"Error: Index {index} is out of range.");
            }
            return;
        }
        if (!allowNull && value == null)
        {
            if (throwOnError)
            {
                throw new ArgumentNullException(nameof(value), "Value cannot be null unless allowNull is true.");
            }
            if (logError)
            {
                Console.WriteLine("Error: Value cannot be null unless allowNull is true.");
            }
            return;
        }
        if (checkType && value != null && value.GetType() != type)
        {
            if (throwOnError)
            {
                throw new ArgumentException($"Value must be of type {type.Name}.");
            }
            if (logError)
            {
                Console.WriteLine($"Error: Value must be of type {type.Name}.");
            }
            return;
        }
        GlobalProperties[ index ] = value;
    }

    internal static void SetGlobalProperty(int index, object value, Type type, bool allowNull = false, bool checkType = true, bool throwOnError = true, bool logError = false, bool ignoreNull = false)
    {
        if (index < 0 || index >= GlobalProperties.Length)
        {
            if (throwOnError)
            {
                throw new IndexOutOfRangeException("Index is out of range of the global properties array.");
            }
            if (logError)
            {
                Console.WriteLine($"Error: Index {index} is out of range.");
            }
            return;
        }
        if (!allowNull && value == null && !ignoreNull)
        {
            if (throwOnError)
            {
                throw new ArgumentNullException(nameof(value), "Value cannot be null unless allowNull is true.");
            }
            if (logError)
            {
                Console.WriteLine("Error: Value cannot be null unless allowNull is true.");
            }
            return;
        }
        if (checkType && value != null && value.GetType() != type)
        {
            if (throwOnError)
            {
                throw new ArgumentException($"Value must be of type {type.Name}.");
            }
            if (logError)
            {
                Console.WriteLine($"Error: Value must be of type {type.Name}.");
            }
            return;
        }
        GlobalProperties[ index ] = value;
    }

    internal static void SetGlobalProperty(int index, object value, Type type, bool allowNull = false, bool checkType = true, bool throwOnError = true, bool logError = false, bool ignoreNull = false, bool ignoreTypeCheck = false)
    {
        if (index < 0 || index >= GlobalProperties.Length)
        {
            if (throwOnError)
            {
                throw new IndexOutOfRangeException("Index is out of range of the global properties array.");
            }
            if (logError)
            {
                Console.WriteLine($"Error: Index {index} is out of range.");
            }
            return;
        }
        if (!allowNull && value == null && !ignoreNull)
        {
            if (throwOnError)
            {
                throw new ArgumentNullException(nameof(value), "Value cannot be null unless allowNull is true.");
            }
            if (logError)
            {
                Console.WriteLine("Error: Value cannot be null unless allowNull is true.");
            }
            return;
        }
        if (!ignoreTypeCheck && checkType && value != null && value.GetType() != type)
        {
            if (throwOnError)
            {
                throw new ArgumentException($"Value must be of type {type.Name}.");
            }
            if (logError)
            {
                Console.WriteLine($"Error: Value must be of type {type.Name}.");
            }
            return;
        }
        GlobalProperties[ index ] = value;
    }
}

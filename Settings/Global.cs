#nullable disable

namespace RevisionTwoApp.RestApi.Settings;

/// <summary>
/// Provides a container for global properties and methods to manage their initialization and reset.
/// </summary>
/// <remarks>This class is intended for internal use only and serves as a centralized location for managing global
/// properties. It includes methods to initialize and reset the properties, ensuring proper setup and maintenance of the
/// global state.</remarks>
internal static class Globals
{
    /// <summary>
    /// Represents a two-dimensional array of global properties.
    /// </summary>
    /// <remarks>This field is intended for internal use only and stores a two-dimensional array of objects. It is
    /// initialized to its default value.</remarks>
    internal static List<Tuple<object, object>> _globalProperties = default;

    /// <summary>
    /// Gets or sets the starting date for the operation or data range.
    /// </summary>
    private static DateTime FromDate { get; set; } = DateTime.Now.AddDays(-90); // Default to 90 days ago   

    /// <summary>
    /// Gets or sets the date value representing the latest date in a date range.
    /// </summary>
    private static DateTime ToDate { get; set; } = DateTime.Now; // Default to current date

    /// <summary>
    /// Gets or sets the total number of records processed or available.
    /// </summary>
    private static int NumRecords { get; set; } = 10; // Default number of records to retrieve

    /// <summary>
    /// Initializes a collection of global properties with their default values.
    /// </summary>
    /// <remarks>This method creates and returns a list of key-value pairs representing global properties. Each
    /// property is initialized with a default value, which can be used to configure application-wide settings.</remarks>
    /// <returns>A list of <see cref="Tuple{T1, T2}"/> objects, where each tuple contains a property name (as an <see
    /// cref="object"/>)  and its corresponding default value (as an <see cref="object"/>).</returns>
    internal static List<Tuple<object, object>> InitializeProperties()
    {
        _globalProperties = new List<Tuple<object, object>>
        {
            new Tuple<object, object>("RefreshFlag" , false),
            new Tuple<object, object>("EditFlag"    , false),
            new Tuple<object, object>("DeleteFlag"  , false),
            new Tuple<object, object>("FromDate"    , FromDate),
            new Tuple<object, object>("ToDate"      , ToDate),
            new Tuple<object, object>("NumRecords"  , NumRecords),
            new Tuple<object, object>("Selected_SalesOrder_Type", "SO"), // Default Sales Order Type
            new Tuple<object, object>("InventoryID" , "AACOMPUT01") // Default Inventory ID for Sales Orders
        };
        return _globalProperties;
    }    

    /// <summary>
    /// Gets a list of global properties represented as key-value pairs.
    /// </summary>
    /// <remarks>The global properties are lazily initialized when accessed. If the list is empty or
    /// uninitialized, it will be populated by the <c>InitializeProperties</c> method.</remarks>
    internal static List<Tuple<object, object>> GlobalProperties
    {
        get
        {
            if (_globalProperties == null || _globalProperties.Count == 0)
            {
                _globalProperties = InitializeProperties();
            }
            return _globalProperties;
        }
    }

    /// <summary>
    /// Retrieves a list of global properties as key-value pairs.
    /// </summary>
    /// <remarks>This method returns a list of tuples representing global properties, where each tuple
    /// contains a key and its associated value. If the global properties have not been initialized or are empty, they
    /// are initialized before being returned.</remarks>
    /// <returns>A list of <see cref="Tuple{T1, T2}"/> objects, where each tuple contains a key and its associated value. The
    /// list will be empty if no global properties are available.</returns>
    internal static List<Tuple<object, object>> GetGlobalProperties( )
    {
        if (_globalProperties == null || _globalProperties.Count == 0)
        {
            _globalProperties = InitializeProperties();
        }
        return _globalProperties;
    }

    /// <summary>
    /// Retrieves the value of a global property by its name.
    /// </summary>
    /// <remarks>This method searches for a global property by name and returns its value if found. If the
    /// property does not exist, an <see cref="ArgumentException"/> is thrown.</remarks>
    /// <param name="propertyName">The name of the global property to retrieve. This parameter is case-sensitive and cannot be null or empty.</param>
    /// <returns>The value of the global property associated with the specified name.</returns>
    /// <exception cref="ArgumentException">Thrown if the specified <paramref name="propertyName"/> does not exist in the global properties.</exception>
    internal static object GetGlobalProperty(string propertyName)
    {
        var property = _globalProperties.FirstOrDefault(p => p.Item1.ToString() == propertyName);
        if (property != null)
        {
            return property.Item2;
        }
        else
        {
            throw new ArgumentException($"Property '{propertyName}' not found in global properties.");
        }
    }

    /// <summary>
    /// Retrieves the value of a global property by its name.
    /// </summary>
    /// <remarks>If the specified property name does not exist in the global properties collection, a warning
    /// is logged using the provided <paramref name="logger"/>.</remarks>
    /// <param name="propertyName">The name of the global property to retrieve. This parameter cannot be null or empty.</param>
    /// <param name="logger">An <see cref="ILogger"/> instance used to log warnings if the specified property is not found.</param>
    /// <returns>The value of the global property if found; otherwise, <see langword="null"/>.</returns>
    internal static object GetGlobalProperty(string propertyName, ILogger logger)
    {
        var property = _globalProperties.FirstOrDefault(p => p.Item1.ToString() == propertyName);
        if (property != null)
        {
            return property.Item2;
        }
        else
        {
            logger.LogWarning($"Global Property '{propertyName}' not found.");
            return null;
        }
    }

    /// <summary>
    /// Logs all global properties to the specified logger.
    /// </summary>
    /// <remarks>This method formats the global properties as a comma-separated list and logs them as an
    /// informational message.</remarks>
    /// <param name="logger">The logger instance used to log the global properties. Cannot be <see langword="null"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="logger"/> is <see langword="null"/>.</exception>
    internal static void LogGlobalProperties(ILogger logger)
    {
        if (logger == null) throw new ArgumentNullException(nameof(logger));
        
        var infoMessage = $"Global Properties: {string.Join(", ", _globalProperties.Select(p => $"{p.Item1}: {p.Item2}"))}";
        logger.LogInformation(infoMessage);
    }

    /// <summary>
    /// Logs the value of a global property with the specified name using the provided logger.
    /// </summary>
    /// <remarks>If the specified global property is found, its name and value are logged as informational
    /// messages. If the property is not found, a warning message is logged instead.</remarks>
    /// <param name="propertyName">The name of the global property to log. This value is case-sensitive.</param>
    /// <param name="logger">The logger instance used to log the property information. Cannot be <see langword="null"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="logger"/> is <see langword="null"/>.</exception>
    internal static void LogGlobalProperty(string propertyName, ILogger logger)
    {
        if (logger == null) throw new ArgumentNullException(nameof(logger));
        
        var property = _globalProperties.FirstOrDefault(p => p.Item1.ToString() == propertyName);
        if (property != null)
        {
            logger.LogInformation($"Global Property - {property.Item1}: {property.Item2}");
        }
        else
        {
            logger.LogWarning($"Global Property '{propertyName}' not found.");
        }
    }   

    /// <summary>
    /// Sets the global properties for the application.
    /// </summary>
    /// <remarks>This method updates the global properties with the provided list of key-value pairs. Ensure
    /// that the list contains valid entries, as the method does not perform additional validation on the individual
    /// keys or values.</remarks>
    /// <param name="properties">A list of key-value pairs representing the global properties to set.  Each tuple contains an object as the key
    /// and an object as the value.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="properties"/> is null or empty.</exception>
    internal static void SetGlobalProperties(List<Tuple<object, object>> properties)
    {
        if (properties == null || properties.Count == 0)
        {
            throw new ArgumentException("Properties cannot be null or empty.", nameof(properties));
        }
        _globalProperties = properties;
    }

    /// <summary>
    /// Sets global properties for the application.
    /// </summary>
    /// <remarks>This method updates the global properties for the application and logs the operation. Ensure
    /// that the <paramref name="properties"/> list contains valid key-value pairs.</remarks>
    /// <param name="properties">A list of key-value pairs representing the global properties to set.  Each tuple contains an object key and an
    /// object value. The list cannot be null or empty.</param>
    /// <param name="logger">The logger instance used to log information about the operation.  Cannot be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="logger"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="properties"/> is null or empty.</exception>
    internal static void SetGlobalProperties(List<Tuple<object, object>> properties, ILogger logger)
    {
        if (logger == null) throw new ArgumentNullException(nameof(logger));
        if (properties == null || properties.Count == 0)
        {
            throw new ArgumentException("Properties cannot be null or empty.", nameof(properties));
        }
        logger.LogInformation("Setting global properties.");
        _globalProperties = properties;
    }

    /// <summary>
    /// Updates the value of an existing global property.
    /// </summary>
    /// <remarks>This method searches for a global property by its name and updates its value if found.  If
    /// the property does not exist, an <see cref="ArgumentException"/> is thrown.</remarks>
    /// <param name="propertyName">The name of the global property to update. This must match the name of an existing property.</param>
    /// <param name="value">The new value to assign to the specified global property.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="propertyName"/> does not match any existing global property.</exception>
    internal static void SetGlobalProperty(string propertyName, object value)
    {
        var property = _globalProperties.FirstOrDefault(p => p.Item1.ToString() == propertyName);
        if (property != null)
        {
            _globalProperties.Remove(property);
            _globalProperties.Add(new Tuple<object, object>(propertyName, value));
        }
        else
        {
            throw new ArgumentException($"Property '{propertyName}' not found in global properties.");
        }
    }

    /// <summary>
    /// Updates the value of an existing global property.
    /// </summary>
    /// <remarks>This method searches for a global property by name and updates its value. If the property is found, 
    /// the update is logged using the provided <paramref name="logger"/>. If the property does not exist,  an exception is
    /// thrown.</remarks>
    /// <param name="propertyName">The name of the global property to update. Must match an existing property.</param>
    /// <param name="value">The new value to assign to the specified global property.</param>
    /// <param name="logger">An <see cref="ILogger"/> instance used to log the update operation. Cannot be <see langword="null"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="logger"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="propertyName"/> does not match any existing global property.</exception>
    internal static void SetGlobalProperty(string propertyName, object value, ILogger logger)
    {
        if (logger == null) throw new ArgumentNullException(nameof(logger));
        var property = _globalProperties.FirstOrDefault(p => p.Item1.ToString() == propertyName);
        if (property != null)
        {
             logger.LogInformation($"Updating global property '{propertyName}' to value: {value}");
            _globalProperties.Remove(property);
            _globalProperties.Add(new Tuple<object, object>(propertyName, value));
        }
        else
        {
            throw new ArgumentException($"Property '{propertyName}' not found in global properties.");
        }
    }

    /// <summary>
    /// Resets the global properties to their default values.
    /// </summary>
    /// <remarks>This method initializes global properties to their default state.  The default values include
    /// a date range spanning the last 90 days,  a default number of records set to 10, and other properties initialized
    /// as required.</remarks>
    internal static void ResetGlobalProperties( )
    {
        _globalProperties = InitializeProperties();
        FromDate = DateTime.Now.AddDays(-90); // Default to 90 days ago
        ToDate = DateTime.Now; // Default to today
        NumRecords = 10; // Default number of records
    }

    /// <summary>
    /// Resets global property flags to their default values.
    /// </summary>
    /// <remarks>This method sets the global properties "RefreshFlag", "EditFlag", and "DeleteFlag" to <see
    /// langword="false"/>. It is intended to restore the default state of these flags.</remarks>
    internal static void ResetGlobalPropertyFlags()
    {
        SetGlobalProperty("RefreshFlag", false);
        SetGlobalProperty("EditFlag", false);
        SetGlobalProperty("DeleteFlag", false);
    }

    /// <summary>
    /// Resets global property flags to their default values and logs the updated state.    
    /// </summary>
    /// <param name="logger">The logger used to record the updated state of the global property flags. Cannot be <see langword="null"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="logger"/> is <see langword="null"/>.</exception>
    internal static void ResetGlobalPropertyFlags(ILogger logger)
    {
        if (logger == null) throw new ArgumentNullException(nameof(logger));

        SetGlobalProperty("RefreshFlag", false);
        SetGlobalProperty("EditFlag", false);
        SetGlobalProperty("DeleteFlag", false);

        LogGlobalProperties(logger);
    }
    /// <summary>
    /// Clears all global properties and resets them to their default state.
    /// </summary>
    /// <remarks>This method removes all entries from the global properties collection and reinitializes  them
    /// to their default values. It is typically used to ensure a clean state for global properties.</remarks>
    internal static void ClearGlobalProperties( )
    {
        _globalProperties.Clear();
        ResetGlobalProperties();
    }

    /// <summary>
    /// Adds a global property to the internal collection.
    /// </summary>
    /// <remarks>This method stores the specified property name and value in an internal collection. The
    /// collection is initialized if it has not been created yet.</remarks>
    /// <param name="propertyName">The name of the property to add. Cannot be <see langword="null"/> or empty.</param>
    /// <param name="value">The value associated with the property. Can be <see langword="null"/>.</param>
    internal static void AddGlobalProperty(string propertyName, object value)
    {
        if (_globalProperties == null)
        {
            _globalProperties = new List<Tuple<object, object>>();
        }
        _globalProperties.Add(new Tuple<object, object>(propertyName, value));
    }

    /// <summary>
    /// Adds a global property to the internal collection.
    /// </summary>
    /// <remarks>This method adds the specified property name and value to an internal collection of global
    /// properties. The addition is logged using the provided <paramref name="logger"/>.</remarks>
    /// <param name="propertyName">The name of the property to add. Cannot be <see langword="null"/> or empty.</param>
    /// <param name="value">The value associated with the property. Can be <see langword="null"/>.</param>
    /// <param name="logger">The logger used to record the addition of the property. Cannot be <see langword="null"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="logger"/> is <see langword="null"/>.</exception>
    internal static void AddGlobalProperty(string propertyName, object value, ILogger logger)
    {
        if (logger == null) throw new ArgumentNullException(nameof(logger));
        if (_globalProperties == null)
        {
            _globalProperties = new List<Tuple<object, object>>();
        }
        _globalProperties.Add(new Tuple<object, object>(propertyName, value));
        logger.LogInformation($"Added global property '{propertyName}' with value: {value}");
    }

    /// <summary>
    /// Removes a global property with the specified name.
    /// </summary>
    /// <remarks>This method searches for a global property by name and removes it if found. If no matching
    /// property exists, an exception is thrown.</remarks>
    /// <param name="propertyName">The name of the global property to remove. This value is case-sensitive and must match the name of an existing
    /// property.</param>
    /// <exception cref="ArgumentException">Thrown if a global property with the specified <paramref name="propertyName"/> does not exist.</exception>
    internal static void RemoveGlobalProperty(string propertyName)
    {
        var property = _globalProperties.FirstOrDefault(p => p.Item1.ToString() == propertyName);
        if (property != null)
        {
            _globalProperties.Remove(property);
        }
        else
        {
            throw new ArgumentException($"Property '{propertyName}' not found in global properties.");
        }
    }

    /// <summary>
    /// Removes a global property with the specified name from the collection of global properties.
    /// </summary>
    /// <remarks>If the specified property is found, it is removed from the collection, and an informational
    /// log entry is created. If the property is not found, a warning log entry is created instead.</remarks>
    /// <param name="propertyName">The name of the global property to remove. This value is case-sensitive.</param>
    /// <param name="logger">The logger used to record information or warnings about the operation. Cannot be <see langword="null"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="logger"/> is <see langword="null"/>.</exception>
    internal static void RemoveGlobalProperty(string propertyName, ILogger logger)
    {
        if (logger == null) throw new ArgumentNullException(nameof(logger));
        var property = _globalProperties.FirstOrDefault(p => p.Item1.ToString() == propertyName);
        if (property != null)
        {
            _globalProperties.Remove(property);
            logger.LogInformation($"Removed global property '{propertyName}'.");
        }
        else
        {
            logger.LogWarning($"Global Property '{propertyName}' not found.");
        }
    }

    /// <summary>
    /// Removes a global property with the specified name.
    /// </summary>
    /// <remarks>If the specified property is found, it is removed and an informational message is logged. If
    /// the property is not found and <paramref name="logWarningIfNotFound"/> is <see langword="true"/>, a warning
    /// message is logged.</remarks>
    /// <param name="propertyName">The name of the global property to remove. This parameter is case-sensitive.</param>
    /// <param name="logger">The logger instance used to log informational or warning messages. Cannot be <see langword="null"/>.</param>
    /// <param name="logWarningIfNotFound">Indicates whether a warning should be logged if the specified property is not found. Defaults to <see
    /// langword="true"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="logger"/> is <see langword="null"/>.</exception>
    internal static void RemoveGlobalProperty(string propertyName, ILogger logger, bool logWarningIfNotFound = true)
    {
        if (logger == null) throw new ArgumentNullException(nameof(logger));
        var property = _globalProperties.FirstOrDefault(p => p.Item1.ToString() == propertyName);
        if (property != null)
        {
            _globalProperties.Remove(property);
            logger.LogInformation($"Removed global property '{propertyName}'.");
        }
        else if (logWarningIfNotFound)
        {
            logger.LogWarning($"Global Property '{propertyName}' not found.");
        }
    }

    /// <summary>
    /// Removes a specified global property from the collection of global properties.
    /// </summary>
    /// <remarks>If the specified global property exists in the collection, it is removed and an informational
    /// log entry is created. If the property does not exist, a warning log entry is created.</remarks>
    /// <param name="property">A tuple representing the global property to remove. The first item is the property key, and the second item is
    /// the property value.</param>
    /// <param name="logger">An <see cref="ILogger"/> instance used to log information or warnings about the removal operation. Cannot be
    /// <see langword="null"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="logger"/> is <see langword="null"/>.</exception>
    internal static void RemoveGlobalProperty(Tuple<object, object> property, ILogger logger)
    {
        if (logger == null) throw new ArgumentNullException(nameof(logger));
        if (_globalProperties.Contains(property))
        {
            _globalProperties.Remove(property);
            logger.LogInformation($"Removed global property '{property.Item1}'.");
        }
        else
        {
            logger.LogWarning($"Global Property '{property.Item1}' not found.");
        }
    }
    
    /// <summary>
    /// Logs information about a global property based on its name.
    /// </summary>
    /// <remarks>If a global property matching <paramref name="propertyName"/> is found, its name and value
    /// are logged as  informational messages. If no matching property is found, a warning message is logged
    /// instead.</remarks>
    /// <param name="propertyName">The name of the global property to search for. This parameter cannot be null or empty.</param>
    /// <param name="logger">The logger used to record information or warnings about the global property. This parameter cannot be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="logger"/> is null.</exception>
    internal static void GetTupleByName(string propertyName, ILogger logger)
    {
        if (logger == null) throw new ArgumentNullException(nameof(logger));
        var property = _globalProperties.FirstOrDefault(p => p.Item1.ToString() == propertyName);
        if (property != null)
        {
            logger.LogInformation($"Global Property - {property.Item1}: {property.Item2}");
        }
        else
        {
            logger.LogWarning($"Global Property '{propertyName}' not found.");
        }
    }
}

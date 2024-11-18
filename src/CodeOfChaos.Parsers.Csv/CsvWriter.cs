// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Reflection;

namespace CodeOfChaos.Parsers.Csv;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvWriter<T>(CsvParserConfig config) where T : new() {
    private bool IsDictionary => typeof(T)
        .GetInterfaces()
        .Any(interfaceType => interfaceType.IsGenericType 
            && interfaceType.GetGenericTypeDefinition() == typeof(IDictionary<,>)  // Must be a dictionary
            && interfaceType.GetGenericArguments()[0] == typeof(string)); // Key type must be a string

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static CsvWriter<T> FromConfig(Action<CsvParserConfig> configAction) {
        var config = new CsvParserConfig();
        configAction(config);
        return new CsvWriter<T>(config);
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public string WriteToString(IEnumerable<T> data) {
        using var writer = new StringWriter();
        if (IsDictionary) {
            DictionaryToCsv(writer, data);
            return writer.ToString();
        }

        ToCsv(writer, data);
        return writer.ToString();
    }

    public async Task<string> WriteToStringAsync(IEnumerable<T> data) {
        await using var writer = new StringWriter();
        if (IsDictionary) {
            await DictionaryToCsvAsync(writer, data);
            return writer.ToString();
        }
        
        await ToCsvAsync(writer, data);
        return writer.ToString();
    }
    
    public void WriteToFile(string filePath, IEnumerable<T> data) {
        using var writer = new StreamWriter(filePath);
        if (IsDictionary) {
            DictionaryToCsv(writer, data);
            return;
        }
        ToCsv(writer, data);
    }
    
    public async Task WriteToFileAsync(string filePath, IEnumerable<T> data) {
        await using var writer = new StreamWriter(filePath);
        if (IsDictionary) {
            await DictionaryToCsvAsync(writer, data);
            return;
        }
        await ToCsvAsync(writer, data);
    }

    #region Property based parsing
    private void ToCsv(TextWriter writer, IEnumerable<T> data) {
        // Write header row
        IEnumerable<T> enumerable = data as T[] ?? data.ToArray();
        PropertyInfo[] propertyInfos = GetCsvProperties(enumerable.FirstOrDefault()); // Dirty but it will work
    
        if (config.IncludeHeader) {
            string[] headers = GetCsvHeaders(propertyInfos).ToArray();
            for (int i = 0; i < headers.Length; i++) {
                writer.Write(headers[i]);
                if (i < headers.Length - 1) writer.Write(config.ColumnSplit);
            }
            writer.Write(Environment.NewLine);
        }

        // Write data rows
        foreach (T? obj in enumerable) {
            string[] values = GetCsvValues(obj, propertyInfos).ToArray();
            for (int i = 0; i < values.Length; i++) {
                writer.Write(values[i]);
                if (i < values.Length - 1) writer.Write(config.ColumnSplit);
            }
            writer.Write(Environment.NewLine);
        }
    }

    private async Task ToCsvAsync(TextWriter writer, IEnumerable<T> data) {
        // Write header row
        IEnumerable<T> enumerable = data as T[] ?? data.ToArray();
        PropertyInfo[] propertyInfos = GetCsvProperties(enumerable.FirstOrDefault());
    
        if (config.IncludeHeader) {
            string[] headers = GetCsvHeaders(propertyInfos).ToArray();
            for (int i = 0; i < headers.Length; i++) {
                await writer.WriteAsync(headers[i]);
                if (i < headers.Length - 1) await writer.WriteAsync(config.ColumnSplit);
            }
            await writer.WriteAsync(Environment.NewLine);
        }

        // Write data rows
        foreach (T? obj in enumerable) {
            string[] values = GetCsvValues(obj, propertyInfos).ToArray();
            for (int i = 0; i < values.Length; i++) {
                await writer.WriteAsync(values[i]);
                if (i < values.Length - 1) await writer.WriteAsync(config.ColumnSplit);
            }
            await writer.WriteAsync(Environment.NewLine);
        }
    }
    

    private static PropertyInfo[] GetCsvProperties(T? obj) => obj?
        .GetType()
        .GetProperties()
        .ToArray() ?? [];

    private IEnumerable<string> GetCsvHeaders(PropertyInfo[] propertyInfos) {
        return propertyInfos
            .Select(p => {
                if (p.GetCustomAttribute<CsvColumnAttribute>() is not {} attribute) 
                    return config.UseLowerCaseHeaders ? p.Name.ToLowerInvariant() : p.Name;

                return config.UseLowerCaseHeaders
                    ? attribute.NameLowerInvariant
                    : attribute.Name;
            });
    }

    private static IEnumerable<string> GetCsvValues(T? obj, PropertyInfo[] propertyInfos) {
        if (obj is null) return [];
        
        PropertyInfo[] properties = propertyInfos.Length != 0
            ? propertyInfos
            : obj.GetType().GetProperties();
        
        return properties
            .Select(p => p.GetValue(obj)?.ToString() ?? string.Empty);
    }
    #endregion
    
    #region Dictionary based Parsing
    private void DictionaryToCsv(TextWriter writer, IEnumerable<T> data) {
        IDictionary<string, object>[] records = data
            .Cast<IDictionary<string, object>>()
            .ToArray();

        // Write header row
        if (config.IncludeHeader) {
            IDictionary<string, object>? firstDictionary = records.FirstOrDefault();
            if (firstDictionary is not null) {
                IEnumerable<string> headers = firstDictionary.Keys;
                writer.WriteLine(string.Join(config.ColumnSplit, headers));
            }
        }

        // Write data rows
        foreach (IDictionary<string, object> dictionary in records) {
            IEnumerable<string> values = dictionary.Values.Select(value => value.ToString() ?? string.Empty);
            writer.WriteLine(string.Join(config.ColumnSplit, values));
        }
    }
    private async Task DictionaryToCsvAsync(TextWriter writer, IEnumerable<T> data) {
        IDictionary<string, object>[] records = data
            .Cast<IDictionary<string, object>>()
            .ToArray();

        // Write header row
        if (config.IncludeHeader) {
            IDictionary<string, object>? firstDictionary = records.FirstOrDefault();
            if (firstDictionary is not null) {
                IEnumerable<string> headers = firstDictionary.Keys;
                await writer.WriteLineAsync(string.Join(config.ColumnSplit, headers));
            }
        }

        // Write data rows
        foreach (IDictionary<string, object> dictionary in records) {
            IEnumerable<string> values = dictionary.Values.Select(value => value.ToString() ?? string.Empty);
            await writer.WriteLineAsync(string.Join(config.ColumnSplit, values));
        }
    }
    #endregion
}

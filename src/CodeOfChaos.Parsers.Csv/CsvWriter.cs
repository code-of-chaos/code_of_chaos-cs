// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Reflection;

namespace CodeOfChaos.Parsers.Csv;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvWriter<T>(CsvParserConfig config) where T : new() {

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
    public string WriteToCsv(IEnumerable<T> data) {
        using var writer = new StringWriter();
        WriteToCsv(writer, data);
        return writer.ToString();
    }

    private void WriteToCsv(TextWriter writer, IEnumerable<T> data) {
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

    public async Task WriteToCsvAsync(TextWriter writer, IEnumerable<T> data) {
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
}

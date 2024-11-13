// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions;
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
        PropertyInfo[] propertyInfos = GetCsvProperties();
        if (config.IncludeHeader) {
            foreach (string header in GetCsvHeaders(propertyInfos)) {
                writer.Write(header);
                writer.Write(config.ColumnSplit);
            }
            writer.Write(Environment.NewLine);
        }

        // Write data rows
        foreach (T obj in data) {
            foreach (string value in GetCsvValues(obj,propertyInfos)) {
                writer.Write(value);
                writer.Write(config.ColumnSplit);
            }
            writer.Write(Environment.NewLine);
        }
    }

    public async Task WriteToCsvAsync(TextWriter writer, IEnumerable<T> data) {
        // Write header row
        PropertyInfo[] propertyInfos = GetCsvProperties();
        if (config.IncludeHeader) {
            foreach (string header in GetCsvHeaders(propertyInfos)) {
                await writer.WriteAsync(header);
                await writer.WriteAsync(config.ColumnSplit);
            }
            await writer.WriteAsync(Environment.NewLine);
        }

        // Write data rows
        foreach (T obj in data) {
            foreach (string value in GetCsvValues(obj, propertyInfos)) {
                await writer.WriteAsync(value);
                await writer.WriteAsync(config.ColumnSplit);
            }
            await writer.WriteAsync(Environment.NewLine);
        }
    }

    private static PropertyInfo[] GetCsvProperties() => typeof(T).GetProperties()
        .ToArray();

    private IEnumerable<string> GetCsvHeaders(PropertyInfo[] propertyInfos) {
        return propertyInfos
            .Select(p => p.GetCustomAttribute<CsvColumnAttribute>() is {} attribute
                ? config.UseLowerCaseHeaders 
                    ? attribute.NameLowerInvariant 
                    : attribute.Name
                : p.Name)
            ;
    }

    private static IEnumerable<string> GetCsvValues(T obj, PropertyInfo[] propertyInfos) {
        if (obj is null) return [];
        
        PropertyInfo[] properties = propertyInfos.Length != 0
            ? propertyInfos
            : obj.GetType().GetProperties();
        
        return properties
            .Select(p => p.GetValue(obj)?.ToString() ?? string.Empty);
    }
}

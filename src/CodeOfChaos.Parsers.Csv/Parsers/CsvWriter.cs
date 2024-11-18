// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Parsers.Csv.Attributes;
using System.Reflection;

namespace CodeOfChaos.Parsers.Csv.Parsers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvWriter<T>(Action<CsvParserConfig> configAction) : CsvParser(configAction)
    where T : class, new() {
    public string WriteToString(IEnumerable<T> data) {
        using var writer = new StringWriter();
        ToCsv(writer, data);
        return writer.ToString();
    }

    public async Task<string> WriteToStringAsync(IEnumerable<T> data) {
        await using var writer = new StringWriter();
        await ToCsvAsync(writer, data);
        return writer.ToString();
    }

    public void WriteToFile(string filePath, IEnumerable<T> data) {
        using var writer = new StreamWriter(filePath);
        ToCsv(writer, data);
    }

    public async Task WriteToFileAsync(string filePath, IEnumerable<T> data) {
        await using var writer = new StreamWriter(filePath);
        await ToCsvAsync(writer, data);
    }

    #region Helper Methods
    private void ToCsv(TextWriter writer, IEnumerable<T> data) {
        // Write header row
        IEnumerable<T> enumerable = data as T[] ?? data.ToArray();
        PropertyInfo[] propertyInfos = GetCsvProperties(enumerable.FirstOrDefault());// Dirty but it will work

        if (Config.IncludeHeader) {
            string[] headers = GetCsvHeaders(propertyInfos).ToArray();
            for (int i = 0; i < headers.Length; i++) {
                writer.Write(headers[i]);
                if (i < headers.Length - 1) writer.Write(Config.ColumnSplit);
            }

            writer.Write(Environment.NewLine);
        }

        // Write data rows
        foreach (T? obj in enumerable) {
            string[] values = GetCsvValues(obj, propertyInfos).ToArray();
            for (int i = 0; i < values.Length; i++) {
                writer.Write(values[i]);
                if (i < values.Length - 1) writer.Write(Config.ColumnSplit);
            }

            writer.Write(Environment.NewLine);
        }
    }

    private async Task ToCsvAsync(TextWriter writer, IEnumerable<T> data) {
        // Write header row
        IEnumerable<T> enumerable = data as T[] ?? data.ToArray();
        PropertyInfo[] propertyInfos = GetCsvProperties(enumerable.FirstOrDefault());

        if (Config.IncludeHeader) {
            string[] headers = GetCsvHeaders(propertyInfos).ToArray();
            for (int i = 0; i < headers.Length; i++) {
                await writer.WriteAsync(headers[i]);
                if (i < headers.Length - 1) await writer.WriteAsync(Config.ColumnSplit);
            }

            await writer.WriteAsync(Environment.NewLine);
        }

        // Write data rows
        foreach (T? obj in enumerable) {
            string[] values = GetCsvValues(obj, propertyInfos).ToArray();
            for (int i = 0; i < values.Length; i++) {
                await writer.WriteAsync(values[i]);
                if (i < values.Length - 1) await writer.WriteAsync(Config.ColumnSplit);
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
                    return Config.UseLowerCaseHeaders ? p.Name.ToLowerInvariant() : p.Name;

                return Config.UseLowerCaseHeaders
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

}

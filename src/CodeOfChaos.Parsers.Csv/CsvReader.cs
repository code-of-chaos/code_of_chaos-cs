// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Reflection;

namespace CodeOfChaos.Parsers.Csv;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvReader<T>(CsvParserConfig config) where T : new() {

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static CsvReader<T> FromConfig(Action<CsvParserConfig> configAction) {
        var config = new CsvParserConfig();
        configAction(config);
        return new CsvReader<T>(config);
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private IEnumerable<T> ReadFromCsv(TextReader reader) {
        string[] headerColumns = [];
        if (reader.ReadLine() is {} lineFull) {
            headerColumns = lineFull.Split(config.ColumnSplit);
        }

        while (true) {
            if (reader.ReadLine() is not {} line) break;

            string[] values = line.Split(config.ColumnSplit);
            var obj = new T();

            foreach (PropertyInfo prop in typeof(T).GetProperties()) {
                if (Attribute.GetCustomAttribute(prop, typeof(CsvColumnAttribute)) is not CsvColumnAttribute attribute) continue;

                int columnIndex = Array.IndexOf(headerColumns, attribute.Name);
                if (columnIndex == -1) continue;

                prop.SetValue(obj, Convert.ChangeType(values[columnIndex], prop.PropertyType));
            }

            yield return obj;
        }
    }

    private async IAsyncEnumerable<T> ReadFromCsvAsync(TextReader reader) {
        string[] headerColumns = [];
        if (await reader.ReadLineAsync() is {} lineFull) {
            headerColumns = lineFull.Split(config.ColumnSplit);
        }

        while (true) {
            if (await reader.ReadLineAsync() is not {} line) break;

            string[] values = line.Split(config.ColumnSplit);
            var obj = new T();

            foreach (PropertyInfo prop in typeof(T).GetProperties()) {
                if (Attribute.GetCustomAttribute(prop, typeof(CsvColumnAttribute)) is not CsvColumnAttribute attribute) continue;

                int columnIndex = Array.IndexOf(headerColumns, attribute.Name);
                if (columnIndex == -1) continue;

                prop.SetValue(obj, Convert.ChangeType(values[columnIndex], prop.PropertyType));
            }

            yield return obj;
        }
    }
    
    public IEnumerable<T> ReadFromCsvFile(string filePath) {
        using var reader = new StreamReader(filePath);
        return ReadFromCsv(reader);
    }

    public IAsyncEnumerable<T> ReadFromCsvFileAsync(string filePath) {
        using var reader = new StreamReader(filePath);
        return ReadFromCsvAsync(reader);
    }
}

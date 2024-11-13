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
    public IEnumerable<T> ReadFromCsv(TextReader reader) {
        string[] headerColumns = [];
        if (reader.ReadLine() is {} lineFull) {
            headerColumns = lineFull.Split(config.ColumnSplit);
        }

        while (true) {
            if (reader.ReadLine() is not {} line) break;

            string[] values = line.Split(config.ColumnSplit);
            var obj = new T();

            SetPropertyFromCsvColumn(obj, headerColumns, values);

            yield return obj;
        }
    }
    private void SetPropertyFromCsvColumn(T value, string[] headerColumns, string[] values) {
        if (value is null) return;
        foreach (PropertyInfo prop in value.GetType().GetProperties()) {
            int columnIndex = Attribute.GetCustomAttribute(prop, typeof(CsvColumnAttribute)) is CsvColumnAttribute attribute
                ? Array.IndexOf(headerColumns, attribute.Name)
                : Array.IndexOf(headerColumns, prop.Name);
            if (columnIndex == -1) continue;

            try {
                object propertyValue = Convert.ChangeType(values[columnIndex], prop.PropertyType);
                prop.SetValue(value, propertyValue);
            }
            catch (Exception e) {
                if (!config.LogErrors) return;
                
                // Todo allow for logger
                Console.WriteLine($"Error setting property {prop.Name} on {value.GetType().Name}: {e.Message}");
            }
        }
    }

    public async IAsyncEnumerable<T> ReadFromCsvAsync(TextReader reader) {
        string[] headerColumns = [];
        if (await reader.ReadLineAsync() is {} lineFull) {
            headerColumns = lineFull.Split(config.ColumnSplit);
        }

        while (true) {
            if (await reader.ReadLineAsync() is not {} line) break;

            string[] values = line.Split(config.ColumnSplit);
            var obj = new T();
            
            SetPropertyFromCsvColumn(obj, headerColumns, values);
            
            yield return obj;
        }
    }
    
    public IEnumerable<T> ReadFromCsvFile(string filePath) => ReadFromCsv(new StreamReader(filePath));
    public IAsyncEnumerable<T> ReadFromCsvFileAsync(string filePath) => ReadFromCsvAsync(new StreamReader(filePath));
}

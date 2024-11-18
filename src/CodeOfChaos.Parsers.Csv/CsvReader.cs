// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Reflection;

namespace CodeOfChaos.Parsers.Csv;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvReader(CsvParserConfig config) {
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static CsvReader FromConfig(Action<CsvParserConfig> configAction) {
        var config = new CsvParserConfig();
        configAction(config);
        return new CsvReader(config);
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IEnumerable<T> FromCsvFile<T>(string filePath) where T : new() => FromTextReader<T>(new StreamReader(filePath));
    public IEnumerable<T> FromCsvString<T>(string data) where T : new() => FromTextReader<T>(new StringReader(data));
    public IEnumerable<Dictionary<string,string>> FromCsvFile(string filePath) => FromTextReader(new StreamReader(filePath));
    public IEnumerable<Dictionary<string,string>> FromCsvString(string data) => FromTextReader(new StringReader(data));
    
    public IAsyncEnumerable<T> FromCsvFileAsync<T>(string filePath) where T : new() => FromTextReaderAsync<T>(new StreamReader(filePath));
    public IAsyncEnumerable<T> FromCsvStringAsync<T>(string data) where T : new() => FromTextReaderAsync<T>(new StringReader(data));
    public IAsyncEnumerable<Dictionary<string,string>> FromCsvFileAsync(string filePath) => FromTextReaderAsync(new StreamReader(filePath));
    public IAsyncEnumerable<Dictionary<string,string>> FromCsvStringAsync(string data) => FromTextReaderAsync(new StringReader(data));

    private IEnumerable<Dictionary<string, string>> FromTextReader(TextReader reader) {
        string[] headerColumns = [];
        if (reader.ReadLine() is {} lineFull) {
            headerColumns = lineFull.Split(config.ColumnSplit);
        }
        
        while (true) {
            if (reader.ReadLine() is not {} line) break;

            string[] values = line.Split(config.ColumnSplit);

            var dict = new Dictionary<string, string>();
            for (int i = 0; i < headerColumns.Length; i++) {
                dict[headerColumns[i]] = values[i];
            }
            yield return dict;
        }
    }

    private IEnumerable<T> FromTextReader<T>(TextReader reader) where T : new() {
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

    private async IAsyncEnumerable<Dictionary<string, string>> FromTextReaderAsync(TextReader reader) {
        string[] headerColumns = [];
        if (await reader.ReadLineAsync() is {} lineFull) {
            headerColumns = lineFull.Split(config.ColumnSplit);
        }

        while (true) {
            if (await reader.ReadLineAsync() is not {} line) break;

            string[] values = line.Split(config.ColumnSplit);

            var dict = new Dictionary<string, string>();
            for (int i = 0; i < headerColumns.Length; i++) {
                dict[headerColumns[i]] = values[i];
            }
            yield return dict;
        }
    }

    private async IAsyncEnumerable<T> FromTextReaderAsync<T>(TextReader reader) where T : new() {
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
    
    private void SetPropertyFromCsvColumn<T>(T value, string[] headerColumns, string[] values) {
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
}

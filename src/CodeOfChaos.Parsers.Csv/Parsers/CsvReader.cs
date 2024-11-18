// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Parsers.Csv.Attributes;
using CodeOfChaos.Parsers.Csv.Contracts;
using System.Reflection;

namespace CodeOfChaos.Parsers.Csv.Parsers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvReader<T>(Action<CsvParserConfig> configAction) : CsvParser(configAction), ICsvReader<T>
    where T : class, new() {
    public IEnumerable<T> FromCsvFile(string filePath) {
        var reader = new StreamReader(filePath);
        return FromTextReader(reader);
    }
    public IEnumerable<T> FromCsvString(string data) {
        var reader = new StringReader(data);
        return FromTextReader(reader);
    }

    public IAsyncEnumerable<T> FromCsvFileAsync(string filePath) {
        var reader = new StreamReader(filePath);
        return FromTextReaderAsync(reader);
    }
    public IAsyncEnumerable<T> FromCsvStringAsync(string data) {
        var reader = new StringReader(data);
        return FromTextReaderAsync(reader);
    }

    #region Helper Methods
    private IEnumerable<T> FromTextReader(TextReader reader) {
        string[] headerColumns = [];
        if (reader.ReadLine() is {} lineFull) {
            headerColumns = lineFull.Split(Config.ColumnSplit);
        }

        while (true) {
            if (reader.ReadLine() is not {} line) break;

            string[] values = line.Split(Config.ColumnSplit);

            var obj = new T();
            SetPropertyFromCsvColumn(obj, headerColumns, values);
            yield return obj;
        }
    }

    private async IAsyncEnumerable<T> FromTextReaderAsync(TextReader reader) {
        string[] headerColumns = [];
        if (await reader.ReadLineAsync() is {} lineFull) {
            headerColumns = lineFull.Split(Config.ColumnSplit);
        }

        while (true) {
            if (await reader.ReadLineAsync() is not {} line) break;

            string[] values = line.Split(Config.ColumnSplit);
            var obj = new T();

            SetPropertyFromCsvColumn(obj, headerColumns, values);

            yield return obj;
        }
    }

    private void SetPropertyFromCsvColumn(T? value, string[] headerColumns, string[] values) {
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
                if (!Config.LogErrors) return;

                // Todo allow for logger
                Console.WriteLine($"Error setting property {prop.Name} on {value.GetType().Name}: {e.Message}");
            }
        }
    }
    #endregion
}

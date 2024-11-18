// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions;
using CodeOfChaos.Parsers.Csv.Contracts;

namespace CodeOfChaos.Parsers.Csv.Parsers;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvDictionaryReader(Action<CsvParserConfig> configAction) : CsvParser(configAction), ICsvReader<Dictionary<string,string?>> {
    public IEnumerable<Dictionary<string, string?>> FromCsvFile(string filePath) {
        var reader = new StreamReader(filePath);
        return FromTextReader(reader);
    }
    
    public IEnumerable<Dictionary<string, string?>> FromCsvString(string data) {
        var reader = new StringReader(data);
        return FromTextReader(reader);
    }
    
    public IAsyncEnumerable<Dictionary<string, string?>> FromCsvFileAsync(string filePath) {
        var reader = new StreamReader(filePath);
        return FromTextReaderAsync(reader);
    }
    
    public IAsyncEnumerable<Dictionary<string, string?>> FromCsvStringAsync(string data) {
        var reader = new StringReader(data);
        return FromTextReaderAsync(reader);
    }

    #region Helper Methods
    private IEnumerable<Dictionary<string, string?>> FromTextReader(TextReader reader) {
        string[] headerColumns = [];
        if (reader.ReadLine() is {} lineFull) {
            headerColumns = lineFull.Split(Config.ColumnSplit);
        }
        
        while (true) {
            if (reader.ReadLine() is not {} line) break;

            string[] values = line.Split(Config.ColumnSplit);

            var dict = new Dictionary<string, string?>();
            for (int i = 0; i < headerColumns.Length; i++) {
                string value = values[i];
                dict[headerColumns[i]] = value.IsNotNullOrEmpty() ? value : null ;
            }
            yield return dict;
        }
    }
    
    private async IAsyncEnumerable<Dictionary<string, string?>> FromTextReaderAsync(TextReader reader) {
        string[] headerColumns = [];
        if (await reader.ReadLineAsync() is {} lineFull) {
            headerColumns = lineFull.Split(Config.ColumnSplit);
        }

        while (true) {
            if (await reader.ReadLineAsync() is not {} line) break;

            string[] values = line.Split(Config.ColumnSplit);

            var dict = new Dictionary<string, string?>();
            for (int i = 0; i < headerColumns.Length; i++) {
                string value = values[i];
                dict[headerColumns[i]] = value.IsNotNullOrEmpty() ? value : null ;
            }
            yield return dict;
        }
    }
    #endregion
}

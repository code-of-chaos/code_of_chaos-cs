// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Parsers.Csv.Contracts;

namespace CodeOfChaos.Parsers.Csv.Parsers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvDictionaryWriter(Action<CsvParserConfig> configAction) : CsvParser(configAction), ICsvWriter<Dictionary<string, string?>> {
    public string WriteToString(IEnumerable<Dictionary<string, string?>> data) {
        using var writer = new StringWriter();
        DictionaryToCsv(writer, data);
        return writer.ToString();
    }

    public async Task<string> WriteToStringAsync(IEnumerable<Dictionary<string, string?>> data) {
        await using var writer = new StringWriter();
        await DictionaryToCsvAsync(writer, data);
        return writer.ToString();
    }

    public void WriteToFile(string filePath, IEnumerable<Dictionary<string, string?>> data) {
        using var writer = new StreamWriter(filePath);
        DictionaryToCsv(writer, data);
    }

    public async Task WriteToFileAsync(string filePath, IEnumerable<Dictionary<string, string?>> data) {
        await using var writer = new StreamWriter(filePath);
        await DictionaryToCsvAsync(writer, data);
    }

    #region Helper Methods
    private void FromDictionaryToCsv(TextWriter writer, IEnumerable<Dictionary<string, string?>> data) {
        IDictionary<string, string?>[] records = data as IDictionary<string, string?>[] ?? data.ToArray<IDictionary<string, string?>>();
        if (records.Length == 0) return;

        // Write header row
        if (Config.IncludeHeader) {
            IDictionary<string, string?> firstDictionary = records.First();
            IEnumerable<string> headers = firstDictionary.Keys;
            writer.WriteLine(string.Join(Config.ColumnSplit, headers));
        }

        // Write data rows
        foreach (IDictionary<string, string?> dictionary in records) {
            IEnumerable<string> values = dictionary.Values.Select(value => value?.ToString() ?? string.Empty);
            writer.WriteLine(string.Join(Config.ColumnSplit, values));
        }
    }
    private async Task DictionaryToCsvAsync(TextWriter writer, IEnumerable<Dictionary<string, string?>> data) {
        IDictionary<string, string?>[] records = data as IDictionary<string, string?>[] ?? data.ToArray<IDictionary<string, string?>>();
        if (records.Length == 0) return;

        // Write header row
        if (Config.IncludeHeader) {
            IDictionary<string, string?> firstDictionary = records.First();
            IEnumerable<string> headers = firstDictionary.Keys;
            await writer.WriteLineAsync(string.Join(Config.ColumnSplit, headers));
        }

        // Write data rows
        foreach (IDictionary<string, string?> dictionary in records) {
            IEnumerable<string> values = dictionary.Values.Select(value => value?.ToString() ?? string.Empty);
            await writer.WriteLineAsync(string.Join(Config.ColumnSplit, values));
        }
    }
    #endregion
}

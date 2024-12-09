// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Parsers.Csv.Attributes;
using System.Reflection;

namespace CodeOfChaos.Parsers.Csv.Parsers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvFileWriter<T>(Action<CsvParserConfig> configAction) : CsvParser(configAction)
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
    
    #endregion

}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Parsers.Csv.Parsers;

namespace CodeOfChaos.Parsers.Csv.Sample;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class Program {
    public async static Task Main(string[] args) {
        var reader = new CsvReader<EveningClassData>(cfg => {
            cfg.ColumnSplit = ";";
            cfg.IncludeHeader = true;
        });

        IAsyncEnumerable<EveningClassData> dataEnumerable = reader.FromCsvFileAsync("AvondSchool.csv");

        await foreach (EveningClassData record in dataEnumerable) {
            Console.WriteLine(record.Geometry);
        }

        var writer = new CsvDictionaryWriter(config => {
            config.ColumnSplit = ";";
            config.UseLowerCaseHeaders = true;
        });

        var data = new Dictionary<string, string?> {
            ["test"] = "one",
            ["data"] = "something"
        };

        await writer.WriteToFileAsync("test.csv", [data]);
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Parsers.Csv.Sample;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class Program {
    public async static Task Main(string[] args) {
        CsvParser reader = CsvParser.FromConfig(cfg => {
            cfg.ColumnSplit = ";";
            cfg.IncludeHeader = true;
        });

        IAsyncEnumerable<EveningClassData> dataEnumerable = reader.ToEnumerableAsync<EveningClassData>("AvondSchool.csv");

        await foreach (EveningClassData record in dataEnumerable) {
            Console.WriteLine(record.Geometry);
        }

        CsvParser parser = CsvParser.FromConfig(config => {
            config.ColumnSplit = ";";
            config.UseLowerCaseHeaders = true;
        });

        var data = new Dictionary<string, string?> {
            ["test"] = "one",
            ["data"] = "something"
        };

        await parser.ParseToFileAsync("test.csv", [data]);
    }
}

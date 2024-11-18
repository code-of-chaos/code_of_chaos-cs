// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Parsers.Csv.Sample;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class Program {
    public async static Task Main(string[] args) {
        CsvReader reader = CsvReader.FromConfig(config => {
            config.ColumnSplit = ";";
        });

        IAsyncEnumerable<EveningClassData> dataEnumerable = reader.FromCsvFileAsync<EveningClassData>("AvondSchool.csv");

        await foreach (EveningClassData record in dataEnumerable) {
            Console.WriteLine(record.Geometry);
        }
        
        
        CsvWriter<Dictionary<string, string>> writer = CsvWriter<Dictionary<string,string>>.FromConfig(config => {
            config.ColumnSplit = ";";
            config.UseLowerCaseHeaders = true;
        });
        
        var data = new Dictionary<string, string> {
            ["test"] = "one",
            ["data"] = "something"
        };
        
        await writer.WriteToFileAsync("test.csv", [data]);
    }
}

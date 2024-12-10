// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Parsers.Csv;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ICsvParser {
    IEnumerable<T> ToEnumerable<T, TReader>(TReader reader) 
        where T : class, new() 
        where TReader : TextReader;
    IEnumerable<T> ToEnumerable<T>(string filePath) 
        where T : class, new();
    IAsyncEnumerable<T> ToEnumerableAsync<T, TReader>(TReader reader, CancellationToken ct = default) 
        where T : class, new() 
        where TReader : TextReader;
    IAsyncEnumerable<T> ToEnumerableAsync<T>(string filePath, CancellationToken ct = default) 
        where T : class, new();

    T[] ToArray<T, TReader>(TReader reader) 
        where T : class, new() 
        where TReader : TextReader;
    T[] ToArray<T>(string filePath) 
        where T : class, new();
    ValueTask<T[]> ToArrayAsync<T, TReader>(TReader reader, CancellationToken ct = default) 
        where T : class, new() 
        where TReader : TextReader;
    ValueTask<T[]> ToArrayAsync<T>(string filePath, CancellationToken ct = default) 
        where T : class, new();

    List<T> ToList<T, TReader>(TReader reader) 
        where T : class, new() 
        where TReader : TextReader;
    List<T> ToList<T>(string filePath) 
        where T : class, new();
    ValueTask<List<T>> ToListAsync<T, TReader>(TReader reader, CancellationToken ct = default) 
        where T : class, new() 
        where TReader : TextReader;
    ValueTask<List<T>> ToListAsync<T>(string filePath, CancellationToken ct = default) 
        where T : class, new();

    IEnumerable<Dictionary<string, string?>> ToDictionaryEnumerable<TReader>(TReader reader) 
        where TReader : TextReader;
    IEnumerable<Dictionary<string, string?>> ToDictionaryEnumerable(string filePath);
    IAsyncEnumerable<Dictionary<string, string?>> ToDictionaryEnumerableAsync<TReader>(TReader reader, CancellationToken ct = default) 
        where TReader : TextReader;
    IAsyncEnumerable<Dictionary<string, string?>> ToDictionaryEnumerableAsync(string filePath, CancellationToken ct = default);

    Dictionary<string, string?>[] ToDictionaryArray<TReader>(TReader reader) 
        where TReader : TextReader;
    Dictionary<string, string?>[] ToDictionaryArray(string filePath);
    ValueTask<Dictionary<string, string?>[]> ToDictionaryArrayAsync<TReader>(TReader reader, CancellationToken ct = default) 
        where TReader : TextReader;
    ValueTask<Dictionary<string, string?>[]> ToDictionaryArrayAsync(string filePath, CancellationToken ct = default);

    List<Dictionary<string, string?>> ToDictionaryList<TReader>(TReader reader) 
        where TReader : TextReader;
    List<Dictionary<string, string?>> ToDictionaryList(string filePath);
    ValueTask<List<Dictionary<string, string?>>> ToDictionaryListAsync<TReader>(TReader reader, CancellationToken ct = default) 
        where TReader : TextReader;
    ValueTask<List<Dictionary<string, string?>>> ToDictionaryListAsync(string filePath, CancellationToken ct = default);

    string ParseToString<T>(IEnumerable<T> data);
    string ParseToString(IEnumerable<Dictionary<string, string?>> data);
    ValueTask<string> ParseToStringAsync<T>(IEnumerable<T> data);
    ValueTask<string> ParseToStringAsync(IEnumerable<Dictionary<string, string?>> data);
    
    void ParseToFile<T>(string filePath, IEnumerable<T> data);
    void ParseToFile(string filePath, IEnumerable<Dictionary<string, string?>> data);
    ValueTask ParseToFileAsync<T>(string filePath, IEnumerable<T> data);
    ValueTask ParseToFileAsync(string filePath, IEnumerable<Dictionary<string, string?>> data);
    
    void ParseToWriter<T, TWriter>(IEnumerable<T> data, TWriter writer)
        where TWriter : TextWriter;
    void ParseToWriter<TWriter>(IEnumerable<Dictionary<string, string?>> data, TWriter writer)
        where TWriter : TextWriter;
    ValueTask ParseToWriterAsync<T, TWriter>(IEnumerable<T> data, TWriter writer)
        where TWriter : TextWriter;
    ValueTask ParseToWriterAsync<TWriter>(IEnumerable<Dictionary<string, string?>> data, TWriter writer)
        where TWriter : TextWriter;
}

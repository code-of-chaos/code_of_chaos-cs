// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions;
using CodeOfChaos.Parsers.Csv.Attributes;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CodeOfChaos.Parsers.Csv;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvParser(CsvParserConfig config) : ICsvParser {

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static CsvParser FromConfig(Action<CsvParserConfig> configAction) {
        var config = new CsvParserConfig();
        configAction(config);
        return new CsvParser(config);
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Input Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region ToEnumerable
    public IEnumerable<T> ToEnumerable<T, TReader>(TReader reader)
        where T : class, new()
        where TReader : TextReader => FromTextReader<T, TReader>(reader);

    public IEnumerable<T> ToEnumerable<T>(string filePath)
        where T : class, new() => FromTextReader<T, StreamReader>(new StreamReader(filePath));

    public IAsyncEnumerable<T> ToEnumerableAsync<T, TReader>(TReader reader, CancellationToken ct = default)
        where T : class, new()
        where TReader : TextReader => FromTextReaderAsync<T, TReader>(reader, ct);

    public IAsyncEnumerable<T> ToEnumerableAsync<T>(string filePath, CancellationToken ct = default)
        where T : class, new() => FromTextReaderAsync<T, StreamReader>(new StreamReader(filePath), ct);
    #endregion
    #region ToArray
    public T[] ToArray<T, TReader>(TReader reader)
        where T : class, new()
        where TReader : TextReader => FromTextReader<T, TReader>(reader).ToArray();

    public T[] ToArray<T>(string filePath)
        where T : class, new() => FromTextReader<T, StreamReader>(new StreamReader(filePath)).ToArray();

    public async ValueTask<T[]> ToArrayAsync<T, TReader>(TReader reader, CancellationToken ct = default)
        where T : class, new()
        where TReader : TextReader {
        var results = new List<T>(config.InitialCapacity);

        await foreach (T item in FromTextReaderAsync<T, TReader>(reader, ct)) {
            results.Add(item);
        }

        return results.ToArray();
    }

    public async ValueTask<T[]> ToArrayAsync<T>(string filePath, CancellationToken ct = default) where T : class, new() {
        using var reader = new StreamReader(filePath);
        var results = new List<T>(config.InitialCapacity);

        await foreach (T item in FromTextReaderAsync<T, StreamReader>(reader, ct)) {
            results.Add(item);
        }

        return results.ToArray();
    }
    #endregion
    #region ToList
    public List<T> ToList<T, TReader>(TReader reader)
        where T : class, new()
        where TReader : TextReader => FromTextReader<T, TReader>(reader).ToList();

    public List<T> ToList<T>(string filePath)
        where T : class, new() {
        var reader = new StreamReader(filePath);
        return FromTextReader<T, StreamReader>(reader).ToList();
    }

    public async ValueTask<List<T>> ToListAsync<T, TReader>(TReader reader, CancellationToken ct = default)
        where T : class, new()
        where TReader : TextReader {
        var results = new List<T>(config.InitialCapacity);

        await foreach (T item in FromTextReaderAsync<T, TReader>(reader, ct)) {
            results.Add(item);
        }

        return results.ToList();
    }

    public async ValueTask<List<T>> ToListAsync<T>(string filePath, CancellationToken ct = default) where T : class, new() {
        using var reader = new StringReader(filePath);
        var results = new List<T>(config.InitialCapacity);

        await foreach (T item in FromTextReaderAsync<T, StringReader>(reader, ct)) {
            results.Add(item);
        }

        return results.ToList();
    }
    #endregion
    #region ToDictionaryEnumerable
    public IEnumerable<Dictionary<string, string?>> ToDictionaryEnumerable<TReader>(TReader reader)
        where TReader : TextReader => FromTextReaderToDictionary(reader);

    public IEnumerable<Dictionary<string, string?>> ToDictionaryEnumerable(string filePath)
        => FromTextReaderToDictionary(new StreamReader(filePath));

    public IAsyncEnumerable<Dictionary<string, string?>> ToDictionaryEnumerableAsync<TReader>(TReader reader, CancellationToken ct = default)
        where TReader : TextReader => FromTextReaderToDictionaryAsync(reader, ct);

    public IAsyncEnumerable<Dictionary<string, string?>> ToDictionaryEnumerableAsync(string filePath, CancellationToken ct = default)
        => FromTextReaderToDictionaryAsync(new StreamReader(filePath), ct);
    #endregion
    #region ToDictionaryArray
    public Dictionary<string, string?>[] ToDictionaryArray<TReader>(TReader reader)
        where TReader : TextReader => FromTextReaderToDictionary(reader).ToArray();

    public Dictionary<string, string?>[] ToDictionaryArray(string filePath) => FromTextReaderToDictionary(new StreamReader(filePath)).ToArray();

    public async ValueTask<Dictionary<string, string?>[]> ToDictionaryArrayAsync<TReader>(TReader reader, CancellationToken ct = default)
        where TReader : TextReader {
        var results = new List<Dictionary<string, string?>>(config.InitialCapacity);

        await foreach (Dictionary<string, string?> item in FromTextReaderToDictionaryAsync(reader, ct)) {
            results.Add(item);
        }
        return results.ToArray();
    }

    public async ValueTask<Dictionary<string, string?>[]> ToDictionaryArrayAsync(string filePath, CancellationToken ct = default) {
        using var reader = new StreamReader(filePath);
        var results = new List<Dictionary<string, string?>>(config.InitialCapacity);

        await foreach (Dictionary<string, string?> item in FromTextReaderToDictionaryAsync(reader, ct)) {
            results.Add(item);
        }
        return results.ToArray();
    }
    #endregion
    #region ToDictionaryList
    public List<Dictionary<string, string?>> ToDictionaryList<TReader>(TReader reader)
        where TReader : TextReader => FromTextReaderToDictionary(reader).ToList();

    public List<Dictionary<string, string?>> ToDictionaryList(string filePath)
        => FromTextReaderToDictionary(new StreamReader(filePath)).ToList();

    public async ValueTask<List<Dictionary<string, string?>>> ToDictionaryListAsync<TReader>(TReader reader, CancellationToken ct = default)
        where TReader : TextReader {
        var results = new List<Dictionary<string, string?>>(config.InitialCapacity);

        await foreach (Dictionary<string, string?> item in FromTextReaderToDictionaryAsync(reader, ct)) {
            results.Add(item);
        }
        return results;
    }

    public async ValueTask<List<Dictionary<string, string?>>> ToDictionaryListAsync(string filePath, CancellationToken ct = default) {
        using var reader = new StringReader(filePath);
        var results = new List<Dictionary<string, string?>>(config.InitialCapacity);
        await foreach (Dictionary<string, string?> item in FromTextReaderToDictionaryAsync(reader, ct)) {
            results.Add(item);
        }
        return results;
    }
    #endregion

    // -----------------------------------------------------------------------------------------------------------------
    // Output
    // -----------------------------------------------------------------------------------------------------------------
    #region ParseToString
    public string ParseToString<T>(IEnumerable<T> data) {
        using var writer = new StringWriter();
        FromDataToTextWriter(writer, data);
        return writer.ToString();
    }
    
    public string ParseToString(IEnumerable<Dictionary<string, string?>> data) {
        using var writer = new StringWriter();
        FromDictionaryToTextWriter(writer, data);
        return writer.ToString();
    }

    public async ValueTask<string> ParseToStringAsync<T>(IEnumerable<T> data) {
        await using var writer = new StringWriter();
        await FromDataToTextWriterAsync(writer, data);
        return writer.ToString();
    }

    public async ValueTask<string> ParseToStringAsync(IEnumerable<Dictionary<string, string?>> data) {
        await using var writer = new StringWriter();
        await FromDictionaryToTextWriterAsync(writer, data);
        return writer.ToString();
    }
    #endregion
    #region ParseToFileAsync
    public void ParseToFile<T>(string filePath, IEnumerable<T> data) {
        using var writer = new StreamWriter(filePath);
        FromDataToTextWriter(writer, data);
    }

    public void ParseToFile(string filePath, IEnumerable<Dictionary<string, string?>> data) {
        using var writer = new StreamWriter(filePath);
        FromDictionaryToTextWriter(writer, data);
    }

    public async ValueTask ParseToFileAsync<T>(string filePath, IEnumerable<T> data) {
        await using var writer = new StreamWriter(filePath);
        await FromDataToTextWriterAsync(writer, data);
    }

    public async ValueTask ParseToFileAsync(string filePath, IEnumerable<Dictionary<string, string?>> data) {
        await using var writer = new StreamWriter(filePath);
        await FromDictionaryToTextWriterAsync(writer, data);
    }
    #endregion
    #region ParseToWriter
    public void ParseToWriter<T, TWriter>(IEnumerable<T> data, TWriter writer) 
        where TWriter : TextWriter => FromDataToTextWriter(writer, data);

    public void ParseToWriter<TWriter>(IEnumerable<Dictionary<string, string?>> data, TWriter writer) 
        where TWriter : TextWriter => FromDictionaryToTextWriter(writer, data);

    public async ValueTask ParseToWriterAsync<T, TWriter>(IEnumerable<T> data, TWriter writer) 
        where TWriter : TextWriter => await FromDataToTextWriterAsync(writer, data);

    public async ValueTask ParseToWriterAsync<TWriter>(IEnumerable<Dictionary<string, string?>> data, TWriter writer) 
        where TWriter : TextWriter => await FromDictionaryToTextWriterAsync(writer, data);
    #endregion

    // -----------------------------------------------------------------------------------------------------------------
    // Actual Parsers
    // -----------------------------------------------------------------------------------------------------------------
    #region Generic Type Parsing
    private IEnumerable<T> FromTextReader<T, TReader>(TReader reader) 
        where T : class, new()
        where TReader : TextReader 
    {
        string[] headerColumns = [];
        int batchSize = config.BatchSize;
        var batch = new List<T>(config.BatchSize);

        if (reader.ReadLine() is {} lineFull) {
            headerColumns = lineFull.Split(config.ColumnSplit);
        }

        while (true) {
            string? line = null;
            for (int i = 0; i < batchSize && (line = reader.ReadLine()) != null; i++) {
                string[] values = line.Split(config.ColumnSplit);
                var obj = new T();
                SetPropertyFromCsvColumn(obj, headerColumns, values);
                batch.Add(obj);
            }

            foreach (T item in batch) {
                yield return item;
            }

            batch.Clear();
            if (line == null) break;
        }
    }

    private async IAsyncEnumerable<T> FromTextReaderAsync<T, TReader>(TReader reader, [EnumeratorCancellation] CancellationToken ct = default) 
        where T : class, new()
        where TReader : TextReader 
    {
        string[] headerColumns = [];
        int batchSize = config.BatchSize;
        var batch = new List<T>(config.BatchSize);

        if (await reader.ReadLineAsync(ct) is {} lineFull) {
            headerColumns = lineFull.Split(config.ColumnSplit);
        }

        while (!ct.IsCancellationRequested) {
            string? line = null;
            for (int i = 0; i < batchSize && (line = await reader.ReadLineAsync(ct)) != null; i++) {
                string[] values = line.Split(config.ColumnSplit);
                var obj = new T();

                SetPropertyFromCsvColumn(obj, headerColumns, values);
                batch.Add(obj);
            }

            foreach (T item in batch) {
                yield return item;
            }

            batch.Clear();
            if (line == null) break;
        }
    }

    private void SetPropertyFromCsvColumn<T>(T? value, string[] headerColumns, string[] values) where T : class, new() {
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
    #endregion
    #region Dictionary Parsing
    private IEnumerable<Dictionary<string, string?>> FromTextReaderToDictionary<TReader>(TReader reader)
        where TReader : TextReader
    {
        string[] headerColumns = [];
        int batchSize = config.BatchSize;
        var batch = new List<Dictionary<string, string?>>();
        if (reader.ReadLine() is {} lineFull) {
            headerColumns = lineFull.Split(config.ColumnSplit);
        }

        while (true) {
            string? line = null;
            for (int i = 0; i < batchSize && (line = reader.ReadLine()) != null; i++) {
                string[] values = line.Split(config.ColumnSplit);

                var dict = new Dictionary<string, string?>();
                for (int j = 0; j < headerColumns.Length; j++) {
                    string value = values[j];
                    dict[headerColumns[j]] = value.IsNotNullOrEmpty() ? value : null;
                }
                batch.Add(dict);
            }

            foreach (Dictionary<string, string?> item in batch) {
                yield return item;
            }

            batch.Clear();
            if (line == null) break;
        }
    }

    private async IAsyncEnumerable<Dictionary<string, string?>> FromTextReaderToDictionaryAsync<TReader>(TReader reader, [EnumeratorCancellation] CancellationToken ct = default)
        where TReader : TextReader
    {
        string[] headerColumns = [];
        int batchSize = config.BatchSize;
        var batch = new List<Dictionary<string, string?>>();
        if (await reader.ReadLineAsync(ct) is {} lineFull) {
            headerColumns = lineFull.Split(config.ColumnSplit);
        }

        while (true) {
            string? line = null;
            for (int i = 0; i < batchSize && (line = await reader.ReadLineAsync(ct)) != null; i++) {
                string[] values = line.Split(config.ColumnSplit);

                var dict = new Dictionary<string, string?>();
                for (int j = 0; j < headerColumns.Length; j++) {
                    string value = values[j];
                    dict[headerColumns[j]] = value.IsNotNullOrEmpty() ? value : null;
                }
                batch.Add(dict);
            }

            foreach (Dictionary<string, string?> item in batch) {
                yield return item;
            }

            batch.Clear();
            if (line == null) break;
        }
    }
    #endregion

    #region Generic Type Writer
    private void FromDataToTextWriter<T, TWriter>(TWriter writer, IEnumerable<T> data) 
        where TWriter : TextWriter 
    {
        // Write header row
        IEnumerable<T> enumerable = data as T[] ?? data.ToArray();
        PropertyInfo[] propertyInfos = GetCsvProperties(enumerable.FirstOrDefault());// Dirty but it will work

        if (config.IncludeHeader) {
            string[] headers = GetCsvHeaders(propertyInfos).ToArray();
            for (int i = 0; i < headers.Length; i++) {
                writer.Write(headers[i]);
                if (i < headers.Length - 1) writer.Write(config.ColumnSplit);
            }

            writer.Write(Environment.NewLine);
        }

        // Write data rows
        foreach (T? obj in enumerable) {
            string[] values = GetCsvValues(obj, propertyInfos).ToArray();
            for (int i = 0; i < values.Length; i++) {
                writer.Write(values[i]);
                if (i < values.Length - 1) writer.Write(config.ColumnSplit);
            }

            writer.Write(Environment.NewLine);
        }
    }

    private async Task FromDataToTextWriterAsync<T, TWriter>(TWriter writer, IEnumerable<T> data)
        where TWriter : TextWriter
    {
        // Write header row
        IEnumerable<T> enumerable = data as T[] ?? data.ToArray();
        PropertyInfo[] propertyInfos = GetCsvProperties(enumerable.FirstOrDefault());

        if (config.IncludeHeader) {
            string[] headers = GetCsvHeaders(propertyInfos).ToArray();
            for (int i = 0; i < headers.Length; i++) {
                await writer.WriteAsync(headers[i]);
                if (i < headers.Length - 1) await writer.WriteAsync(config.ColumnSplit);
            }

            await writer.WriteAsync(Environment.NewLine);
        }

        // Write data rows
        foreach (T? obj in enumerable) {
            string[] values = GetCsvValues(obj, propertyInfos).ToArray();
            for (int i = 0; i < values.Length; i++) {
                await writer.WriteAsync(values[i]);
                if (i < values.Length - 1) await writer.WriteAsync(config.ColumnSplit);
            }

            await writer.WriteAsync(Environment.NewLine);
        }
    }


    private static PropertyInfo[] GetCsvProperties<T>(T? obj) => obj?
            .GetType()
            .GetProperties()
            .ToArray()
        ?? [];

    private IEnumerable<string> GetCsvHeaders(PropertyInfo[] propertyInfos) {
        return propertyInfos
            .Select(p => {
                if (p.GetCustomAttribute<CsvColumnAttribute>() is not {} attribute)
                    return config.UseLowerCaseHeaders ? p.Name.ToLowerInvariant() : p.Name;

                return config.UseLowerCaseHeaders
                    ? attribute.NameLowerInvariant
                    : attribute.Name;
            });
    }

    private static IEnumerable<string> GetCsvValues<T>(T? obj, PropertyInfo[] propertyInfos) {
        if (obj is null) return [];

        PropertyInfo[] properties = propertyInfos.Length != 0
            ? propertyInfos
            : obj.GetType().GetProperties();

        return properties
            .Select(p => p.GetValue(obj)?.ToString() ?? string.Empty);
    }
    #endregion
    #region Dictionary Writer
    private void FromDictionaryToTextWriter<TWriter>(TWriter writer, IEnumerable<Dictionary<string, string?>> data) 
        where TWriter : TextWriter
    {
        IDictionary<string, string?>[] records = data as IDictionary<string, string?>[] ?? data.ToArray<IDictionary<string, string?>>();
        if (records.Length == 0) return;

        // Write header row
        if (config.IncludeHeader) {
            IDictionary<string, string?> firstDictionary = records.First();
            IEnumerable<string> headers = firstDictionary.Keys;
            writer.WriteLine(string.Join(config.ColumnSplit, headers));
        }

        // Write data rows
        foreach (IDictionary<string, string?> dictionary in records) {
            IEnumerable<string> values = dictionary.Values.Select(value => value?.ToString() ?? string.Empty);
            writer.WriteLine(string.Join(config.ColumnSplit, values));
        }
    }

    private async Task FromDictionaryToTextWriterAsync<TWriter>(TWriter writer, IEnumerable<Dictionary<string, string?>> data) 
        where TWriter : TextWriter
    {
        IDictionary<string, string?>[] records = data as IDictionary<string, string?>[] ?? data.ToArray<IDictionary<string, string?>>();
        if (records.Length == 0) return;

        // Write header row
        if (config.IncludeHeader) {
            IDictionary<string, string?> firstDictionary = records.First();
            IEnumerable<string> headers = firstDictionary.Keys;
            await writer.WriteLineAsync(string.Join(config.ColumnSplit, headers));
        }

        // Write data rows
        foreach (IDictionary<string, string?> dictionary in records) {
            IEnumerable<string> values = dictionary.Values.Select(value => value?.ToString() ?? string.Empty);
            await writer.WriteLineAsync(string.Join(config.ColumnSplit, values));
        }
    }
    #endregion
}

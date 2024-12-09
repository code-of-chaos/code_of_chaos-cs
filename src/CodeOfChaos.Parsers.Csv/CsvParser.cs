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
public abstract class CsvParser(Action<CsvParserConfig> configAction) {
    protected CsvParserConfig Config { get; } = FromConfig(configAction);

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    private static CsvParserConfig FromConfig(Action<CsvParserConfig> configAction) {
        var config = new CsvParserConfig();
        configAction(config);
        return config;
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Input Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region ToEnumerable
    public IEnumerable<T> ToEnumerable<T, TReader>(TReader reader)
        where T : class, new()
        where TReader : TextReader => FromTextReader<T>(reader);

    public IEnumerable<T> ToEnumerable<T>(string filePath)
        where T : class, new() => FromTextReader<T>(new StringReader(filePath));

    public IAsyncEnumerable<T> ToEnumerableAsync<T, TReader>(TReader reader, CancellationToken ct = default)
        where T : class, new()
        where TReader : TextReader => FromTextReaderAsync<T>(reader, ct);

    public IAsyncEnumerable<T> ToEnumerableAsync<T>(string filePath, CancellationToken ct = default)
        where T : class, new() => FromTextReaderAsync<T>(new StringReader(filePath), ct);
    #endregion
    #region ToArray
    public T[] ToArray<T, TReader>(TReader reader)
        where T : class, new()
        where TReader : TextReader => FromTextReader<T>(reader).ToArray();

    public T[] ToArray<T>(string filePath)
        where T : class, new() => FromTextReader<T>(new StringReader(filePath)).ToArray();

    public async ValueTask<T[]> ToArrayAsync<T, TReader>(TReader reader, CancellationToken ct = default)
        where T : class, new()
        where TReader : TextReader {
        var results = new List<T>(Config.InitialCapacity);

        await foreach (T item in FromTextReaderAsync<T>(reader, ct)) {
            results.Add(item);
        }

        return results.ToArray();
    }

    public async ValueTask<T[]> ToArrayAsync<T>(string filePath, CancellationToken ct = default) where T : class, new() {
        using var reader = new StringReader(filePath);
        var results = new List<T>(Config.InitialCapacity);

        await foreach (T item in FromTextReaderAsync<T>(reader, ct)) {
            results.Add(item);
        }

        return results.ToArray();
    }
    #endregion
    #region ToDictionaryEnumerable
    public IEnumerable<Dictionary<string, string?>> ToDictionaryEnumerable<TReader>(TReader reader)
        where TReader : TextReader => FromTextReaderToDictionary(reader);

    public IEnumerable<Dictionary<string, string?>> ToDictionaryEnumerable(string filePath)
        => FromTextReaderToDictionary(new StringReader(filePath));

    public IAsyncEnumerable<Dictionary<string, string?>> ToDictionaryEnumerableAsync<TReader>(TReader reader, CancellationToken ct = default)
        where TReader : TextReader => FromTextReaderToDictionaryAsync(reader, ct);

    public IAsyncEnumerable<Dictionary<string, string?>> ToDictionaryEnumerableAsync(string filePath, CancellationToken ct = default)
        => FromTextReaderToDictionaryAsync(new StringReader(filePath), ct);
    #endregion
    #region ToDictionaryArray
    public Dictionary<string, string?>[] ToDictionaryArray<TReader>(TReader reader)
        where TReader : TextReader => FromTextReaderToDictionary(reader).ToArray();

    public Dictionary<string, string?>[] ToDictionaryArray(string filePath) => FromTextReaderToDictionary(new StringReader(filePath)).ToArray();

    public async ValueTask<Dictionary<string, string?>[]> ToDictionaryArrayAsync<TReader>(TReader reader, CancellationToken ct = default)
        where TReader : TextReader {
        var results = new List<Dictionary<string, string?>>(Config.InitialCapacity);

        await foreach (Dictionary<string, string?> item in FromTextReaderToDictionaryAsync(reader, ct)) {
            results.Add(item);
        }
        return results.ToArray();
    }

    public async ValueTask<Dictionary<string, string?>[]> ToDictionaryArrayAsync(string filePath, CancellationToken ct = default) {
        using var reader = new StringReader(filePath);
        var results = new List<Dictionary<string, string?>>(Config.InitialCapacity);

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
        => FromTextReaderToDictionary(new StringReader(filePath)).ToList();

    public async ValueTask<List<Dictionary<string, string?>>> ToDictionaryListAsync<TReader>(TReader reader, CancellationToken ct = default)
        where TReader : TextReader {
        var results = new List<Dictionary<string, string?>>(Config.InitialCapacity);

        await foreach (Dictionary<string, string?> item in FromTextReaderToDictionaryAsync(reader, ct)) {
            results.Add(item);
        }
        return results;
    }

    public async ValueTask<List<Dictionary<string, string?>>> ToDictionaryListAsync(string filePath, CancellationToken ct = default) {
        using var reader = new StringReader(filePath);
        var results = new List<Dictionary<string, string?>>(Config.InitialCapacity);
        await foreach (Dictionary<string, string?> item in FromTextReaderToDictionaryAsync(reader, ct)) {
            results.Add(item);
        }
        return results;
    }
    #endregion

    // -----------------------------------------------------------------------------------------------------------------
    // Output
    // -----------------------------------------------------------------------------------------------------------------
    

    // -----------------------------------------------------------------------------------------------------------------
    // Actual Parsers
    // -----------------------------------------------------------------------------------------------------------------
    #region Generic Type Parsing
    private IEnumerable<T> FromTextReader<T>(TextReader reader) where T : class, new() {
        string[] headerColumns = [];
        int batchSize = Config.BatchSize;
        var batch = new List<T>();

        if (reader.ReadLine() is {} lineFull) {
            headerColumns = lineFull.Split(Config.ColumnSplit);
        }

        while (true) {
            string? line = null;
            for (int i = 0; i < batchSize && (line = reader.ReadLine()) != null; i++) {
                string[] values = line.Split(Config.ColumnSplit);
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

    private async IAsyncEnumerable<T> FromTextReaderAsync<T>(TextReader reader, [EnumeratorCancellation] CancellationToken ct = default) where T : class, new() {
        string[] headerColumns = [];
        int batchSize = Config.BatchSize;
        var batch = new List<T>(Config.BatchSize);

        if (await reader.ReadLineAsync(ct) is {} lineFull) {
            headerColumns = lineFull.Split(Config.ColumnSplit);
        }

        while (!ct.IsCancellationRequested) {
            string? line = null;
            for (int i = 0; i < batchSize && (line = await reader.ReadLineAsync(ct)) != null; i++) {
                string[] values = line.Split(Config.ColumnSplit);
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
                if (!Config.LogErrors) return;

                // Todo allow for logger
                Console.WriteLine($"Error setting property {prop.Name} on {value.GetType().Name}: {e.Message}");
            }
        }
    }
    #endregion
    #region Dictionary Parsing
    private IEnumerable<Dictionary<string, string?>> FromTextReaderToDictionary(TextReader reader) {
        string[] headerColumns = [];
        int batchSize = Config.BatchSize;
        var batch = new List<Dictionary<string, string?>>();
        if (reader.ReadLine() is {} lineFull) {
            headerColumns = lineFull.Split(Config.ColumnSplit);
        }

        while (true) {
            string? line = null;
            for (int i = 0; i < batchSize && (line = reader.ReadLine()) != null; i++) {
                string[] values = line.Split(Config.ColumnSplit);

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

    private async IAsyncEnumerable<Dictionary<string, string?>> FromTextReaderToDictionaryAsync(TextReader reader, [EnumeratorCancellation] CancellationToken ct = default) {
        string[] headerColumns = [];
        int batchSize = Config.BatchSize;
        var batch = new List<Dictionary<string, string?>>();
        if (await reader.ReadLineAsync(ct) is {} lineFull) {
            headerColumns = lineFull.Split(Config.ColumnSplit);
        }

        while (true) {
            string? line = null;
            for (int i = 0; i < batchSize && (line = await reader.ReadLineAsync(ct)) != null; i++) {
                string[] values = line.Split(Config.ColumnSplit);

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
    private void FromDataToTextWriter<T>(TextWriter writer, IEnumerable<T> data) {
        // Write header row
        IEnumerable<T> enumerable = data as T[] ?? data.ToArray();
        PropertyInfo[] propertyInfos = GetCsvProperties(enumerable.FirstOrDefault());// Dirty but it will work

        if (Config.IncludeHeader) {
            string[] headers = GetCsvHeaders(propertyInfos).ToArray();
            for (int i = 0; i < headers.Length; i++) {
                writer.Write(headers[i]);
                if (i < headers.Length - 1) writer.Write(Config.ColumnSplit);
            }

            writer.Write(Environment.NewLine);
        }

        // Write data rows
        foreach (T? obj in enumerable) {
            string[] values = GetCsvValues(obj, propertyInfos).ToArray();
            for (int i = 0; i < values.Length; i++) {
                writer.Write(values[i]);
                if (i < values.Length - 1) writer.Write(Config.ColumnSplit);
            }

            writer.Write(Environment.NewLine);
        }
    }

    private async Task FromDataToTextWriterAsync<T>(TextWriter writer, IEnumerable<T> data) {
        // Write header row
        IEnumerable<T> enumerable = data as T[] ?? data.ToArray();
        PropertyInfo[] propertyInfos = GetCsvProperties(enumerable.FirstOrDefault());

        if (Config.IncludeHeader) {
            string[] headers = GetCsvHeaders(propertyInfos).ToArray();
            for (int i = 0; i < headers.Length; i++) {
                await writer.WriteAsync(headers[i]);
                if (i < headers.Length - 1) await writer.WriteAsync(Config.ColumnSplit);
            }

            await writer.WriteAsync(Environment.NewLine);
        }

        // Write data rows
        foreach (T? obj in enumerable) {
            string[] values = GetCsvValues(obj, propertyInfos).ToArray();
            for (int i = 0; i < values.Length; i++) {
                await writer.WriteAsync(values[i]);
                if (i < values.Length - 1) await writer.WriteAsync(Config.ColumnSplit);
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
                    return Config.UseLowerCaseHeaders ? p.Name.ToLowerInvariant() : p.Name;

                return Config.UseLowerCaseHeaders
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
    private void FromDictionaryToTextWriter(TextWriter writer, IEnumerable<Dictionary<string, string?>> data) {
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

    private async Task FromDictionaryToTextWriterAsync(TextWriter writer, IEnumerable<Dictionary<string, string?>> data) {
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

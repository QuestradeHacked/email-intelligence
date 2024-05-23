using Questrade.FinCrime.Email.Intelligence.Domain.Services;
using StatsdClient;
using Environment = Questrade.FinCrime.Email.Intelligence.Infra.Utils.Environment;

namespace Questrade.FinCrime.Email.Intelligence.Infra.Services;

public class MetricService : IMetricService
{
    private readonly IDogStatsd _dogStatsd;

    public MetricService(IDogStatsd dogStatsd)
    {
        _dogStatsd = dogStatsd;
    }

    public void Distribution(string statName, double value, IList<string>? tags = null)
    {
        tags ??= new List<string>();

        AddEnvironment(tags);

        _dogStatsd.Distribution(statName, value, tags: ToArray(tags));
    }

    public void Increment(string statName, IList<string> tags)
    {
        AddEnvironment(tags);

        _dogStatsd.Increment(statName, tags: ToArray(tags));
    }

    public IDisposable StartTimer(string statName, IList<string>? tags = null)
    {
        tags ??= new List<string>();

        AddEnvironment(tags);

        return _dogStatsd.StartTimer(statName, tags: ToArray(tags));
    }

    private static void AddEnvironment(ICollection<string> tags)
    {
        tags.Add($"env:{Environment.Name.ToUpperInvariant()}");
    }

    private static string[] ToArray(ICollection<string> collection)
    {
        var array = new string[collection.Count];

        collection.CopyTo(array, arrayIndex: 0);

        return array;
    }
}

namespace ToyEngine.Utils;

public class AssetWatcher
{
    private bool _isDirty;

    public AssetWatcher(string directory, params string[] filters)
    {
        var watcher = new FileSystemWatcher(directory);
        watcher.NotifyFilter = NotifyFilters.LastWrite;

        foreach (string filter in filters)
            watcher.Filters.Add(filter);

        watcher.Changed += (_, _) => _isDirty = true;
        watcher.EnableRaisingEvents = true;
    }

    public bool IsDirty()
    {
        if (_isDirty)
        {
            _isDirty = false;
            return true;
        }
        return false;
    }
}


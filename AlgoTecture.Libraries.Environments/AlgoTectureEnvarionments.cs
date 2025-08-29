namespace Algotecture.Libraries.Environments;

public static class AlgoTectureEnvironments
{
    private const string DataFolder = "Data";
    private static string? GetPathToSolution() => Directory.GetParent(Environment.CurrentDirectory)?.FullName;
    
    public static string GetPathToImages()
    {
        var pathToSolution = GetPathToSolution();
        if (string.IsNullOrEmpty(pathToSolution)) throw new ArgumentNullException(nameof(pathToSolution));

        var pathToImages = Path.Combine(pathToSolution, "AlgoTecture.Data.Images");
        
        if (OperatingSystem.IsLinux())
        {
            pathToImages = Path.Combine(pathToSolution, "algotecture-images");
        }

        return pathToImages;
    }
}
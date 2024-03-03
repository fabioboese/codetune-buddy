namespace CodeTune.Buddy
{
    public class AboutMe
    {
        public static string? GetVersion() => System.Reflection.Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString();
    }
}

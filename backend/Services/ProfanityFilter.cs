using System.Text.RegularExpressions;

public class ProfanityFilter
{
    private static readonly List<string> BadWords = new()
    {
        "shit", "fuck", "damn", "crap", "ass", "bitch", "bastard",
        "dammit", "hell", "asshole", "dick", "pussy", "motherfucker",
        "piss", "cunt", "cocksucker", "twat", "wanker", "bollocks",
        "bugger", "sod", "arsehole", "slut", "whore"
    };

    public static bool ContainsProfanity(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return false;

        var lowerText = text.ToLower();
        return BadWords.Any(word => Regex.IsMatch(lowerText, $@"\b{Regex.Escape(word)}\b"));
    }

    public static string GetProfanityErrorMessage()
    {
        return "Your comment contains inappropriate language and cannot be posted.";
    }
}

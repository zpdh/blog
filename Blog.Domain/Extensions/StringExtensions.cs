namespace Blog.Domain.Extensions;

public static class StringExtensions {
    public static bool IsNotEmpty(this string str) {
        return !string.IsNullOrWhiteSpace(str);
    }
}
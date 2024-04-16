using Microsoft.AspNetCore.Http;

public static class SessionExtensions
{
    // Extension method for incrementing integer stored in a session
    public static int IncrementSessionInt(this ISession session, string key)
    {
        var value = session.GetInt32(key) ?? 0;
        value++;
        session.SetInt32(key, value);
        return value;
    }
}
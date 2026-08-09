namespace Mentoring.EF.Helpers;

public static class EmailBodyBuilder
{
    public static string GenerateEmailBody(string template , Dictionary<string, string> placeholders)
    {
        var templatePath = $"{Directory.GetCurrentDirectory()}/Templates/{template}.html"; 
        var streamReader = new StreamReader(templatePath);
        var body = streamReader.ReadToEnd();
        streamReader.Close();

        foreach(var placeholder in placeholders)
            body = body.Replace(placeholder.Key, placeholder.Value);
        

        return body;
    }
}

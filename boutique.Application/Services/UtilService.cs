using System.Text;
using boutique.Application.Interfaces.Services;

namespace boutique.Application.Services;

public class UtilService : IUtilService
{
    private readonly Random _random = new Random();

    public Task<string> GenerateRandomString(int size, bool lowerCase = false)
    {
    // Instantiate random number generator.
        var builder = new StringBuilder(size);

        // Unicode/ASCII Letters are divided into two blocks
        // (Letters 65–90 / 97–122):
        // The first group containing the uppercase letters and
        // the second group containing the lowercase.

        // char is a single Unicode character
        char offset = lowerCase ? 'a' : 'A';
        const int lettersOffset = 26; // A...Z or a..z: length=26

        for (var i = 0; i < size; i++)
        {
            var @char = (char)_random.Next(offset, offset + lettersOffset);
            builder.Append(@char);
        }

        return Task.FromResult(lowerCase ? builder.ToString().ToLower() : builder.ToString());
    }
}
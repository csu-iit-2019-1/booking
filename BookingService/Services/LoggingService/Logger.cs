using System.IO;
using System.Threading.Tasks;

namespace BookingService.Services.LoggingrService
{
    public class Logger
    {
        public async Task WriteLogAsync(string message)
        {
            using (StreamWriter writer = new StreamWriter("Log.txt", true))
            {
                await writer.WriteLineAsync(message);
                writer.Close();
            }
        }

        public async Task<string> ReadLogAsync()
        {
            var textFromFile = "";

            using (StreamReader reader = new StreamReader("Log.txt", true))
            {
                textFromFile = await reader.ReadToEndAsync();
                reader.Close();
            }

            return textFromFile;
        }
    }
}

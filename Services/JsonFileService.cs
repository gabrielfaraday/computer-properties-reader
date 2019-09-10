using System;
using System.IO;
using Newtonsoft.Json;

namespace computer_check.Services
{
    public class JsonFileService
    {
        public static void WriteFile(object objToSave)
        {
            Console.WriteLine("Writing JSON file...");

            try
            {
                Directory.CreateDirectory(@"c:\history-check");

                using (var file = File.CreateText($@"c:\history-check\{DateTime.Now.ToString("yyMMdd-hhmmss")}.txt"))
                {
                    var serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(file, objToSave);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while writing json file: {ex.ToString()}");
            }
        }
    }
}
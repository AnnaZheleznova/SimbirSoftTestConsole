using System;
using System.IO;
using System.Threading.Tasks;

namespace SimbirSoftTestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Word word = new Word();
            Console.WriteLine("Enter web-site:");
            string website = Console.ReadLine();

            try
            { 
            await word.GetText(website);
            }
            catch (FileNotFoundException notFound) { Console.WriteLine(notFound.Message); }
            catch (System.Net.WebException web) { Console.WriteLine($"Address is not correct\n{web.Message}"); }

        }
    }
}

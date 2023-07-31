using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace MvcDirectory.Models
{
    public class Extractor
    {
        public static string ExtractName(string input)
        {
            // Define a pattern to capture first name and last name
            string namePattern = @"\b\p{Lu}\p{Ll}+\b(?:\s+\p{Lu}\p{Ll}+)+";
            string[] namePatterns = new string[]
            {
                @"\b\p{Lu}\p{Ll}+\b(?:\s+\p{Lu}\p{Ll}+)+",
                @"\b[A-Z][a-zA-Z'-]+\b"

            };

            Match match = Regex.Match(input, namePattern);

            return match.Success ? match.Value : null;
        }

        public static List<string> ExtractPhoneNumber(string input)
        {
            List<string> result = new List<string>();

            string[] phonePatterns = new string[]
            {
        @"\b\d{3}\s\d{3}\s\d{2}\s\d{2}\b",
        @"\b\d{3}[-.]\d{3}[-.]\d{4}\b",
        @"\b\+\d{2}\s\d{3}\s\d{3}\s\d{2}\s\d{2}\b",
        @"\b\d{4}\s?\d{7}\b"
            };

            foreach (string pattern in phonePatterns)
            {
                MatchCollection matches = Regex.Matches(input, pattern);
                foreach (Match match in matches)
                {
                    string phoneNumber = match.Value;
                    // Kontrol et, eğer bu numara zaten eklenmişse ekleme.
                    if (!result.Contains(phoneNumber))
                    {
                        result.Add(phoneNumber);
                        break; // İlk eşleşmeyi bulduktan sonra döngüden çık.
                    }
                }
            }

            return result;
        }




        public static List<string> ExtractEmailAddresses(string input)
        {
            List<string> result = new List<string>();
        
            string[] emailPatterns = new string[]
        {
        @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b", // Standart e-posta adresi formatı
        @"\b[A-Za-z0-9._%+-]+@domain1\.com\b", // domain1.com alan adına sahip e-posta adresleri
        @"\b[A-Za-z0-9._%+-]+@domain2\.net\b", // domain2.net alan adına sahip e-posta adresleri
        @"\b[A-Za-z0-9._%+-]+@(example|test)\.com\b", // example.com veya test.com alan adına sahip e-posta adresleri
        @"\b[A-Za-z0-9._%+-]+@company\.com\b", // company.com alan adına sahip e-posta adresleri
        @"\b[A-Za-z0-9._%+-]+@(gmail|yahoo|outlook)\.com\b", // Gmail, Yahoo veya Outlook alan adına sahip e-posta adresleri
        @"\b[A-Za-z0-9._%+-]+@edu\.edu\b", // edu.edu alan adına sahip e-posta adresleri (eğitim kurumları için)
        @"\b[A-Za-z0-9._%+-]+@(hotmail|live|msn)\.com\b",
        @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b",
        @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b",
        @"\b[A-Za-z0-9]+@ornek\.com\b",
        @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}",
        @"(?:[a-z0-9!#$%&'+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'+/=?^_`{|}~-]+)|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])"")©(?:(?:[a-z0-9](?:[a-z0-9-][a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-][a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])"
        };
            foreach (string pattern in emailPatterns)
            {
                MatchCollection matches = Regex.Matches(input, pattern);
                foreach (Match match in matches)
                {
                    result.Add(match.Value);
                    Console.WriteLine("Matched Email: " + match.Value);
                }
            }

            return result;
        }




    }
}

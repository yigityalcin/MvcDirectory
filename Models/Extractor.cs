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
                @"\b[A-Z][a-zA-Z'-]+\b",
                @"(?<=\b[A-Z][a-zA-Z]*\s)[^\s]+"
            };

            Match match = Regex.Match(input, namePattern);

            return match.Success ? match.Value : null;
        }

        public static List<string> ExtractPhoneNumber(string input)
        {
            List<string> result = new List<string>();

            string[] phonePatterns = new string[]
            {
         @"\d{3}\s\d{3}\s\d{2}\s\d{2}",
            @"\d{3}[-.]\d{3}[-.]\d{4}",
            @"\+\d{2}\s\d{3}\s\d{3}\s\d{2}\s\d{2}",
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
                @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b", // Standard email address format
                @"\b[A-Za-z0-9._%+-]+@domain1\.com\b", // email addresses with domain1.com domain
                @"\b[A-Za-z0-9._%+-]+@domain2\.net\b", // email addresses with domain2.net domain
                @"\b[A-Za-z0-9._%+-]+@(example|test)\.com\b", // email addresses with example.com or test.com domain
                @"\b[A-Za-z0-9._%+-]+©creamobile\.com\b", // email addresses with company.com domain
                @"\b[A-Za-z0-9._%+-]+@(gmail|yahoo|outlook)\.com\b", // email addresses with Gmail, Yahoo, or Outlook domain
                @"\b[A-Za-z0-9._%+-]+@edu\.edu\b", // email addresses with edu.edu domain (for educational institutions)
                @"\b[A-Za-z0-9._%+-]+@(hotmail|live|msn)\.com\b", // email addresses with Hotmail, Live, or MSN domain
            }; // Add other custom domain patterns or specific email patterns as needed.

            foreach (string pattern in emailPatterns)
            {
                input = Regex.Replace(input, @"©\s*([^©\s]+)\s*©", "@$1@");
                MatchCollection matches = Regex.Matches(input, pattern);
                foreach (Match match in matches)
                {

                    string emailAddress = match.Groups[0].Value;
                    // Check if this email address is already added to the result list, if not, add it.
                    if (!result.Contains(emailAddress))
                    {
                        result.Add(emailAddress);
                        break; // After finding the first match, break the loop.
                    }
                }
            }

            // Replace © with @
            input = Regex.Replace(input, @"©\s*([^©\s]+)\s*©", "@$1@");

            // Find and add newly replaced email addresses


            return result;
        }

        public static string ExtractWebsiteURL(string input)
        {
            // This regular expression matches most common website URL formats.
            // It may not cover all possible URL variations.
            string urlPattern = @"\b(?:https?://|www\.)\S+\b";

            Match match = Regex.Match(input, urlPattern);

            if (match.Success)
            {
                return match.Value;
            }
            else
            {
                return null; // No website URL found in the input string.
            }
        }
        public static string ExtractAddress(string input)
        {
            string addressPattern = @"\b[A-Za-züğşıöçİĞŞÜÇ]+\s+Mah\.\s+[A-Za-züğşıöçİĞŞÜÇ]+\s+[A-Za-züğşıöçİĞŞÜÇ]+\s+No:\d+\s+\d+\s+[A-Za-züğşıöçİĞŞÜÇ]+\/[A-Za-züğşıöçİĞŞÜÇ]+\b";



            Match match = Regex.Match(input, addressPattern, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                string address = match.Value.Trim();
                return address;
            }

            // Eğer adres bulunamazsa, boş bir string döndürülebilir veya null değeri kullanılabilir.
            return string.Empty;
        }
    }
}
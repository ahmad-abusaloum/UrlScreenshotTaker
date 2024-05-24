using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using PuppeteerSharp;
using ShellProgressBar;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        // Display a nice banner
        PrintBanner();

        // Read URLs from domains.txt
        string[] domains = File.ReadAllLines("domains.txt");

        // Ensure the screenshots directory exists
        string screenshotsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "screenshots");
        if (!Directory.Exists(screenshotsDirectory))
        {
            Directory.CreateDirectory(screenshotsDirectory);
        }

        // Initialize progress bar
        var options = new ProgressBarOptions
        {
            ProgressCharacter = '─',
            ProgressBarOnBottom = true,
            DisplayTimeInRealTime = false,
        };

        using (var pbar = new ProgressBar(domains.Length, "Processing URLs...", options))
        {
            // Initialize Puppeteer
            await new BrowserFetcher().DownloadAsync(); // Download the default browser
            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }))
            {
                foreach (string domain in domains)
                {
                    bool success = await ProcessUrl(browser, domain, screenshotsDirectory);
                    string message = success ? $"{SubstringFirst25Character(domain)} - Finished" : $"{domain} - Failed";
                    
                    pbar.Tick(message);
                }
            }
        }

        Console.WriteLine("\nAll URLs processed ^_^");
        Console.ReadLine();
    }

    static async Task<bool> ProcessUrl(IBrowser browser, string domain, string screenshotsDirectory)
    {
        try
        {
            if (IsNotDomainOrUrl(domain))
            {
                domain = ExtractUrlFromBrackets(domain);

                if (domain == null)
                    return false;
            }

            // Add https:// if the domain does not start with http:// or https://
            string url = domain.StartsWith("http://") || domain.StartsWith("https://") ? domain : "https://" + domain;

            using (var page = await browser.NewPageAsync())
            {
                await page.SetViewportAsync(new ViewPortOptions { Width = 1280, Height = 720 });
                await page.GoToAsync(url);
                string fileName = $"{GetFileName(url)}.png";
                string filePath = Path.Combine(screenshotsDirectory, fileName);
                await page.ScreenshotAsync(filePath);

                await page.DisposeAsync();
                await page.CloseAsync();
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    static string GetFileName(string url)
    {
        return url.Replace("https://", "").Replace("http://", "").Replace("/", "_");
    }
    
    static string SubstringFirst25Character(string input)
    {
        if (input.Length > 25)
        {
            string truncatedString = input.Substring(0, 25);
            return truncatedString;
        }
        else
        {
            return input;
        }
    }

    static bool IsNotDomainOrUrl(string input)
    {
        // Regular expression to match a valid domain or subdomain
        string domainPattern = @"^((?!-)[A-Za-z0-9-]{1,63}(?<!-)\.)+[A-Za-z]{2,6}$";

        // Check if input is a valid domain or subdomain
        if (Regex.IsMatch(input, domainPattern))
        {
            return false;
        }

        // Check if input is a valid URL
        if (Uri.TryCreate(input, UriKind.Absolute, out Uri uriResult) &&
            (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
        {
            return false;
        }

        return true;
    }

    static string ExtractUrlFromBrackets(string input)
    {
        // Regular expression to match a URL inside square brackets and ignore ANSI escape codes
        string pattern = @"\[\x1b\[[0-9;]*m?(https?://[^\s\]\x1b]+)\x1b\[0m?\]";

        Match match = Regex.Match(input, pattern);

        if (match.Success)
        {
            return match.Groups[1].Value;
        }

        return null;
    }

    static void PrintBanner()
    {
        Console.ForegroundColor = ConsoleColor.Green;

        string banner1 = @"                     __                        __ 
              ____ _/ /_  ____ ___  ____ _____/ / 
             / __ `/ __ \/ __ `__ \/ __ `/ __  /  
            / /_/ / / / / / / / / / /_/ / /_/ /   
            \__,_/_/ /_/_/ /_/ /_/\__,_/\__,_/    ";

        Console.Write(banner1);

        string banner = @"
     ___    __             _____       __                    
    /   |  / /_  __  __   / ___/____ _/ /___  __  ______ ___ 
   / /| | / __ \/ / / /   \__ \/ __ `/ / __ \/ / / / __ `__ \
  / ___ |/ /_/ / /_/ /   ___/ / /_/ / / /_/ / /_/ / / / / / /
 /_/  |_/_.___/\__,_/   /____/\__,_/_/\____/\__,_/_/ /_/ /_/ ";
        Console.Write(banner);

        Console.ResetColor();

        // Set the color to red for the second banner
        Console.ForegroundColor = ConsoleColor.Red;

        string heart = @"
                  
                                                ***   *** 
                                               ***** ***** 
 https://www.facebook.com/anonymous.albaq3awy  *********** 
         < URL Screenshot Taker >               ********* 
                                                  *****   
                                                    *     ";
        Console.WriteLine(heart);


        Console.ResetColor();
    }

}

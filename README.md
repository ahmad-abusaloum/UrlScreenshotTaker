# URL Screenshot Taker

URL Screenshot Taker is a .NET console application that takes screenshots of web pages specified in a `domains.txt` file. The application uses PuppeteerSharp to automate browser tasks and capture the screenshots, storing them in a designated directory.

## Features
* Reads URLs from a domains.txt file.
* Ensures a screenshots directory exists to store captured screenshots.
* Utilizes PuppeteerSharp for headless browser automation.
* Provides progress feedback via a progress bar.
* Handles various URL formats and validates input.

## Installation
1. Clone the repository:
   - git clone https://github.com/yourusername/url-screenshot-taker.git
   - cd url-screenshot-taker

2. Restore the necessary .NET packages:
   - dotnet restore

3. Install PuppeteerSharp:
   - npm install puppeteer

## Image Example :
![image](https://github.com/ahmad-abusaloum/UrlScreenshotTaker/assets/25351143/b9368365-804a-4023-8737-c23386e60d77)

## If you are working on a Windows environment:
1. Download `.Net Framework 4.7.2` library
2. Download code
3. Go to this path: `bin\Debug`
4. Find the `domains.txt` file and fill in the links you want to target
5. Go to the `\bin\Debug\screenshots` folder and make sure it is empty.
6. Search for the program called `UrlScreenshotTaker.exe`

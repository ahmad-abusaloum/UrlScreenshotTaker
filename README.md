# UrlScreenshotTaker

![Project Image](https://github.com/ahmad-abusaloum/UrlScreenshotTaker/assets/25351143/b9368365-804a-4023-8737-c23386e60d77)

## Overview

UrlScreenshotTaker is a tool designed to take screenshots of a list of domains on Windows. This project aims to simplify the process of capturing website screenshots programmatically.

## Features

- Capture screenshots of multiple domains
- Easy to configure and use
- Built with .NET for Windows

## Prerequisites

- Windows OS
- Visual Studio (Community, Professional, or Enterprise)
- .NET SDK

## Installation

1. **Clone the repository:**

    ```sh
    git clone https://github.com/ahmad-abusaloum/UrlScreenshotTaker.git
    ```

2. **Open the project in Visual Studio:**

    - Launch Visual Studio.
    - Click on `File -> Open -> Project/Solution`.
    - Navigate to the cloned repository and select `UrlScreenshotTaker.sln`.

3. **Restore dependencies:**

    Visual Studio should automatically restore the required NuGet packages. If not, right-click on the solution in the Solution Explorer and select `Restore NuGet Packages`.

## Usage

1. **Build the project:**

    - In Visual Studio, select `Build -> Build Solution` or press `Ctrl+Shift+B`.

2. **Run the project:**

    - Set the project as the startup project (if not already set).
    - Press `F5` or click on `Debug -> Start Debugging` to run the application.

3. **Configure domains:**

    - Edit the `domains.txt` file located in the project directory to include the list of domains you wish to capture screenshots for.

4. **Capture screenshots:**

    - The application will read the domains from the `domains.txt` file and capture screenshots, saving them to the designated output directory.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any improvements or bug fixes.

---

Ahmad Abu Saloum

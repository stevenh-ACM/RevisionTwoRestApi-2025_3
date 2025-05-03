<img style="height:10rem;" src="wwwroot/img/airplane5d.jpg" align="right" alt="Acumatica Inc" />
<img style="height: 5rem; " src="wwwroot/img/acumatica-2024-logo.png" alt="Acumatica Inc" />

Revision Two is an imaginary company that Acumatica Inc uses for demos. 
There's an associated data set called SalesDemo which is installed when installing a local Acumatica ERP instance. 
The data can also be installed using the installation wizard and select the db maintenance feature.

# Razor Pages Project

This is a Razor Pages project built with .NET 9. Razor Pages is a page-based programming model that makes building web UI easier and more productive.

## Features

- Built with .NET 9
- Razor Pages for clean and organized page-based development
- Supports modern web development practices
- Easily extensible and maintainable

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) installed
- A code editor like [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

## Getting Started

1. Clone the repository:

git clone <repository-url> cd <repository-folder>

2. Restore dependencies:

dotnet restore

3. Run the application:

dotnet run


4. Open your browser and navigate to `https://localhost:5001` (or the URL specified in the console output).

## Project Structure

- **Pages/**: Contains Razor Pages (`.cshtml` files) and their associated page models (`.cshtml.cs` files).
- **wwwroot/**: Static files like CSS, JavaScript, and images.
- **Startup.cs**: Configures services and middleware for the application.
- **appsettings.json**: Configuration settings for the application.

## Building and Running

To build the project:

dotnet build

## Deployment

5. Publish the application:

dotnet publish -c Release -o ./publish


6. Deploy the contents of the `./publish` folder to your web server.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).

---

Feel free to customize this `README.md` to better suit your project's specific needs.
# FootballAPI

FootballAPI is a robust and efficient web API built using ASP.NET Core 8. It leverages the power of Entity Framework Core 8 as an Object-Relational Mapping (ORM) tool. The project is designed using the principles of Domain-Driven Design (DDD) and Clean Architecture, ensuring separation of concerns and making the codebase maintainable and scalable.

## Features

- **ASP.NET Core 8**: A framework for building HTTP services with the latest features and improvements.
- **Entity Framework Core 8 (EF Core 8)**: A lightweight, extensible, open-source, and cross-platform version of the popular Entity Framework data access technology.
- **Domain-Driven Design (DDD)**: An architectural approach that focuses on modeling the domain and its logic, promoting a clear structure and understanding of business requirements.
- **Clean Architecture**: Separates the software into concentric layers with a strong emphasis on separation of concerns, resulting in a loosely coupled and easily testable application.
- **Repository Pattern**: Mediates data access between the domain layer and persistence layer (e.g., EF Core).
- **Unit of Work**: Coordinates transactional operations across multiple repositories to ensure data consistency.
- **Mediator Pattern (MediatR)**: Decouples components by encapsulating requests, commands, and queries, improving maintainability and testability.
- **AutoMapper**: Simplifies object-to-object mapping, reducing boilerplate code for data transformations.
- **Microsoft Identity**: Provides robust authentication and authorization, integrated with ASP.NET Core Identity for user and role management.
- **JWT Authentication**: Securely authenticates API users, leveraging Microsoft Identity for token generation and validation.
- **Minimal API**: A lightweight project showcasing streamlined HTTP service development with ASP.NET Core Minimal APIs.

## CORS Policy Note

Please be aware that this project has been configured to allow all Cross-Origin Resource Sharing (CORS) requests. While this makes the API accessible to all users, it poses security risks as it allows any domain to interact with your API. It's recommended to restrict CORS to trusted origins in production environments to safeguard your application.

## Getting Started

To get started with FootballAPI, follow these steps:

1. Clone the repository:
   
   git clone https://github.com/Hamza-Darouzi/FootballAPI.git
   
   
2. Navigate to the project directory:
   
   cd FootballAPI
   
   
3. Set up the development environment with ASP.NET Core 8 and EF Core 8.
4. Run the application:
   
   dotnet run
   

## License

This project is licensed under the terms of the MIT license. See the [LICENSE](LICENSE) file for details.

Enjoy using FootballAPI! If you have any questions or feedback, feel free to open an issue or submit a pull request. Happy coding! 🚀

---

Feel free to modify any sections further if needed!

# Project 2 Specs

## Description

An ASP.NET WebAPI that will serve as a back end to a Real Estate Marketplace site. It will allow for full CRUD operations on a variety of entities, separated into their own ASP.NET Controllers. Data will be persisted on a locally running MS SQL Server database containerized using Docker. The team will use EF Core as their ORM and xUnit for unit testing.

## Epics and Stories

|Epic                               |Title                                       |Description                                                                                |
|-----------------------------------|--------------------------------------------|-------------------------------------------------------------------------------------------|
|Stretch Goals: Property Media      |Upload Photos for Properties                |Let users upload and attach images to their listings.                                      |
|Stretch Goals: Property Media      |Display Thumbnails on Listings Page         |Show a preview image on each property card.                                                |
|Stretch Goals: Analytics           |Property Listing View Count                 |Track and display number of views per listing.                                             |
|Stretch Goals: Analytics           |Most Popular Listings Feature               |Show a list of most-viewed or most-favorited properties.                                   |
|MVP: User Interaction & Navigation |Create Simple Homepage                      |Intro page with welcome message and basic links.                                           |
|MVP: User Interaction & Navigation |Basic Routing Between Pages                 |Use frontend router for Login, Register, Listings, etc.                                    |
|MVP: User Interaction & Navigation |Show Logged-in State                        |Display username or 'Logout' when a user is logged in.                                     |
|MVP: Property Search & Bookmarking |Search Properties by City or Price Range    |Filter listings using query parameters.                                                    |
|MVP: Property Search & Bookmarking |Favorite or Bookmark Listings               |Allow users to mark listings for later.                                                    |
|MVP: Property Listings             |View All Properties (Public)                |List all property cards with summary info.                                                 |
|MVP: Property Listings             |View Property Details                       |Show full property info including address, price, etc.                                     |
|MVP: Property Listings             |Add New Property (Authenticated Only)       |Allow users to create new property listings.                                               |
|MVP: Property Listings             |Update Property (Owner Only)                |Edit an existing property the user owns.                                                   |
|MVP: Property Listings             |Delete Property (Owner Only)                |Delete a listing the user created.                                                         |
|MVP: Project Setup & Dev Workflow  |Initialize Git Repository                   |Set up version control and branch strategy for the team.                                   |
|MVP: Project Setup & Dev Workflow  |Setup Solution and Projects                 |Scaffold ASP.NET Core Web API, Class Libraries, and xUnit projects - and create a solution.|
|MVP: Project Setup & Dev Workflow  |Configure Local Docker SQL Server           |Set up SQL Server in Docker and connect to backend with EF Core.                           |
|MVP: Project Setup & Dev Workflow  |Define Entity Models and Seed Data          |Create models (User, Property, Address, etc) and add seed data.                            |
|MVP: Project Setup & Dev Workflow  |ERD and Planning Documentation              |Create ERD and README files for your project repository.                                   |
|MVP: Implementation                |xUnit Testing                               |Create xUnit tests for the service layer methods (80% coverage.)                           |
|MVP: Authentication & Authorization|User Registration Endpoint                  |Allow new users to sign up.                                                                |
|MVP: Authentication & Authorization|User Login with Token Generation            |Authenticate users and return JWT.                                                         |
|MVP: Authentication & Authorization|Protect API Routes Using [Authorize]        |Restrict endpoints to authenticated users.                                                 |
|MVP: Authentication & Authorization|Store Token on Frontend and Use for Requests|Send token with every protected API call.                                                  |
|MVP: Admin Basics                  |Admin: Remove Listings                      |Allow admins to delete fraudulent listings.                                                |
|MVP: Admin Basics                  |Admin Login                                 |Allow admins to log in with elevated privileges.                                           |
|MVP: Admin Basics                  |Admin Dashboard Access                      |Provide a dashboard for admin-only access.                                                 |
|MVP: Admin Basics                  |Admin: View User List                       |Admins can view a list of all registered users.                                            |
|MVP: Admin Basics                  |Admin: Deactivate User                      |Allow admins to deactivate or ban a user.                                                  |
|MVP: Admin Basics                  |Admin: View All Listings                    |Admins can see all property listings across all users.                                     |

## Technologies Used

- C#
- ASP.NET
- EF Core
- xUnit
- HTML/CSS/JS
- SQL Server
- Docker

## Agile Team

... TBD

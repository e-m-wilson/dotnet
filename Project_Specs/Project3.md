# Project 3 (Due: 01/31/25)

## Overview

For P3, you and your team will create a full stack web application: separate frontend and backend and a complete CI/CD pipeline. You will iterate upon your P2's backend in order to do so.

## Requirements

- Application Must build and run
- Unit Testing
  - Backend: 70% branch coverage for Services and any utilities
  - Frontend: 20% branch coverage
- CI/CD Pipeline
- Implement Secure Authentication (user login / registration)
- Frontend
  - React
  - Styling: plain CSS, Tailwind, bootstrap, etc.
  - Hosted on Azure
- Backend
  - ASP.NET API
  - Docker containerized
  - Hosted on Azure
- SQLServer DB hosted on Azure

## Technologies

- C# (Backend programming language)
- EF Core (ORM)
- SQL Server (Azure hosted)
- ASP.NET Core (Web API Framework)
- xUnit/Moq (Backend Testing)
- Azure (for application hosting)
- React
- Javascript
- Github Actions for CI/CD Pipelines
- Docker for containerization

## Additional features required

- Complex filtering for properties, filtering by things like bedrooms, price, garage included, etc.
- Featured Listings (carousel of popular properties)
- Search Bar: A search input that will filter properties based on address (show properties x distance from address entered, with the exact address listing first) [(Check out TurfJS!)](https://turfjs.org/docs/getting-started)
- Search Results Overview (cards or grid-style layout with images of properties that match search criteria; feature for users to favorite a property)
- Unified UI design language (Check out [MUI](https://mui.com/material-ui/) or [Chakra!](https://chakra-ui.com/docs/components/concepts/overview))

## Stretch goals/features

- Users should be able to view search results rendered as a map in the UI [(Check out LeafletJS!)](https://leafletjs.com/index.html)


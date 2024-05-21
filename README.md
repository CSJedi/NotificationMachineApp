# Notification Machine API
## Overview
The Notification Machine API is a .NET WebAPI service designed to manage customer interactions by identifying customers with low activity and notifying them accordingly. This service is especially useful for businesses that want to engage customers who might be less active than usual, specifically targeting those with fewer than three recorded activities in April 2024.

## Features
- **Customer Identification**: Identifies customers from various companies with fewer than three activities in April 2024.
- **Notification Dispatch**: Sends out notifications to the identified quiet customers to increase engagement.
- **Client Code Generation**: Generates unique client codes for each customer using a specified format, aiding in personalized communication strategies.
- **Single-Action Trigger**: Allows the operation to be initiated with a single button click, simplifying user interaction and backend processing.
- **Logging and Monitoring**: Integrates comprehensive logging to monitor the API's operations and troubleshoot potential issues.

## Client Code Format
The service generates a client code for each customer using a three-part format:
- **Part1 (KCA)**: Derived by taking the first letter off the customer's first name, taking the next three letters, and reversing them.
- **Part2 (RAP)**: Applies the same transformation to the customer's last name.
- **Part3 (JSW)**: Takes the first letter of each word in the organization’s name.

For example, for Jack Sparrow from "Jack Sparrow Warship," the client code would be `KCA-RAP-JSW`.

## Technical Setup

### Prerequisites
- .NET 8.0
- Microsoft SQL Server (for database interactions)
- Kafka (for managing notifications as message queues)

### Dependencies
- Entity Framework Core (for ORM)
- Confluent.Kafka (for Kafka integration)
- Serilog (for logging)

### Configuration
Ensure the `appsettings.json` file is configured with the correct database connection strings, Kafka server details, and email settings for SMTP transactions.

## Running the API
1. Clone the repository.
2. Open the solution in Visual Studio or any compatible IDE.
3. Restore NuGet packages and rebuild the solution.
4. Ensure the database connection and Kafka services are configured correctly.
5. Run the application.

The API provides endpoints to trigger the identification and notification processes and can be tested using any API client like Postman or Swagger (integrated into the API).

## Testing
Unit testsare provided under the `NotificationMachineApp.Tests` project. Ensure to run these tests to verify the correctness of the business logic and API endpoints.

## Logging
Logging is set up via Serilog to provide insights into the application's performance and issues. Logs are written to both the console and text files (rotated daily).

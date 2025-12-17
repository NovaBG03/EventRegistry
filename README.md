# EventRegistry

A distributed event logging and notification system built with .NET 10.0 and Azure Service Bus.

## Overview

EventRegistry allows applications to register and log events with automatic routing and notifications for critical/error level events. The system uses a microservices architecture with asynchronous message processing.

## Architecture

```
EventRegistry.Api          → REST API for app registration & event logging
EventRegistry.Business     → Core business logic & services
EventRegistry.Data         → Entities & in-memory repositories
EventRegistry.Messaging    → Azure Service Bus publishers & subscribers
EventRegistry.EventProcessor      → Background worker that processes events
EventRegistry.NotificationService → Background worker for critical notifications
```

## Event Flow

1. Client registers application → receives API key
2. Client logs events via API (with API key)
3. Events are queued to Azure Service Bus
4. EventProcessor consumes and persists events
5. Critical/Error events are routed to notifications topic
6. NotificationService handles alerting

## Prerequisites

- .NET 10.0 SDK
- Docker & Docker Compose

## Quick Start

**1. Start Azure Service Bus Emulator:**
```bash
cd emulator
docker-compose up -d
```

**2. Run the services (in separate terminals):**
```bash
dotnet run --project EventRegistry.Api
dotnet run --project EventRegistry.EventProcessor
dotnet run --project EventRegistry.NotificationService
```

## API Usage

**Register an application:**
```http
POST /api/applications
Content-Type: application/json

{ "name": "MyApp", "description": "My Application" }
```

**Log an event:**
```http
POST /api/events
X-Api-Key: <your-api-key>
Content-Type: application/json

{
  "category": "Error",
  "message": "Something went wrong",
  "timestamp": "2025-12-17T10:00:00Z",
  "metadata": { "userId": 123 }
}
```

## Event Categories

- `Debug` - Diagnostic information
- `Info` - General information
- `Warning` - Potential issues
- `Error` - Errors (triggers notification)
- `Critical` - Critical failures (triggers notification)

## API Documentation

Scalar UI available at root path (`/`) when running the API.

# Taskmanager

Task Management System API

This is the README file for the Task Management System API, a backend application developed using ASP.NET Core. The objective of this project is to design and implement a backend API for managing tasks, projects, users, and notifications. Users can create, manage, and track tasks, group tasks under projects, and receive notifications for relevant events.

Table of Contents
Entities & Relationships
API Endpoints
Clean Architecture
SOLID Principles
Global Error Handling
Documentation
Entities & Relationships
Task
Attributes:
Title
Description
Due Date
Priority (e.g., low, medium, high)
Status (e.g., pending, in-progress, completed)
Project
Attributes:
Name
Description
List of associated tasks
User
Attributes:
Name
Email
List of tasks they've created
Notification
Attributes:
Type (e.g., due date reminder, status update)
Message
Timestamp
API Endpoints
The API provides the following endpoints:

CRUD operations for Tasks, Projects, Users, and Notifications.
Fetch tasks based on their status or priority.
Fetch tasks due for the current week.
Assign a task to a project or remove it from a project.
Mark a notification as read or unread.
Notifications
Users receive notifications when:

A task's due date is within 48 hours.
A task they created is marked as completed.
They are assigned a new task.
Notifications are sent using a background service to periodically check and notify users based on the criteria mentioned above.

Clean Architecture
The application follows a clean architecture approach to ensure separation of concerns. It is structured into the following layers:

Presentation
Application
Domain
Infrastructure
Dependency Injection is used where appropriate to manage dependencies between these layers.

Domain-Driven Design (Optional)
Optionally, you can implement Domain-Driven Design (DDD) principles by identifying and designing aggregates, entities, and value objects. You may also implement domain events for significant actions, such as when a task's due date is updated.

SOLID Principles
The application adheres to SOLID principles to demonstrate a strong understanding of object-oriented design. SOLID principles are applied to ensure code readability, maintainability, and organization.

Error Handling
Proper error handling is implemented throughout the API. It returns appropriate HTTP status codes and error messages for different types of errors, including validation errors and not found errors.

Documentation
For detailed instructions on setting up, running, and testing the API, please refer to the accompanying documentation. This README provides an overview of the project's objectives, requirements, and architecture. Additionally, the documentation should include any design decisions or assumptions made during development.

Evaluation Criteria
The project will be evaluated based on the following criteria:

Adherence to the specified requirements.
Implementation of clean architecture and separation of concerns.
Code readability, maintainability, and organization.
Proper error handling and consideration of edge cases.




(Kindly use a valid email while registering so that you will recieve the notifications)
 
================== TO SETUP AND RUN TASK MANAGER ==============
Step 1: Build the appication in visual studio with all the dependencies.
Step 2: Hit the run button to start the background service.


================== TO TEST TASK MANAGER ==============
THE FOLLOWING ARE LIST OF ENUMS USED IN THIS APPLICATION TO ACCEPT SOME OF THE INPUTS SO WHEN YOU SEE THEM IN THE REQUEST YOU WILL KNOW WHAT EACH OF THEM MEAN:
1.  Priority
    {
        1 = Low,
        2 = Medium,
        3 = High
    }
2.  Status
    {
        1 = Pending,
        2 = In_progress,
        3 = completed
    }
3.  NotificationType
    {
        1 = Due_date,
        2 = Status_update
    }
4.  AddOrDelete
    {
        Add = 1,
        Delete = 0
    }


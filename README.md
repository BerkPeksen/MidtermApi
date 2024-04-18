# University Tuition Management API
This project is an ASP.NET Core API for managing university tuition fees. It allows students to check their tuition fee status and make payments via online banking. The API also includes endpoints for administrators to add tuition amounts for students and view a list of students with unpaid tuition fees.

# Design
## Assumptions
Each student has a unique student number and ID (StudentNo, ID).

The 'Term' field represents the academic term for which the tuition fee is applicable.

The 'TuitionTotal' field represents the total tuition fee for the term.

The 'Balance' field represents the remaining balance in the student's bank account

The 'Status' field indicates whether the tuition fee is paid or unpaid.


## Issues Encountered
Database Issue: Initially, the project was configured to use a local SQL server for database operations. However, when attempting to connect to a cloud SQL database, there were difficulties in establishing the connection and finding a free PostgreSQL server host. I ended up using ElephantSQL 
free hosting service, this allowed for successful database operations without the need to manage the database server manually.

Hosting API on Azure:  Attempted to host the API on Azure for public access, but encountered difficulties in configuring the deployment and ensuring the API was accessible. To demonstrate the functionality of the API, I ended up running the API locally using localhost for demonstration purposes.


## Data Model
The data model for the project is as follows:

<a href="https://imgbb.com/"><img src="https://i.ibb.co/GnZ9z8h/Screenshot-2024-04-18-204038.png" alt="Screenshot-2024-04-18-204038" border="0" /></a>

ID (int, primary key)

StudentNo (string)

Term (string)

TuitionTotal (int)

Balance (int)

Status (string)

You can use two different tables for Student and The bank information (In this case just the 'Balance') but I decided to use a single table for simplicity in this project 

## Video
Youtube:


<a href="https://youtu.be/PXFdO77KNPA"><img src="https://img.youtube.com/vi/PXFdO77KNPA/0.jpg" alt="vid" border="0" width="355" height="200" /></a>


Drive:


<a href="https://drive.google.com/file/d/1uzy0w8jMStGZ-3xjisXb59fOfpzfheio/view?usp=sharing"><img src="https://i.ibb.co/82jQpBK/vid.png" alt="vid" border="0" width="355" height="200" /></a>



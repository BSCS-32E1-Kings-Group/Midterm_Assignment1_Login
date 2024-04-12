# User Registration and Login System
Welcome to the User Registration and Login System repository!

## Introduction
This repository contains a user registration and login system developed to demonstrate the implementation of user authentication features in web applications. The system allows users to register for a new account and subsequently log in using their credentials.

## Features
### User Registration
- New users can register for an account by providing a username, password, and optional email address.
- Username must be at least 6 characters long and cannot contain spaces.
- Password must be at least 8 characters long and meet complexity requirements (include at least one uppercase letter, lowercase letter, number, and special character).
- Upon successful registration, users are redirected to the login page or presented with a confirmation message.
  
### User Login
- Registered users can log in to the application using their username and password.
- The system securely validates the provided credentials against stored data.
- Passwords are hashed using a one-way cryptographic algorithm (e.g., bcrypt) with a random salt for enhanced security.
- After successful login, users are redirected to their designated dashboard or home page.
- Login attempts are limited to 3 consecutive failures. After exceeding the limit, users are prompted with a message indicating the remaining attempts and suggesting re-registration.
- Informative error messages are displayed for invalid username or password, indicating the number of attempts remaining.




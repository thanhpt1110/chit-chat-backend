# Chit Chat - Backend Services

A backend services for a social networking application, built with `.NET Core` and deployed using `Docker` & `AWS`. This repository includes all the RESTful APIs and logic required to support core functionalities of the social network, including a **Recommendation System** and **Real-Time Communication** via WebRTC.

---

## ✨ Features

- **Feeds**: Personalized newsfeed based on user activity.
- **Notifications**: Real-time notifications for interactions.
- **Real-Time Communication**: Audio/video calls and real-time chat powered by WebRTC.
- **Recommendation System**: Personalized suggestions for friends, groups, or content using collaborative filtering and user activity analysis.
- **Search**: Search for users, posts, or hashtags.
- **Secure Authentication**: Token-based authentication using JWT.
- **Social Interactions**: Follow/unfollow, posting, and commenting.
- **User Management**: Registration, login, and profile updates.

---

## 🛠 Tech Stack

- **Framework**: .NET Core (Clean Architecture)
- **Database**: Amazon Aurora MySQL
- **Authentication**: JWT (JSON Web Token)
- **Recommendation System**: Content-based and Collaborative Filtering (e.g., Matrix Factorization, User-Item Similarity)
- **Real-Time Communication**: WebRTC, SignalR
- **Containerization**: Docker
- **Other Tools**: Entity Framework Core, AutoMapper, FluentValidation

---

## 🚀 Installation Guide

### Prerequisites

- Install [Docker](https://www.docker.com/)
- Install [.NET Core SDK](https://dotnet.microsoft.com/download)

### Steps

1. **Clone the repository**:

   ```bash
   git clone https://github.com/thanhpt1110/chit-chat-backend
   cd chit-chat-backend
   ```

2. **Set up environment variables**:
   Create a .env file in the `ChitChat.WebAPI` directory with the following content:

   ```bash
   SecretKey =  # The secret key used for signing and verifying tokens
   TokenValidityInDays =  # The number of days the token is valid
   RefreshTokenValidityInDays =  # The number of days the refresh token is valid
   Issuer =  # The issuer of the token
   Audience =  # The audience for whom the token is issued
   CloudinaryCloudName =  # The Cloudinary cloud name
   CloudinaryApiKey =  # The Cloudinary API key
   CloudinaryApiSecret =  # The Cloudinary API secret key
   ```

3. **Build and run with Docker**:

   ```bash
   docker-compose up --build
   ```

4. **Access the application**:

   ```bash
   The backend will be running on http://localhost:8080.
   ```

5. **Build and run with Docker**:

   ```bash
   Visit http://localhost:8080/swagger/index.html to explore the API endpoints using Swagger UI.
   ```

---

## 🤝 Authors

[Nguyễn Phúc Bình](https://github.com/leesoonduck3009)

[Phan Tuấn Thành](https://github.com/thanhpt1110)

[Lê Thanh Tuấn](https://github.com/thtuanlegithub)

# Prog6221-poe-part-2
# Cyber Guardian – Cybersecurity Awareness Chatbot

## Overview

Cyber Guardian is a C# Windows Forms (WinForms) cybersecurity awareness chatbot developed using .NET 8. The application is designed to educate users about online safety, phishing scams, password protection, privacy, and safe browsing habits through an interactive chatbot interface.

The project combines a graphical user interface (GUI), chatbot logic, audio integration, and structured response handling to create an engaging cybersecurity learning experience.

---

# Features

## User-Friendly Interface

* Built using Windows Forms (WinForms)
* Modern dark-themed interface
* Interactive chatbot conversation area
* Easy-to-use text input and buttons
* Personalized greeting system

## Cybersecurity Awareness Topics

The chatbot can respond to questions related to:

* Phishing attacks
* Password safety
* Safe browsing
* Online scams
* Privacy protection
* General cybersecurity awareness

## Personalized Chat Experience

* Users enter their name before chatting
* Bot responses include the user’s name
* Topic-based responses for better interaction

## Audio Integration

* Greeting audio playback support
* Uses `SoundPlayer` for WAV file playback
* Audio error handling included

## Intelligent Response System

* Uses dictionaries to store chatbot responses
* Randomized responses for natural conversations
* Sentiment-based responses
* Topic recognition system

## Error Handling

* Audio file validation
* Exception handling for playback errors
* Input validation and safe execution

---

# Technologies Used

* C#
* .NET 8
* Windows Forms (WinForms)
* Object-Oriented Programming (OOP)
* Dictionaries and Collections
* Event-Driven Programming
* SoundPlayer Audio Library

---

# Project Structure

```plaintext
CyberSecurityChatbot_POE/
│
├── AudioPlayer.cs          # Handles greeting audio playback
├── Chatbot.cs              # Chatbot class structure
├── ChatbotEngine.cs        # Main chatbot logic and responses
├── ConsoleHelper.cs        # Console utility/helper methods
├── Form1.cs                # Main WinForms interface
├── Form1.Designer.cs       # Auto-generated UI designer code
├── Program.cs              # Application entry point
├── CyberSecurityChatbot_POE.csproj
└── CyberSecurityChatbot_POE.slnx
```

---

# WPF / GUI Explanation

Although the project mainly uses Windows Forms for the graphical interface, the application still follows GUI development principles similar to WPF applications.

The graphical interface includes:

* Labels
* Buttons
* Text boxes
* Rich text display areas
* Custom colors and fonts
* Event-driven interactions

The GUI was designed to:

* Improve user interaction
* Make conversations easier to read
* Create a modern cybersecurity-themed appearance
* Provide a more engaging learning experience compared to a console application

The form is dynamically built inside `Form1.cs`, where controls are created, customized, and added programmatically.

---

# How the Chatbot Works

## Step 1 – User Starts the Program

The application launches through `Program.cs`.

```csharp
Application.Run(new Form1());
```

This opens the main chatbot window.

## Step 2 – User Enters Their Name

The chatbot stores the user’s name and personalizes responses.

## Step 3 – User Sends a Message

The chatbot processes the input and checks:

* Keywords
* Topics
* Matching cybersecurity categories

## Step 4 – Bot Generates a Response

Responses are retrieved from dictionaries inside `ChatbotEngine.cs`.

The chatbot uses:

* General responses
* Password safety responses
* Phishing responses
* Safe browsing responses
* Sentiment responses

## Step 5 – Response Display

The chatbot response is displayed in the chat area using a RichTextBox.

---

# Audio System

The project includes audio greeting support through the `AudioPlayer.cs` class.

## Features

* WAV file playback
* File existence checking
* Error handling
* Non-blocking playback

## Example

```csharp
SoundPlayer player = new SoundPlayer(filePath);
player.Play();
```

---

# Object-Oriented Programming Concepts Used

## Classes

The application is separated into multiple classes:

* `ChatbotEngine`
* `AudioPlayer`
* `Form1`
* `Program`

## Encapsulation

Data and chatbot logic are organized inside dedicated classes.

## Delegates

The chatbot includes a custom delegate:

```csharp
public delegate string BotResponseDelegate(string input);
```

This improves flexibility in handling chatbot responses.

## Collections

The project uses:

* Dictionaries
* Lists
* Randomized response collections

---

# Installation Guide

## Requirements

* Visual Studio 2022 or newer
* .NET 8 SDK
* Windows OS

## Steps

1. Clone the repository:

```bash
git clone <repository-url>
```

2. Open the solution file:

```plaintext
CyberSecurityChatbot_POE.slnx
```

3. Build the project in Visual Studio.

4. Run the application.

---

# Example Questions

Users can ask questions such as:

* “What is phishing?”
* “How do I create a strong password?”
* “Why is cybersecurity important?”
* “How can I browse safely online?”
* “What are online scams?”

---

# Future Improvements

Possible future upgrades include:

* Database integration
* AI-generated responses
* Voice recognition
* Text-to-speech functionality
* User authentication system
* Chat history storage
* Machine learning integration
* Advanced cybersecurity training modules

---

# Learning Outcomes

This project demonstrates:

* GUI application development
* C# programming skills
* Object-oriented programming
* Event-driven programming
* Cybersecurity awareness implementation
* Audio integration
* User interaction design
* Error handling techniques


# Author

Developed as part of a cybersecurity awareness programming project.

---

# License

This project is for educational purposes.

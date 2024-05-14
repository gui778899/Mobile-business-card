

## Overview
This document provides an overview of a software development project for an application designed using C# and .NET Maui framework. The app's primary functionality is to manage and share contact information via QR codes.

## Motivation 
As the world moves quickly to using more digital tools, the usual paper business card is changing too. This app changes how people share and keep track of their contact info. Instead of using physical cards, you can now share them easily using QR codes with this app. It helps solve the problem of losing or having too many paper cards by keeping all your contacts in one easy-to-find digital place. This new way is really important nowadays because being able to quickly get and use important contact details is a big part of doing well in business.

## Development Framework and Language
- **Programming Language**: C#
- **Framework**: .NET Maui

## Project Setup and Configuration
- **NuGet Package Management**: Installed necessary packages including SQLite, Firebase, Microsoft.Extensions.Configuration.Json (version 7), Microsoft.Extensions.Configuration.Binder (version 7).
- **Additional Packages**: CommunityToolkit.Mvvm, CommunityToolkit.Maui 5.3, Sharpnado.Tabs.Maui, SQLitePCLRaw.bundle_green, QR Coder, ZXing.Net.Maui.Controls, camera.maui.
- **Android-Specific Settings**: Modified the Android manifest to activate camera access.

## Application Concept
The application serves as a digital tool for storing and sharing contact information through QR codes, akin to digital business cards.

## User Interface Complexity
- **Implemented UI Controls**:
  - CheckBoxes, RadioButtons, Buttons, Tabs, Popup Pages, Entries, Switches, Labels
  - Advanced: CameraView (from zxing library), Borders, Frames
- **Custom Controls**:
  - **GeneratedCard** : This control displays user-created business cards in the app. It shows essential details like name, email, job title, and a QR code. Users can interact with these cards for actions such as editing.
  - **ScannedCard** : Designed for business cards added via QR code scanning, this control focuses on displaying the scanned card information. It differentiates in design from GeneratedCard to help users easily identify scanned cards.

## Features
- **Login and Registration**: Implemented using Firebase Cloud, this feature provides a secure way for users to create accounts and log in to the app. 

- **QR Code Scanning**: Scan and Decode: The app uses the device's camera to scan QR codes. Upon scanning, it decodes the information embedded in the QR code, typically containing contact details from another user's digital business card.

- **Contact Card Operations: CRUD Operations**
  - **Create**: Add new business card information.
  - **Read**: View details of existing cards.
  - **Update**: Edit information on an existing card.
  - **Delete**: Remove a card from the system.


- **Sorting**:  Users can sort their digital business cards based on different criteria like name, company, job title, etc., making it easier to find the desired contact.

- **Search Functionality**: A search bar allows users to quickly locate a specific card by searching for relevant details like names, email addresses, or phone numbers.

- **QR Code Generating**: This feature allows users to generate QR codes from their business card details. These QR codes can be shared with others, who can then scan them to quickly save the user's contact details.

- **User can change themes**:Users can switch between different themes (like dark and light themes) to customize the visual experience of the app. 

## MVVM Integration
**ViewModels Utilised**: Key ViewModels include CardPageViewModel, ChangeCardPageViewModel, MainPageViewModel, QrCodePageViewModel, SignPageViewModel, and NewCardPageViewModel. These ViewModels serve as the binding layer between the UI (Views) and the business logic/data (Models), handling user inputs and presenting data.

## Software Design Patterns
- Dependency Injection for services.
- Inversion of Control (IoC) for ViewModels and Pages.
- Application settings read from appsettings.json.
- Separation of Concerns: Organized classes in respective folders.

## Database Integration
- Local SQLite database for CRUD operations.

## Cloud Integration
- Firebase for user authentication.
- FirebaseAuthClient injected for API calls.

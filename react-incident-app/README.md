# Incident Management System

A React-based web application for managing incidents with comprehensive features for creating, viewing, and updating incident statuses.

## Features

- **View Incidents**: Browse and view all reported incidents in a structured list format
- **Create Incidents**: Add new incidents with detailed information
- **Update Status**: Track and update the status of existing incidents
- **Material-UI Components**: Clean and professional user interface using Material-UI (MUI)
- **Tabbed Interface**: Easy navigation between different sections using tabs

## Project Structure

```
src/
├── components/
│   ├── IncidentForm.js      # Form component for creating new incidents
│   ├── IncidentList.js      # Component for displaying all incidents
│   └── UpdateStatus.js      # Component for updating incident status
├── api.js                   # API integration and backend communication
├── App.js                   # Main application component with tab navigation
├── App.css                  # Application styling
├── index.js                 # Application entry point
```

## Getting Started

### Prerequisites

- Node.js (v14 or higher)
- npm or yarn package manager

### Installation

1. Clone the repository:
```bash
git clone <repository-url>
cd react-incident-app
```

2. Install dependencies:
```bash
npm install
```

3. Start the development server:
```bash
npm start
```

The application will open in your browser at `http://localhost:3000`

## Usage

### Viewing Incidents
1. Navigate to the **Incidents** tab
2. Browse through all reported incidents with their details

### Creating a New Incident
1. Click on the **Create Incident** tab
2. Fill out the incident form with necessary details
3. Submit the form to create the incident

### Updating Incident Status
1. Click on the **Update Status** tab
2. Select an incident and update its status
3. Save the changes

## Technologies Used

- **React**: JavaScript library for building user interfaces
- **Material-UI (MUI)**: React component library for UI design
- **JavaScript (ES6+)**: Modern JavaScript features
- **CSS**: Styling and layout

## API Integration

The application communicates with a backend API through the `api.js` module. Ensure your backend server is running before using the application.

## Component Overview

### IncidentList Component
Displays all incidents in a user-friendly format with relevant details and actions.

### IncidentForm Component
Provides a form interface for users to create new incidents with validation and error handling.

### UpdateStatus Component
Allows users to update the status of existing incidents with a simple and intuitive interface.

## Contributing

Feel free to submit issues and enhancement requests!

## License

© 2025 Aniruddha Pol

## Support

For support or questions about the application, please open an issue in the repository.

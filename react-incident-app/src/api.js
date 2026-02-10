// src/api.js
import axios from "axios";

// Use environment variable for base URL
export const api = axios.create({
  baseURL: "https://incident-api-app-gvbfaycncbefbuhh.centralus-01.azurewebsites.net/api",
});

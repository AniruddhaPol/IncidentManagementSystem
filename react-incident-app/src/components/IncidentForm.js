import React, { useState } from "react";
import axios from "axios";
import { api } from "../api"; // import axios instance
import { TextField, Button, Box, MenuItem, Alert, Typography } from "@mui/material";

const API = "https://localhost:7193/api/Incidents";

export default function CreateIncident() {
  const [message, setMessage] = useState("");
  const [severity, setSeverity] = useState("Low");
  const [file, setFile] = useState(null);
  const [fileError, setFileError] = useState("");

  async function submit(e) {
    e.preventDefault();

    // ---------- File Validation ----------
    if (!file) {
      setFileError("File attachment is required.");
      return;
    }
    setFileError("");

    const form = new FormData();
    form.append("title", e.target.title.value);
    form.append("description", e.target.description.value);
    form.append("severity", severity);
    form.append("file", file);

    await api.post("/Incidents", form);
    setMessage("Incident created successfully!");
    e.target.reset();
    setFile(null);
  }

  function handleFileChange(e) {
    const selected = e.target.files[0];
    setFile(selected);
    if (!selected) {
      setFileError("File attachment is required.");
    } else {
      setFileError("");
    }
  }

  return (
    <Box
      component="form"
      onSubmit={submit}
      sx={{ display: "flex", flexDirection: "column", gap: 2 }}
    >
      {message && <Alert severity="success">{message}</Alert>}

      <TextField name="title" label="Title" required />
      <TextField name="description" label="Description" multiline rows={3} required />

      <TextField
        select
        label="Severity"
        value={severity}
        onChange={(e) => setSeverity(e.target.value)}
      >
        <MenuItem value="Low">Low</MenuItem>
        <MenuItem value="Medium">Medium</MenuItem>
        <MenuItem value="High">High</MenuItem>
      </TextField>

      {/* FILE INPUT */}
      <Box>
        <input type="file" onChange={handleFileChange} />
        {fileError && (
          <Typography color="error" variant="caption">
            {fileError}
          </Typography>
        )}
      </Box>

      <Button variant="contained" type="submit">
        Create
      </Button>
    </Box>
  );
}

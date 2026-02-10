import { useEffect, useState } from "react";
import { api } from "../api"; // import axios instance
import { Box, Button, MenuItem, TextField, Alert } from "@mui/material";


export default function UpdateStatus() {
  const [incList, setIncList] = useState([]);
  const [selectedId, setSelectedId] = useState("");
  const [status, setStatus] = useState("Open");
  const [message, setMessage] = useState("");

  useEffect(() => {
    load();
  }, []);

  async function load() {
    const res = await api.get("Incidents");
    setIncList(res.data.items || res.data);
  }

  async function update() {
    await api.put(`${"Incidents"}/${selectedId}/status`, { status });
    setMessage("Status updated!");
  }

  return (
    <Box sx={{ display: "flex", flexDirection: "column", gap: 2 }}>
      {message && <Alert severity="success">{message}</Alert>}

      <TextField
        select
        label="Incident"
        value={selectedId}
        onChange={(e) => setSelectedId(e.target.value)}
      >
        {incList.map((i) => (
          <MenuItem key={i.id} value={i.id}>
            {i.title}
          </MenuItem>
        ))}
      </TextField>

      <TextField
        select
        label="New Status"
        value={status}
        onChange={(e) => setStatus(e.target.value)}
      >
        <MenuItem value="Open">Open</MenuItem>
        <MenuItem value="InProgress">In Progress</MenuItem>
        <MenuItem value="Resolved">Resolved</MenuItem>
      </TextField>

      <Button variant="contained" onClick={update}>
        Update Status
      </Button>
    </Box>
  );
}

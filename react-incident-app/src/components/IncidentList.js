import { useEffect, useState } from "react";
import { api } from "../api"; // import axios instance
import {
  Card,
  CardContent,
  Typography,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Box,
  Link
} from "@mui/material";

const SeverityMap = {
  0: "Low",
  1: "Medium",
  2: "High"
};

const StatusMap = {
  0: "Open",
  1: "In Progress",
  2: "Resolved"
};


export default function IncidentList() {
  const [incidents, setIncidents] = useState([]);
  const [severityFilter, setSeverityFilter] = useState("");
  const [statusFilter, setStatusFilter] = useState("");

  useEffect(() => {
    load();
  }, []);

  async function load() {
    const res = await api.get("Incidents");
    setIncidents(res.data.items || res.data);
  }

  const filtered = incidents.filter(i =>
    (severityFilter !== "" ? i.severity === Number(severityFilter) : true) &&
    (statusFilter !== "" ? i.status === Number(statusFilter) : true)
  );

  return (
    <Box>
      <Box sx={{ display: "flex", gap: 2, mb: 3 }}>
        <FormControl fullWidth>
          <InputLabel>Severity</InputLabel>
          <Select
            value={severityFilter}
            label="Severity"
            onChange={(e) => setSeverityFilter(e.target.value)}
          >
            <MenuItem value="">All</MenuItem>
            <MenuItem value={0}>Low</MenuItem>
            <MenuItem value={1}>Medium</MenuItem>
            <MenuItem value={2}>High</MenuItem>
          </Select>
        </FormControl>

        <FormControl fullWidth>
          <InputLabel>Status</InputLabel>
          <Select
            value={statusFilter}
            label="Status"
            onChange={(e) => setStatusFilter(e.target.value)}
          >
            <MenuItem value="">All</MenuItem>
            <MenuItem value={0}>Open</MenuItem>
            <MenuItem value={1}>In Progress</MenuItem>
            <MenuItem value={2}>Resolved</MenuItem>
          </Select>
        </FormControl>
      </Box>

      {filtered.map((inc) => (
        <Card key={inc.id} sx={{ mb: 2 }}>
          <CardContent>
            <Typography variant="h6">{inc.title}</Typography>
            <Typography variant="body2" color="text.secondary">
              {inc.description}
            </Typography>

            <Typography sx={{ mt: 1 }}>
              <strong>Severity:</strong> {SeverityMap[inc.severity]} &nbsp; | &nbsp;
              <strong>Status:</strong> {StatusMap[inc.status]}
            </Typography>

            {inc.attachments?.length > 0 && (
              <Box sx={{ mt: 1 }}>
                <strong>Attachments:</strong>
                {inc.attachments.map((att) => (
                  <Box key={att.id}>
                    <Link href={att.blobUrl} target="_blank">
                      {att.fileName}
                    </Link>
                  </Box>
                ))}
              </Box>
            )}
          </CardContent>
        </Card>
      ))}
    </Box>
  );
}

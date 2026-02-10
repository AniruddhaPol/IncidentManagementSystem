import React, { useState } from "react";
import { Container, Tabs, Tab, Box, Typography } from "@mui/material";
import IncidentList from "./components/IncidentList";
import CreateIncident from "./components/IncidentForm";
import UpdateStatus from "./components/UpdateStatus";

export default function App() {
  const [tab, setTab] = useState(0);

  return (
    <Container maxWidth="md" sx={{ mt: 4 }}>

      {/* HEADER */}
      <Typography
        variant="h4"
        align="center"
        sx={{ mb: 3, color: "#1976d2", fontWeight: "bold" }}
      >
        Incident Management System
      </Typography>

      {/* TABS */}
      <Tabs value={tab} onChange={(e, v) => setTab(v)} centered>
        <Tab label="Incidents" />
        <Tab label="Create Incident" />
        <Tab label="Update Status" />
      </Tabs>

      {/* TAB CONTENT */}
      <Box sx={{ mt: 4 }}>
        {tab === 0 && <IncidentList />}
        {tab === 1 && <CreateIncident />}
        {tab === 2 && <UpdateStatus />}
      </Box>

      {/* FOOTER */}
      <Typography
        variant="body2"
        align="center"
        sx={{ mt: 6, mb: 2, color: "gray" }}
      >
        © {new Date().getFullYear()} Aniruddha Pol
      </Typography>
    </Container>
  );
}

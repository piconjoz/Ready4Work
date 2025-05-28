import { Routes, Route } from "react-router";

import LoginPage from "./features/auth/pages/LoginPage.jsx";
import LogoutPage from "./features/auth/pages/LogoutPage.jsx";
import CompaniesPage from "./features/admin/company/CompaniesPage.jsx";

function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<div>Page needs to be removed</div>} />

      <Route path="auth">
        <Route path="login" element={<LoginPage />} />
        <Route path="logout" element={<LogoutPage />} />
      </Route>

      <Route path="admin">
        <Route path="companies" element={<CompaniesPage />} />
      </Route>
    </Routes>
  );
}

export default AppRoutes;

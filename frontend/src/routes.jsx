// frontend/src/routes.jsx
import { Routes, Route, Navigate } from "react-router";
import ProtectedRoute from "./components/ProtectedRoute"; // ← Add this import

// Authentication Pages
import LoginPage from "./features/auth/pages/LoginPage.jsx";
import LogoutPage from "./features/auth/pages/LogoutPage.jsx";
import LoginPageDemo from "./features/auth/pages/LoginPageDemo.jsx";
import LogoutPageDemo from "./features/auth/pages/LogoutPageDemo.jsx";

// Miscellaneous Pages
import CompaniesPage from "./features/admin/company/CompaniesPage.jsx";
import BasePage from "./features/applicant/pages/BasePage.jsx";

// Admin Pages
import AdminUsersPage from "./features/admin/pages/AdminUsersPage.jsx";
import AdminListingsPage from "./features/admin/pages/AdminListingsPage.jsx";

// Applicant Pages
import ApplicantOnboardingPage from "./features/applicant/pages/ApplicantOnboardingPage.jsx";
import ApplicantHomePage from "./features/applicant/pages/ApplicantHomePage.jsx";
import BookmarkPage from "./features/applicant/pages/BookmarkPage.jsx";
import ApplyPage from "./features/applicant/pages/ApplyPage.jsx";
import SettingPage from "./features/applicant/pages/SettingPage.jsx";
import SearchPage from "./features/applicant/pages/SearchPage.jsx";

// Recruiter Pages
import RecruiterHomePage from "./features/recruiter/pages/RecruiterHomePage.jsx";
import RecruiterOnboardingPage from "./features/recruiter/pages/RecruiterOnboardingPage.jsx";
import RecruiterListingPage from "./features/recruiter/pages/ListingPage.jsx";
import RecruiterSettingPage from "./features/recruiter/pages/SettingPage.jsx";

// Listing Pages - Using existing ListingPage.jsx (now enhanced with AI functionality)
import ListingPage from "./features/listing/pages/ListingPage.jsx";
import ListingEditPage from "./features/listing/pages/ListingEditPage.jsx";

function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/auth/login" />} />

      <Route path="base" element={<BasePage />} />

      <Route path="auth">
        <Route index element={<Navigate to="login" />} />
        {/* Public auth routes */}
        <Route path="login" element={<LoginPage />} />
        <Route path="logout" element={<LogoutPage />} />

        {/* Demo routes - can be removed later */}
        <Route path="login_demo" element={<LoginPageDemo />} />
        <Route path="logout_demo" element={<LogoutPageDemo />} />

        <Route path="login" element={<LoginPage />} />
        <Route path="logout" element={<LogoutPage />} />
      </Route>

      {/* Admin Routes - Protected for Admin users only */}
      <Route path="admin">
        <Route
          path="companies"
          element={
            <ProtectedRoute requiredUserType={3}>
              <CompaniesPage />
            </ProtectedRoute>
          }
        />
        <Route
          path="users"
          element={
            <ProtectedRoute requiredUserType={3}>
              <AdminUsersPage />
            </ProtectedRoute>
          }
        />
        <Route
          path="listings"
          element={
            <ProtectedRoute requiredUserType={3}>
              <AdminListingsPage />
            </ProtectedRoute>
          }
        />
        <Route
          path="dashboard"
          element={
            <ProtectedRoute requiredUserType={3}>
              <div>Admin Dashboard Coming Soon...</div>
            </ProtectedRoute>
          }
        />
      </Route>

      {/* Applicant Routes - Protected for Student users only */}
      <Route path="applicant">
        {/* Onboarding might be public or require basic auth */}
        <Route path="onboard" element={<ApplicantOnboardingPage />} />

        <Route
          path="home"
          element={
            <ProtectedRoute requiredUserType={1}>
              <ApplicantHomePage />
            </ProtectedRoute>
          }
        />
        <Route
          path="search"
          element={
            <ProtectedRoute requiredUserType={1}>
              <SearchPage />
            </ProtectedRoute>
          }
        />
        <Route
          path="apply"
          element={
            <ProtectedRoute requiredUserType={1}>
              <ApplyPage />
            </ProtectedRoute>
          }
        />
        <Route
          path="bookmarks"
          element={
            <ProtectedRoute requiredUserType={1}>
              <BookmarkPage />
            </ProtectedRoute>
          }
        />
        <Route
          path="settings"
          element={
            <ProtectedRoute requiredUserType={1}>
              <SettingPage />
            </ProtectedRoute>
          }
        />
      </Route>

      {/* Recruiter Routes - Protected for Recruiter users only */}
      <Route path="recruiter">
        {/* Onboarding might be public or require basic auth */}
        <Route path="onboard" element={<RecruiterOnboardingPage />} />

        <Route
          path="home"
          element={
            <ProtectedRoute requiredUserType={2}>
              <RecruiterHomePage />
            </ProtectedRoute>
          }
        />
        <Route
          path="listings"
          element={
            <ProtectedRoute requiredUserType={2}>
              <RecruiterListingPage />
            </ProtectedRoute>
          }
        />
        <Route
          path="settings"
          element={
            <ProtectedRoute requiredUserType={2}>
              <RecruiterSettingPage />
            </ProtectedRoute>
          }
        />
      </Route>

      <Route path="listing">
        {/* Using existing ListingPage.jsx (now enhanced with AI cover letter functionality) */}
        <Route
          path="info"
          element={
            <ProtectedRoute>
              <ListingPage />
            </ProtectedRoute>
          }
        />
        <Route
          path="edit"
          element={
            <ProtectedRoute requiredUserType={2}>
              <ListingEditPage />
            </ProtectedRoute>
          }
        />
      </Route>
    </Routes>
  );
}

export default AppRoutes;
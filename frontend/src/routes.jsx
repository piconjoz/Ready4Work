// frontend/src/routes.jsx
import { Routes, Route, Navigate } from "react-router";

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
      <Route path="/" element={<Navigate to="/applicant/home" />} />

      <Route path="base" element={<BasePage />} />

      <Route path="auth">
        <Route index element={<Navigate to="login" />} />
        {/* To be removed */}
        <Route path="login_demo" element={<LoginPageDemo />} />
        <Route path="logout_demo" element={<LogoutPageDemo />} />

        <Route path="login" element={<LoginPage />} />
        <Route path="logout" element={<LogoutPage />} />
      </Route>

      <Route path="admin">
        <Route path="companies" element={<CompaniesPage />} />
        <Route path="users" element={<AdminUsersPage />} />
        <Route path="listings" element={<AdminListingsPage />} />
      </Route>

      <Route path="applicant">
        <Route path="onboard" element={<ApplicantOnboardingPage />} />
        <Route path="home" element={<ApplicantHomePage />} />
        <Route path="search" element={<SearchPage />} />
        <Route path="apply" element={<ApplyPage/>} />
        <Route path="bookmarks" element={<BookmarkPage/>} />
        <Route path="settings" element={<SettingPage />} />
      </Route>

      <Route path="recruiter">
        <Route path="home" element={<RecruiterHomePage />} />
        <Route path="onboard" element={<RecruiterOnboardingPage />} />
        <Route path="listings" element={<RecruiterListingPage />} />
        <Route path="settings" element={<RecruiterSettingPage />} />
      </Route>

      <Route path="listing">
        {/* Using existing ListingPage.jsx (now enhanced with AI cover letter functionality) */}
        <Route path="info" element={<ListingPage />} />
        <Route path="edit" element={<ListingEditPage />} />
      </Route>
    </Routes>
  );
}

export default AppRoutes;
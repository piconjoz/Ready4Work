import { Navigate, useLocation } from "react-router-dom";
import { isAuthenticated, getCurrentUser } from "../services/authAPI";

const ProtectedRoute = ({
  children,
  requiredUserType,
  redirectTo = "/auth/login",
}) => {
  const location = useLocation();

  // Check if user is logged in
  if (!isAuthenticated()) {
    console.log("User not authenticated, redirecting to login");
    // Save the attempted URL to redirect back after login
    return <Navigate to="/auth/login" state={{ from: location }} replace />;
  }

  // If no specific user type required, just check authentication
  if (!requiredUserType) {
    return children;
  }

  // Check user type if specified
  const user = getCurrentUser();
  if (!user) {
    console.log("No user data found, redirecting to login");
    return <Navigate to="/auth/login" replace />;
  }

  if (user.userType !== requiredUserType) {
    console.log(
      `User type ${user.userType} not authorized for required type ${requiredUserType}`
    );

    // Redirect to appropriate dashboard based on actual user type
    switch (user.userType) {
      case 1: // Applicant
        return <Navigate to="/applicant/home" replace />;
      case 2: // Recruiter
        return <Navigate to="/recruiter/home" replace />;
      case 3: // Admin
        return <Navigate to="/admin/dashboard" replace />;
      default:
        return <Navigate to="/auth/login" replace />;
    }
  }

  // User is authenticated and has correct permissions
  return children;
};

export default ProtectedRoute;

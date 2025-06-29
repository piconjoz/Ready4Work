import { Navigate, useLocation } from "react-router-dom";
import { useState, useEffect } from "react";
import { useAuth } from "../contexts/AuthContext";

const ProtectedRoute = ({
  children,
  requiredUserType,
  redirectTo = "/auth/login",
}) => {
  const location = useLocation();
  const { isAuthenticated, isLoading, validateUserAccess } = useAuth();

  const [isValidating, setIsValidating] = useState(true);
  const [accessResult, setAccessResult] = useState(null);

  // Reset state when requiredUserType changes (important for route changes)
  useEffect(() => {
    setAccessResult(null);
    setIsValidating(true);
  }, [requiredUserType]);

  useEffect(() => {
    // Skip if we already have a result for this userType
    if (accessResult !== null) return;

    const checkAccess = async () => {
      // Wait for auth context to initialize
      if (isLoading) return;

      // If not authenticated at all, redirect to login
      if (!isAuthenticated) {
        setAccessResult({ isValid: false, redirect: "/auth/login" });
        setIsValidating(false);
        return;
      }

      // If no specific user type required, just check authentication
      if (!requiredUserType) {
        setAccessResult({ isValid: true });
        setIsValidating(false);
        return;
      }

      try {
        const result = await validateUserAccess(requiredUserType);

        if (result.isValid) {
          setAccessResult({ isValid: true });
        } else {
          let redirectPath = "/auth/login";

          if (result.actualUserType) {
            switch (result.actualUserType) {
              case 1:
                redirectPath = "/applicant/home";
                break;
              case 2:
                redirectPath = "/recruiter/home";
                break;
              case 3:
                redirectPath = "/admin/dashboard";
                break;
              default:
                redirectPath = "/auth/login";
            }
          }

          setAccessResult({
            isValid: false,
            redirect: redirectPath,
            reason: result.reason,
          });
        }
      } catch (error) {
        console.error("Access validation error:", error);
        setAccessResult({ isValid: false, redirect: "/auth/login" });
      } finally {
        setIsValidating(false);
      }
    };

    checkAccess();
  }, [isLoading, isAuthenticated, requiredUserType, accessResult]);

  // Show loading while context or access validation is in progress
  if (isLoading || isValidating) {
    return (
      <div className="flex items-center justify-center min-h-screen">
        <div className="text-lg">Loading...</div>
      </div>
    );
  }

  // Handle access result
  if (!accessResult?.isValid) {
    return (
      <Navigate
        to={accessResult?.redirect || "/auth/login"}
        state={{ from: location }}
        replace
      />
    );
  }

  // User is authenticated and authorized
  return children;
};

export default ProtectedRoute;

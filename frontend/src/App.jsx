import AppRoutes from "./routes.jsx";
import { Toaster } from "react-hot-toast";
import { AuthProvider } from "./contexts/AuthContext.jsx";

function App() {
  return (
    <AuthProvider>
      {/* Wrap everything with AuthProvider */}
      <AppRoutes />
      <Toaster
        position="top-right"
        toastOptions={{
          duration: 4000,
          style: {
            background: "#363636",
            color: "#fff",
          },
          success: {
            duration: 3000,
            theme: {
              primary: "green",
              secondary: "black",
            },
          },
        }}
      />
    </AuthProvider>
  );
}

export default App;

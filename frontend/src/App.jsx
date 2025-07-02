import AppRoutes from "./routes.jsx";
import {Toaster} from 'react-hot-toast';

function App() {
    return (
        <>
            <AppRoutes/>
            <Toaster
                position="top-right"
                toastOptions={{
                    duration: 4000,
                    style: {
                        background: '#363636',
                        color: '#fff',
                    },
                    success: {
                        duration: 3000,
                        theme: {
                            primary: 'green',
                            secondary: 'black',
                        },
                    },
                }}
            />
        </>
    );
}

export default App;

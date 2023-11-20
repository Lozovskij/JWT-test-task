import Register from './components/Register'
import Login from './components/Login'
import Layout from './components/Layout'
import Missing from './components/Missing'
import Home from './components/Home'
import RequireAuth from './components/RequireAuth'
import { Routes, Route } from 'react-router-dom'
import UserRequestFrom from './components/UserRequestForm'
import UserRequests from './components/UserRequests'

function App() {
    return (
        <Routes>
            <Route path="/" element={<Layout />}>
                {/* public routes */}
                <Route path="/" element={<Login />} />
                <Route path="login" element={<Login />} />
                <Route path="register" element={<Register />} />

                {/* we want to protect these routes */}
                <Route element={<RequireAuth />}>
                    <Route path="home" element={<Home />} />
                    <Route path="request" element={<UserRequestFrom />} />
                    <Route path="my-requests" element={<UserRequests />} />
                </Route>

                {/* catch all */}
                <Route path="*" element={<Missing />} />
            </Route>
        </Routes>
    )
}

export default App

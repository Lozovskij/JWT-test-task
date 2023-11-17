import { useRef, useState, useEffect } from 'react'
import useAuth from '../hooks/useAuth'
import { Link, useLocation, Navigate } from 'react-router-dom'

import axios from '../api/axios'
const LOGIN_URL = 'api/auth/login'

const Login = () => {
    const { setAuth } = useAuth()

    const userRef = useRef()
    const errRef = useRef()

    const [user, setUser] = useState('')
    const [pwd, setPwd] = useState('')
    const [errMsg, setErrMsg] = useState('')
    const [success, setSuccess] = useState(false)

    const location = useLocation()

    useEffect(() => {
        userRef.current.focus()
    }, [])

    useEffect(() => {
        setErrMsg('')
    }, [user, pwd])

    const handleSubmit = async (e) => {
        e.preventDefault()

        try {
            const response = await axios.post(
                LOGIN_URL,
                JSON.stringify({ Username: user, Password: pwd }),
                {
                    headers: { 'Content-Type': 'application/json' },
                    withCredentials: true,
                }
            )
            console.log(JSON.stringify(response?.data))
            //console.log(JSON.stringify(response));

            const accessToken = response?.data?.token
            // const roles = response?.data?.roles
            setAuth({ user, pwd, accessToken })
            setUser('')
            setPwd('')
            setSuccess(true)
        } catch (err) {
            if (!err?.response) {
                setErrMsg('No Server Response')
            } else if (err.response?.status === 400) {
                setErrMsg('Missing Username or Password')
            } else if (err.response?.status === 401) {
                setErrMsg('Unauthorized')
            } else {
                setErrMsg('Login Failed')
            }
            errRef.current.focus()
        }
    }

    return (
        <>
            {success ? (
                <Navigate to="/home" state={{ from: location }} replace />
            ) : (
                <section>
                    <p
                        ref={errRef}
                        className={errMsg ? 'errmsg' : 'offscreen'}
                        aria-live="assertive"
                    >
                        {errMsg}
                    </p>
                    <h1>Войти</h1>
                    <form onSubmit={handleSubmit}>
                        <label htmlFor="username">Имя пользователя:</label>
                        <input
                            type="text"
                            id="username"
                            ref={userRef}
                            autoComplete="off"
                            onChange={(e) => setUser(e.target.value)}
                            value={user}
                            required
                        />

                        <label htmlFor="password">Пароль:</label>
                        <input
                            type="password"
                            id="password"
                            onChange={(e) => setPwd(e.target.value)}
                            value={pwd}
                            required
                        />
                        <button>Войти</button>
                    </form>
                    <p>
                        Нет аккаунта?
                        <br />
                        <span className="line">
                            <Link to="/register">Зарегестрироваться</Link>
                        </span>
                    </p>
                </section>
            )}
        </>
    )
}

export default Login

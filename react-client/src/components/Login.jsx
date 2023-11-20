import { useRef, useState, useEffect } from 'react'
import useAuth from '../hooks/useAuth'
import { Link, useLocation, useNavigate } from 'react-router-dom'

import axios from '../api/axios'
const LOGIN_URL = 'api/auth/login'

const Login = () => {
    const { setAuth } = useAuth()

    const navigate = useNavigate()
    const location = useLocation()
    // const from = location.state?.from?.pathname || '/home' //doesn't work for registration
    const from = '/home'

    const userRef = useRef()
    const errRef = useRef()

    const [user, setUser] = useState('')
    const [pwd, setPwd] = useState('')
    const [errMsg, setErrMsg] = useState('')

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
            const accessToken = response?.data
            // const roles = response?.data?.roles
            setAuth({ user, pwd, accessToken })
            setUser('')
            setPwd('')
            navigate(from, { replace: true })
        } catch (err) {
            if (!err?.response) {
                setErrMsg('Сервер не отвечает')
            } else if (err.response?.status === 400) {
                setErrMsg('Логин или пароль введен неверно')
            } else if (err.response?.status === 401) {
                setErrMsg('Ошибка авторизации')
            } else {
                setErrMsg('Что-то пошло не так')
            }
            errRef.current.focus()
        }
    }

    return (
        <div className="form-container">
            <section className="form-section ">
                <p
                    ref={errRef}
                    className={errMsg ? 'errmsg' : 'offscreen'}
                    aria-live="assertive"
                >
                    {errMsg}
                </p>
                <h1>Войти</h1>
                <form className="login-form" onSubmit={handleSubmit}>
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
                    <button className="form-submit-button">Войти</button>
                </form>
                <p>
                    Нужен аккаунт?
                    <br />
                    <span className="line">
                        <Link to="/register">Зарегестрироваться</Link>
                    </span>
                </p>
            </section>
        </div>
    )
}

export default Login

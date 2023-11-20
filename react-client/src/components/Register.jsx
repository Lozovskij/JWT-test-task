import { useRef, useState, useEffect } from 'react'
import {
    faCheck,
    faTimes,
    faInfoCircle,
} from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import axios from '../api/axios'
import { Link, useLocation, Navigate } from 'react-router-dom'

const REGISTER_URL = '/api/auth/register'

const Register = () => {
    const userRef = useRef()
    const errRef = useRef()

    const [user, setUser] = useState('')
    const [validName, setValidName] = useState(false)
    const [userFocus, setUserFocus] = useState(false)

    const [pwd, setPwd] = useState('')
    const [validPwd, setValidPwd] = useState(false)
    const [pwdFocus, setPwdFocus] = useState(false)

    const [matchPwd, setMatchPwd] = useState('')
    const [validMatch, setValidMatch] = useState(false)
    const [matchFocus, setMatchFocus] = useState(false)

    const [errMsg, setErrMsg] = useState('')
    const [success, setSuccess] = useState(false)

    useEffect(() => {
        userRef.current.focus()
    }, [])

    useEffect(() => {
        setValidName(user.length >= 4 && user.length <= 24)
    }, [user])

    useEffect(() => {
        setValidPwd(pwd.length >= 4 && pwd.length <= 24)
        setValidMatch(pwd === matchPwd)
    }, [pwd, matchPwd])

    useEffect(() => {
        setErrMsg('')
    }, [user, pwd, matchPwd])

    const location = useLocation()

    const handleSubmit = async (e) => {
        e.preventDefault()
        try {
            const response = await axios.post(
                REGISTER_URL,
                JSON.stringify({ Username: user, Password: pwd }),
                {
                    headers: { 'Content-Type': 'application/json' },
                    withCredentials: true,
                }
            )
            setSuccess(true)
            //clear state and controlled inputs
            //need value attrib on inputs for this
            setUser('')
            setPwd('')
            setMatchPwd('')
        } catch (err) {
            if (!err?.response) {
                setErrMsg('Сервер не отвечает')
            } else if (err.response?.status === 409) {
                setErrMsg('Имя занято')
            } else {
                setErrMsg('Ошибка')
            }
            errRef.current.focus()
        }
    }

    return (
        <>
            {success ? (
                <Navigate to="/login" state={{ from: location }} replace />
            ) : (
                <div className="form-container">
                    <section className="form-section">
                        <p
                            ref={errRef}
                            className={errMsg ? 'errmsg' : 'offscreen'}
                            aria-live="assertive"
                        >
                            {errMsg}
                        </p>
                        <h1>Регистрация</h1>
                        <form
                            className="registration-form"
                            onSubmit={handleSubmit}
                        >
                            <label htmlFor="username">
                                Имя пользователя:
                                <FontAwesomeIcon
                                    icon={faCheck}
                                    className={validName ? 'valid' : 'hide'}
                                />
                                <FontAwesomeIcon
                                    icon={faTimes}
                                    className={
                                        validName || !user ? 'hide' : 'invalid'
                                    }
                                />
                            </label>
                            <input
                                type="text"
                                id="username"
                                ref={userRef}
                                autoComplete="off"
                                onChange={(e) => setUser(e.target.value)}
                                value={user}
                                required
                                aria-invalid={validName ? 'false' : 'true'}
                                aria-describedby="uidnote"
                                onFocus={() => setUserFocus(true)}
                                onBlur={() => setUserFocus(false)}
                            />
                            <p
                                id="uidnote"
                                className={
                                    userFocus && user && !validName
                                        ? 'instructions'
                                        : 'offscreen'
                                }
                            >
                                от 4-x до 24-x символов
                            </p>

                            <label htmlFor="password">
                                Пароль:
                                <FontAwesomeIcon
                                    icon={faCheck}
                                    className={validPwd ? 'valid' : 'hide'}
                                />
                                <FontAwesomeIcon
                                    icon={faTimes}
                                    className={
                                        validPwd || !pwd ? 'hide' : 'invalid'
                                    }
                                />
                            </label>
                            <input
                                type="password"
                                id="password"
                                onChange={(e) => setPwd(e.target.value)}
                                value={pwd}
                                required
                                aria-invalid={validPwd ? 'false' : 'true'}
                                aria-describedby="pwdnote"
                                onFocus={() => setPwdFocus(true)}
                                onBlur={() => setPwdFocus(false)}
                            />
                            <p
                                id="pwdnote"
                                className={
                                    pwdFocus && !validPwd
                                        ? 'instructions'
                                        : 'offscreen'
                                }
                            >
                                от 4-x до 24-x символов
                            </p>

                            <label htmlFor="confirm_pwd">
                                Повторите пароль:
                                <FontAwesomeIcon
                                    icon={faCheck}
                                    className={
                                        validMatch && matchPwd
                                            ? 'valid'
                                            : 'hide'
                                    }
                                />
                                <FontAwesomeIcon
                                    icon={faTimes}
                                    className={
                                        validMatch || !matchPwd
                                            ? 'hide'
                                            : 'invalid'
                                    }
                                />
                            </label>
                            <input
                                type="password"
                                id="confirm_pwd"
                                onChange={(e) => setMatchPwd(e.target.value)}
                                value={matchPwd}
                                required
                                aria-invalid={validMatch ? 'false' : 'true'}
                                aria-describedby="confirmnote"
                                onFocus={() => setMatchFocus(true)}
                                onBlur={() => setMatchFocus(false)}
                            />
                            <p
                                id="confirmnote"
                                className={
                                    matchFocus && !validMatch
                                        ? 'instructions'
                                        : 'offscreen'
                                }
                            >
                                должно совпадать с паролем
                            </p>

                            <button
                                className="form-submit-button"
                                disabled={
                                    !validName || !validPwd || !validMatch
                                        ? true
                                        : false
                                }
                            >
                                Зарегестрироваться
                            </button>
                        </form>
                        <p>
                            Уже зарегестрированы?
                            <br />
                            <span className="line">
                                <Link to="/login">Войти</Link>
                            </span>
                        </p>
                    </section>
                </div>
            )}
        </>
    )
}

export default Register

import useAuth from '../hooks/useAuth'
import { useNavigate, Link } from 'react-router-dom'

const Header = () => {
    const { setAuth, auth } = useAuth()
    const navigate = useNavigate()
    const logout = async () => {
        setAuth({})
        navigate('/')
    }

    return (
        <header>
            <nav>
                {auth?.user ? (
                    <ul>
                        <li>
                            <Link to="/home">Главная страница</Link>
                        </li>
                        <li>Мои запросы</li>
                        <li>
                            <button onClick={logout}>Выйти</button>
                        </li>
                    </ul>
                ) : (
                    <ul>
                        <li>
                            <Link to="/register">Регистрация</Link>
                        </li>
                        <li>
                            <Link to="/login">Войти</Link>
                        </li>
                    </ul>
                )}
            </nav>
        </header>
    )
}

export default Header

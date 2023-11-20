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
                {auth?.user && (
                    <ul className="header-links">
                        <li className="header-links__home">
                            <Link to="/home">Главная страница</Link>
                        </li>
                        <div className="header-links__placeholder"></div>
                        <li className="header-links__my-requests">
                            <Link to="/my-requests">Мои запросы</Link>
                        </li>
                        <li className="header-links__exit">
                            <button onClick={logout}>Выйти</button>
                        </li>
                    </ul>
                )}
            </nav>
        </header>
    )
}

export default Header

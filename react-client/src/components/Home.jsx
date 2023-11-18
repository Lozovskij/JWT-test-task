import useAxiosPrivate from '../hooks/useAxiosPrivate'
import { useNavigate, useLocation } from 'react-router-dom'

const Home = () => {
    const axiosPrivate = useAxiosPrivate()
    const navigate = useNavigate()
    const location = useLocation()
    const test = async () => {
        try {
            const response = await axiosPrivate.get('/api/auth')
            console.log(response.data)
        } catch (err) {
            console.error(err)
            navigate('/login', { state: { from: location }, replace: true })
        }
    }

    return (
        <>
            <h1>Hello world</h1>
            <button onClick={() => test()}>get user name to the console</button>
        </>
    )
}

export default Home

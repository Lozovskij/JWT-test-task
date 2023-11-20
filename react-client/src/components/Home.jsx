import useAxiosPrivate from '../hooks/useAxiosPrivate'
import { useNavigate, useLocation, Link } from 'react-router-dom'
import { useState, useEffect } from 'react'

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

    const [requests, setRequests] = useState([])
    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axiosPrivate.get('/api/requests')
                setRequests(response.data)
            } catch (error) {
                // Handle error
            }
        }

        fetchData()
    }, [])

    const handleClick = (request) => {
        console.log('Clicked Request:', request)
    }

    return (
        <>
            {/* <h1>Hello world</h1>
            <button onClick={() => test()}>get user name to the console</button> */}
            <div>
                <h1>Выберите запрос / проблему</h1>
                <ul>
                    {requests.map((request) => (
                        <li
                            key={request.requestId}
                            onClick={() => handleClick(request)}
                        >
                            <Link
                                to={{
                                    pathname: `/request`,
                                }}
                                state={{ request }}
                            >
                                {request.description}
                            </Link>
                        </li>
                    ))}
                </ul>
            </div>
        </>
    )
}

export default Home

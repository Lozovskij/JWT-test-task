import useAxiosPrivate from '../hooks/useAxiosPrivate'
import { Link } from 'react-router-dom'
import { useState, useEffect } from 'react'

const Home = () => {
    const axiosPrivate = useAxiosPrivate()

    const [requests, setRequests] = useState([])
    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axiosPrivate.get('/api/requests')
                setRequests(response.data)
            } catch (error) {
                // Handle error
                console.error(err)
            }
        }

        fetchData()
    }, [])

    return (
        <>
            <div>
                <h1>Выберите запрос / проблему</h1>
                <ul className="requests">
                    {requests.map((request) => (
                        <li className="request-item" key={request.requestId}>
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

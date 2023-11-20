import { useState, useEffect } from 'react'
import useAxiosPrivate from '../hooks/useAxiosPrivate'

const UserRequests = () => {
    const axiosPrivate = useAxiosPrivate()
    const [userRequests, setUserRequests] = useState([])

    const fetchData = async () => {
        try {
            const response = await axiosPrivate.get('/api/UserRequest')
            console.log(response)
            setUserRequests(response.data)
        } catch (error) {
            console.error(err)
            navigate('/login', { state: { from: location }, replace: true })
        }
    }

    useEffect(() => {
        fetchData()
    }, [])

    const handleCancelClick = async (ur) => {
        try {
            console.log(ur)
            const response = await axiosPrivate.post(
                '/api/UserRequest/cancel',
                JSON.stringify({ UserRequestId: ur.userRequestId })
            )
            console.log(response.data)
            fetchData()
        } catch (err) {
            console.error(err)
            navigate('/login', { state: { from: location }, replace: true })
        }
    }

    return (
        <div>
            <h1>Ваши запросы:</h1>
            <ul>
                {userRequests.map((ur) => (
                    <>
                        <li key={ur.requestId}>
                            {ur.requestDescription}
                            <br />
                            Комментарий: {ur.userComment || '-'}
                        </li>
                        <button onClick={() => handleCancelClick(ur)}>
                            Отменить
                        </button>
                    </>
                ))}
            </ul>
        </div>
    )
}

export default UserRequests

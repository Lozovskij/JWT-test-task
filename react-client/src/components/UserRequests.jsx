import { useState, useEffect } from 'react'
import useAxiosPrivate from '../hooks/useAxiosPrivate'
import { useLocation, useNavigate } from 'react-router-dom'

const statusMapping = {
    0: 'на рассмотрении',
    1: 'отменен',
    2: 'проблема устранена',
}

const UserRequests = () => {
    const axiosPrivate = useAxiosPrivate()
    const [userRequests, setUserRequests] = useState([])
    const navigate = useNavigate()
    const location = useLocation()

    const fetchData = async () => {
        try {
            const response = await axiosPrivate.get('/api/UserRequest')
            console.log(response)
            setUserRequests(response.data)
        } catch (error) {
            console.error(error)
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
            <h3>Ваши запросы:</h3>
            <div className="user-requests-container">
                {userRequests.map((ur) => (
                    <>
                        <div key={ur.requestId} className="user-request">
                            <div className="user-request__info">
                                <p className="user-request__title">
                                    {ur.requestDescription} (
                                    <span className="status">
                                        {statusMapping[ur.requestStatus]}
                                    </span>
                                    )
                                </p>
                                <div> - {ur.userComment || '-'}</div>
                            </div>
                            <button
                                className="user-request__cancel-button"
                                onClick={() => handleCancelClick(ur)}
                            >
                                Отменить
                            </button>
                        </div>
                    </>
                ))}
            </div>
        </div>
    )
}

export default UserRequests

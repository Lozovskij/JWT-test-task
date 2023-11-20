import { useLocation } from 'react-router-dom'
import { useState } from 'react'
import useAuth from '../hooks/useAuth'
import useAxiosPrivate from '../hooks/useAxiosPrivate'

const UserRequestFrom = () => {
    const location = useLocation()
    const { request } = location.state
    const [comment, setComment] = useState('')
    const { auth } = useAuth()

    const axiosPrivate = useAxiosPrivate()

    const handleChange = (e) => {
        setComment(e.target.value)
    }

    const handleSubmit = async (e) => {
        e.preventDefault()

        try {
            const response = await axiosPrivate.post(
                '/api/UserRequest/create',
                JSON.stringify({
                    userComment: comment,
                    username: auth.user,
                    RequestId: request.requestId,
                })
            )
            console.log('Response:', response.data)
        } catch (error) {
            console.log(error)
        }
    }

    return (
        <>
            <h3>{request.description}</h3>

            <form onSubmit={handleSubmit}>
                <label>
                    Опишите вашу проблему (опционально):
                    <br></br>
                    <textarea value={comment} onChange={handleChange} />
                </label>
                <button type="submit">Submit</button>
            </form>
        </>
    )
}

export default UserRequestFrom

import { useLocation, Link } from 'react-router-dom'
import { useState } from 'react'
import useAuth from '../hooks/useAuth'
import useAxiosPrivate from '../hooks/useAxiosPrivate'

const UserRequestFrom = () => {
    const location = useLocation()
    const { request } = location.state
    const [comment, setComment] = useState('')
    const { auth } = useAuth()
    const [success, setSuccess] = useState(false)

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
            setSuccess(true)
        } catch (error) {
            console.log(error)
        }
    }

    return (
        <>
            {success ? (
                <>
                    <h1>Запрос успешно оформлен</h1>
                    <p>Ваши запросы доступны по ссылке:</p>
                    <p>
                        <Link to="/my-requests">Мои запросы</Link>
                    </p>
                </>
            ) : (
                <>
                    <h3>{request.description}</h3>

                    <form className="user-request-form" onSubmit={handleSubmit}>
                        <label>
                            Опишите вашу проблему (опционально):
                            <br></br>
                            <textarea
                                className="user-request-form__textarea"
                                value={comment}
                                onChange={handleChange}
                            />
                        </label>
                        <button type="submit">Submit</button>
                    </form>
                </>
            )}
        </>
    )
}

export default UserRequestFrom

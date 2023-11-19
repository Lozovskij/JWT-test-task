import axios from '../api/axios'
import useAuth from './useAuth'

const useRefreshToken = () => {
    const { setAuth, auth } = useAuth()

    const refresh = async () => {
        const response = await axios.get('/api/auth/new-access-token', {
            withCredentials: true,
            params: { username: auth.user },
        })

        const accessToken = response.data
        setAuth((prev) => {
            return { ...prev, accessToken: accessToken }
        })
        return accessToken
    }
    return refresh
}

export default useRefreshToken

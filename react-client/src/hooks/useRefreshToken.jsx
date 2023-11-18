import axios from '../api/axios'
import useAuth from './useAuth'

const useRefreshToken = () => {
    const { setAuth } = useAuth()

    const refresh = async () => {
        const response = await axios.get('/api/auth/new-access-token', {
            withCredentials: true,
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

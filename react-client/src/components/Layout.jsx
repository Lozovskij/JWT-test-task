import { Outlet } from 'react-router-dom'

import Header from './Header'

const Layout = () => {
    return (
        <>
            <Header />
            <main className="App">
                <Outlet />
            </main>
            <footer></footer>
        </>
    )
}

export default Layout

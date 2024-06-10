import Footer from "../Component/Footer/Footer";
import NavBar2 from "../Component/NavBar/NavBar2";
import { useAuth } from "../Context/AuthProvider";
import CheckAuth from "../Helpers/CheckAuth";
function MainLayout({ children }) {
    const [isAuth, setAuth] = useAuth();
    return (
        <div>
            <CheckAuth></CheckAuth>
            <NavBar2 />
            <main>
                {children}
            </main>
            <Footer></Footer>
        </div>
    );
}

export default MainLayout;
import Footer from "../Component/Footer/Footer";
import NavBar2 from "../Component/NavBar/NavBar2";
import { useAuth } from "../Context/AuthProvider";
function MainLayout({ children }) {
    const [isAuth, setAuth] = useAuth();
    return (
        <div>
            <NavBar2 />
            <main>
                {children}
            </main>
            <Footer></Footer>
        </div>
    );
}

export default MainLayout;
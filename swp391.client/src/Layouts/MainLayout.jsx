import Footer from "../Component/Footer/Footer";
import NavBar2 from "../Component/NavBar/NavBar2";

function MainLayout({ children }) {
  return (
      <>
          <NavBar2/>
          <main>
              {children}
          </main>
          <Footer></Footer>
      </>
    );
}

export default MainLayout;
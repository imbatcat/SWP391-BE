
import Hero from '../../Component/Hero/Hero'
import MainLayout from '../../Layouts/MainLayout'
import HomeContent from '../../Component/Home Content/HomeContent'
function Home () {
  return (
    <div>
          <MainLayout>
            <Hero></Hero>
            <HomeContent/>
          </MainLayout>
    </div>
  )
}

export default Home